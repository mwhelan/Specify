namespace Specify
{
    public interface IContainerRoot : IContainer
    {
        IContainer GetChildContainer();
        //IChildContainerBuilder Overrides { get; }
    }
}