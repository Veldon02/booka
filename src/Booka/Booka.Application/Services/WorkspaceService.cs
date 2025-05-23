﻿using Booka.Application.Exceptions;
using Booka.Domain.Interfaces.Repositories;
using Booka.Domain.Interfaces.Services;
using Booka.Domain.Models;

namespace Booka.Application.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly IWorkspaceRepository _workspaceRepository;

    public WorkspaceService(IWorkspaceRepository workspaceRepository)
    {
        _workspaceRepository = workspaceRepository;
    }

    public Task<Workspace> AddAsync(Workspace workspace)
    {
        return _workspaceRepository.Add(workspace);
    }

    public Task<List<Workspace>> GetAllAsync()
    {
        return _workspaceRepository.GetAll();
    }

    public async Task UpdateAsync(int workspaceId, Workspace workspace)
    {
        var existingWorkspace = await _workspaceRepository.GetById(workspaceId, false)
                                ?? throw new NotFoundException($"Workspace {workspaceId} is not found");

        existingWorkspace.Name = workspace.Name;
        existingWorkspace.Address = workspace.Address;
        existingWorkspace.Email = workspace.Email;
        
        await _workspaceRepository.Update(existingWorkspace);
    }

    public async Task<Workspace> GetByIdAsync(int workspaceId)
    {
        return await _workspaceRepository.GetById(workspaceId) 
               ?? throw new NotFoundException($"Workspace {workspaceId} is not found");
    }
}