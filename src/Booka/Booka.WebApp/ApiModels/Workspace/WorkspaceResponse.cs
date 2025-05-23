﻿using Booka.Core.Domain.enums.Workspace;

namespace Booka.WebApp.ApiModels.Workspace;

public class WorkspaceResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public List<WorkspaceTag> Tags { get; set; }
}