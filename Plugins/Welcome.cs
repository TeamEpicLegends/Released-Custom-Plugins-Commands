/*

Copyright (c) 2021 Epic Legends

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

 █▀▀ █▀█ █▀▀ ▄▀█ ▀█▀ █▀█ █▀█ ▀
 █▄▄ █▀▄ ██▄ █▀█ ░█░ █▄█ █▀▄ ▄


 ███╗░░██╗██╗███╗░░██╗░░░░░██╗░█████╗║
 ████╗░██║██║████╗░██║░░░░░██║██╔══██║
 ██╔██╗██║██║██╔██╗██║░░░░░██║███████║
 ██║╚████║██║██║╚████║██╗░░██║██╔══██║
 ██║░╚███║██║██║░╚███║╚█████╔╝██║░░██║
 
*/

using System;
using System.Threading;

using MCGalaxy;
using MCGalaxy.Tasks;
using MCGalaxy.Events;
using MCGalaxy.Events.PlayerEvents;

namespace McGalaxy {
    public class Welcome : Plugin {  
        public override string creator { get { return "Nin"; } }
        public override string MCGalaxy_Version { get { return "1.9.3.0"; } }
        public override string name { get { return "Welcome"; } }

        public override void Load(bool startup) {
            // Register events using handlers. OnPlayerConnectEvent -> HandlePlayerConnect for example
            // Every event needs a handler, you can name it whatever you like but ideally Handle[event name here] is best.
            OnPlayerConnectEvent.Register(HandlePlayerConnect, Priority.High);
        }
        
        public override void Unload(bool shutdown) {
            // Unregister event(s)
        }
    
        // Player connect event handler
        void HandlePlayerConnect(Player p) {
            // Better to not use a command for this
            p.SendCpeMessage(CpeMessageType.BigAnnouncement, "%b♦ %iWelcome to $server %b♦");
            p.SendCpeMessage(CpeMessageType.SmallAnnouncement, "%bType in chat %a/faq %band %a/rules %bto get started");
        }

         public override void Help(Player p) {
            p.Message("%TAnnounces Welcome Message When Player Joins.");
        }
    }
}
