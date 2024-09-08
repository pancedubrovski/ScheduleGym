using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleGym.Utility
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next){
            _next = next;
        }

        public async Task Invoke(HttpContext context){

            Console.WriteLine("Before executing the next middleware.");

        await _next(context);

        // Logic to be executed after the next middleware in the pipeline
        Console.WriteLine("After executing the next middleware.");
        }
    }
}