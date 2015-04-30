using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using ContosoUniversity.Infrastructure.DAL.Repositories;
using ContosoUniversity.Infrastructure.Mapping;
using Mapper = ContosoUniversity.Infrastructure.Mapping.Mapper;

namespace ContosoUniversity
{
    public static class DependenciesConfig
    {
        public static IContainer RegisterDependencies(ContainerBuilder builder = null)
        {
            if(builder == null) 
                builder = new ContainerBuilder();

            ConfigureDependencies(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }

        public static void ConfigureDependencies(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<CourseRepository>().As<ICourseRepository>();//.InstancePerRequest();
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>();
            builder.RegisterType<InstructorRepository>().As<IInstructorRepository>();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();//.InstancePerRequest();

            builder.RegisterInstance(AutoMapper.Mapper.Engine).As<IMappingEngine>();
            builder.RegisterType<Mapper>().As<IMapper>();
        }
    }
}