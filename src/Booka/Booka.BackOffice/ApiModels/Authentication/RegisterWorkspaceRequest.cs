﻿namespace Booka.BackOffice.ApiModels.Authentication;

public class RegisterWorkspaceRequest
{
    public string Name { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}