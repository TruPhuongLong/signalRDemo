using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication2
{
    //[HubName("HubPersistent")]
    public class HubPersistent : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Send(connectionId, "Welcome!");
            
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            //return Connection.Send(connectionId, data);

            var payload = JsonConvert.DeserializeAnonymousType(data, new { content = "", from = "" });
            var body = string.Format("{0} said: {1}", payload.from, payload.content);
            return Connection.Broadcast(body);
        }

        protected override bool AuthorizeRequest(IRequest request)
        {
            return request.User.Identity.Name == @"DOMAIN/user";
        }

        public void sayHello()
        {
            
        }
    }

    public class Payload
    {
        [JsonProperty("s")]
        public string Salutation { get; set; }
        [JsonIgnore]
        public string DummyStuff { get; set; }
    }

}