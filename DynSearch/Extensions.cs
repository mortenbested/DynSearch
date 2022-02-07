using Microsoft.Xrm.Sdk;

namespace DynSearch
{
    internal static class Extensions
    {
        public static string GetFormattedValue(this Entity entity, string attributeName)
        {
            if (entity.FormattedValues.ContainsKey(attributeName))
            {
                return entity.FormattedValues[attributeName];
            }
            else
            {
                return null;
            }
        }
    }
}
