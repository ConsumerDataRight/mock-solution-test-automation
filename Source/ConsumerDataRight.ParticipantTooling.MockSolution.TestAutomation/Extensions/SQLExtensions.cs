﻿using Microsoft.Data.SqlClient;
using Serilog;
using System.Text;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    static public class SqlExtensions
    {
        /// <summary>
        /// Execute scalar command and return result as Int32. Throw error if no results or conversion error
        /// </summary>
        static public Int32 ExecuteScalarInt32(this SqlCommand command)
        {
            var res = command.ExecuteScalar();

            if (res == DBNull.Value || res == null)
            {
                // Build list of paramters
                var sb = new StringBuilder();
                foreach (SqlParameter p in command.Parameters)
                    sb.Append($"{p.ParameterName}={p.Value.ToString()}");

                // Throw exception
                throw new System.Data.DataException($"Command returns no results - CommandText=\"{command.CommandText}\", Parameters: {sb.ToString()}");
            }

            return Convert.ToInt32(res);
        }

        /// <summary>
        /// Execute scalar command and return result as string. Throw error if no results or conversion error
        /// </summary>
        static public string ExecuteScalarString(this SqlCommand command)
        {
            var res = command.ExecuteScalar();

            if (res == DBNull.Value || res == null)
            {
                
                // Build list of paramters
                var sb = new StringBuilder();
                foreach (SqlParameter p in command.Parameters)
                    sb.Append($"{p.ParameterName}={p.Value.ToString()}");

                // Throw exception
                throw new System.Data.DataException($"Command returns no results - CommandText=\"{command.CommandText}\", Parameters: {sb.ToString()}");
            }

            return Convert.ToString(res);
        }

        static public Int32 ExecuteScalarInt32(this SqlConnection connection, string sql)
        {
            Log.Information("Calling {FunctionName} with SQL command: {sql}", nameof(ExecuteScalarInt32), sql);
            using var command = new SqlCommand(sql, connection);
            return command.ExecuteScalarInt32();
        }

        static public string ExecuteScalarString(this SqlConnection connection, string sql)
        {
            Log.Information("Calling {FunctionName} with SQL command: {sql}", nameof(ExecuteScalarString), sql);
            using var command = new SqlCommand(sql, connection);
            return command.ExecuteScalarString();
        }

        static public void ExecuteNonQuery(this SqlConnection connection, string sql)
        {
            Log.Information("Calling {FunctionName} with SQL command: {sql}", nameof(ExecuteNonQuery), sql);
            using var command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
