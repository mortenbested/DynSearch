using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynSearch
{
    internal static class Icons
    {
        public const int IconProcessIndex = 0;
        public const int IconFieldIndex = 1;
        public const int IconEntityIndex = 2;
        public const int IconBusinessRuleIndex = 3;
        public const int IconBusinessWebResourceIndex = 4;
        public const int IconRoleIndex = 5;
        public const int IconSolutionIndex = 6;

        public static IEnumerable<Image> GetIcons()
        {
            return new List<Image>()
            {
                Properties.Resources.IconProcess,
                Properties.Resources.IconField,
                Properties.Resources.IconEntity,
                Properties.Resources.IconBusinessRule,
                Properties.Resources.IconWebResource,
                Properties.Resources.IconSecurityRole,
                Properties.Resources.IconSolution
            };
        }
    }
}
