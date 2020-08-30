using CityCab.Libraries.Dto.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CityCab.Libraries.HubConnector.AuthenticationService
{
    public class AuthenticationHubConnector
    {
        private HubConnection Connection { get; set; }

        public AuthenticationHubConnector()
        {
            ConnectAsync().Wait();
        }

        private async Task ConnectAsync()
        {
            Connection = new HubConnectionBuilder().WithUrl("https://localhost:44390/auth").Build();

            RegisterEvents();

            try
            {
                await Connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task OnClosed(Exception arg)
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await Connection.StartAsync();
        }

        private void RegisterEvents()
        {
            Connection.Closed += OnClosed;
            Connection.On<LoginResponse>("LoginResponse", (response) => OnLoginResponse(response));
            Connection.On<LogoutResponse>("LogoutResponse", (response) => OnLogoutResponse(response));
        }

        #region Login
        public async Task LoginRequest(LoginRequest request)
        {
            try
            {
                await Connection.InvokeAsync("LoginRequest", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public event EventHandler LoginResponse;
        protected virtual void OnLoginResponse(LoginResponse response)
        {
            LoginResponse?.Invoke(this, response);
        }
        #endregion

        #region Logout
        public async Task LogoutRequest()
        {
            try
            {
                await Connection.InvokeAsync("LogoutRequest");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public event EventHandler LogoutResponse;
        protected virtual void OnLogoutResponse(LogoutResponse response)
        {
            LogoutResponse?.Invoke(this, response);
        }
        #endregion
    }
}
