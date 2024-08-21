using MyShopy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopy.Models.Services
{
    public class DbService: IDbService
    {
        private readonly IDbConnection _dbConnection;

        public DbService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void OpenConnection()
        {
            try
            {
                _dbConnection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to open connection: {ex.Message}");
            }
        }

        public IDbConnection GetDbConnection() { return _dbConnection; }

        public void CloseConnection()
        {
            try
            {
                _dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to close connection: {ex.Message}");
            }
        }

        public IDbTransaction BeginTransaction()
        {
            try
            {
                return _dbConnection.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to begin transaction: {ex.Message}");
            }
        }

        public IDbCommand CreateCommand(string? sql = null)
        {
            try
            {
                var command = _dbConnection.CreateCommand();
                command.CommandText = sql;
                return command;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create command: {ex.Message}");
            }
        }

        public void ExecuteNonQueryCommand(string sql)
        {
            try
            {
                IDbCommand command = CreateCommand(sql);
                var result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute non-query command: {ex.Message}");
            }
        }

        public void ExecuteNonQueryCommand(IDbCommand command)
        {
            try
            {
                var result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute non-query command: {ex.Message}");
            }
        }

        public DataTable ExecuteReaderCommand(string sql, string tableName)
        {
            DataTable dtResult = new DataTable(tableName);
            try
            {
                IDbCommand command = CreateCommand(sql);
                IDataReader reader = command.ExecuteReader();
                dtResult.Load(reader);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute reader command: {ex.Message}");
            }
            return dtResult;
        }

        public DataTable ExecuteReaderCommand(IDbCommand command, string tableName)
        {
            DataTable dtResult = new DataTable(tableName);
            try
            {
                IDataReader reader = command.ExecuteReader();
                dtResult.Load(reader);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute reader command: {ex.Message}");
            }
            return dtResult;
        }

        public T ExecuteScalarCommand<T>(string sql)
        {
            try
            {
                IDbCommand command = CreateCommand(sql);
                var result = command.ExecuteScalar();
                if (result != null && result.GetType() != typeof(DBNull))
                    return (T)result;
                else
                    throw new Exception("Data not found");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute scalar command: {ex.Message}");
            }
        }

        public T ExecuteScalarCommand<T>(IDbCommand command)
        {
            try
            {
                var result = command.ExecuteScalar();
                if (result != null && result.GetType() != typeof(DBNull))
                    return (T)result;
                else
                    throw new Exception("Data not found");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute scalar command: {ex.Message}");
            }
        }

        public void AddParameterWithValue(IDbCommand command, string name, object value)
        {
            try
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = name;
                parameter.Value = value;
                command.Parameters.Add(parameter);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add parameter: {ex.Message}");
            }
        }
    }
}
