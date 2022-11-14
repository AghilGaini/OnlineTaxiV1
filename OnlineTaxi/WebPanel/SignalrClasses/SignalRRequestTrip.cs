using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebPanel.SignalrClasses
{
    public class SignalRRequestTrip : Hub
    {
        public static long number = 0;
        public override async Task OnConnectedAsync()
        {
            number++;
            if (number % 2 == 0)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "even");
                await SendMsgToOdd();
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "odd");
                await SendMsgToEven();
            }
        }

        public async Task SendMsgToAll(string msg)
        {
            try
            {
                await Clients.All.SendAsync("RecieveMsg", "From Signal : " + msg);
            }
            catch (System.Exception ex)
            {
                await Clients.Caller.SendAsync("SignalRNotification", ex.Message);
            }
        }

        public async Task SendMsgToOdd()
        {
            try
            {
                string msg = "msg for odd";
                await Clients.Group("odd").SendAsync("SignalRNotification", msg);
            }
            catch (System.Exception ex)
            {
                await Clients.Caller.SendAsync("SignalRNotification", ex.Message);
            }
        }

        public async Task SendMsgToEven()
        {
            try
            {
                string msg = "msg for even";
                await Clients.Group("even").SendAsync("SignalRNotification", msg);
            }
            catch (System.Exception ex)
            {
                await Clients.Caller.SendAsync("SignalRNotification", ex.Message);
            }
        }
    }
}
