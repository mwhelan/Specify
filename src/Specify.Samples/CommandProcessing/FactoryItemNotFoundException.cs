using System;

namespace Specify.Samples.CommandProcessing
{
    public class FactoryItemNotFoundException : Exception
    {
        public FactoryItemNotFoundException(Type type)
            : base(string.Format("Handler not found for command type: {0}", type.Name))
        {
        }
    }
}