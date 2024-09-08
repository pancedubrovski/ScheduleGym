using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Models.Commands;
using ScheduleGym.Models.Database;
using ScheduleGym.Models;
using ScheduleGym.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ScheduleGym.Models.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ScheduleGym.Models.Responses;

namespace ScheduleGym.Repositories
{
    public class ScheduleRepository : IScheduleRepository 
    {
        private readonly ScheduleGymContext _dbContext;
        private readonly IMapper _mapper;

        public ScheduleRepository(ScheduleGymContext dbContext,IMapper mapper){
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<AppoimnentResponse>> getSchedules(ScheduleQuery query)
        {
            var appointments = _dbContext.appointments.AsQueryable();


            appointments = appointments.Include(a => a.User);
            appointments = appointments.Include(a => a.Place);

            if (query.UserId != null && query.MyPlaces)
            {
                appointments = appointments.Include(a => a.Place)
                    .Where(a => a.Place.OwnerId == query.UserId);
            }
            else if (query.UserId != null)
            {
                appointments = appointments.Where(s => s.User.Id == query.UserId);
            }
            
            if (query.PlaceId != null)
            {
                appointments = appointments.Where(s => s.PlaceId == query.PlaceId);
            }

            var items = await appointments.ToListAsync();
            List<AppoimnentResponse> response = new List<AppoimnentResponse>();
            foreach(var a in items)
            {
                var t = _mapper.Map<AppoimnentResponse>(a);
                response.Add(t);

            }

            return response;
        }
        
        public async Task<bool> schedulePlace(ScheduleCommand command){
            DateTime s = Convert.ToDateTime(command.StartTime); 
            DateTime e = Convert.ToDateTime(command.EndTime); 
            
            
            var place = await _dbContext.places.FirstOrDefaultAsync(p => p.Id == command.PlaceId);
            var user = await _dbContext.users.FirstOrDefaultAsync(u => u.Id == command.UserId);


            Appointments ap = _mapper.Map<Appointments>(command);
            ap.User = user;
            _dbContext.appointments.Add(ap);
            place.appointments.Add(ap);

            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Review> createReview(ReviewCommand command){

            var place = await _dbContext.places.Include(p => p.reviewes).FirstOrDefaultAsync(place => place.Id == command.PlaceId);

            var user = await _dbContext.users.FirstOrDefaultAsync(u => u.Id == command.UserId);
            Review review = _mapper.Map<Review>(command);
            review.user = user;
            await _dbContext.reviewes.AddAsync(review);
            place.reviewes.Add(review);
            place.Rating = place.reviewes.Select(r => r.NumberOfStars).Average();

            await _dbContext.SaveChangesAsync();
           

            return review;
        }
    }
}