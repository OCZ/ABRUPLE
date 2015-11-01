using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Abruple.App.Hubs
{
    [HubName("contests")]
    public class ContestsHub : Hub
    {
        public void UpdateContest()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ContestsHub>();

            hubContext.Clients.All.updateContests();
        }
    }
}