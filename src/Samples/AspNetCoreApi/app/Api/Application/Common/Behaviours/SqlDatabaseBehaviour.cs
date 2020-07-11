using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiTemplate.Api.Application.Common.Behaviours
{
    public class SqlDatabaseBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IUnitOfWork _db;
        private readonly IRequestHandler<TRequest, TResponse> _innerHandler;
        private readonly ILogger<TRequest> _logger;

        public SqlDatabaseBehaviour(IUnitOfWork db, IRequestHandler<TRequest, TResponse> innerHandler, 
            ILogger<TRequest> logger)
        {
            _db = db;
            _innerHandler = innerHandler;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var strategy = _db.CreateExecutionStrategy();

            return await strategy.ExecuteAsync<TResponse>(async () =>
            {
                try
                {
                    await _db.BeginTransactionAsync(GetIsolationLevelBasedOnHandlerType());

                    var response = await next();

                    await _db.CommitTransactionAsync();

                    return response;
                }
                catch (DbUpdateException ex)
                {
                    _db.RollbackTransaction();

                    _logger.LogError(ex, "Database update exception.");

                    return DbUtil.HandleDatabaseException(ex, typeof(TRequest).Name) as TResponse;
                }
            });
        }

        public IsolationLevel GetIsolationLevelBasedOnHandlerType()
        {
            return _innerHandler.GetType().FullName.EndsWith("QueryHandler") 
                ? IsolationLevel.ReadUncommitted 
                : IsolationLevel.ReadCommitted;
        }
    }
}
