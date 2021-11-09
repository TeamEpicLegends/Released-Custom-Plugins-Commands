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

namespace Core {
    public class SilentModel : Plugin {  
        public override string creator { get { return "Ninja_King"; } }
        public override string MCGalaxy_Version { get { return "1.9.3.0"; } }
        public override string name { get { return "SilentModel"; } }

        public override void Load(bool startup) {
            Command.Register(new CmdSilentModel());
        }
        
        public override void Unload(bool shutdown) {
        	Command.Unregister(Command.Find("SilentModel"));
        }
    }
    
        public sealed class CmdSilentModel : EntityPropertyCmd
    {
        public override string name { get { return "SilentModel"; } }
        public override string type { get { return CommandTypes.Other; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public override CommandPerm[] ExtraPerms
        {
            get { return new[] { new CommandPerm(LevelPermission.Operator, "can change the model of others") }; }
        }

        public override void Use(Player p, string message, CommandData data)
        {
            if (message.IndexOf(' ') == -1)
            {
                message = "-own " + message;
                message = message.TrimEnd();
            }
            UseBotOrOnline(p, data, message, "model");
        }

        protected override void SetBotData(Player p, PlayerBot bot, string model)
        {
            model = ParseModel(p, bot, model);
            if (model == null) return;
            bot.UpdateModel(model);

            BotsFile.Save(p.level);
        }

        protected override void SetOnlineData(Player p, Player who, string model)
        {
            string orig = model;
            model = ParseModel(p, who, model);
            if (model == null) return;
            who.UpdateModel(model);

            if (!model.CaselessEq("humanoid"))
            {
                Server.models.Update(who.name, model);
            }
            else
            {
                Server.models.Remove(who.name);
            }
            Server.models.Save();

            // Remove model scale too when resetting model
            //if (orig.Length == 0) CmdModelScale.UpdateSavedScale(who);
        }

        static string ParseModel(Player dst, Entity e, string model)
        {
            // Reset entity's model
            if (model.Length == 0)
            {
                e.ScaleX = 0; e.ScaleY = 0; e.ScaleZ = 0;
                return "humanoid";
            }

            model = model.ToLower();
            model = model.Replace(':', '|'); // since users assume : is for scale instead of |.

            float max = ModelInfo.MaxScale(e, model);
            // restrict player model scale, but bots can have unlimited model scale
            if (ModelInfo.GetRawScale(model) > max)
            {
                dst.Message("%WScale must be {0} or less for {1} model",
                            max, ModelInfo.GetRawModel(model));
                return null;
            }
            return model;
        }

        public override void Help(Player p) { }
        }
    }
}
