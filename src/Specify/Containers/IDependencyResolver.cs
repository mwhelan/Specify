namespace Specify.Containers
{
    public interface IDependencyResolver : IContainer
    {
        IContainer CreateChildContainer();
    }
}
