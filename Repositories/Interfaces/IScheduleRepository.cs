using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Models;
using ScheduleGym.Models.Commands;
using ScheduleGym.Models.Query;
using ScheduleGym.Models.Responses;

namespace ScheduleGym.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        public Task<List<AppoimnentResponse>> getSchedules(ScheduleQuery query);
        public Task<bool> schedulePlace(ScheduleCommand command);
        public Task<Review> createReview(ReviewCommand command);
    }
}