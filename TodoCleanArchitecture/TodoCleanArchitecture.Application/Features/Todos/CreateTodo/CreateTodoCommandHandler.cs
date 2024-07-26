using AutoMapper;
using MediatR;
using TodoCleanArchitecture.Application.Service;
using TodoCleanArchitecture.Domain.Abstractions;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Events;
using TodoCleanArchitecture.Domain.Repositories;

namespace TodoCleanArchitecture.Application.Features.Todos.CreateTodo;

internal sealed class CreateTodoCommandHandler(
    ITodoRepository todoRepository,
    IMapper mapper,
    IMediator mediator,
    IOutBoxEmailRepository outBoxEmailRepository,
    IUnitOfWork unitofWork,
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

        OutBoxEmail outBoxEmail = new()
        {
            TodoId = todo.Id,
            IsSuccesful = true,  // bu false olmalı, deneme işlemi bittiği için true seçildi
        };

        await outBoxEmailRepository.CreateAsync(outBoxEmail, cancellationToken);

        await unitofWork.SaveChangesAsync(cancellationToken);

        //var response = Result<string>.Success("Create is successful");
        cache.Remove("todos");


        //TodoService.SendEmail();
        //TodoService.SendSms();
        await mediator.Publish(new TodoDomainEvent(todo));

        return "Create is successful";
    }
}

