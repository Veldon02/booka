using Microsoft.AspNetCore.Mvc;

namespace Booka.BackOffice.Filters.Attributes;

public class OnlyLoggedWorkspaceAttribute : TypeFilterAttribute
{
    public OnlyLoggedWorkspaceAttribute()
        : base(typeof(OnlyLoggedWorkspaceFilter))
    {
    }
}