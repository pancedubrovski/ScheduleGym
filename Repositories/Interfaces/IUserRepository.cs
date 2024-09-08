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
        public Task<User> login(LoginRequest request);

        public Task<bool> register(RegisterCommand command);
    }
}