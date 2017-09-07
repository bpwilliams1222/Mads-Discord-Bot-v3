using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadBot_3.Models
{
    
    public class MadBot
    {
        private DiscordClient client;
        private CommandService commands;
        private bool atWar = false;
        private string atWarTag = "";
        public Channels channels;

        public void Main()
        {
            Stopwatch TimeConnected = new Stopwatch();

            //create client
            client = new DiscordClient(input =>
            {
                //input.LogHandler = Log;
                input.LogLevel = LogSeverity.Error;
            });

            // Define Using Commands Options
            client.UsingCommands(input =>
            {
                input.PrefixChar = '!';
                input.AllowMentionPrefix = true;
            });
            
            // Get the command service to add commands to
            commands = client.GetService<CommandService>();

            #region First Command
            /*  The first command written in this bot, it is no longer needed but I keep it around for reminiscing
             * 
             * commands.CreateCommand("Hello").Do(async (e) =>
            {
                await e.Channel.SendMessage("Hello World, im the Stats Bot, brought to you by MadNudist.");
            });*/
            #endregion

            #region Help
            /*
             * Command: help || commands
             * 
             * Purpose:
             * Sends Help context on bot commands to user requesting.
             * 
             */
            commands.CreateCommand("help").Do((e) =>
            {
                using (var help = new HelpModel())
                {
                    help.SendHelpMessage(e.User);
                }
            });
            commands.CreateCommand("commands").Do((e) =>
            {
                using (var help = new HelpModel())
                {
                    help.SendHelpMessage(e.User);
                }
            });
            #endregion

            /*
             * Command: assign
             * 
             * Purpose:
             * Allows a user to assign countries to another users ownership
             * 
             */
            commands.CreateCommand("assign").Parameter("userData", ParameterType.Multiple).Do(async (e) =>
            {                
                var messageParts = e.Message.RawText.Split(' ');
                if (messageParts.Count() >= 3)
                {
                    ulong userId;
                    if(ulong.TryParse(messageParts[1], out userId))
                    {
                        try
                        {
                            var countryNumbers = frmDiscordBot.Storage.CountryStorage.GetNumbersFromInput(messageParts);
                            if (countryNumbers != null)
                            {
                                frmDiscordBot.Storage.CountryStorage.AssignCountries(countryNumbers, userId);
                                await e.Channel.SendMessage("Country or Countries were added successfully to " + frmDiscordBot.Storage.UsersStorage.Get(userId).Username + "'s claimed countries.");
                            }
                            else
                                await e.User.SendMessage("A problem was experienced when attempting to assign those countries.");
                        }
                        catch
                        {
                            await e.Channel.SendMessage("An error was encountered when attempted to assign the country or countries provided to the DiscordUserId provided.");
                        }
                    }
                    else
                        await e.Channel.SendMessage("The userId provided was not in the expected format, please check all parameters.");
                }
                else
                    await e.Channel.SendMessage("Not all required parameters where included with the command.");
            });

            /*
            * Command: Own
            * 
            * Purpose:
            * Claim Countries for further functionality for users
            * 
            */
            commands.CreateCommand("own").Parameter("Countries", ParameterType.Multiple).Do(async (e) =>
            {
                try
                {
                    var countryNumbers = frmDiscordBot.Storage.CountryStorage.GetNumbersFromInput(e.Message.RawText.Split(' '));
                    if (countryNumbers != null)
                    {
                        // Process Claiming Countries
                        MadBot_3.frmDiscordBot.Storage.CountryStorage.ClaimCountry(countryNumbers, e.User.Name);
                        await e.User.SendMessage("Countries added succesfully.");
                    }
                    else
                        await e.User.SendMessage("A problem was experienced when attempting to claim those countries for you.");
                }
                catch
                {
                    await e.User.SendMessage("I am sorry to report but there was a problem claiming your countries, speak with the bot admin.");
                }

            });

            /*
             * Command: Disown
             * 
             * Purpose:
             * Disown Claimed Countries
             * 
             */
            commands.CreateCommand("disown").Parameter("Countries", ParameterType.Multiple).Do(async (e) =>
            {
                try
                {
                    var result = frmDiscordBot.Storage.CountryStorage.UnClaimCountry(frmDiscordBot.Storage.CountryStorage.GetNumbersFromInput(e.Message.RawText.Split(' ')));
                    if (result)
                        await e.User.SendMessage("Countries removed succesfully.");
                    else
                        await e.User.SendMessage("There was a problem encountered when removing those country from your claimed list of countries, if you contonue to have trouble speak with a bot admin.");
                }
                catch
                {
                    await e.User.SendMessage("I am sorry to report but there was a problem disowning your countries, speak with the bot admin.");
                }

            });

            /*
             * Command: whois
             * 
             * Purpose:
             * Determine who own a country, if anyone
             * 
             */
            commands.CreateCommand("whois").Parameter("username", ParameterType.Required).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);
                if(parameters != null)
                {
                    int nbr = 0;
                    if (Int32.TryParse(parameters[0], out nbr))
                    {
                        var user = frmDiscordBot.Storage.CountryStorage.WhoIs(nbr);
                        if (user == "" || user == null)
                            user = "Unknown";
                        await e.Channel.SendMessage("Country #" + nbr.ToString() + " belongs to " + user);
                    }
                    else
                        await e.User.SendMessage("I am sorry but I didn't understand your request, if you continue to have trouble speak with a bot admin.");
                }
                else
                    await e.User.SendMessage("I am sorry but I didn't understand your request, if you continue to have trouble speak with a bot admin.");                
            });

            /*
             * Command: lookup
             * 
             * Purpose:
             * Displays users' alive and dead countries
             * 
             */
            commands.CreateCommand("lookup").Parameter("user", ParameterType.Required).Do(async (e) =>
            {
                string message;
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);
                if (parameters != null)
                {
                    var countries = frmDiscordBot.Storage.CountryStorage.LookupUser(parameters[0]);
                    if (countries[0] == null && countries[1] == null)
                        message = "User " + parameters[0] + ", does not have any claimed countries.";
                    else if (countries[0] == null && countries[1] != null)
                        message = "User " + parameters[0] + ", does not have any alive claimed countries, but has the following dead countries " + countries[1] + ".";
                    else if (countries[0] != null && countries[1] == null)
                        message = "User " + parameters[0] + ", has claimed the following alive countries, " + countries[0] + ", but does not have any dead countries currently.";
                    else
                        message = "User " + parameters[0] + " has claimed the following alive countries " + countries[0] + ", and the following dead countries " + countries[1] + ".";
                }
                else
                    message = "No parameters were provided, please execute the command and provide me a users' name.";

                await e.Channel.SendMessage(message);
            });

            /*
             * Command: insult
             * 
             * Purpose:
             * Used to publicly insult another user
             * 
             */
            commands.CreateCommand("insult").Parameter("personToInsult", ParameterType.Required).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);
                if(parameters != null)
                {
                    var insult = frmDiscordBot.Storage.InsultStorage.Insult();
                    if (parameters[0].ToLower() == "madnudist")
                    {
                        await e.Channel.SendMessage(e.User.Name + ", " + insult);
                    }
                    else
                        await e.Channel.SendMessage(parameters[1] + ", " + insult);
                }
                else
                    await e.User.SendMessage("The person's name to insult was not included, please include all parameters.");                
            });

            #region Stats
            /*
             * Command: stats.net
             * 
             * Purpose:
             * Used to provide netting type stats
             * 
             */
            commands.CreateCommand("stats.net").Do((e) =>
            {
                var stats = new NettingStats();
                stats.GetStatsInfo();
                stats.DisplayStats(e.Channel);                
            });

            /*
             * Command: stats.war
             * 
             * Purpose:
             * Used to provide war stats
             * 
             */
            commands.CreateCommand("stats.war").Parameter("data", ParameterType.Multiple).Do(async (e) =>
            {
                //                                24 Hr Stats                   
                //| Username | Spec |  GS  |  BR  |  AB  | Missiles |  CM  |  NM  |  EM  | Kills | Deaths | Defends |
                // Parameters: [timerperiod] [tagA] [tagB] == 4 with command
                // Paramaters: [username] [timeperiod] == 3 with command
                var stats = new WarStats();
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 3);
                if (parameters.Count() >= 2)
                {
                    if (parameters.Count() >= 3)
                    {
                        stats.tagA = parameters[1];
                        stats.tagB = parameters[2];
                        stats.username = null;
                    }
                    else
                    {
                        stats.username = parameters[0];
                        stats.tagA = null;
                        stats.tagB = null;
                    }
                    if (parameters[1].ToLower().Contains("h") || parameters[1].ToLower().Contains("d"))
                    {
                        if (parameters[1].ToLower().Contains("h"))
                        {
                            short timePeriod = 0;
                            if (Int16.TryParse(parameters[1].TrimEnd('h'), out timePeriod))
                            {
                                stats.Timeperiod_Date = DateTime.UtcNow.AddHours(-timePeriod);
                                stats.Timeperiod_Unix = StorageModel.ConvertToUnixTime(stats.Timeperiod_Date);
                                stats.GetStatInfo();
                                stats.DisplayStats(e.Channel);
                            }
                            else
                                await e.User.SendMessage("There was a problem parsing the time period provided, please abreviate time periods using h for hours and d for days. Example 24h or 1d.");
                        }
                        else
                        {
                            short timePeriod = 0;
                            if (Int16.TryParse(parameters[1].TrimEnd('d'), out timePeriod))
                            {
                                stats.Timeperiod_Date = DateTime.UtcNow.AddDays(-timePeriod);
                                stats.Timeperiod_Unix = StorageModel.ConvertToUnixTime(stats.Timeperiod_Date);
                                stats.GetStatInfo();
                                stats.DisplayStats(e.Channel);
                            }
                            else
                                await e.User.SendMessage("There was a problem parsing the time period provided, please abreviate time periods using h for hours and d for days. Example 24h or 1d.");
                        }
                    }
                    else
                        await e.User.SendMessage("Not all required parameters were provided, please provide me first with a user and second with a time peroid. You can abreviate hours with an h and days with a d, for example 24h or 1d.");
                }
                else
                    await e.User.SendMessage("Not all required parameters were provided, please provide me first with a user and second with a time peroid. You can abreviate hours with an h and days with a d, for example 24h or 1d.");
            });

            /*
             * Command: stats.tagchanges
             * 
             * Purpose:
             * Used to provide tag change stats on countries or tags
             * 
             */
            commands.CreateCommand("stats.tagchanges").Parameter("data", ParameterType.Required).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);

                var tagChangeData = frmDiscordBot.Storage.CountryStorage.GetTagChangeData();

                if (parameters.Count() > 0)
                {
                    // determine number or tag provided
                    int num = 0;
                    if (Int32.TryParse(parameters[0], out num))
                    {
                        // num
                        if (tagChangeData.Count() > 0)
                        {
                            var tagChanges = tagChangeData.Where(c => c.Number == num).OrderByDescending(C => C.timestamp).ToList();
                            if (tagChanges.Count() > 0)
                            {
                                channels.SendTagChangeToChannel(tagChanges, e.Channel);
                            }
                            else
                                await e.Channel.SendMessage("Either the country does not exists or there was no data found on that country, please verify number is valid and try again.");
                        }
                    }
                    else
                    {
                        // tag provided
                        if (tagChangeData.Count() > 0)
                        {
                            var tagChanges = tagChangeData.Where(c => c.FromTag == parameters[0] || c.ToTag == parameters[0]).OrderByDescending(C => C.timestamp).ToList();
                            if (tagChanges.Count() > 0)
                            {
                                channels.SendTagChangeToChannel(tagChanges, e.Channel);
                            }
                            else
                                await e.Channel.SendMessage("Either the tag does not exists or there was no data found on that tag, please verify tag is valid and try again.");
                        }
                    }
                }
                else
                    await e.Channel.SendMessage("Not all required paramteters were included, please try again with all parameters included.");

            });

            /*
             * Command: stats.market
             * 
             * Purpose:
             * Used to provide market stats
             * 
             */
            commands.CreateCommand("stats.market").Parameter("timePeriod", ParameterType.Optional).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);              
                if (parameters.Count() > 0)
                {
                    var marketStats = new MarketStats();
                    if (parameters[0].Contains('d'))
                    {
                        short increment = 0;
                        if (Int16.TryParse(parameters[0].TrimEnd('d'), out increment))
                        {
                            var marketData = frmDiscordBot.Storage.MarketStorage.LoadDataFromXML(DateTime.UtcNow.AddDays(-increment));

                            marketStats.GetStatInfo(marketData);

                            marketStats.totalTransactions = marketData.Count();

                            marketStats.DisplayStats(e.Channel);
                        }
                        else
                            await e.Channel.SendMessage("The parameter provided was not in the expected format, For example !stats.market 1d or !stats.market 1h");
                    }
                    else if (parameters[0].Contains('h'))
                    {
                        short increment = 0;
                        if (Int16.TryParse(parameters[0].TrimEnd('h'), out increment))
                        {
                            var marketData = frmDiscordBot.Storage.MarketStorage.LoadDataFromXML(DateTime.UtcNow.AddHours(-increment));

                            marketStats.GetStatInfo(marketData);

                            marketStats.totalTransactions = marketData.Count();

                            marketStats.DisplayStats(e.Channel);
                        }
                        else
                            await e.Channel.SendMessage("The parameter provided was not in the expected format, For example !stats.market 1d or !stats.market 1h");
                    }
                    else
                        await e.Channel.SendMessage("I was not able to determine what time period you specified, you can use h for hours and d for days. For example !stats.market 1d");
                }
                else
                {
                    var marketStats = new MarketStats();
                    var marketData = frmDiscordBot.Storage.MarketStorage.LoadDataFromXML(DateTime.UtcNow.AddDays(-1));

                    marketStats.GetStatInfo(marketData);

                    marketStats.totalTransactions = marketData.Count();

                    marketStats.DisplayStats(e.Channel);
                }
            });

            /*
             * Command: stats.ops
             * 
             * Purpose:
             * Used to provide stats on progression/regression between spy ops
             * 
             */
            commands.CreateCommand("stats.ops").Parameter("data", ParameterType.Multiple).Do(async (e) =>
            {
                var stats = new OpStats();
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 2);
                // Parameters: [CountryNum] [lastOps]
                if (parameters.Count() >= 2)
                {
                    int cnum;
                    short nbrOps;
                    if (Int32.TryParse(parameters[0], out cnum))
                    {
                        if (Int16.TryParse(parameters[1], out nbrOps))
                        {
                            // gather ops
                            stats.GatherOps(cnum, nbrOps);
                            // parse for stats
                            stats.GetStatInfo();
                            // Display Stats
                            stats.DisplayStats(e.Channel);
                        }
                        else
                            await e.User.SendMessage("Parameters included were not valid numbers, please check command and parameters and try again.");
                    }
                    else
                        await e.User.SendMessage("Parameters included were not valid numbers, please check command and parameters and try again.");
                }
                else
                    await e.User.SendMessage("Not all the required parameters were provided, please provide a country number and how many ops to track.");
            });

            #endregion

            /*
            * Command: atwar
            * 
            * Purpose:
            * Used to toggle war mode on or off
            * 
            * Result:
            * Enables online country detection messages
            * 
            */
            commands.CreateCommand("war").Parameter("tag", ParameterType.Optional).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);
                if (parameters.Count() > 0)
                {
                    var tag = parameters[0];
                    atWarTag = tag;
                    atWar = true;
                    await e.Channel.SendMessage("War mode has been enabled, your oponent has been set to " + tag + ".");
                }
                else
                {
                    atWar = false;
                    atWarTag = "";
                    await e.Channel.SendMessage("War mode has been disabled.");
                }
            });

            /*
             * Command: online
             * 
             * Purpose:
             * Used to provide details about a countries or tags login habits
             * 
             */
            commands.CreateCommand("online").Parameter("tag", ParameterType.Required).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);
                if (parameters.Count() > 0)
                {
                    int nbr;
                    if (Int32.TryParse(parameters[0], out nbr))
                    {
                        var logins = frmDiscordBot.Storage.CountryStorage.GetLoginData(nbr);
                        if (logins.Any(c => c.countryNum == nbr))
                        {
                            channels.SendLoginDataToChannel(logins.Single(c => c.countryNum == nbr), e.Channel);
                        }
                        else
                            await e.Channel.SendMessage("I have not detected any logins for that number.");
                    }
                    else
                    {
                        var tag = parameters[0];
                        var logins = frmDiscordBot.Storage.CountryStorage.GetLoginData(tag);
                        if(logins != null)
                        {
                            channels.SendLoginDataToChannel(logins, e.Channel);
                        }
                        else
                            await e.Channel.SendMessage("I have not detected any logins for that tag.");
                    }
                }
                else
                    await e.Channel.SendMessage("You have not provided me either a tag name or a country number, please try again using all parameters.");                
            });

            #region Country Lookup Commands

            /*
             * Command: cty.info
             * 
             * Purpose:
             * Used to provide details on a country
             * 
             */
            commands.CreateCommand("cty.info").Parameter("country", ParameterType.Required).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);

                if (parameters.Count() > 0)
                {
                    int num = 0;
                    if (Int32.TryParse(parameters[0], out num))
                    {
                        var country = frmDiscordBot.Storage.CountryStorage.Get(num);
                        var news = frmDiscordBot.Storage.NewsStorage.LoadDataFromXML(DateTime.UtcNow.AddDays(-1), num);
                        if (country != null)
                        {
                            if (news.Count() > 0)
                            {
                                var dr = country.DetermineDR(news);
                                await e.Channel.SendMessage(country.Name + "(#" + country.Number + ") [" + country.Tag + "] | Networth:" + country.ConvertNumberToDisplay(country.Networth) + " | Land:" + country.ConvertNumberToDisplay(country.Land) + " | DR:" + dr);
                            }
                            else
                                await e.Channel.SendMessage(country.Name + "(#" + country.Number + ") [" + country.Tag + "] | Networth:" + country.ConvertNumberToDisplay(country.Networth) + " | Land:" + country.ConvertNumberToDisplay(country.Land) + " | DR: Unknown");
                        }
                        else
                            await e.User.SendMessage("I don't have any data on that country yet, wait 5 minutes and I should receive new data.");
                        
                    }
                    else
                        await e.User.SendMessage("The country number provided was not a proper number, please try again with a country's number.");
                }
                else
                    await e.User.SendMessage("Not all parameters were included, please try again with all parameters.");
            });

            /*
             * Command: cty.op
             * 
             * Purpose:
             * Used to provide the latest spyop on a country
             * 
             */
            commands.CreateCommand("cty.op").Parameter("number", ParameterType.Required).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);

                if (parameters.Count() > 0)
                {
                    int num = 0;
                    if (Int32.TryParse(parameters[0], out num))
                    {
                        var op = frmDiscordBot.Storage.SpyOpsStorage.Get(num);
                        if (op != null)
                        {
                            var opInfo = new SpyOpInfo(op.json, op.type, op.uploader_api_key);
                            channels.SendSpyOpToChannel(opInfo, e.Channel);
                        }
                        else
                            await e.User.SendMessage("I do not have an op for that country.");

                    }
                    else
                        await e.User.SendMessage("The country number provided was not valid, please try again.");
                }
                else
                    await e.User.SendMessage("Not all required paramaters were included, please try again.");
            });

            #endregion

            #region User Commands
            /*
             * Command: add.user
             * 
             * Purpose:
             * Used to add a user to the db
             * 
             */
            commands.CreateCommand("add.user").Parameter("userId", ParameterType.Multiple).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 2);
                // command + discordid, apikey = 3 params
                if (parameters.Count() >= 2)
                {
                    ulong discordId = (ulong)Convert.ToInt64(parameters[0]);
                    var message = frmDiscordBot.Storage.UsersStorage.AddUser(channels.LoCServer.GetUser(discordId).Name, discordId, parameters[1]);
                    await e.User.SendMessage(message);
                    var discordUser = channels.LoCServer.GetUser(discordId);
                    if (discordUser != null)
                        await discordUser.SendMessage("You were successfully added to the team.");
                } // command + discordid = 2 params
                else if (parameters.Count() == 1)
                {
                    ulong discordId = (ulong)Convert.ToInt64(parameters[0]);
                    var message = frmDiscordBot.Storage.UsersStorage.AddUser(channels.LoCServer.GetUser(discordId).Name, discordId);
                    await e.User.SendMessage(message);
                    var discordUser = channels.LoCServer.GetUser(discordId);
                    if (discordUser != null)
                        await discordUser.SendMessage("You were successfully added to the team.");
                }
                else
                    await e.User.SendMessage("Not all required parameters where provided, please include at minimium the username and Discord UserId.");
            });

            /*
             * Command: remove.user
             * 
             * Purpose:
             * Used to remove a user from the db
             * 
             */
            commands.CreateCommand("remove.user").Parameter("userid", ParameterType.Required).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);
                string message;
                if (parameters != null)
                    message = frmDiscordBot.Storage.UsersStorage.RemoveUser((ulong)Convert.ToInt64(parameters[0]));
                else
                    message = "I did not receive a user's Discord ID, please try again.";
                await e.User.SendMessage(message);
            });

            /*
             * Command: update.api
             * 
             * Purpose:
             * Used to update a users EE api code
             * 
             */
            commands.CreateCommand("update.api").Parameter("userdata", ParameterType.Multiple).Do(async (e) =>
            {
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 2);
                if (parameters.Count() >= 2)
                {
                    ulong discordId;
                    if (ulong.TryParse(parameters[0], out discordId))
                    {
                        var message = frmDiscordBot.Storage.UsersStorage.UpdateAPIKey(discordId, parameters[1]);
                        await e.User.SendMessage(message);
                    }
                    else
                        await e.User.SendMessage("The Discord UserId provided was not valid, please varify the parameter are used in the right order. !update.api [discordUserId] [EE-API]");
                }
            });

            /*
             * Command: update.username 
             * 
             *  Purpose:
             *  Used to update a users username
             * 
             */
            commands.CreateCommand("update.username").Parameter("userdata", ParameterType.Multiple).Do(async (e) =>
            {
                string message = "";
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 2);
                if (parameters != null)
                {
                    ulong UserId;
                    if (ulong.TryParse(parameters[0], out UserId))
                    {
                        message = frmDiscordBot.Storage.UsersStorage.UpdateUsersName(UserId, parameters[1]);
                    }
                    else
                        message = "The DiscordUserId provided was not in the correct format, please check all parameters.";
                }
                else
                    message = "You will need to provide a users Discord ID and their name, please try again.";
                await e.Channel.SendMessage(message);
            });

            /*
             * Command: Mass Message
             * 
             * Purpose:
             * Sends a Mass Message to all users
             * 
             */
            commands.CreateCommand("mm").Parameter("data", ParameterType.Multiple).Do(async (e) =>
            {
                string message = "";
                var messageParts = e.Message.RawText.Split(' ');
                for(int i = 1; i < messageParts.Count(); i++)
                {
                    message += messageParts[i] + " ";
                }
                message = message.TrimEnd(' ');
                if (message != "")
                    channels.PrivateMessageAllUsers(message);
                else
                    await e.User.SendMessage("Please provide a message.");
            });
            #endregion

            #region Relation Commands
            /*
             * Command: relation.add
             * 
             * Purpose:
             * Adds the provided relation and saves it to XML
             * 
             */
            commands.CreateCommand("relation.add").Parameter("relation", ParameterType.Multiple).Do(async (e) =>
            {
                string message = "";
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 2);
                if (parameters.Count() == 2)
                {
                    var relation = new Relation();
                    relation.clanTag = parameters[0];
                    if (relation.DetermineRelationType(parameters[1]) != RelationTypes.NULL)
                    {
                        relation.RelationType = relation.DetermineRelationType(parameters[1]);
                        relation.timeStamp = DateTime.UtcNow;
                        var result = frmDiscordBot.Storage.RelationsStorage.Add(relation);
                        if (result)
                            message = "Relation was successfully saved to the database.";
                        else
                            message = "There was a problem encountered when adding that relation to the database.";
                    }
                    else
                        message = "I did not recognize that pact type, please try again.";
                }
                else
                    message = "I was not provided enough parameters to complete your request, please try again with all the required parameters.";
                await e.User.SendMessage(message);
            });

            /*
             * Command: relation.remove
             * 
             * Purpose:
             * Removes the provided relation and its XML
             * 
             */
            commands.CreateCommand("relation.remove").Parameter("relation", ParameterType.Required).Do(async (e) =>
            {
                string message = "";
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);
                if (parameters.Count() != 1)
                {
                    if (frmDiscordBot.Storage.RelationsStorage.Remove(parameters[0]))
                    {
                        message = "Provided relation was removed successfully.";
                    }
                    else
                        message = "A problem was encountered when attempting to remove the provided relation, please try again.";
                }
                else
                    message = "I was not provided enough parameters to complete your request, please try again with all the required parameters.";
                await e.Channel.SendMessage(message);
            });

            /*
             * Command: relations
             * 
             * Purpose:
             * Messages the channel the current relations 
             * 
             */
            commands.CreateCommand("relations").Do(async (e) =>
            {
                await e.Channel.SendMessage(channels.BuildRelationGreetingMessage());
            });
            #endregion

            #region KillList Commands
            /*
             * Command: killlist.add
             * 
             * Purpose:
             * Adds the provided country to the Kill List
             * 
             */
            commands.CreateCommand("killlist.add").Parameter("country", ParameterType.Required).Do(async (e) =>
            {
                string message = "";
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);
                if (parameters.Count() == 1)
                {
                    int nbr;
                    if (Int32.TryParse(parameters[0], out nbr))
                    {
                        if (frmDiscordBot.Storage.CountryStorage.AddCountryToKillList(nbr))
                            message = "I successfully added that country to the Kill List.";
                        else
                            message = "I encountered a problem with adding the country provided to the KillList.";
                    }
                    else
                        message = "I was not able to determine what country number you provided, please try again.";
                }
                else
                    message = "I was not provided enough parameters to handle your request, please try again.";
                await e.User.SendMessage(message);
            });

            /*
             * Command: killlist.remove
             * 
             * Purpose:
             * Removes the provided country from the Kill List
             * 
             */
            commands.CreateCommand("killlist.remove").Parameter("country", ParameterType.Required).Do(async (e) =>
            {
                string message = "";
                var parameters = GetCommandParametersFromInput(e.Message.RawText, 1);
                if (parameters.Count() == 1)
                {
                    int nbr;
                    if (Int32.TryParse(parameters[0], out nbr))
                    {
                        if (frmDiscordBot.Storage.CountryStorage.RemoveCountryFromKillList(nbr))
                            message = "I successfully removed that country to the Kill List.";
                        else
                            message = "I encountered a problem with removing the country provided to the KillList.";
                    }
                    else
                        message = "I was not able to determine what country number you provided, please try again.";
                }
                else
                    message = "I was not provided enough parameters to handle your request, please try again.";
                await e.User.SendMessage(message);
            });

            /*
             * Command: killlist
             * 
             * Purpose:
             * Sends a message to the channel with the current kill list
             * 
             */
            commands.CreateCommand("killlist").Do(async (e) =>
            {
                await e.Channel.SendMessage(channels.BuildKillListGreetingMessage());
            });
            #endregion

            Thread connectThread = new Thread(() => Connect(client));
            connectThread.Start();
        }
        
        /*
         * Connect
         * 
         * Purpose:
         * This method handles making the connection to the Discord Server
         * 
         */ 
        public void Connect(DiscordClient client)
        {
            try
            {
                client.ExecuteAndWait(async () =>
                {
                    await client.Connect("MzE3MDA2NDUzNjY1OTU1ODQy.DI5aNg.BltoLgnXSWbmvGj7fazNL89sCUQ", Discord.TokenType.Bot);
                    Thread.Sleep(500);
                    channels = new Channels(client);
                });
            }
            catch
            {

            }
        }


        /*
         * Get Command Parameters From Input Method
         * 
         * Purpose:
         * Returns a list of parameters provided from the raw message received
         * 
         */
        public List<string> GetCommandParametersFromInput(string input, int numParameters)
        {
            List<string> Parameters = new List<string>();

            var messageParts = input.Split(' ');

            //if ((messageParts.Count() - 1) >= numParameters)
            //{
                for(int i = 1; i < messageParts.Count(); i++)
                {
                        Parameters.Add(messageParts[i]);
                }
            //}

            return Parameters;
        }
    }
}
        
