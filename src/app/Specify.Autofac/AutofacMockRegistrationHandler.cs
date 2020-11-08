using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Decorators;
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
        /// Retrieve a registration for an unregistered service, to be used by the container.
        /// </summary>
        /// <param name="service">The service that was requested.</param>
        /// <param name="registrationAccessor"></param>
        /// <remarks>
        ///     Since Autofac v4.9.0 the DecoratorService also passed though this
        ///     registration source, make sure this is not mocked out by a proxy.
        /// </remarks>
        /// <returns>
        /// Registrations for the service.
        /// </returns>
        public IEnumerable<IComponentRegistration> RegistrationsFor
            (Service service, Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            var typedService = service as IServiceWithType;
            
            if (typedService == null ||
                !typedService.ServiceType.IsInterface() ||
                typedService.ServiceType.IsGenericType() &&
                typedService.ServiceType.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                typedService.ServiceType.IsArray ||
                typedService.ServiceType.CanBeCastTo<IStartable>() ||
                service is DecoratorService)
                return Enumerable.Empty<IComponentRegistration>();

            var rb = RegistrationBuilder.ForDelegate(
                    (c, p) => _mockFactory.CreateMock(typedService.ServiceType))
                .As(service)
                .InstancePerLifetimeScope();

            return new[] { rb.CreateRegistration() };
        }

        public bool IsAdapterForIndividualComponents => false;
    }
}