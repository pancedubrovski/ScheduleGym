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
using System.Numerics;


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
            var photos = await savePhotoAsync(command.Photos);
            place.Photos = photos;
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
                .Any(a => a.Date.Date == date.Date && (a.EndTime.TimeOfDay < startTime.TimeOfDay
                || a.StartTime.TimeOfDay > endTime.TimeOfDay)));


                

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
                .Include(p => p.Photos)
                .Include(p => p.reviewes).ThenInclude(p => p.user)
                .FirstOrDefaultAsync(place => place.Id == id);

            return _mapper.Map<PlaceResponse>(place);
        }

        public async Task<bool> deletePlace(int id)
        {
            Place place = await _dbContext.places.FirstOrDefaultAsync(place => place.Id == id);

            if(place == null)
            {
                throw new Exception("place with id "+id+" doesn't exist");
            }
            _dbContext.places.Remove(place);
            return true;
        }

        private bool IsAvilablGym(Appointments appointment, DateTime startTime, DateTime endTime)
        {
            if (!(appointment.EndTime < startTime || appointment.StartTime > endTime))
            {
                return false;
            }
            return true;
        }

        private async Task<List<Photo>> savePhotoAsync(List<IFormFile> photos)
        {
            List<Photo> photolist = new List<Photo>();
            if (photos.Count > 0)
            {
                foreach (var formFile in photos)
                {
                    if (formFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(memoryStream);
                           
                                var newphoto = new Photo()
                                {
                                    Bytes = memoryStream.ToArray(),
                                    Description = formFile.FileName,
                                    FileExtension = Path.GetExtension(formFile.FileName),
                                    Size = formFile.Length,
                                };
                                photolist.Add(newphoto);
                        }
                    }
                }
            }
            return photolist;
        }
        
    }
}