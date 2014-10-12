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
    }
}
