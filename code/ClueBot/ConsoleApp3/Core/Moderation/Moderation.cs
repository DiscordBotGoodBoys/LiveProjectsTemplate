using ClueBot.Resources.Settings;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ClueBot.Resources.Datatypes;
using Newtonsoft.Json;

namespace ClueBot.Core.Moderation
{
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        [Command("reload"), Summary("Reloads the settings.json file whilst the bot is running")]
        public async Task Reload()
        {
            if(Context.User.Id == ESettings.Owner)
            {
                await Context.Channel.SendMessageAsync("You are not the bot owner.");
                return;
            }

            string SettingsLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Replace(@"bin\Debug\netcoreapp2.0", @"Data\Settings.json");
            if (!File.Exists(SettingsLocation))
            {
                await Context.Channel.SendMessageAsync("File not found. The expected location can be found in the log.");
                Console.WriteLine(SettingsLocation);
                return;
            }

            string JSON = "";
            using (var Stream = new FileStream(SettingsLocation, FileMode.Open, FileAccess.Read))
            using (var ReadSettings = new StreamReader(Stream))
            {
                JSON = ReadSettings.ReadToEnd();
            }

            Setting Setttings = JsonConvert.DeserializeObject<Setting>(JSON);

            Setting Settings = JsonConvert.DeserializeObject<Setting>(JSON);
            //ESettings.Log = Settings.log;
            ESettings.Owner = Settings.owner;
            ESettings.Token = Settings.token;
            ESettings.Version = Settings.version;

            await Context.Channel.SendMessageAsync("Settings successfully updated!");

        }
    }
}
