using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Models;
using Route = Entities.Models.Route;
using Task = Entities.Models.Task;

namespace DRIVERI_MANAGEMENT_PROJECT_BACKEND.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender))
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => (int)src.BloodGroup));

            CreateMap<PersonDto, Person>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (Genders)src.Gender))
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => (BloodGroups)src.BloodGroup))
                .ForMember(dest => dest.Driver, opt => opt.Ignore());

            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => (int)src.RoleName))
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId));

            CreateMap<RoleDto, Role>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => (Roles)src.RoleName))
                .ForMember(dest => dest.Person, opt => opt.Ignore());

            CreateMap<Garage, GarageDto>().ReverseMap();

            CreateMap<Chief, ChiefDto>()
                 .ForMember(dest => dest.ChiefFirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                 .ForMember(dest => dest.ChiefLastName, opt => opt.MapFrom(src => src.Person.LastName))
                 .ForMember(dest => dest.GarageName, opt => opt.MapFrom(src => src.Garage.GarageName))
                 .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                 .ForMember(dest => dest.GarageId, opt => opt.MapFrom(src => src.GarageId))
                 .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId));

            CreateMap<ChiefDto, Chief>()
                .ForMember(dest => dest.Person, opt => opt.Ignore())
                .ForMember(dest => dest.Garage, opt => opt.Ignore())
                .ForMember(dest => dest.Drivers, opt => opt.Ignore());

            CreateMap<Chieftiency, ChieftaincyDto>()
                .ForMember(dest => dest.GarageName, opt => opt.MapFrom(src => src.Garage.GarageName));

            CreateMap<ChieftaincyDto, Chieftiency>()
                .ForMember(dest => dest.Garage, opt => opt.Ignore());

            CreateMap<Driver, DriverDto>()
               .ForMember(dest => dest.PersonFirstName, opt => opt.MapFrom(src => src.Person.FirstName))
               .ForMember(dest => dest.PersonLastName, opt => opt.MapFrom(src => src.Person.LastName))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Person.Phone))
               .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(src => src.Person.RegistrationNumber))
               .ForMember(dest => dest.Garage, opt => opt.MapFrom(src => src.Garage.GarageName))
               .ForMember(dest => dest.ChiefId, opt => opt.MapFrom(src => src.ChiefId))
               .ForMember(dest => dest.ChiefFirstName, opt => opt.MapFrom(src => src.Chief.Person.FirstName))
               .ForMember(dest => dest.ChiefLastName, opt => opt.MapFrom(src => src.Chief.Person.LastName))
               .ForMember(dest => dest.Cadre, opt => opt.MapFrom(src => (int)src.Cadre))
               .ForMember(dest => dest.DayOff, opt => opt.MapFrom(src => (int)src.DayOff))
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

            CreateMap<DriverDto, Driver>()
                .ForMember(dest => dest.Person, opt => opt.Ignore())
                .ForMember(dest => dest.Garage, opt => opt.Ignore())
                .ForMember(dest => dest.Chief, opt => opt.Ignore())
                .ForMember(dest => dest.Cadre, opt => opt.MapFrom(src => (CadreTypes)src.Cadre))
                .ForMember(dest => dest.DayOff, opt => opt.MapFrom(src => (Days)src.DayOff));

            CreateMap<LineDto, Line>();
            CreateMap<Line, LineDto>();

            CreateMap<RouteDto, Route>();
            CreateMap<Route, RouteDto>();

            CreateMap<Vehicle, VehicleDto>()
                 .ForMember(dest => dest.GarageName, opt => opt.MapFrom(src => src.Garage.GarageName))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status));

            CreateMap<VehicleDto, Vehicle>()
                .ForMember(dest => dest.Garage, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (VehicleStatuses)src.Status));

            CreateMap<Task, TaskDto>()
                .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(src => src.Driver.Person.RegistrationNumber))
                .ForMember(dest => dest.DoorNo, opt => opt.MapFrom(src => src.Vehicle.DoorNo))
                .ForMember(dest => dest.RouteName, opt => opt.MapFrom(src => src.Route.RouteName))
                .ForMember(dest => dest.LineCode, opt => opt.MapFrom(src => src.LineCode.LineName))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => (int)src.Direction))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status));

            CreateMap<TaskDto, Task>()
                .ForMember(dest => dest.Driver, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
                .ForMember(dest => dest.Route, opt => opt.Ignore())
                .ForMember(dest => dest.LineCode, opt => opt.Ignore())
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => (Directions)src.Direction))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (Tasks)src.Status));
        }
    }
}
