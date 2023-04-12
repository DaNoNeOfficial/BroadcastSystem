using BroadcastSystem;
using Qurre.API;
using Qurre.API.Attributes;
using Qurre.Events;
using Qurre.Events.Structs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[PluginInit("BroadcastSystem", "DaNoNe", "1.0.0")]
static class Plugin 
{
    private static List<string> bcMessages;
    public static Random rnd = new Random();

    [PluginEnable]
    static internal void Enabled()
    {
        Configs.LoadReloadConfig();
        Log.Info("Plugin Enabled");
    }
    [PluginDisable]
    static internal void Disabled()
    {
        Log.Info("Plugin Disabled");
    }

    [EventMethod(PlayerEvents.Join)]
    static internal void Join(JoinEvent ev)
    {
        Player player = ev.Player;
        player.Client.Broadcast($"{Configs.joinPlayerText.Replace("%player%", player.UserInfomation.Nickname)}", (ushort) Configs.displayBroadcast);
    }
    [EventMethod(RoundEvents.Start)]
    static internal async void RoundStart(RoundStartedEvent ev)
    {
        bcMessages = new List<string>(Configs.broadcastMessages);
        await Task.Delay(Configs.startWaiting * 1000);
        BroadcastTimingAsync();
    }
    static internal async void BroadcastTimingAsync()
    {
        while (!Round.Ended)
        {
            int randomValue = rnd.Next(0, bcMessages.Count - 1);
            Map.Broadcast($"{bcMessages[randomValue]}", (ushort)Configs.displayBroadcast);
            bcMessages.Remove(bcMessages[randomValue]);
            if (bcMessages.Count == 0)
            {
                bcMessages = new List<string>(Configs.broadcastMessages);
            }

            await Task.Delay(Configs.nextBroadcast * 1000);
        }
    }
}