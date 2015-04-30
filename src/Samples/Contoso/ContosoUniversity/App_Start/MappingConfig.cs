using AutoMapper;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity
{
    public static class MappingConfig
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<Course, CourseViewModel>();
            Mapper.CreateMap<Department, DepartmentViewModel>();
            Mapper.CreateMap<Enrollment, EnrollmentViewModel>();
            Mapper.CreateMap<Instructor, InstructorViewModel>();
            Mapper.CreateMap<OfficeAssignment, OfficeAssignmentViewModel>();
            Mapper.CreateMap<Student, StudentViewModel>();
        }
    }
}