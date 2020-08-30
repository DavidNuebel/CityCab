using CityCab.Libraries.Database.Context;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityCab.Services.DriverService.Hubs
{
    public class DriversHub : Hub
    {
        private CityCabServiceContext DbContext { get; set; }

        public DriversHub(CityCabServiceContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task CreateJob()
        {

        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            await base.OnConnectedAsync();
        }
    }
}
