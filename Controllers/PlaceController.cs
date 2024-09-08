using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleGym.Repositories.Interfaces;
using ScheduleGym.Models.Commands;
using Microsoft.AspNetCore.Authorization;
using ScheduleGym.Models.Query;

namespace ScheduleGym.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/schedule-gym/places")]
    public class PlaceController : ControllerBase
    {
        
        public readonly IPlaceRepository _placeRepository; 

        public PlaceController(IPlaceRepository placeRepository){
            _placeRepository = placeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> createPlace([FromBody] PlaceCommand command){
            var res = await _placeRepository.savePlace(command);
            
            
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> retrivePlace([FromQuery] GetPlacesQuery query){
            var res = await _placeRepository.getPlaces(query);
       
            
            return Ok(res);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getPlaceById([FromRoute] int id)
        {
            var res = await _placeRepository.getPlaceById(id);


            return Ok(res);
        }


    }
    
}