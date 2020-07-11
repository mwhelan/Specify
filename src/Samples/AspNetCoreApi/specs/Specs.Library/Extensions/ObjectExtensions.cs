using System.Linq;
using System.Reflection;

namespace Specs.Library.Extensions
{
    public static class ObjectExtensions
    {
        public static T MapTo<T>(this object source, T target) 
        {
            foreach (PropertyInfo sourceProp in source.GetType().GetProperties())
            {
                PropertyInfo targetProp = target.GetType().GetProperties().FirstOrDefault(p => p.Name == sourceProp.Name);
                if (targetProp != null && targetProp.GetType().Name == sourceProp.GetType().Name)
                {
                    targetProp.SetValue(target, sourceProp.GetValue(source));
                }
            }
            return target;
        }
    }
}