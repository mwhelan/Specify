using System;
using System.Linq;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace Specs.Integration.ApiTemplate.Infrastructure.Persistence
{
    public static class SqlExceptionFactory
    {
        public static SqlException CreateSqlException(int number, Guid? connectionId = null)
        {
            var errorCtors = typeof(SqlError)
                .GetTypeInfo()
                .DeclaredConstructors;

            var error = (SqlError)errorCtors.First(c => c.GetParameters().Length == 8)
                .Invoke(new object[] { number, (byte)0, (byte)0, "Server", "ErrorMessage", "Procedure", 0, null });
            var errors = (SqlErrorCollection)typeof(SqlErrorCollection)
                .GetTypeInfo()
                .DeclaredConstructors
                .Single()
                .Invoke(null);

            typeof(SqlErrorCollection).GetRuntimeMethods().Single(m => m.Name == "Add").Invoke(errors, new object[] { error });

            var exceptionCtors = typeof(SqlException)
                .GetTypeInfo()
                .DeclaredConstructors;

            return (SqlException)exceptionCtors.First(c => c.GetParameters().Length == 4)
                .Invoke(new object[] { "Bang!", errors, null, connectionId ?? Guid.NewGuid() });
        }
    }
}