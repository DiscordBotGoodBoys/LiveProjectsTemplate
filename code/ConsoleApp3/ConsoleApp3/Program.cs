using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;    


namespace ConsoleApp3
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        

        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug,
            });
##
            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            this.Client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());

            Client.Ready += Client_Ready;
            Client.Log += Client_Log;
            string token = "NTE2MjY5MjI2MTI2NzM3NDE5.Dt7FeQ.HstD-Czwzf4Dt0-kVKycb4pBLCE";
            //using (var Stream = new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Replace("")));    
            //^^Use only if token is in a text file.
            await Client.LoginAsync(TokenType.Bot, token);
        }

        private Task Client_Log(LogMessage arg)
        {
            throw new NotImplementedException();
        }

        private Task Client_Ready()
        {
            throw new NotImplementedException();
        }

        private Task Client_MessageReceived(SocketMessage arg)
        {
            throw new NotImplementedException();
        }
    }
}
