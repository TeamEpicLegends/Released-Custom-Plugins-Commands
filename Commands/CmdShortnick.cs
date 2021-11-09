/*

 Copyright (c) 2021 Epic Legends


 █▀▀ █▀█ █▀▀ ▄▀█ ▀█▀ █▀█ █▀█ ▀
 █▄▄ █▀▄ ██▄ █▀█ ░█░ █▄█ █▀▄ ▄


 ███╗░░██╗██╗███╗░░██╗░░░░░██╗░█████╗║
 ████╗░██║██║████╗░██║░░░░░██║██╔══██║
 ██╔██╗██║██║██╔██╗██║░░░░░██║███████║
 ██║╚████║██║██║╚████║██╗░░██║██╔══██║
 ██║░╚███║██║██║░╚███║╚█████╔╝██║░░██║
 
 */

using System;
using MCGalaxy;
using MCGalaxy.DB;

namespace MCGalaxy
{
    public sealed class CmdShortName : Command2
    {
        public override string name { get { return "ShortName"; } }
        public override string shortcut { get { return "Name"; } }
        public override string type { get { return "other"; } }

        public override void Use(Player p, string message)
        {
            string[] args = message.SplitSpaces();

            if (message.Length == 0)
            {
                p.Message("You need to provide a nickname.");
                return;
            }

            string colorlessNick = Colors.Strip(args[0]);
            bool isQualified = p.truename.CaselessContains(colorlessNick);

            if (!isQualified)
            {
                p.Message("Nick must contain part of your username.");
                return;
            }
            else
            {
                string color = p != null ? p.color : Group.GroupIn(p.name).Color;
                Chat.MessageGlobal(p.ColoredName + " &Shad their nick set to " + color + args[0]);
                p.DisplayName = args[0];
                //TabList.Update(p, true);
                PlayerDB.SetNick(p.name, args[0]);
            }
        }

        public override void Help(Player p) {
            p.Message("&T/Name [Args]");
            p.Message("&T/Shortname [Args]");
            p.Message("&HIt's /nick but you can only use part[s] of your username.");
        }
    }
}
