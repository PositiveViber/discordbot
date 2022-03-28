using Discord.Commands;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedtimeBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        int absoluteTime;
        string brandenMention = "@BigIronDawg#9741";
        DateTime now = DateTime.Now;
        DateTime bedtimes = DateTime.Parse("6/22/2009 10:00:00 PM");

        [Command("bedtime")]
        public async Task Bedtime()
        {
            
            string time = bedtimes.ToString("hh:mm:ms tt");
            await ReplyAsync("Branden's Bedtime is " + time + " o'clock! :brandenburnfart:");
        }
        [Command("whenbedtime")]
        public async Task WhenBedTime()
        {
            string timeBrand = now.ToString("hh:mm:ms tt");
            string[] split = timeBrand.Split(":");
            int parsedTime = int.Parse(split[0]);
  
            if(bedtimes.Hour - now.Hour < 0)
            {
                absoluteTime = 24 - (now.Hour - bedtimes.Hour);
            }
            else
                absoluteTime = Math.Abs(bedtimes.Hour - now.Hour);

            if(absoluteTime != 0)
            {
                await ReplyAsync("Branden's Bedtime is in " + (absoluteTime) + " hours!");
            }
            else if(absoluteTime == 1)
            {
                await ReplyAsync("Branden's Bedtime is in " + (absoluteTime) + " hour!");
            }
            else
                await ReplyAsync("Branden's Bedtime is in " + (absoluteTime) + " hours! It is Branden's Bedtime. Go to bed Branden :zzz: XD");

        }

        [Command("website")]
        public async Task Website()
        {
            await ReplyAsync("https://positiveviber.github.io/site");
        }

        [Command("helpme")]

        public async Task Help()
        {
            await ReplyAsync("Commands :" + System.Environment.NewLine +
                             "bedtime: calls bedtime" + System.Environment.NewLine +
                             "whenbedtime: how long until bedtime XD" + System.Environment.NewLine +
                             "website: my website, check it out!");
        }
    }
}
