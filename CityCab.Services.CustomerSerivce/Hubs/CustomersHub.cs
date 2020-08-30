using CityCab.Libraries.Database.Context;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityCab.Services.CustomerSerivce.Hubs
{
    public class CustomersHub : Hub
    {
        private CityCabServiceContext DbContext { get; set; }

        public CustomersHub(CityCabServiceContext dbContext)
        {
            DbContext = dbContext;
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            await base.OnConnectedAsync();
        }
    }
}
