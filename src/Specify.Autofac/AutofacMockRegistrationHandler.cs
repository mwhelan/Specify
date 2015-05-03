using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Specify.Mocks;

namespace Specify.Autofac
{
    /// <summary> Resolves unknown interfaces and Mocks using the <see cref="IMockFactory"/>. </summary>
    public class AutofacMockRegistrationHandler : IRegistrationSource
    {
        private readonly IMockFactory _mockFactory;

        public AutofacMockRegistrationHandler(IMockFactory mockFactory)
        {
            _mockFactory = mockFactory;
        }

        /// <summary>
        /// Retrieve a registration for an unregistered service, to be used
        /// by the container.
        /// </summary>
        /// <param name="service">The service that was requested.</param>
        /// <param name="registrationAccessor"></param>
        /// <returns>
        /// Registrations for the service.
        /// </returns>
        public IEnumerable<IComponentRegistration> RegistrationsFor
            (Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            var typedService = service as IServiceWithType;
            if (typedService == null ||
                !typedService.ServiceType.IsInterface ||
                typedService.ServiceType.IsGenericType &&
                typedService.ServiceType.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                typedService.ServiceType.IsArray ||
                typeof(IStartable).IsAssignableFrom(typedService.ServiceType))
                return Enumerable.Empty<IComponentRegistration>();

            var rb = RegistrationBuilder.ForDelegate<object>((c, p) => _mockFactory.CreateMock(typedService.ServiceType))
                .As(service)
                .InstancePerLifetimeScope();

            return new[] { rb.CreateRegistration() };
        }

        public bool IsAdapterForIndividualComponents
        {
            get { return false; }
        }
    }
}