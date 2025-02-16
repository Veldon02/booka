using AutoMapper;
using Booka.Core.Domain;
using Booka.Core.DTOs.Security;
using Booka.WebApp.ApiModels.Authentication;
using Booka.WebApp.ApiModels.Booking;
using Booka.WebApp.ApiModels.Workspace;

namespace Booka.WebApp.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserRegisterRequest, User>()
            .ForMember(x => x.Email, y => y.MapFrom(x => x.UserEmail));

        CreateMap<UserLogInRequest, TokenRequestDto>()
            .ForMember(x => x.Email, y => y.MapFrom(x => x.UserEmail));

        CreateMap<Workspace, WorkspaceResponse>();
        CreateMap<Workplace, WorkplaceResponse>()
            // TODO Move this from here
            .ForMember(x => x.IsAvailable, y => y.MapFrom(x => x.IsAvailable(DateTime.UtcNow)));

        CreateMap<Booking, BookingResponse>()
            .ForMember(x => x.WorkspaceId, y => y.MapFrom(x => x.Workplace.WorkspaceId));
        CreateMap<CreateBookingRequest, Booking>();
    }
}