using Qurre.API.Addons;
using System.Collections.Generic;
using Qurre.API.Attributes;

namespace BroadcastSystem
{
    public static class Configs
    {
        internal static JsonConfig Config { get; private set; }
        public static int startWaiting { get; private set; }
        public static int nextBroadcast { get; private set; }
        public static int displayBroadcast { get; private set; }
        public static string joinPlayerText { get; private set; }
        public static List<string> broadcastMessages { get; private set; }
        [EventMethod(4001u, 0)]
        internal static void LoadReloadConfig()
        {
            if (Config == null)
            {
                Config = new JsonConfig("BroadcastSystem");
            }

            startWaiting = Config.SafeGetValue("BroadcastStartWaiting", 30, "How many seconds after the start of the round will the messages start working?");
            nextBroadcast = Config.SafeGetValue("BroadcastInterval", 120, "Interval between messages (in seconds)");
            displayBroadcast = Config.SafeGetValue("BroadcastDisplay", 15, "How long will these messages be shown to the player?");
            joinPlayerText = Config.SafeGetValue("JoinPlayerText", "<color=yellow>Hello <color=red>%player%</color> are you playing on the server</color>\n<b><color=yellow>My Server Name</color></b>", "Text for the player when he logged in to the server.");
            broadcastMessages = Config.SafeGetValue("BroadcastMessages", new List<string>()
            {
                "<color=yellow>Hello</color>",
                "<color=yellow>How are you</color>",
                "<color=yellow>Join in my Discord</color>"
            }, "Messages that will be sent at n intervals");

            JsonConfig.UpdateFile();
        }
    }
}
