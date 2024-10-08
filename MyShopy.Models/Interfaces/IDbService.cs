﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopy.Models.Interfaces
{
    public interface IDbService
    {
        void OpenConnection();
        IDbConnection GetDbConnection();
        void CloseConnection();
        IDbTransaction BeginTransaction();
        IDbCommand CreateCommand(string? sql = null);
        void ExecuteNonQueryCommand(string sql);
        void ExecuteNonQueryCommand(IDbCommand command);
        DataTable ExecuteReaderCommand(string sql, string tableName);
        DataTable ExecuteReaderCommand(IDbCommand command, string tableName);
        T ExecuteScalarCommand<T>(string sql);
        T ExecuteScalarCommand<T>(IDbCommand command);
        void AddParameterWithValue(IDbCommand command, string name, object value);
    }
}
