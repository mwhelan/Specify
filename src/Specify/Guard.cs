using System;

namespace Specify
{
    public static class Guard
    {
        public static void Against(bool condition, string errorMessage)
        {
            if (condition)
            {
                throw new ArgumentException(errorMessage);
            }
        }

        public static void AgainstNull(object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
