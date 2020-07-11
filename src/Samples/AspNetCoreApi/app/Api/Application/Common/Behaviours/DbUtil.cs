using System.Linq;
using ApiTemplate.Api.Domain.Common;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Api.Application.Common.Behaviours
{
    public static class DbUtil
    {
        private const int ReferenceConstraint = 547;
        private const int CannotInsertNull = 515;
        private const int CannotInsertDuplicateKeyUniqueIndex = 2601;
        private const int CannotInsertDuplicateKeyUniqueConstraint = 2627;
        private const int ArithmeticOverflow = 8115;
        private const int StringOrBinaryDataWouldBeTruncated = 8152;

        public static Result HandleDatabaseException(DbUpdateException dbUpdateException, string propertyName)
        {
            var result = Resultz.Error("SQL Server database update exception");

            if (dbUpdateException.GetBaseException() is SqlException dbException)
            {
                var error =
                    dbException.Number switch
                    {
                        ReferenceConstraint => new AppError(propertyName, "Reference constraint violation"),
                        CannotInsertNull => new AppError(propertyName, "Cannot insert null"),
                        CannotInsertDuplicateKeyUniqueIndex => new AppError(propertyName,
                            "Unique constraint violation"),
                        CannotInsertDuplicateKeyUniqueConstraint => new AppError(propertyName,
                            "Unique constraint violation"),
                        ArithmeticOverflow => new AppError(propertyName, "Numeric overflow"),
                        StringOrBinaryDataWouldBeTruncated => new AppError(propertyName,
                            "Maximum length exceeded"),
                        _ => new AppError(propertyName, "SQL Server database update exception")
                    };

                result.Reasons.Add(error);
            }
            else
            {
                var message = $"Possible concurrency exception. {dbUpdateException.Message}";
                result.AddError(propertyName, message);
            }

            return result;
        }

        public static TEntity GetRelatedEntity<TEntity>(int requestId, IQueryable<TEntity> entities, int? id, string name, Result result) where TEntity : Entity
        {
            if (id == null)
                return null;

            var entity = entities.FirstOrDefault(x => x.Id == id);
            if (entity == null)
                result.AddResult(Resultz.Error(name, $"The selected {name} is not valid.", requestId));

            return entity;
        }
    }
}