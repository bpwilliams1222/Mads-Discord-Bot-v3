using Discord;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MadBot_3.Models
{
    class HelpModel : IDisposable
    {
        List<string> helpCommands;

        public HelpModel()
        {
            helpCommands = new List<string>();
            helpCommands.Add("```Command:!own Paramaters:[CountryNbr] Example:!own 1 or !own 1, 2, 3```");
            helpCommands.Add("```Command:!disown Paramaters:[CountryNbr] Example:!disown 1 or !disown 1, 2, 3```");
            helpCommands.Add("```Command:!whois Paramaters:[CountryNbr] Example:!whois 33```");
            helpCommands.Add("```Command:!lookupuser Paramaters:[username] Example:!lookup MadNudist```");
            helpCommands.Add("```Command:!insult Paramaters:[username] Example:!insult mob```");
            helpCommands.Add("```Command:!whois Paramaters:[CountryNbr] Example:!whois 33```");
            helpCommands.Add("```Command:!stats.net Paramaters:n/a Example:!stats.net```");
            helpCommands.Add("```Command:!stats.war Paramaters:[username]&[timeFrame(h|d)] Example:!stats.war MadNudist 1d or !stats.war mob 1h```");
            helpCommands.Add("```Command:!stats.tagchanges Paramaters:[CountryNumber||tag] Example:!stats.tagchanges 33 or !stats.tagchanges LoC```");
            helpCommands.Add("```Command:!stats.market Paramaters:[timeFrame(h|d)] Example:!stats.market or !stats.market 2d or !stats.market 3h```");
            helpCommands.Add("```Command:!stats.ops Paramaters:[CountryNum]&[numOpsToCompare] Example:!stats.ops 33```");
            helpCommands.Add("```Command:!atwar Paramaters:Optional[Tag] Example:!atwar CC or !atwar(turn war mode off)```");
            helpCommands.Add("```Command:!online Paramaters:[CountryNum]&[Tag] Example:!online 33 or !online LoC```");
            helpCommands.Add("```Command:!cty.info Paramaters:[CountryNum] Example:!cty.info 33```");
            helpCommands.Add("```Command:!cty.op Paramaters:[CountryNum] Example:!cty.op 33```");
            helpCommands.Add("```Command:!add.user Paramaters:[DiscordUserId]&/OR[APIKey] Example: !add.user 1234 APIKEY1234 OR !add.user 1234```");
            helpCommands.Add("```Command:!remove.user Paramaters:[DiscordUserId] Example:!remove.user 33```");
            helpCommands.Add("```Command:!update.username Paramaters:[DiscordUserId]&[username] Example:!update.username 1234 mob```");
        }

        public async void  SendHelpMessage(Discord.User user)
        {
            string message = "";
            foreach(var command in helpCommands)
            {
                if ((message.Length + command.Length + Environment.NewLine.Length) < 2000)
                    message += command + Environment.NewLine;
                else
                {
                    await user.SendMessage(message);
                    message = command + Environment.NewLine;
                }
            }
        }

        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                helpCommands.Clear();
            }            
            disposed = true;
        }
    }
}
