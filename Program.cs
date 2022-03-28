using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BedtimeBot
{
    class DiscordBot
    {
        static async Task Main(string[] args)
        {
            await new DiscordBot().RunBot();
        }

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        
        public async Task RunBot()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = (IServiceProvider)new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            String token = "OTU3NzIyMzQyODU4MDU5Nzc4.YkC6cA.bVP96Pr7C5c2rVfJNf0epOrlBqk";

            _client.Log += _client_Log;
            await RegisterCommands();
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommands()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context  =  new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("!", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if(!result.IsSuccess) Console.WriteLine(result.Error);
            }
        }
    }

}