using System.Collections.Generic;
using WebApi.Enums;

namespace WebApi.Extensions
{
    public static class StringExtensions
    {
        public static string Transform(this ValidAssignedTo value)
        {
            var assignedToKeys = new Dictionary<ValidAssignedTo, string>()
            {
                { ValidAssignedTo.CurrentUser, "A" },
                { ValidAssignedTo.Workgroup, "B" }
            };

            return assignedToKeys[value];
        }
    }
}