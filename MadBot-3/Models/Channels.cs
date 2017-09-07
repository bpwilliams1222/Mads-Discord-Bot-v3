using Discord;
using Discord.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadBot_3.Models
{
    public class Channels
    {
        public Server LoCServer;
        public Channel chanHallOfChaos;
        public Channel chanEENews;
        public Channel chanLeaderboard;
        public Channel chanForeignAffairs;
        public Channel chanSpyops;
        public Channel chanKillRunRoom1;
        public Channel chanKillRunRoom2;
        public Channel chanKillRunSpy;
        public Channel chanBotUpdates;
        public Channel chanOnlineCountries;
        public Channel chanBotErrors;
        List<UserJoin> LastJoins = new List<UserJoin>();
        private BackgroundWorker OnlineUserDetectionService;
        
        public Channels(DiscordClient client)
        {
            ulong serverId = 317005397598666753;
            ulong EENewsChanId = 317009651386417153;
            ulong HallOfChaosChanId = 317005397598666753;
            ulong LeaderBoardChanId = 321625978923253760;
            ulong ForeignAffairsChanId = 322185152132546561;
            ulong spyOpsChanId = 322189909811134474;
            ulong KR1ChanId = 322190947951837184;
            ulong KR2ChanId = 322191885449822218;
            ulong KRSpyChanId = 322193057195753473;
            ulong botUpdatesChanId = 324546319047852034;
            ulong onlineCountryFeedChanId = 332955434296279052;
            ulong botErrorsChanId = 335417156084957185;

            LoCServer = client.GetServer(serverId);
            chanEENews = findTextChannel(LoCServer, EENewsChanId);
            chanHallOfChaos = findTextChannel(LoCServer, HallOfChaosChanId);
            chanLeaderboard = findTextChannel(LoCServer, LeaderBoardChanId);
            chanForeignAffairs = findTextChannel(LoCServer, ForeignAffairsChanId);
            chanSpyops = findTextChannel(LoCServer, spyOpsChanId);
            chanKillRunRoom1 = findTextChannel(LoCServer, KR1ChanId);
            chanKillRunRoom2 = findTextChannel(LoCServer, KR2ChanId);
            chanKillRunSpy = findTextChannel(LoCServer, KRSpyChanId);
            chanBotUpdates = findTextChannel(LoCServer, botUpdatesChanId);
            chanOnlineCountries = findTextChannel(LoCServer, onlineCountryFeedChanId);
            chanBotErrors = findTextChannel(LoCServer, botErrorsChanId);

            using (OnlineUserDetectionService = new BackgroundWorker())
            {
                OnlineUserDetectionService.DoWork += CheckForOnlineUsers;
                TimeSpan x = new TimeSpan(0, 5, 0);
                System.Timers.Timer timer = new System.Timers.Timer(x.TotalMilliseconds);
                timer.Elapsed += CheckOnlineUsersService;
                timer.Start();
            }
        }

        /*
         * Find Channel Method
         * 
         * Purpose:
         * Returns a channel object based on discordChannelId
         * 
         */
        public Channel findTextChannel(Server server, ulong id)
        {
            try
            {
                if (server != null)
                {
                    foreach (Channel channel in server.TextChannels)
                    {
                        if (channel.Id == id)
                            return channel;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /*
         * Send Tag Change Object To Channel Method
         * 
         * Purpose:
         * Parses the TagChange data received and sends a message to the channel provided
         * 
         */
        public async void SendTagChangeToChannel(List<TagChange> TagChanges, Channel chan)
        {
            string message = "```", tempMessage = "";
            foreach (var tagChange in TagChanges)
            {
                string fromTag, toTag;
                if (tagChange.FromTag == "" || tagChange.FromTag == null)
                    fromTag = "Untagged";
                else
                    fromTag = tagChange.FromTag;
                if (tagChange.ToTag == "" || tagChange.ToTag == null)
                    toTag = "Untagged";
                else
                    toTag = tagChange.ToTag;
                tempMessage += "Country (#" + tagChange.Number + ") - Was detected changing from [" + fromTag + "] to [" + toTag + "].";
                if((message.Length + tempMessage.Length + Environment.NewLine.Length) < 2000)
                {
                    message += tempMessage + Environment.NewLine;
                }
                else
                {
                    await chan.SendMessage(message + "```");
                    message = "```";
                    message += tempMessage + Environment.NewLine;
                }
            }
            await chan.SendMessage(message + "```");
        }

        /*
         * Send Login Object To Channel Method
         * 
         * Purpose:
         * Parses the Login data received and sends a message to the channel provided
         * 
         */
        public async void SendLoginDataToChannel(countryLogin logins, Channel chan)
        {
            // display stats for country
            if (logins != null)
            {
                int[] quarters = new int[4];
                quarters[0] = 0;
                quarters[1] = 0;
                quarters[2] = 0;
                quarters[3] = 0;
                int[] detectedBy = new int[3];
                detectedBy[0] = 0;
                detectedBy[1] = 0;
                detectedBy[2] = 0;
                foreach (var login in logins.DetectedBy)
                {
                    if (login.timeDetected.Hour > 0 && login.timeDetected.Hour <= 6)
                        quarters[0]++;
                    else if (login.timeDetected.Hour > 6 && login.timeDetected.Hour <= 12)
                        quarters[1]++;
                    else if (login.timeDetected.Hour > 12 && login.timeDetected.Hour <= 18)
                        quarters[2]++;
                    else if (login.timeDetected.Hour > 18 && login.timeDetected.Hour <= 24)
                        quarters[3]++;
                    if (login.DetectedBy == DetectionMethod.Land)
                        detectedBy[0]++;
                    else if (login.DetectedBy == DetectionMethod.Networth)
                        detectedBy[1]++;
                    else if (login.DetectedBy == DetectionMethod.News)
                        detectedBy[2]++;
                }
                var message = "``` Last Login was detected on: " + logins.DetectedBy.OrderByDescending(c => c.timeDetected).FirstOrDefault().timeDetected + ", and was detected by: " + logins.DetectedBy.OrderByDescending(c => c.timeDetected).FirstOrDefault().DetectedBy.ToString() + "."
                + Environment.NewLine
                + "# of Logins Detected between 00:00 and 06:00- " + quarters[0] + "."
                + Environment.NewLine
                + "# of Logins Detected between 06:00 and 12:00- " + quarters[1] + "."
                + Environment.NewLine
                + "# of Logins Detected between 12:00 and 18:00- " + quarters[2] + "."
                + Environment.NewLine
                + "# of Logins Detected between 18:00 and 24:00- " + quarters[3] + ".";
                if (detectedBy[0] >= detectedBy[1] && detectedBy[0] > detectedBy[2])
                    message += Environment.NewLine + "This country was detected online most often by: Land Detection```";
                else if (detectedBy[1] >= detectedBy[2])
                    message += Environment.NewLine + "This country was detected online most often by: Networth Detection```";
                else
                    message += Environment.NewLine + "This country was detected online most often by: News Detection```";
                await chan.SendMessage(message);
            }
            else
                await chan.SendMessage("I have not detected a login for that country number.");
        }

        /*
         * Send Login Objects To Channel Method
         * 
         * Purpose:
         * Parses the Login data received and sends a message to the channel provided
         * 
         */
        public async void SendLoginDataToChannel(List<countryLogin> logins, Channel chan)
        {
            // display stats for country
            if (logins != null)
            {
                int[] quarters = new int[4];
                quarters[0] = 0;
                quarters[1] = 0;
                quarters[2] = 0;
                quarters[3] = 0;
                int[] detectedBy = new int[3];
                detectedBy[0] = 0;
                detectedBy[1] = 0;
                detectedBy[2] = 0;
                foreach (var login in logins)
                {
                    foreach (var detection in login.DetectedBy)
                    {
                        if (detection.timeDetected.Hour > 0 && detection.timeDetected.Hour <= 6)
                            quarters[0]++;
                        else if (detection.timeDetected.Hour > 6 && detection.timeDetected.Hour <= 12)
                            quarters[1]++;
                        else if (detection.timeDetected.Hour > 12 && detection.timeDetected.Hour <= 18)
                            quarters[2]++;
                        else if (detection.timeDetected.Hour > 18 && detection.timeDetected.Hour <= 24)
                            quarters[3]++;
                        if (detection.DetectedBy == DetectionMethod.Land)
                            detectedBy[0]++;
                        else if (detection.DetectedBy == DetectionMethod.Networth)
                            detectedBy[1]++;
                        else if (detection.DetectedBy == DetectionMethod.News)
                            detectedBy[2]++;
                    }
                }
                var message = "``` Last Login was detected on: " + logins.SelectMany(c => c.DetectedBy).OrderByDescending(c => c.timeDetected).FirstOrDefault().timeDetected + ", and was detected by: " + logins.SelectMany(c => c.DetectedBy).OrderByDescending(c => c.timeDetected).FirstOrDefault().DetectedBy.ToString() + "."
                + Environment.NewLine
                + "# of Logins Detected between 00:00 and 06:00- " + quarters[0] + "."
                + Environment.NewLine
                + "# of Logins Detected between 06:00 and 12:00- " + quarters[1] + "."
                + Environment.NewLine
                + "# of Logins Detected between 12:00 and 18:00- " + quarters[2] + "."
                + Environment.NewLine
                + "# of Logins Detected between 18:00 and 24:00- " + quarters[3] + ".";
                if (detectedBy[0] >= detectedBy[1] && detectedBy[0] > detectedBy[2])
                    message += Environment.NewLine + "This country was detected online most often by: Land Detection```";
                else if (detectedBy[1] >= detectedBy[2])
                    message += Environment.NewLine + "This country was detected online most often by: Networth Detection```";
                else
                    message += Environment.NewLine + "This country was detected online most often by: News Detection```";
                await chan.SendMessage(message);
            }
            else
                await chan.SendMessage("I have not detected a login for that country number.");
        }

        /*
         * SendSpyOpToChannel
         * 
         * Purpose:
         * This method handles incomming SpyOps as well as !cty.op bot command to send Countries Spy Op Data to the Channel requested
         * 
         */
        public async void SendSpyOpToChannel(SpyOpInfo op, Channel chan)
        {
            try
            {
                if (chan == null)
                    await frmDiscordBot.Bot.channels.chanSpyops.SendMessage(op.GetSpyOpMessage());
                else
                    await chan.SendMessage(op.GetSpyOpMessage());

                // TO DO:
                // Later we can add relation detection, if relation is detected display a warnign message to user.
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        /*
         * Build Kill List Message Method
         * 
         * Purpose:
         * Returns a string representing a message based on the current kill list
         * 
         */
        public string BuildKillListGreetingMessage()
        {
            string message = "```";

            if(frmDiscordBot.Storage.CountryStorage.Get().Where(c => c.KillList == 1).Count() == 0)
            {
                message += "There currently are no countries on our kill list.";
            }
            else
            {
                foreach(var country in frmDiscordBot.Storage.CountryStorage.Get().Where(c => c.KillList == 1))
                {
                    string tagTemp = "";
                    if (country.Tag == "")
                        tagTemp = "Untagged";
                    else
                        tagTemp = country.Tag;
                    message += "Country: " + country.Name + " (#" + country.Number + ")[" + tagTemp +
                        "] Net:" + country.ConvertNumberToDisplay(country.Networth) +
                        " Land:" + country.ConvertNumberToDisplay(country.Land) + Environment.NewLine;
                }
            }

            return message + "```";
        }

        /*
         * Build Relation Message Method
         * 
         * Purpose:
         * Returns a string representing a message based on the current relations
         * 
         */
        public string BuildRelationGreetingMessage()
        {
            string message = "```";
            if (frmDiscordBot.Storage.RelationsStorage.Get().Count() == 0)
            {
                message += "Your clan leader has not specified any relations yet.";
            }
            else
            {
                foreach (var relation in frmDiscordBot.Storage.RelationsStorage.Get())
                {
                    message += "ClangTag: [" + relation.clanTag + "] -> PactType: " + relation.RelationType + Environment.NewLine;
                }
            }
            return message + "```";
        }

        /*
         * Check Online Users Service
         * 
         * Purpose:
         * Checks to see if a process is in operation, if not runs the CheckForOnlineUsers BackgroundWorker
         * 
         */
        public void CheckOnlineUsersService(object sender, EventArgs e)
        {
            if (!OnlineUserDetectionService.IsBusy)
                OnlineUserDetectionService.RunWorkerAsync();
        }

        /*
         * Check Online Users
         * 
         * Purpose:
         * Checks to see if users are online, if they are sends an automated message(Max 1 message/day) to keep them up to date with clan affairs.
         * If Users are not online for 3 days, bot sends them a message anyway
         * 
         */
        public async void CheckForOnlineUsers(object sender, DoWorkEventArgs e)
        {
            try
            {
                foreach (var user in LoCServer.Users)
                {
                    if (!user.Name.Contains("MadBot"))
                    {
                        if (user.Status.Value == UserStatus.Online || user.Status.Value == UserStatus.Idle)
                        {
                            if (LastJoins.Any(c => c.userName == user.Name))
                            {
                                if (LastJoins.Where(c => c.userName == user.Name).OrderByDescending(c => c.timestamp).FirstOrDefault().timestamp < DateTime.UtcNow.AddDays(-1))
                                {
                                    await user.SendMessage(BuildRelationGreetingMessage());
                                    await user.SendMessage(BuildKillListGreetingMessage());
                                }
                                LastJoins.RemoveAll(c => c.userName == user.Name);
                                LastJoins.Add(new UserJoin
                                {
                                    userName = user.Name,
                                    timestamp = DateTime.UtcNow
                                });
                            }
                            else
                            {
                                LastJoins.Add(new UserJoin
                                {
                                    userName = user.Name,
                                    timestamp = DateTime.UtcNow
                                });
                                await user.SendMessage(BuildRelationGreetingMessage());
                                await user.SendMessage(BuildKillListGreetingMessage());
                            }
                        }
                        else
                        {
                            if (LastJoins.Any(c => c.userName == user.Name))
                            {
                                if (LastJoins.FirstOrDefault(C => C.userName == user.Name).timestamp < DateTime.UtcNow.AddDays(-3))
                                {
                                    await user.SendMessage(BuildRelationGreetingMessage());
                                    await user.SendMessage(BuildKillListGreetingMessage());
                                }
                                LastJoins.RemoveAll(c => c.userName == user.Name);
                                LastJoins.Add(new UserJoin
                                {
                                    userName = user.Name,
                                    timestamp = DateTime.UtcNow
                                });
                            }
                            else
                            {
                                LastJoins.Add(new UserJoin
                                {
                                    userName = user.Name,
                                    timestamp = DateTime.UtcNow
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        
        }

        /*
         * Message all Users Method
         * 
         * Purpose:
         * Takes a message received and sends it to all users
         * 
         */
        public async void PrivateMessageAllUsers(string message)
        {
            foreach(var user in LoCServer.Users)
            {
                if(!user.Name.Contains("MadBot"))
                    await user.SendMessage(message);
            }
        }
    }
}
