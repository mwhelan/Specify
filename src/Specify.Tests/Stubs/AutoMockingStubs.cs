using System.Collections.Generic;

namespace Specify.Tests.Stubs
{
    class ConcreteObjectWithNoConstructor
    {
    }
    class ConcreteObjectWithOneInterfaceConstructor
    {
        public readonly IDependency1 Dependency1;

        public ConcreteObjectWithOneInterfaceConstructor(IDependency1 dependency1)
        {
            Dependency1 = dependency1;
        }
    }
    class ConcreteObjectWithOneInterfaceCollectionConstructor
    {
        public readonly IEnumerable<IDependency3> Collection;

        public ConcreteObjectWithOneInterfaceCollectionConstructor(IEnumerable<IDependency3> collection)
        {
            Collection = collection;
        }
    }
    class ConcreteObjectWithOneConcreteConstructor
    {
        public readonly Dependency1 Dependency1;

        public ConcreteObjectWithOneConcreteConstructor(Dependency1 dependency1)
        {
            Dependency1 = dependency1;
        }
    }

    class ConcreteObjectWithMultipleConstructors
    {
        public readonly IDependency1 Dependency1;
        public readonly IDependency2 Dependency2;

        public ConcreteObjectWithMultipleConstructors() { }
        public ConcreteObjectWithMultipleConstructors(IDependency1 dependency1)
        {
            Dependency1 = dependency1;
        }
        public ConcreteObjectWithMultipleConstructors(IDependency1 dependency1, IDependency2 dependency2)
        {
            Dependency1 = dependency1;
            Dependency2 = dependency2;
        }
    }

    public interface IDependency1
    {
        int Value { get; set; }
    }
    public class Dependency1 : IDependency1
    {
        private int _value = 5;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
    class Dependency2 : IDependency2
    {
        public void Run(){}
    }
    class Dependency3 : IDependency3 {}
    class Dependency4 : IDependency3 {}

    public interface IDependency2
    {
        void Run();
    }
    public interface IDependency3 { }
}
