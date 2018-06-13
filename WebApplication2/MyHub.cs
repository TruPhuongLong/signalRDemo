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
        static readonly HashSet<string> connectionIds = new HashSet<string>();

        // subcribe in to group:
        public void subcribe(string group)
        {
            // for message group:
            Groups.Add(Context.ConnectionId, group);

            // for send all but this:
            var connectionId = Context.ConnectionId;
            connectionIds.UnionWith(new[] { connectionId });
            Clients.All.connections(connectionIds);
        }

        // leave group:
        public void unSubcribe(string group)
        {
            Groups.Remove(Context.ConnectionId, group);
        }

        // send all client:
        public void messageAll(string mes)
        {
            var _mes = string.Format("Hello client with id: {0}, this time is: {1:F}, mes: {2}", Context.ConnectionId, DateTime.Now, mes);
            Clients.All.message(_mes);
        }

        // send to client just come:
        public void messageCaller(string mes)
        {
            var _mes = string.Format("Hello client with id: {0}, this time is: {1:F}, mes: {2}", Context.ConnectionId, DateTime.Now, mes);
            Clients.Caller.message(_mes);
        }


        // send to group:
        public void messageGroup(string mes, string group)
        {
            var _mes = string.Format("Hello client with id: {0}, this time is: {1:F}, mes: {2}", Context.ConnectionId, DateTime.Now, mes);
            Clients.Group(group).message(mes);
        }

        // send to Group except caller:
        public void messageGroupExcept(string mes, string group)
        {
            var _mes = string.Format("Hello client with id: {0}, this time is: {1:F}, mes: {2}", Context.ConnectionId, DateTime.Now, mes);
            Clients.Group(group, Context.ConnectionId).message(mes);
        }

        // send to other except the sender: any group can be get this mes.
        public void messageOthers(string mes)
        {
            var _mes = string.Format("Hello client with id: {0}, this time is: {1:F}, mes: {2}", Context.ConnectionId, DateTime.Now, mes);
            Clients.Others.message(mes);
        }

        // send to All except caller:
        public void messageAllExcept(string mes, string excludeConnectionId)
        {
            var _mes = string.Format("Hello client with id: {0}, this time is: {1:F}, mes: {2}", Context.ConnectionId, DateTime.Now, mes);
            var allExcept = Clients.AllExcept(excludeConnectionId);
            allExcept.message(_mes);
        }

        //============================================== part 2===================================================
        public string returnMessage(string message)
        {
            //return mes;
            if (new Random().Next(2) == 0)
                throw new ApplicationException("Doh!");
            return message;
        }

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
