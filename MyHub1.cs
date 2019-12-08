using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Italianos
{
    public class ChatHub : Hub
    {
        string s = "hi" + " bye";
    }
}