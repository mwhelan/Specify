using System;

namespace Specify
{
    public static class Catch
    {
        public static Exception Exception(Action action)
        {
            try
            {
                action.Invoke();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
