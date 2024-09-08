using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Models.Database;
using ScheduleGym.Models.Commands;
using ScheduleGym.Models;
using ScheduleGym.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScheduleGym.Models.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ScheduleGym.Models.Responses;


namespace ScheduleGym.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly ScheduleGymContext _dbContext;
        private readonly IMapper _mapper;

        public PlaceRepository(ScheduleGymContext dbContext,IMapper mapper){
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Place> savePlace(PlaceCommand command){
           
            Place place =  _mapper.Map<Place>(command);
            await _dbContext.places.AddAsync(place);
            await _dbContext.SaveChangesAsync();
            return place;
        }
        public async Task<PaginanationList<PlaceResponse>> getPlaces(GetPlacesQuery query){
            var places = _dbContext.places.AsQueryable();

            if (!string.IsNullOrEmpty(query.City)){
                places = places.Where(p => p.City == query.City);
            }

            if (!String.IsNullOrEmpty(query.Includes) &&  query.Includes.Split(',').ToList().Find(a => a == "appointments") != null)
            {
                places = places.Include(p => p.appointments);
            }

            places = places
                 .Skip((query.Page - 1) * query.PageSize)
                 .Take(query.PageSize);
            if (query.GetFreeGyms)
            {
                DateTime date = Convert.ToDateTime(query.Date);
                DateTime startTime = Convert.ToDateTime(query.StartTime);
                DateTime endTime = Convert.ToDateTime(query.EndTime);

                places = places.Where(p => !p.appointments
                .Any(a => a.StartTime < endTime || a.EndTime > startTime));


                places = places.Include(p => p.avalableTerms);
            }
            var items = await places.ToListAsync();
            return new PaginanationList<PlaceResponse>
            {
                Items = _mapper.Map<List<PlaceResponse>>(items),
                Page = query.Page,
                PageSize = query.PageSize,
                Count = items.Count()
            };


        }

        public async Task<PlaceResponse> getPlaceById(int id)
        {
           var place = await _dbContext.places
                .Include(p => p.appointments)
                .Include(p => p.reviewes).ThenInclude(p => p.user)
                .FirstOrDefaultAsync(place => place.Id == id);

            return _mapper.Map<PlaceResponse>(place);
        }

        private bool IsAvilablGym(Appointments appointment, DateTime startTime, DateTime endTime)
        {
            if (!(appointment.EndTime < startTime || appointment.StartTime > endTime))
            {
                return false;
            }
            return true;
        }
    }
}