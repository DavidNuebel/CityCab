using CityCab.Libraries.Dto.Authentication;
using CityCab.Libraries.HubConnector.AuthenticationService;
using System;
using System.Threading.Tasks;

namespace CityCab.Clients.Cmd
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AuthenticationHubConnector authhub = new AuthenticationHubConnector();
            authhub.LoginResponse += OnLoginResponse;
            authhub.LogoutResponse += OnLogoutResponse;

            while(true)
            {
                Console.WriteLine("CMDS: Login | Logout");
                if (Console.ReadLine().ToLower() == "login")
                    await authhub.LoginRequest(new LoginRequest()
                    {
                        Username = "testuser",
                        Email = "test@mail.com",
                        Password = "test"
                    });
                else
                    await authhub.LogoutRequest();
            }
        }

        private static void OnLoginResponse(object sender, EventArgs e)
        {
            LoginResponse response = e as LoginResponse;
            Console.WriteLine($"LoginResponse received");
            Console.WriteLine($"AccountID: {response.AccountID}");
            Console.WriteLine($"AccessToken: {response.AccessToken}");
            Console.WriteLine($"ErrorCode: {response.ErrorCode}");
            Console.WriteLine();
        }

        private static void OnLogoutResponse(object sender, EventArgs e)
        {
            LogoutResponse response = e as LogoutResponse;
            Console.WriteLine($"LogoutResponse received");
            Console.WriteLine($"State: {response.State}");
            Console.WriteLine();
        }
    }
}
