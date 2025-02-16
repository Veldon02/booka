using Microsoft.AspNetCore.Mvc;

namespace Booka.WebApp.Filters.Attributes;

public class OnlyCurrentUserAttribute : TypeFilterAttribute<OnlyCurrentUserFilter>
{
}