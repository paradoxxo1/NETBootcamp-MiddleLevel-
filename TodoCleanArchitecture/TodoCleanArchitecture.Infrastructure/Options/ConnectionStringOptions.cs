﻿namespace TodoCleanArchitecture.Infrastructure.Options;
public sealed class ConnectionStringOptions
{
    public string SqlServer { get; set; } = default!;
    public string PostgreSql { get; set; } = default!;
    public string MongoDb { get; set; } = default!;
}