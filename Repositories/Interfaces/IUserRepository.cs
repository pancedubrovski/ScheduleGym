using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Models.Commands;
using ScheduleGym.Models;

namespace ScheduleGym.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> Login(LoginRequest request);

        public Task<User> Register(RegisterCommand command);
    }
}