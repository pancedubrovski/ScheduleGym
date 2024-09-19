using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleGym.Models.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ScheduleGym.Repositories.Interfaces;
using System.Collections;
using Swashbuckle.AspNetCore.SwaggerGen;



namespace ScheduleGym.Controllers
{
    [ApiController]
    [Route("v1/schedule-gym")]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public UserController(IConfiguration config,IUserRepository userRepository){
            _config = config;
            _userRepository = userRepository;
        }

        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest){
            
           
            try {
                var user = await _userRepository.Login(loginRequest);
                
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                List<Claim> claims = new List<Claim>{
                    new Claim(ClaimTypes.Name,user.Username),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                    
                };

                IDictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("username", user.Username);
                dic.Add("role", user.Role.ToString());
                dic.Add("ownerId", user.Id.ToString());


                JwtPayload payload = new JwtPayload(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],null, dic,null, DateTime.Now.AddMinutes(120), null);


                var Sectoken = new JwtSecurityToken(new JwtHeader(credentials), payload);
           

                var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

                var response = new
                {
                    token = token,
                    user = user
                };
             
                return Ok(response);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }

           
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command){
            await _userRepository.Register(command); 
            return StatusCode(201);
        }
    }
}