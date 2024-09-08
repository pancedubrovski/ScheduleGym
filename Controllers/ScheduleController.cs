using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleGym.Models.Commands;
using ScheduleGym.Models.Query;
using ScheduleGym.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace ScheduleGym.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/schedule-gym")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _scheduleRepository;
       

        public ScheduleController(IScheduleRepository scheduleRepository){
            _scheduleRepository = scheduleRepository;
        }
        [HttpGet]
        public IActionResult getSchedule(){
            return Ok("OK");
        }
        [HttpGet]
        [Route("schedules")]
        public async Task<IActionResult> getSchedules([FromQuery] ScheduleQuery query)
        {
            
            try
            {
                var schedules = await _scheduleRepository.getSchedules(query);
                return Ok(schedules);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPost]
        [Route("schedule")]
        public async Task<IActionResult> scheduleGym([FromBody] ScheduleCommand command){
          
            try {
                await _scheduleRepository.schedulePlace(command);
                return StatusCode(201);
            } catch (Exception e){
                return BadRequest(e.Message);
            }
           
        }
        [HttpGet]
        [Route("review")]
        public IActionResult getReviews([FromQuery] ReviewQuery query){
            return Ok("OKK");
        }
        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> createReview([FromBody] ReviewCommand command){
            var review = await _scheduleRepository.createReview(command);
            return Ok(review);
        }
    }
}