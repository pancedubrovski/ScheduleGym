using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Models.Commands;
using ScheduleGym.Models;
using ScheduleGym.Models.Query;
using ScheduleGym.Models.Responses;

namespace ScheduleGym.Repositories.Interfaces
{
    public interface IPlaceRepository
    {
        public Task<Place> savePlace(PlaceCommand command);
        public Task<PaginanationList<PlaceResponse>> getPlaces(GetPlacesQuery query);

        public Task<PlaceResponse> getPlaceById(int id);
        
    }
}