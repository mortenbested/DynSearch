using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynSearch
{
    internal class Searcher
    {
        public static IEnumerable<CrmObject> Search(string searchTerm, string entityFilter, Type typeFilter, IEnumerable<CrmObject> crmObjects)
        {
            searchTerm = searchTerm.ToLower();

            if (searchTerm.Length <= 2 && string.IsNullOrWhiteSpace(entityFilter) && (typeFilter == null || typeFilter == typeof(CrmAttribute)))
            {
                return new List<CrmObject>();
            }

            IEnumerable<CrmObject> results = crmObjects.AsParallel();

            //Entity filter
            if (!string.IsNullOrEmpty(entityFilter))
            {
                results = results.Where(crmObject => crmObject.EntityDisplayName == entityFilter);
            }

            //Type filter
            if (typeFilter != null)
            {
                results = results.Where(crmObject => crmObject.GetType() == typeFilter);
            }

            //String filter
            results = results.AsParallel().Where(crmObject => crmObject.SearchString.Contains(searchTerm));

            return results.ToList();
        }
    }
}
