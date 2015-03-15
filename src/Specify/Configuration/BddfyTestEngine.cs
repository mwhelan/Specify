using TestStack.BDDfy;

namespace Specify.Configuration
{
    internal class BddfyTestEngine : ITestEngine
    {
        public void Execute(ISpecification specification)
        {
            if (specification.Examples == null)
            {
                specification
                    .BDDfy();
            }
            else
            {
                specification
                    .BDDfy()
                    .WithExamples(specification.Examples);
            }
        }
    }
}