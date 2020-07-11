using System;
using System.Linq;
using ApiTemplate.Api.Application.Common.Interfaces;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using ApiTemplate.Api.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Specs.Library;
using Specs.Library.Data;
using Specs.Library.Identity;
using TestStack.Dossier;

namespace Specs.Integration.ApiTemplate.Infrastructure.Persistence
{
    public class DatabasebRetrySucceeds : ScenarioFor<object>
    {
        private AppDbContext _db;
        private DisposalReason _result;

        public void Given_transient_fault_handling_is_enabled()
        {
            TransientFailureCausingCommandInterceptor.Reset();
            var services = new ServiceCollection();

            var connectionString = TestSettings.ConnectionString;
            services.AddScoped<ICurrentUserService, TestCurrentUserService>();
            services.AddDbContext<AppDbContext>(opt => opt
                .UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure())
                .AddInterceptors(new TransientFailureCausingCommandInterceptor()));

            var provider = services.BuildServiceProvider();

            _db = provider.GetRequiredService<AppDbContext>();
            Builder<DisposalReason>.CreateNew().Persist();
        }

        public void When_I_try_to_access_the_database_and_it_is_not_available()
        {
            _result = _db.DisposalReasons.FirstOrDefault();
            //var strategy = db.Database.CreateExecutionStrategy();
            //var entity = strategy.Execute(() =>
            //    db.DisposalReasons.FirstOrDefault());
        }

        public void Then_it_should_retry_and_complete_if_database_becomes_available()
        {
            _result.Should().NotBeNull();
            TransientFailureCausingCommandInterceptor.RetryRunningTotal.Should().Be(3);
        }
    }

    public class DatabaseRetryExceedsMaxRetryCount : ScenarioFor<object>
    {
        private AppDbContext _db;
        private RetryLimitExceededException _result;

        public void Given_transient_fault_handling_is_enabled()
        {
            TransientFailureCausingCommandInterceptor.Reset();
            var services = new ServiceCollection();

            var connectionString = TestSettings.ConnectionString;
            services.AddScoped<ICurrentUserService, TestCurrentUserService>();
            services.AddDbContext<AppDbContext>(opt => opt
                .UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure(maxRetryCount:2))
                .AddInterceptors(new TransientFailureCausingCommandInterceptor(5)));

            var provider = services.BuildServiceProvider();


            _db = provider.GetRequiredService<AppDbContext>();
            Builder<DisposalReason>.CreateNew().Persist();
        }

        public void When_I_try_to_access_the_database_and_it_the_retries_exceed_the_max_count()
        {
            try
            {
                var entity = _db.DisposalReasons.FirstOrDefault();
            }
            catch (RetryLimitExceededException exception)
            {
                _result = exception;
            }
        }

        public void Then_it_should_throw_a_RetryLimitExceededException()
        {
            _result.Should().NotBeNull();
        }
    }

}