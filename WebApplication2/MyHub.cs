using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace WebApplication2
{

    [HubName("echo1")]
    //[Authorize(Users = "DOMAIN/user")]
    public class MyHub : Hub
    {
        public static int count = 0;
        static readonly HashSet<User> connectionIds = new HashSet<User>();

        // subcribe in to group:
        public void subcribe(string group)
        {
            Clients.Caller.name = Clients.Caller.name ?? "no name: ";
            // for message group:
            Groups.Add(Context.ConnectionId, group);

            // for send all but this:
            var connectionId = Context.ConnectionId;
            connectionIds.UnionWith(new[] { new User { name = Clients.Caller.name, connectionId = connectionId } });
            Clients.All.connections(connectionIds);

            
        }

        // leave group:
        public void unSubcribe(string group)
        {
            Groups.Remove(Context.ConnectionId, group);
        }

        private string _getMes(string mes)
        {
            return Clients.Caller.name + " : " + mes;
        }

        //================================message =======================================
        // send all client:
        public void messageAll(string mes)
        {
            Clients.All.message(_getMes(mes));
        }

        // send to client just come:
        public void messageCaller(string mes)
        {
            Clients.Caller.message(_getMes(mes));
        }

        // send to one client:
        public void messageClient(string id, string mes)
        {
            Clients.Client(id).message(_getMes(mes));
        }


        // send to group:
        public void messageGroup(string mes, string group)
        {
            Clients.Group(group).message(_getMes(mes));
        }

        // send to Group except caller:
        public void messageGroupExcept(string mes, string group)
        {
            Clients.Group(group, Context.ConnectionId).message(_getMes(mes));
        }

        // send to other except the sender: any group can be get this mes.
        public void messageOthers(string mes)
        {
            Clients.Others.message(_getMes(mes));
        }

        // send to All except caller:
        public void messageAllExcept(string mes, string excludeConnectionId)
        {
            var allExcept = Clients.AllExcept(excludeConnectionId);
            allExcept.message(_getMes(mes));
        }

        //============================================== part 2===================================================
       

        //======================overide ===============================
        public override Task OnConnected()
        {
            Trace.WriteLine(string.Format("Connected: {0}", Context.ConnectionId));
            return base.OnConnected();
        }
        //public override Task OnDisconnected()
        //{
        //    Trace.WriteLine(string.Format("Disconnected: {0}", Context.ConnectionId));
        //    return base.OnDisconnected();
        //}
        public override Task OnReconnected()
        {
            Trace.WriteLine(string.Format("Reconnected: {0}",  Context.ConnectionId));
            return base.OnReconnected();
        }

    }

    class User
    {
        public string name { get; set; }
        public string connectionId { get; set; }
    }
}

/*
 require for SignalR:
 Microsoft.AspNet.SignalR library
 +file Startup: 
    public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
+ create hub class implement Hub

     */

/*
+require for other app c#:
Microsoft.AspNet.SignalR.Client library:
var url = "http://localhost:60579";
            var connection = new HubConnection(url);
            var hub = connection.CreateHubProxy("echo1");
            connection.Start().Wait();
            hub.Invoke("HelloServer ", "Hello SignalR!");
            Console.ReadLine();
     */

/*
Clients
    .All
    .Caller
    .Group(groupName)
    .Others

Groups
    .Add(id, GroupName)
    .Remove(groupName)
     */
