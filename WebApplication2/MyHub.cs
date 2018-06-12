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
        public static int count = 0;

        public void listGroup()
        {
            Clients.All.listGroup(Groups);
        }

        public void subcribe(string group)
        {
            Groups.Add(Context.ConnectionId, group);
            listGroup();
        }
        public void message(string mes, string group)
        {
            var _mes = string.Format("Hello client with id: {0}, this time is: {1:F}, you are client no: {2}", Context.ConnectionId, DateTime.Now, ++count);

            // send all client:
            //Clients.All.message(_mes);

            // send to client just come:
            //Clients.Caller.message(_mes);

            // send to group:
            Clients.Group(group).message(mes);
        }
    }
}

/*
 Clients.
    All
    Caller
    Group(groupName)

Groups.
    Add(id, GroupName)
     */