using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Specs.Library.Data;

namespace Specs.Integration.ApiTemplate.Infrastructure.Persistence
{
    public class TransientFailureCausingCommandInterceptor : DbCommandInterceptor
    {
        public int NumberOfTimesToThrow { get; }
        public static int RetryRunningTotal = 0;

        public TransientFailureCausingCommandInterceptor(int numberOfTimesToThrow = 3)
        {
            NumberOfTimesToThrow = numberOfTimesToThrow;
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            const int ErrorNumber = 49920;
            RetryRunningTotal++;
            if (RetryRunningTotal % NumberOfTimesToThrow != 0)
            {
                throw SqlExceptionFactory.CreateSqlException(ErrorNumber);
            }
            return base.ReaderExecuting(command, eventData, result);
        }

        public static void Reset()
        {
            RetryRunningTotal = 0;
        }
    }
}