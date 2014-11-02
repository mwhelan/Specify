using Chill;

namespace Specify
{
    public class SpecifyChillTestInitializer<TContainerType> : DefaultChillTestInitializer<TContainerType> 
        where TContainerType : IChillContainer, new()
    {
    }
}
