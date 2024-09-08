using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Repositories.Interfaces;
using ScheduleGym.Models.Commands;
using ScheduleGym.Models;
using ScheduleGym.Utility;
using ScheduleGym.Models.Database;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ScheduleGym.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ScheduleGymContext _dbContext;
        private readonly IMapper _mapper;
        private readonly string _pepper;
        private readonly int _iteration = 3;

        public UserRepository(ScheduleGymContext dbContext,IMapper mapper){
            _dbContext = dbContext;
            _mapper = mapper;
            _pepper = "papper";

        }
        public async Task<User> login(LoginRequest request){

            User user =  await _dbContext.users
            .FirstOrDefaultAsync(u => u.Username == request.Username);
            var passwordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, _pepper, _iteration);

            if(user == null){
                throw new Exception("username doesn't exist");
            }
            if(user.PasswordHash != passwordHash){
                throw new Exception("password don't match");
            }
            return user;
         }

        public async Task<bool> register(RegisterCommand command){

            User user = _mapper.Map<User>(command);
            user.PasswordSalt = PasswordHasher.GenerateSalt();
            user.PasswordHash = PasswordHasher.ComputeHash(command.Password, user.PasswordSalt, _pepper, _iteration);
          
            await _dbContext.users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        
    }
}