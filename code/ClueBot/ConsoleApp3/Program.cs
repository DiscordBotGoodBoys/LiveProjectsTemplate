using System;
using System.IO;
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
            string token = "";      //DO NOT PUT TOKEN HERE. Read it from a separate text file, and never upload that file.
            using (var Stream = new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Replace(""));    
            
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
