﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Booka.BackOffice.Controller;

[ApiController]
[Route("api")]
public class BaseController : ControllerBase
{
    public int CurrentUserId =>
        int.Parse(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
}