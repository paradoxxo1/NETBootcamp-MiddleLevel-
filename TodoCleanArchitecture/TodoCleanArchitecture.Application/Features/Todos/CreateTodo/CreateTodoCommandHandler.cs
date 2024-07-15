using AutoMapper;
using MediatR;
using TodoCleanArchitecture.Application.Service;
using TodoCleanArchitecture.Domain.Abstractions;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;

namespace TodoCleanArchitecture.Application.Features.Todos.CreateTodo;

internal sealed class CreateTodoCommandHandler(
    ITodoRepository todoRepository,
    IMapper mapper,
    ICacheService cache) : IRequestHandler<CreateTodoCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        bool isWorkExists = await todoRepository.AnyAsync(p => p.Work == request.Work, cancellationToken);

        if (isWorkExists)
        {
            //throw new ArgumentException("This record already exists.");
            //throw new DublicateRecordWorkException();
            //var errorResponse = new Result<string>(500, "This record already exists.");
            var errorResponse = Result<string>.Failure(500, "This record already exists.");
            return errorResponse;
        }

        Todo todo = mapper.Map<Todo>(request);
        await todoRepository.CreateAsync(todo, cancellationToken);

        //var response = Result<string>.Success("Create is successful");
        cache.Remove("todos");
        return "Create is successful";
    }
}

