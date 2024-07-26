using MediatR;
using TodoCleanArchitecture.Domain.Entities;

namespace TodoCleanArchitecture.Domain.Events;
public sealed class TodoDomainEvent : INotification
{
    public Todo Todo { get; set; }
    public TodoDomainEvent(Todo todo)
    {
        Todo = todo;
    }
}

public sealed class TodoSendEmailDomainEvent : INotificationHandler<TodoDomainEvent>
{
    public async Task Handle(TodoDomainEvent notification, CancellationToken cancellationToken)
    {
        //Send Email
        await Task.CompletedTask;
    }
}

public sealed class TodoSendSmsDomainEvent : INotificationHandler<TodoDomainEvent>
{
    public async Task Handle(TodoDomainEvent notification, CancellationToken cancellationToken)
    {
        //Send SMS
        await Task.CompletedTask;
    }
}

public sealed class TodoSendWhatsappDomainEvent : INotificationHandler<TodoDomainEvent>
{
    public async Task Handle(TodoDomainEvent notification, CancellationToken cancellationToken)
    {
        //Send Whatsapp
        await Task.CompletedTask;
    }
}
