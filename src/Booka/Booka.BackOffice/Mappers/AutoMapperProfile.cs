using AutoMapper;
using Booka.BackOffice.ApiModels.Workspace;
using Booka.Domain.Models;

namespace Booka.BackOffice.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddWorkspaceRequest, Workspace>();
        CreateMap<UpdateWorkspaceRequest, Workspace>();
        CreateMap<Workspace, WorkspaceResponse>();
    }
}