using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebApplication2
{
    [HubName("echo1")]
    public class MyHub : Hub
    {
        public void HelloServer(string mes)
        {
            Debug.WriteLine("hub run");
            Clients.All.Hello(mes);
        }
    }
}