namespace Specify.Autofac.Samples.ChildContainers
{
    public interface IDependency1
    {
        int Value { get; set; }
    }
    public class Dependency1 : IDependency1
    {
        public int Value { get; set; } = 5;
    }
    public class TestDependency1 : IDependency1
    {
        public int Value { get; set; } = 99;
    }
    public class ConcreteObject
    {
        public readonly IDependency1 Dependency1;

        public ConcreteObject(IDependency1 dependency1)
        {
            Dependency1 = dependency1;
        }
    }
}