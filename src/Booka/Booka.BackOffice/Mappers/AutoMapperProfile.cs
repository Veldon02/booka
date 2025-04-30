using AutoMapper;
using Booka.BackOffice.ApiModels.Authentication;
using Booka.BackOffice.ApiModels.Workplace;
using Booka.BackOffice.ApiModels.Workspace;
using Booka.Core.Domain;
using Booka.Core.DTOs.Security;
using Booka.Core.DTOs.Workspace;

namespace Booka.BackOffice.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterWorkspaceRequest, Workspace>();
        CreateMap<UpdateWorkspaceRequest, WorkspaceDto>();
        CreateMap<Workspace, WorkspaceResponse>();
        CreateMap<LoginWorkspaceRequest, TokenRequestDto>();
        CreateMap<CreateWorkplaceRequest, Workplace>();
        CreateMap<UpdateWorkplaceRequest, Workplace>();
        CreateMap<Workplace, WorkplaceResponse>();
    }
}