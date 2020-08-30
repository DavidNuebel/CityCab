using CityCab.Libraries.Database.Context;
using CityCab.Libraries.Dto.Authentication;
using CityCab.Libraries.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace CityCab.Services.AuthenticationService.Hubs
{
    public class AuthenticationHub : Hub
    {
        private CityCabServiceContext DbContext { get; set; }

        public AuthenticationHub(CityCabServiceContext dbContext)
        {
            DbContext = dbContext;

            if(!DbContext.Accounts.Any())
            {
                DbContext.Accounts.Add(new Account()
                {
                    Username = "testuser",
                    Email = "test@mail.com",
                    Password = "test",
                    Person = new Person()
                    {
                        Lastname = "Mustermann",
                        Firstname = "Max"
                    }
                });
                dbContext.SaveChanges();
            }
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"{Context.ConnectionId} disconnected");
            await LogoutAccount();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task LoginRequest(LoginRequest request)
        {
            Console.WriteLine($"{Context.ConnectionId} -> LoginRequest");
            if (request != null) // check if request is empty
            {
                // try getting account with correct cedentials
                Account acc = DbContext.Accounts.Where(acc => (acc.Email == request.Email || acc.Username == request.Username)).First();

                // clear all connections if available
                foreach (Connection con in DbContext.Connections.Where(con => con.Account.ID == acc.ID))
                    DbContext.Connections.Remove(con);

                if (acc != null) // check if account was found
                {
                    if (acc.Password == acc.Password) // check if password is correct
                    {
                        acc.Connections = null; // clear connections
                        acc.AccessToken = Guid.NewGuid(); // create new access token

                        // creating new connection for this service
                        acc.Connections = new List<Connection>() 
                        {
                            new Connection()
                            {
                                ConnectionID = Context.ConnectionId
                            }
                        };

                        DbContext.Update(acc); // update data
                        await DbContext.SaveChangesAsync(); // save updated data

                        // send response with account id and accesstoken
                        await Clients.Caller.SendAsync("LoginResponse", new LoginResponse()
                        {
                            AccountID = acc.ID,
                            AccessToken = acc.AccessToken
                        });

                        await Groups.AddToGroupAsync(Context.ConnectionId, "AuthenticatedUsers");

                        Console.WriteLine($"{Context.ConnectionId} -> logged in");
                    }
                    else
                    {
                        await Clients.Caller.SendAsync("LoginResponse", new LoginResponse()
                        {
                            ErrorCode = "Auth03" // incorrect password
                        });

                        Console.WriteLine($"{Context.ConnectionId} -> login failed");
                    }
                }
                else
                {
                    await Clients.Caller.SendAsync("LoginResponse", new LoginResponse()
                    {
                        ErrorCode = "Auth02" // not account was found
                    });

                    Console.WriteLine($"{Context.ConnectionId} -> login failed");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("LoginResponse", new LoginResponse()
                {
                    ErrorCode = "Auth01" // empty is request
                });
            }
               
        }

        public async Task LogoutRequest()
        {
            Console.WriteLine($"{Context.ConnectionId} -> LogoutRequest");
            await LogoutAccount();
            await Clients.Caller.SendAsync("LogoutResponse", true);
        }

        public async Task AddConnection(/*AddConnectionRequest?*/)
        {
            // TODO
            // add connections for other services to authenticate client sessions
        }

        public async Task RemoveConnection(/*RemoveConnectionRequest?*/)
        {
            // TODO
            // remove connections to unsubscribe hubs
        }

        private async Task LogoutAccount()
        {
            Console.WriteLine($"{Context.ConnectionId} logging out");
            try
            {
                Account acc = DbContext.Accounts.Where(acc => acc.Connections.Contains(DbContext.Connections.Where(c => c.ConnectionID == Context.ConnectionId).First())).First();
                foreach (Connection con in DbContext.Connections.Where(con => con.Account.ID == acc.ID))
                    DbContext.Connections.Remove(con);
                acc.AccessToken = Guid.Empty;
                DbContext.Update(acc);
                await DbContext.SaveChangesAsync();

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "AuthenticatedUsers");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
