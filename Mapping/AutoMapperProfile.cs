using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ScheduleGym.Models;
using ScheduleGym.Models.Commands;
using ScheduleGym.Models.Enums;
using ScheduleGym.Models.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ScheduleGym.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            CreateMap<PlaceCommand, Place>()
                .ForSourceMember(x => x.Photos, y => y.DoNotValidate());
       

            CreateMap<Appointments, AppoimnentResponse>();

            CreateMap<User, UserResponse>();

            CreateMap<Place, PlaceResponse>()
                .ForMember(d => d.Kind, opt => 
                    opt.MapFrom(x =>x.Kind.ToString()))
                .ForMember(s => s.appointments, c => c.MapFrom(m => m.appointments));

            CreateMap<AvailableTermCommand, AvalableTerms>()
             .ForMember(d => d.StartTime, opt =>  
             opt.MapFrom(x=> DateTime.Parse(x.StartTime)))
              .ForMember(d => d.EndTime, opt =>  
             opt.MapFrom(x=> DateTime.Parse(x.EndTime)));

             CreateMap<List<AvailableTermCommand>, List<AvalableTerms>>();

            CreateMap<RegisterCommand, User>()
               .ForMember(x => x.Role, opt => opt.MapFrom(src => Enum.Parse(typeof(Role),src.Role)));

            CreateMap<ScheduleCommand, Appointments>()
              .ForMember(d => d.StartTime, opt =>  
             opt.MapFrom(x=> DateTime.Parse(x.StartTime)))
              .ForMember(d => d.EndTime, opt =>  
             opt.MapFrom(x=> DateTime.Parse(x.EndTime)));

            CreateMap<ReviewCommand, Review>();

            CreateMap<List<Appointments>, List<AppoimnentResponse>>();

        }
    }
}