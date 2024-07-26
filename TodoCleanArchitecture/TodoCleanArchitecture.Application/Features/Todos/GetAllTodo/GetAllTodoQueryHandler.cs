using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoCleanArchitecture.Application.Service;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;

namespace TodoCleanArchitecture.Application.Features.Todos.GetAllTodo;

internal sealed class GetAllTodoQueryHandler(
    // IApplicationDbContext dbContext  Yöntem-1
    ITodoRepository todoRepository,
    ICacheService cache
    ) : IRequestHandler<GetAllTodoQuery, List<Todo>>
{
    public async Task<List<Todo>> Handle(GetAllTodoQuery request, CancellationToken cancellationToken)
    {
        //var result = dbContext.Todos.ToList(); Yöntem-1

        cache.TryGetValue("todos", out List<Todo>? todos);

        if (todos is null)
        {
            todos = await todoRepository.GetAll().ToListAsync(cancellationToken);

            cache.Set("todos", todos);
        }
        return todos;
    }
}
