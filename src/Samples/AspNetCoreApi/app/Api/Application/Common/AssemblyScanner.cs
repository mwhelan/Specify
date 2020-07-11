using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApiTemplate.Api.Application.Common.Mappings;
using Autofac;
using MediatR;
using TypeExtensions = Autofac.TypeExtensions;

namespace ApiTemplate.Api.Application.Common
{
    public static class AssemblyScanner
    {
        public static Assembly ApplicationAssembly = typeof(AssemblyScanner).Assembly;

        public const string CommandSuffix = "Command";
        public const string CommandHandlerSuffix = "CommandHandler";
        public const string QuerySuffix = "Query";
        public const string QueryHandlerSuffix = "QueryHandler";

        public static List<Type> AllCommandsQueries()
        {
            return ApplicationAssembly.GetTypes()
                .Where(t => t.IsClass && TypeExtensions.IsClosedTypeOf(t, typeof(IRequest<>)) && !t.IsAbstract)
                .ToList();
        }

        public static bool IsCommandHandler(this Type type)
        {
            var isHandler = type.IsClass &&
                            type.IsClosedTypeOf(typeof(IRequestHandler<,>)) &&
                            type.Name.EndsWith(CommandHandlerSuffix);

            return isHandler;
        }

        public static bool IsQueryHandler(this Type type)
        {
            var isHandler = type.IsClass &&
                            type.IsClosedTypeOf(typeof(IRequestHandler<,>)) &&
                            type.Name.EndsWith(QueryHandlerSuffix);

            return isHandler;
        }

        public static List<Type> AllMappedDestinations()
        {
            return ApplicationAssembly.GetTypes()
                .Where(t => t.IsClass && t.IsClosedTypeOf(typeof(IMapFrom<>)) && !t.IsAbstract)
                .ToList();
        }

    }
}