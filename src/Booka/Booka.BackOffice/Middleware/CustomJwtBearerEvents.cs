﻿using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Booka.BackOffice.Middleware;

public class CustomJwtBearerEvents : JwtBearerEvents
{
    public override Task MessageReceived(MessageReceivedContext context)
    {
        if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
        {
            var token = context.Request.Headers["Authorization"].ToString();

            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token["Bearer ".Length..].Trim();
            }

            context.Token = token;
        }

        return Task.FromResult(0);
    }

    public override Task Challenge(JwtBearerChallengeContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromException(new InvalidTokenException("Token is not found"));
        }

        return Task.FromResult(0);
    }

    public override async Task TokenValidated(TokenValidatedContext context)
    {
        var jwtToken = context.SecurityToken as JsonWebToken;

        var workspaceRepository = context.HttpContext.RequestServices.GetService<IWorkspaceRepository>();

        if (!int.TryParse(jwtToken?.Subject, out int intId))
        {
            throw new NotFoundException($"Invalid user : unable to parse id ({jwtToken?.Subject})");
        }

        var workspace = await workspaceRepository.GetById(intId);

        if (jwtToken == null || workspace == null)
        {
            throw new InvalidTokenException($"Invalid user ({jwtToken?.Subject})");
        }

        if (workspace.UpdateDate.HasValue && workspace.UpdateDate.Value > jwtToken.IssuedAt)
        {
            throw new InvalidTokenException($"Workspace data has been updated ({jwtToken?.Subject})");
        }
    }

    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        return context.Exception switch
        {
            SecurityTokenExpiredException => Task.FromException(new InvalidTokenException("Token is expired")),
            SecurityTokenValidationException => Task.FromException(new InvalidTokenException("Token is invalid")),
            _ => Task.FromResult(0)
        };
    }
}