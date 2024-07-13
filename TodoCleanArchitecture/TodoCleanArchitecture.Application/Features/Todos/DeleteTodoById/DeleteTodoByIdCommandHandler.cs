﻿using MediatR;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;

namespace TodoCleanArchitecture.Application.Features.Todos.DeleteTodoById;

internal sealed class DeleteTodoByIdCommandHandler(
    ITodoRepository todoRepository) : IRequestHandler<DeleteTodoByIdCommand>
{
    public async Task Handle(DeleteTodoByIdCommand request, CancellationToken cancellationToken)
    {
        Todo? todo = await todoRepository.GetByIdAsync(request.Id, cancellationToken);
        if (todo is null)
        {
            throw new ArgumentNullException("Todo not found");
        }
        await todoRepository.DeleteAsync(todo, cancellationToken);
    }
}