using System;

namespace Specify.Autofac
{
    public class AutofacImmutableContainerException : Exception
    {
        public AutofacImmutableContainerException()
            : base(GetErrorMessage())
        {
        }

        private static string GetErrorMessage()
        {
            return "Registering types in tests is not currently supported for Autofac since its Container became immutable from version 5.0. You can still register items in the Container in the Bootstrapper, but to register items in the Container from tests requires Specify.Autofac v3.0.0-beta01 or earlier and Autofac 4.9.4 or earlier.";
        }
    }
}