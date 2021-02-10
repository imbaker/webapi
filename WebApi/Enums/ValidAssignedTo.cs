using System;

namespace WebApi.Enums
{
    [Flags]
    public enum ValidAssignedTo
    {
        None = 0,
        CurrentUser = 1,
        Workgroup = 2
    }
}