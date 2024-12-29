﻿using Npgsql;
using System.Data.Common;
using Evently.Modules.Events.Application.Abstractions.Data;

namespace Evently.Modules.Events.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
