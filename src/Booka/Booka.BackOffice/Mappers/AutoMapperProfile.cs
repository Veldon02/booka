using AutoMapper;
using Booka.BackOffice.ApiModels.Authentication;
using Booka.BackOffice.ApiModels.Workspace;
using Booka.Core.Domain;
using Booka.Core.DTOs;

namespace Booka.BackOffice.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterWorkspaceRequest, Workspace>();
        CreateMap<UpdateWorkspaceRequest, Workspace>();
        CreateMap<Workspace, WorkspaceResponse>();
        CreateMap<LoginWorkspaceRequest, TokenRequestDto>();
    }
}