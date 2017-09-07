using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadBot_3.Models
{
    public class StatStylizingCommands
    {
        /*
         * Convert Number To Display Method
         * 
         * Purpose:
         * Returns a string representing an abbreviated number based on the long provided
         * 
         */
        public string ConvertNumberToDisplay(long nbr)
        {
            string displayStat = "";
            bool negNbr = false;
            if (nbr < 0)
            {
                nbr = (0 - nbr);
                negNbr = true;
            }

            if (nbr > 1000000000)
            {
                displayStat = (nbr / (double)1000000000).ToString("N1") + "B";
            }
            else if (nbr > 1000000)
            {
                displayStat = (nbr / (double)1000000).ToString("N1") + "M";
            }
            else if (nbr > 1000)
            {
                displayStat = (nbr / (double)1000).ToString("N1") + "K";
            }
            else
            {
                displayStat = nbr.ToString("N0");
            }

            if (negNbr)
                displayStat.Insert(0, "-");

            return displayStat;
        }

        /*
         * Convert Number To Display Method
         * 
         * Purpose:
         * Returns a string representing an abbreviated number based on the int provided
         * 
         */
        public string ConvertNumberToDisplay(int num)
        {
            string display = "";
            if (num > 1000000000)
                display = (num / 1000000000.0).ToString("N1") + "G";
            else if (num > 1000000)
                display = (num / 1000000.0).ToString("N1") + "M";
            else if (num > 1000)
                display = (num / 1000.0).ToString("N1") + "k";
            else
                display = num.ToString("N0");
            return display;
        }

        /*
         * Trim To Max Method
         * 
         * Purpose:
         * Returns a string Trimmed to the max length provided
         * 
         */
        public string TrimToMax(int maxLen, string message)
        {
            if (message.Length > maxLen)
            {
                message = message.Remove(maxLen - 1, ((message.Length - 1) - (maxLen - 1)));
            }
            if (message.Length % 2 != 0)
                message += " ";
            return message;
        }

        /*
         * Determine Spaces Method
         * 
         * Purpose:
         * Returns an int determining how many spaces needed to be added equally before and after the message to 
         * meet the given length requirement provided
         * 
         */
        public int DetermineSpaces(int messageLen, int maxLen)
        {
            int spaces = 2;
            if (messageLen < maxLen)
                spaces = maxLen - messageLen;
            return spaces;
        }

        /*
         * Add Spaces Method
         * 
         * Purpose:
         * Returns a string with the provided number of spaces split equally between the beginging and the end of the string provided
         * 
         */
        public string AddSpaces(int spaces, string message)
        {
            string display = "";

            if (spaces % 2 == 0)
            {
                for (int i = 0; i < spaces / 2; i++)
                {
                    display += " ";
                }
                display += message;
                for (int i = spaces / 2; i < spaces; i++)
                {
                    display += " ";
                }
            }
            else
            {
                for (int i = 0; i < Math.Floor(spaces / (double)2); i++)
                {
                    display += " ";
                }
                display += message;
                for (int i = (int)Math.Ceiling(spaces / (double)2); i < spaces; i++)
                {
                    display += " ";
                }
            }

            if (display.Length % 2 != 0)
                display += " ";
            return display;
        }

        /*
         * Trim Enum Product To String Method
         * 
         * Purpose:
         * Returns an abbreviated string representing the product provided
         * 
         */
        public string TrimEnumProductToString(products product)
        {
            switch (product)
            {
                case products.AgricultureTech:
                    return "AgrTech";
                case products.Barrels:
                    return "Barrels";
                case products.Bushels:
                    return "Bushels";
                case products.BusinessTech:
                    return "BusTech";
                case products.IndustrialTech:
                    return "IndTech";
                case products.Jets:
                    return "Jets";
                case products.MedicalTech:
                    return "MedTech";
                case products.MilitaryStratTech:
                    return "MStrat";
                case products.MilitaryTech:
                    return "MilTech";
                case products.ResidentialTech:
                    return "ResTech";
                case products.SDITech:
                    return "SDITech";
                case products.SpyTech:
                    return "SpyTech";
                case products.Tanks:
                    return "Tanks";
                case products.Troops:
                    return "Troops";
                case products.Turrets:
                    return "Turrets";
                case products.WarfareTech:
                    return "WarTech";
                case products.WeaponsTech:
                    return "WeapTech";
                default:
                    return "Unknown";
            }
        }
    }

    public class NettingStats : StatStylizingCommands
    {
        public List<UserNetStat> Stats = new List<UserNetStat>();

        /*
         * Net Stats Method
         * 
         * Purpose:
         * Returns a Netting Stats object
         * 
         */
        public void GetStatsInfo()
        {
            try
            {
                var date = DateTime.UtcNow;
                var timestamp24hrsAgo = date.AddDays(-1);
                var timestamp48hrsAgo = date.AddDays(-2);
                var timestamp72hrsAgo = date.AddDays(-3);
                var countries = frmDiscordBot.Storage.CountryStorage.ClaimedCountries();
                var news = frmDiscordBot.Storage.NewsStorage.LoadDataFromXML(DateTime.UtcNow.AddDays(-3));
                var countryData = frmDiscordBot.Storage.CountryStorage.CountryData();
                //for each 24, 48 and 72 hours
                if (countries.Count() > 0)
                {
                    var users = countries.Select(c => c.User).Distinct().ToList();
                    foreach (var user in users)
                    {
                        var userStat = new UserNetStat();
                        userStat.username = user;
                        userStat.stat24hrs = new netStat();
                        userStat.stat48hrs = new netStat();
                        userStat.stat72hrs = new netStat();
                        // get each country number belonging to that user
                        foreach (var country in countries.Where(c => c.User == user).ToList())
                        {
                            //# of SS
                            userStat.stat24hrs.TotalSS += news.Where(c => c.attacker_num == country.Number && c.Type == "1" && c.timestamp >= timestamp24hrsAgo).Count();
                            userStat.stat48hrs.TotalSS += news.Where(c => c.attacker_num == country.Number && c.Type == "1" && c.timestamp >= timestamp48hrsAgo).Count();
                            userStat.stat72hrs.TotalSS += news.Where(c => c.attacker_num == country.Number && c.Type == "1" && c.timestamp >= timestamp72hrsAgo).Count();
                            //# of PS
                            userStat.stat24hrs.TotalPS += news.Where(c => c.attacker_num == country.Number && c.Type == "2" && c.timestamp >= timestamp24hrsAgo).Count();
                            userStat.stat48hrs.TotalPS += news.Where(c => c.attacker_num == country.Number && c.Type == "2" && c.timestamp >= timestamp48hrsAgo).Count();
                            userStat.stat72hrs.TotalPS += news.Where(c => c.attacker_num == country.Number && c.Type == "2" && c.timestamp >= timestamp72hrsAgo).Count();
                            //Total Land
                            userStat.stat24hrs.TotalLandGained += (int)news.Where(c => c.attacker_num == country.Number && (c.Type == "!" || c.Type == "2") && c.timestamp >= timestamp24hrsAgo).Select(c => c.result2).Sum();
                            userStat.stat48hrs.TotalLandGained += (int)news.Where(c => c.attacker_num == country.Number && (c.Type == "!" || c.Type == "2") && c.timestamp >= timestamp48hrsAgo).Select(c => c.result2).Sum();
                            userStat.stat72hrs.TotalLandGained += (int)news.Where(c => c.attacker_num == country.Number && (c.Type == "!" || c.Type == "2") && c.timestamp >= timestamp72hrsAgo).Select(c => c.result2).Sum();
                            //Ghost Acres
                            userStat.stat24hrs.TotalGA += (int)news.Where(c => c.attacker_num == country.Number && (c.Type == "!" || c.Type == "2") && c.timestamp >= timestamp24hrsAgo).Select(c => c.result2 - c.result1).Sum();
                            userStat.stat48hrs.TotalGA += (int)news.Where(c => c.attacker_num == country.Number && (c.Type == "!" || c.Type == "2") && c.timestamp >= timestamp48hrsAgo).Select(c => c.result2 - c.result1).Sum();
                            userStat.stat72hrs.TotalGA += (int)news.Where(c => c.attacker_num == country.Number && (c.Type == "!" || c.Type == "2") && c.timestamp >= timestamp72hrsAgo).Select(c => c.result2 - c.result1).Sum();
                            //net change
                            if (countryData[0].Count() > 0)
                                userStat.stat24hrs.NetChange += (country.Networth - countryData[0].SingleOrDefault(c => c.Number == country.Number).Networth);
                            if (countryData[1].Count() > 0)
                                userStat.stat48hrs.NetChange += (country.Networth - countryData[1].SingleOrDefault(c => c.Number == country.Number).Networth);
                            if (countryData[2].Count() > 0)
                                userStat.stat72hrs.NetChange += (country.Networth - countryData[2].SingleOrDefault(c => c.Number == country.Number).Networth);
                            //land change
                            if (countryData[0].Count() > 0)
                                userStat.stat24hrs.LandChange += (country.Land - countryData[0].SingleOrDefault(c => c.Number == country.Number).Land);
                            if (countryData[1].Count() > 0)
                                userStat.stat48hrs.LandChange += (country.Land - countryData[1].SingleOrDefault(c => c.Number == country.Number).Land);
                            if (countryData[2].Count() > 0)
                                userStat.stat72hrs.LandChange += (country.Land - countryData[2].SingleOrDefault(c => c.Number == country.Number).Land);
                        }
                        Stats.Add(userStat);
                    }
                }
                countries.Clear();
                countryData[0].Clear();
                countryData[1].Clear();
                countryData[2].Clear();
                news.Clear();
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        /*
         * Display Stats Method
         * 
         * Purpose:
         * Posts a message to the channel provided parsed from the Stats object
         * 
         */
        public async void DisplayStats(Channel chan)
        {
            string message = "```";
            message += "|__________________________________24 Hrs_________________________________|" + Environment.NewLine;
            message += "|  Username  |    SS    |   PS   |  Land+ |   GA+    |  Net+/-  | Land+/- |" + Environment.NewLine;
            foreach (var userStats in Stats)
            {
                userStats.username = TrimToMax(10, userStats.username);
                message += "|" + AddSpaces(DetermineSpaces(userStats.username.Length, 9), userStats.username) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat24hrs.TotalSS).Length, 10), ConvertNumberToDisplay(userStats.stat24hrs.TotalSS)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat24hrs.TotalPS).Length, 8), ConvertNumberToDisplay(userStats.stat24hrs.TotalPS)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat24hrs.TotalLandGained).Length, 8), ConvertNumberToDisplay(userStats.stat24hrs.TotalLandGained)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat24hrs.TotalGA).Length, 9), ConvertNumberToDisplay(userStats.stat24hrs.TotalGA)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat24hrs.NetChange).Length, 9), ConvertNumberToDisplay(userStats.stat24hrs.NetChange)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat24hrs.LandChange).Length, 8), ConvertNumberToDisplay(userStats.stat24hrs.LandChange)) + "|" + Environment.NewLine;
            }
            message += "|__________________________________48 Hrs_________________________________|" + Environment.NewLine;
            message += "|  Username  |    SS    |   PS   |  Land+ |   GA+    |  Net+/-  | Land+/- |" + Environment.NewLine;
            foreach (var userStats in Stats)
            {
                userStats.username = TrimToMax(10, userStats.username);
                message += "|" + AddSpaces(DetermineSpaces(userStats.username.Length, 9), userStats.username) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat48hrs.TotalSS).Length, 10), ConvertNumberToDisplay(userStats.stat48hrs.TotalSS)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat48hrs.TotalPS).Length, 8), ConvertNumberToDisplay(userStats.stat48hrs.TotalPS)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat48hrs.TotalLandGained).Length, 8), ConvertNumberToDisplay(userStats.stat48hrs.TotalLandGained)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat48hrs.TotalGA).Length, 10), ConvertNumberToDisplay(userStats.stat48hrs.TotalGA)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat48hrs.NetChange).Length, 9), ConvertNumberToDisplay(userStats.stat48hrs.NetChange)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat48hrs.LandChange).Length, 8), ConvertNumberToDisplay(userStats.stat48hrs.LandChange)) + "|" + Environment.NewLine;
            }
            message += "|__________________________________72 Hrs_________________________________|" + Environment.NewLine;
            message += "|  Username  |    SS    |   PS   |  Land+ |   GA+    |  Net+/-  | Land+/- |" + Environment.NewLine;
            foreach (var userStats in Stats)
            {
                userStats.username = TrimToMax(10, userStats.username);
                message += "|" + AddSpaces(DetermineSpaces(userStats.username.Length, 9), userStats.username) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat72hrs.TotalSS).Length, 10), ConvertNumberToDisplay(userStats.stat72hrs.TotalSS)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat72hrs.TotalPS).Length, 8), ConvertNumberToDisplay(userStats.stat72hrs.TotalPS)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat72hrs.TotalLandGained).Length, 8), ConvertNumberToDisplay(userStats.stat72hrs.TotalLandGained)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat72hrs.TotalGA).Length, 11), ConvertNumberToDisplay(userStats.stat72hrs.TotalGA)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat72hrs.NetChange).Length, 9), ConvertNumberToDisplay(userStats.stat72hrs.NetChange)) + "|";
                message += AddSpaces(DetermineSpaces(ConvertNumberToDisplay(userStats.stat72hrs.LandChange).Length, 8), ConvertNumberToDisplay(userStats.stat72hrs.LandChange)) + "|" + Environment.NewLine;
            }
            message += "```";
            await chan.SendMessage(message);
        }
    }

    public class UserNetStat
    {
        public string username { get; set; }
        public netStat stat24hrs { get; set; }
        public netStat stat48hrs { get; set; }
        public netStat stat72hrs { get; set; }
    }

    public class netStat
    {
        public int TotalSS { get; set; }
        public int TotalPS { get; set; }
        public int TotalLandGained { get; set; }
        public int TotalDefends { get; set; }
        public int TotalLandLost { get; set; }
        public int TotalGA { get; set; }
        public long NetChange { get; set; }
        public int LandChange { get; set; }
    }

    public class WarStats : StatStylizingCommands
    {
        public string username { get; set; }
        public string tagA { get; set; }
        public string tagB { get; set; }
        public DateTime Timeperiod_Date { get; set; }
        public long Timeperiod_Unix { get; set; }
        public warStat Stats { get; set; }
        public warStat tagA_Stats { get; set; }
        public warStat tagB_Stats { get; set; }

        /*
         * War Stats Method
         * 
         * Purpose:
         * returns a War stats for a user
         * 
         */
        public void GetStatInfo()
        {
            try
            {
                var countries = frmDiscordBot.Storage.CountryStorage.ClaimedCountries();
                var news = frmDiscordBot.Storage.NewsStorage.LoadDataFromXML(Timeperiod_Date);
                if (tagA == null && tagB == null)
                {
                    // get list of country numbers
                    var countryNums = countries.Where(c => c.User == username).Select(c => c.Number).ToList();
                    // build list of news within timeperiod

                    Stats = new warStat();
                    foreach (var countryNum in countryNums)
                    {
                        Stats.AB += news.Where(c => c.Type == "7" && c.attacker_num == countryNum).Count();
                        Stats.BR += news.Where(c => c.Type == "6" && c.attacker_num == countryNum).Count();
                        Stats.GS += news.Where(c => c.Type == "5" && c.attacker_num == countryNum).Count();
                        Stats.CM += news.Where(c => c.Type == "11" && c.attacker_num == countryNum).Count();
                        Stats.NM += news.Where(c => c.Type == "10" && c.attacker_num == countryNum).Count();
                        Stats.EM += news.Where(c => c.Type == "12" && c.attacker_num == countryNum).Count();
                        Stats.specAttacks = Stats.AB + Stats.BR + Stats.GS;
                        Stats.Missiles = Stats.NM + Stats.CM + Stats.EM;
                        Stats.kills += news.Where(c => c.killhit == 1 && c.attacker_num == countryNum).Count();
                        Stats.deaths += news.Where(c => c.killhit == 1 && c.defender_num == countryNum).Count();
                        Stats.defends += news.Where(c => c.defender_num == countryNum).Count();
                    }
                }
                else
                {
                    news = news.Where(c => (c.a_tag == tagA || c.d_tag == tagA) && (c.a_tag == tagB || c.d_tag == tagB)).ToList();
                    tagA_Stats = new warStat();
                    tagA_Stats.AB = news.Where(c => c.Type == "7" && c.a_tag == tagA).Count();
                    tagA_Stats.BR = news.Where(c => c.Type == "6" && c.a_tag == tagA).Count();
                    tagA_Stats.GS = news.Where(c => c.Type == "5" && c.a_tag == tagA).Count();
                    tagA_Stats.CM = news.Where(c => c.Type == "11" && c.a_tag == tagA).Count();
                    tagA_Stats.NM = news.Where(c => c.Type == "10" && c.a_tag == tagA).Count();
                    tagA_Stats.EM = news.Where(c => c.Type == "12" && c.a_tag == tagA).Count();
                    tagA_Stats.specAttacks = Stats.AB + Stats.BR + Stats.GS;
                    tagA_Stats.Missiles = Stats.NM + Stats.CM + Stats.EM;
                    tagA_Stats.kills = news.Where(c => c.killhit == 1 && c.a_tag == tagA).Count();
                    tagA_Stats.deaths = news.Where(c => c.killhit == 1 && c.d_tag == tagA).Count();
                    tagA_Stats.defends = news.Where(c => c.d_tag == tagA).Count();

                    tagB_Stats = new warStat();
                    tagB_Stats.AB = news.Where(c => c.Type == "7" && c.a_tag == tagA).Count();
                    tagB_Stats.BR = news.Where(c => c.Type == "6" && c.a_tag == tagA).Count();
                    tagB_Stats.GS = news.Where(c => c.Type == "5" && c.a_tag == tagA).Count();
                    tagB_Stats.CM = news.Where(c => c.Type == "11" && c.a_tag == tagA).Count();
                    tagB_Stats.NM = news.Where(c => c.Type == "10" && c.a_tag == tagA).Count();
                    tagB_Stats.EM = news.Where(c => c.Type == "12" && c.a_tag == tagA).Count();
                    tagB_Stats.specAttacks = Stats.AB + Stats.BR + Stats.GS;
                    tagB_Stats.Missiles = Stats.NM + Stats.CM + Stats.EM;
                    tagB_Stats.kills = news.Where(c => c.killhit == 1 && c.a_tag == tagA).Count();
                    tagB_Stats.deaths = news.Where(c => c.killhit == 1 && c.d_tag == tagA).Count();
                    tagB_Stats.defends = news.Where(c => c.d_tag == tagA).Count();
                }
                countries.Clear();
                news.Clear();
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        /*
         * Send War Stats Method
         * 
         * Purpose:
         * Handles sending war stat data to channel
         * 
         */
        public async void DisplayStats(Discord.Channel channel)
        {
            try
            {
                string message = "";
                if (username != null)
                {
                    message = "```"
                        + Environment.NewLine
                        + "|                    Stats for: " + TrimToMax(9, username) + " || From " + Timeperiod_Date.ToShortDateString() + " to Today                  |"
                        + Environment.NewLine
                        + "| Spec |  GS  |  BR  |  AB  | Missiles |  CM  |  NM  |  EM  | Kills | Deaths | Defends |"
                        + Environment.NewLine
                        + "|" + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.specAttacks).Count(), 6), ConvertNumberToDisplay(Stats.specAttacks)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.GS).Count(), 6), ConvertNumberToDisplay(Stats.GS)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.BR).Count(), 6), ConvertNumberToDisplay(Stats.BR)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.AB).Count(), 6), ConvertNumberToDisplay(Stats.AB)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.Missiles).Count(), 10), ConvertNumberToDisplay(Stats.Missiles)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.CM).Count(), 6), ConvertNumberToDisplay(Stats.CM)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.NM).Count(), 6), ConvertNumberToDisplay(Stats.NM)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.EM).Count(), 6), ConvertNumberToDisplay(Stats.EM)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.kills).Count(), 6), ConvertNumberToDisplay(Stats.kills)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.deaths).Count(), 8), ConvertNumberToDisplay(Stats.deaths)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats.defends).Count(), 9), ConvertNumberToDisplay(Stats.defends)) + "|```";
                }
                else
                {
                    message = "```"
                        + "|  TAG  | Spec |  GS  |  BR  |  AB  | Missiles |  CM  |  NM  |  EM  | Kills | Deaths | Defends |"
                        + Environment.NewLine
                        + "|" + AddSpaces(DetermineSpaces(tagA.Length, 6), tagA) + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.specAttacks).Count(), 6), ConvertNumberToDisplay(tagA_Stats.specAttacks)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.GS).Count(), 6), ConvertNumberToDisplay(tagA_Stats.GS)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.BR).Count(), 6), ConvertNumberToDisplay(tagA_Stats.BR)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.AB).Count(), 6), ConvertNumberToDisplay(tagA_Stats.AB)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.Missiles).Count(), 10), ConvertNumberToDisplay(tagA_Stats.Missiles)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.CM).Count(), 6), ConvertNumberToDisplay(tagA_Stats.CM)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.NM).Count(), 6), ConvertNumberToDisplay(tagA_Stats.NM)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.EM).Count(), 6), ConvertNumberToDisplay(tagA_Stats.EM)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.kills).Count(), 6), ConvertNumberToDisplay(tagA_Stats.kills)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.deaths).Count(), 8), ConvertNumberToDisplay(tagA_Stats.deaths)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagA_Stats.defends).Count(), 9), ConvertNumberToDisplay(tagA_Stats.defends)) + "|"
                        + Environment.NewLine
                        + "|" + AddSpaces(DetermineSpaces(tagB.Length, 6), tagB) + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.specAttacks).Count(), 6), ConvertNumberToDisplay(tagB_Stats.specAttacks)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.GS).Count(), 6), ConvertNumberToDisplay(tagB_Stats.GS)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.BR).Count(), 6), ConvertNumberToDisplay(tagB_Stats.BR)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.AB).Count(), 6), ConvertNumberToDisplay(tagB_Stats.AB)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.Missiles).Count(), 10), ConvertNumberToDisplay(tagB_Stats.Missiles)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.CM).Count(), 6), ConvertNumberToDisplay(tagB_Stats.CM)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.NM).Count(), 6), ConvertNumberToDisplay(tagB_Stats.NM)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.EM).Count(), 6), ConvertNumberToDisplay(tagB_Stats.EM)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.kills).Count(), 6), ConvertNumberToDisplay(tagB_Stats.kills)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.deaths).Count(), 8), ConvertNumberToDisplay(tagB_Stats.deaths)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(tagB_Stats.defends).Count(), 9), ConvertNumberToDisplay(tagB_Stats.defends)) + "|"
                        + "```";
                }
                await channel.SendMessage(message);
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }
    }

    public class warStat
    {
        public int specAttacks { get; set; }
        public int GS { get; set; }
        public int BR { get; set; }
        public int AB { get; set; }
        public int Missiles { get; set; }
        public int CM { get; set; }
        public int NM { get; set; }
        public int EM { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int defends { get; set; }
    }

    public class MarketStats : StatStylizingCommands
    {
        public int totalTransactions { get; set; }
        public List<productStat> ProductStats { get; set; }

        public MarketStats()
        {
            ProductStats = new List<productStat>();
            ProductStats.Add(new productStat { product = products.Troops });
            ProductStats.Add(new productStat { product = products.Jets });
            ProductStats.Add(new productStat { product = products.Turrets });
            ProductStats.Add(new productStat { product = products.Tanks });
            ProductStats.Add(new productStat { product = products.Bushels });
            ProductStats.Add(new productStat { product = products.Barrels });
            ProductStats.Add(new productStat { product = products.MilitaryTech });
            ProductStats.Add(new productStat { product = products.MedicalTech });
            ProductStats.Add(new productStat { product = products.BusinessTech });
            ProductStats.Add(new productStat { product = products.ResidentialTech });
            ProductStats.Add(new productStat { product = products.AgricultureTech });
            ProductStats.Add(new productStat { product = products.WarfareTech });
            ProductStats.Add(new productStat { product = products.WeaponsTech });
            ProductStats.Add(new productStat { product = products.MilitaryStratTech });
            ProductStats.Add(new productStat { product = products.IndustrialTech });
            ProductStats.Add(new productStat { product = products.SpyTech });
            ProductStats.Add(new productStat { product = products.SDITech });
        }
        
        /*
         * Send Market Stats Method
         * 
         * Purpose:
         * Handles sending market stat data to channel
         * 
         */
        public async void DisplayStats(Channel chan)
        {
            try
            {
                string message = "```";

                message += AddSpaces(DetermineSpaces("Prod".Length, 6), "Prod") + "    |" +
                           AddSpaces(DetermineSpaces("Min".Length, 6), "Min") + "|" +
                           AddSpaces(DetermineSpaces("Avg".Length, 6), "Avg") + "|" +
                           AddSpaces(DetermineSpaces("Max".Length, 6), "Max") + "|" +
                           AddSpaces(DetermineSpaces("Sold".Length, 6), "Sold") + "|" +
                           AddSpaces(DetermineSpaces("#Trans".Length, 6), "#Trans") + "|" +
                           AddSpaces(DetermineSpaces("%Trans".Length, 6), "%Trans") + Environment.NewLine;

                foreach(var product in ProductStats.Select(c => c.product).Distinct())
                {
                    message += AddSpaces(DetermineSpaces(TrimEnumProductToString(product).Length, 6), TrimEnumProductToString(product)) + "|" +
                               AddSpaces(DetermineSpaces(ProductStats.SingleOrDefault(c => c.product == product).minPrice.ToString("N0").Length, 6), ProductStats.SingleOrDefault(c => c.product == product).minPrice.ToString("N0")) + "|" +
                               AddSpaces(DetermineSpaces(ProductStats.SingleOrDefault(c => c.product == product).avgPrice.ToString("N0").Length, 6), ProductStats.SingleOrDefault(c => c.product == product).avgPrice.ToString("N0")) + "|" +
                               AddSpaces(DetermineSpaces(ProductStats.SingleOrDefault(c => c.product == product).maxPrice.ToString("N0").Length, 6), ProductStats.SingleOrDefault(c => c.product == product).maxPrice.ToString("N0")) + "|" +
                               AddSpaces(DetermineSpaces(ConvertNumberToDisplay(ProductStats.SingleOrDefault(c => c.product == product).quantitySold).Length, 6), ConvertNumberToDisplay(ProductStats.SingleOrDefault(c => c.product == product).quantitySold)) + "|" +
                               AddSpaces(DetermineSpaces(ProductStats.SingleOrDefault(c => c.product == product).totalNbrOfTrans.ToString("N0").Length, 6), ProductStats.SingleOrDefault(c => c.product == product).totalNbrOfTrans.ToString("N0")) + "|" +
                               AddSpaces(DetermineSpaces(ProductStats.SingleOrDefault(c => c.product == product).percentageOfTotalTransactions.ToString("P1").Length, 6), ProductStats.SingleOrDefault(c => c.product == product).percentageOfTotalTransactions.ToString("P1")) +
                               Environment.NewLine;
                }
                message += "```";

                await chan.SendMessage(message);
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        /*
         * Build Market Stats Method 
         * 
         * Purpose:
         * Handles parsing Market Transactions, building a MarketStats object
         * 
         */
        public MarketStats GetStatInfo(List<api_mtrans> trans)
        {
            try
            {
                // instantiate new stats object

                totalTransactions = trans.Count();
                // check every transaction and record stats accurately to each product
                foreach (var tran in trans)
                {
                    #region Tally up Stats
                    switch (tran.goodid)
                    {
                        case 1:
                            ProductStats.SingleOrDefault(c => c.product == products.Troops).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.Troops).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.Troops).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.Troops).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Troops).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.Troops).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Troops).maxPrice = tran.cost;
                            break;
                        case 2:
                            ProductStats.SingleOrDefault(c => c.product == products.Jets).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.Jets).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.Jets).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.Jets).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Jets).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.Jets).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Jets).maxPrice = tran.cost;
                            break;
                        case 3:
                            ProductStats.SingleOrDefault(c => c.product == products.Turrets).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.Turrets).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.Turrets).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.Turrets).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Turrets).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.Turrets).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Turrets).maxPrice = tran.cost;
                            break;
                        case 4:
                            ProductStats.SingleOrDefault(c => c.product == products.Tanks).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.Tanks).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.Tanks).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.Tanks).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Tanks).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.Tanks).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Tanks).maxPrice = tran.cost;
                            break;
                        case 5:
                            ProductStats.SingleOrDefault(c => c.product == products.Bushels).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.Bushels).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.Bushels).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.Bushels).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Bushels).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.Bushels).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Bushels).maxPrice = tran.cost;
                            break;
                        case 6:
                            ProductStats.SingleOrDefault(c => c.product == products.Barrels).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.Barrels).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.Barrels).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.Barrels).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Barrels).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.Barrels).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.Barrels).maxPrice = tran.cost;
                            break;
                        case 7:
                            ProductStats.SingleOrDefault(c => c.product == products.MilitaryTech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.MilitaryTech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.MilitaryTech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.MilitaryTech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.MilitaryTech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.MilitaryTech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.MilitaryTech).maxPrice = tran.cost;
                            break;
                        case 8:
                            ProductStats.SingleOrDefault(c => c.product == products.MedicalTech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.MedicalTech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.MedicalTech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.MedicalTech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.MedicalTech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.MedicalTech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.MedicalTech).maxPrice = tran.cost;
                            break;
                        case 9:
                            ProductStats.SingleOrDefault(c => c.product == products.BusinessTech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.BusinessTech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.BusinessTech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.BusinessTech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.BusinessTech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.BusinessTech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.BusinessTech).maxPrice = tran.cost;
                            break;
                        case 10:
                            ProductStats.SingleOrDefault(c => c.product == products.ResidentialTech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.ResidentialTech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.ResidentialTech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.ResidentialTech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.ResidentialTech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.ResidentialTech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.ResidentialTech).maxPrice = tran.cost;
                            break;
                        case 11:
                            ProductStats.SingleOrDefault(c => c.product == products.AgricultureTech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.AgricultureTech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.AgricultureTech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.AgricultureTech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.AgricultureTech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.AgricultureTech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.AgricultureTech).maxPrice = tran.cost;
                            break;
                        case 12:
                            ProductStats.SingleOrDefault(c => c.product == products.WarfareTech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.WarfareTech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.WarfareTech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.WarfareTech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.WarfareTech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.WarfareTech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.WarfareTech).maxPrice = tran.cost;
                            break;
                        case 13:
                            ProductStats.SingleOrDefault(c => c.product == products.MilitaryStratTech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.MilitaryStratTech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.MilitaryStratTech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.MilitaryStratTech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.MilitaryStratTech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.MilitaryStratTech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.MilitaryStratTech).maxPrice = tran.cost;
                            break;
                        case 14:
                            ProductStats.SingleOrDefault(c => c.product == products.WeaponsTech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.WeaponsTech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.WeaponsTech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.WeaponsTech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.WeaponsTech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.WeaponsTech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.WeaponsTech).maxPrice = tran.cost;
                            break;
                        case 15:
                            ProductStats.SingleOrDefault(c => c.product == products.IndustrialTech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.IndustrialTech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.IndustrialTech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.IndustrialTech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.IndustrialTech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.IndustrialTech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.IndustrialTech).maxPrice = tran.cost;
                            break;
                        case 16:
                            ProductStats.SingleOrDefault(c => c.product == products.SpyTech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.SpyTech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.SpyTech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.SpyTech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.SpyTech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.SpyTech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.SpyTech).maxPrice = tran.cost;
                            break;
                        case 17:
                            ProductStats.SingleOrDefault(c => c.product == products.SDITech).quantitySold += tran.quantity;
                            ProductStats.SingleOrDefault(c => c.product == products.SDITech).totalNbrOfTrans++;
                            ProductStats.SingleOrDefault(c => c.product == products.SDITech).totalCostSold += tran.cost;
                            if (tran.cost < ProductStats.SingleOrDefault(c => c.product == products.SDITech).minPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.SDITech).minPrice = tran.cost;
                            if (tran.cost > ProductStats.SingleOrDefault(c => c.product == products.SDITech).maxPrice)
                                ProductStats.SingleOrDefault(c => c.product == products.SDITech).maxPrice = tran.cost;
                            break;
                    }
                    #endregion
                }
                // now that we have totals determine average and percentage of transactions
                foreach (var tran in ProductStats)
                {
                    if (tran.totalCostSold > 0 && tran.totalNbrOfTrans > 0)
                    {
                        tran.avgPrice = tran.totalCostSold / tran.totalNbrOfTrans;
                    }
                    if (totalTransactions > 0 && tran.totalNbrOfTrans > 0)
                    {
                        tran.percentageOfTotalTransactions = (double)tran.totalNbrOfTrans / (double)totalTransactions;
                    }
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
            return null;
        }
    }

    public class productStat
    {
        public products product { get; set; }
        public long avgPrice { get; set; }
        public long minPrice { get; set; }
        public long maxPrice { get; set; }
        public long quantitySold { get; set; }
        public int totalNbrOfTrans { get; set; }
        public double percentageOfTotalTransactions { get; set; }
        public long totalCostSold { get; set; }
        public productStat()
        {
            minPrice = 9999;
            avgPrice = 0;
            maxPrice = 0;
            quantitySold = 0;
            totalCostSold = 0;
            percentageOfTotalTransactions = 0;
            totalCostSold = 0;
        }
    }

    public enum products
    {
        Troops,
        Jets,
        Turrets,
        Tanks,
        Bushels,
        Barrels,
        MilitaryTech,
        MedicalTech,
        BusinessTech,
        ResidentialTech,
        AgricultureTech,
        WarfareTech,
        MilitaryStratTech,
        WeaponsTech,
        IndustrialTech,
        SpyTech,
        SDITech
    }

    public class OpStatInfo
    {
        public api_spyops Op { get; set; }
        public SpyOpInfo OpInfo { get; set; }

        public OpStatInfo()
        {
            Op = new api_spyops();
            OpInfo = new SpyOpInfo();
        }

        /*
         * Determine Military Strength Method
         * 
         * Purpose:
         * Returns a long value calculated based on the details in the OpInfo object provided
         * 
         */
        public long DetermineMilitaryStr()
        {
            double govBonus = 1, weapBonus = 1;

            if (OpInfo.Gov == "I")
                govBonus = 1.2;
            else if (OpInfo.Gov == "R")
                govBonus = 0.9;

            weapBonus = OpInfo.CalculateAllTech(OpInfo.WeaponsPts, "t_weap");
            if (weapBonus >= 100)
                weapBonus = weapBonus / 100.00;

            return Convert.ToInt64(govBonus * weapBonus * ((OpInfo.Troops * 0.5) + (OpInfo.Turrets) + (OpInfo.Tanks * 2)));
        }
    }

    public class OpStat
    {
        public OpStatInfo StartOp { get; set; }
        public OpStatInfo EndOp { get; set; }
        public long netChange { get; set; }
        public int landChange { get; set; }
        public double percBuilt { get; set; }
        public long militaryStrChange { get; set; }
        public long totalTech { get; set; }
        public int turnsUsed { get; set; }
        public int turnsLeft { get; set; }
        public OpStat()
        {
            StartOp = new OpStatInfo();
            EndOp = new OpStatInfo();
        }
    }

    public class OpStats : StatStylizingCommands
    {
        public List<OpStat> Stats { get; set; }

        public OpStats()
        {
            Stats = new List<OpStat>();
        }

        /*
         * Gather Ops Method 
         * 
         * Purpose:
         * Obtains ops and op info for the country number provided
         * 
         */
        public void GatherOps(int cnum, int numOps)
        {
            try
            {
                Stats = new List<OpStat>();
                var ops = frmDiscordBot.Storage.SpyOpsStorage.Get(cnum, numOps);
                for (int i = ops.Count() - 1; i > 0; i--)
                {
                    Stats.Add(
                        new OpStat
                        {
                            StartOp = new OpStatInfo { Op = ops[i - 1], OpInfo = new SpyOpInfo(ops[i - 1].json, ops[i - 1].type, ops[i - 1].uploader_api_key) },
                            EndOp = new OpStatInfo { Op = ops[i], OpInfo = new SpyOpInfo(ops[i].json, ops[i].type, ops[i].uploader_api_key) }
                        });
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        /*
         * Determine Op Stats Method 
         * 
         * Purpose:
         * Parses provided OpStats object and determine stats
         * 
         */
        public void GetStatInfo()
        {
            try
            {
                for (int i = 0; i < Stats.Count(); i++)
                {
                    Stats[i].landChange = Stats[i].StartOp.OpInfo.Land - Stats[i].EndOp.OpInfo.Land;
                    Stats[i].militaryStrChange = Stats[i].StartOp.DetermineMilitaryStr() - Stats[i].EndOp.DetermineMilitaryStr();
                    Stats[i].netChange = Stats[i].StartOp.OpInfo.Networth - Stats[i].EndOp.OpInfo.Networth;
                    Stats[i].percBuilt = 1.00 - ((double)Stats[i].EndOp.OpInfo.UnusedLands / (double)Stats[i].EndOp.OpInfo.Land);
                    Stats[i].totalTech = Stats[i].StartOp.OpInfo.TechTotal - Stats[i].EndOp.OpInfo.TechTotal;
                    Stats[i].turnsLeft = (Stats[i].EndOp.OpInfo.turnsLeft + Stats[i].EndOp.OpInfo.turnsStored);
                    Stats[i].turnsUsed = Stats[i].StartOp.OpInfo.turnsTaken - Stats[i].EndOp.OpInfo.turnsTaken;
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        /*
         * Display Op Stats Method 
         * 
         * Purpose:
         * Displays Op Stats
         * 
         */
        public async void DisplayStats(Channel chan)
        {
            try
            {
                string message = "```";
                message += "| Time B2ween Ops | Op Age From Now | Net +/- | Land +/- | % Built | Mil Str +/- | Total Tech | Turns Used | Turns Left |" + Environment.NewLine;
                //          |    Max = 17     |     Max = 17    | Max = 9 | Max = 10 | Max = 9 |  Max = 13   |  Max = 12  |  Max = 12  |  Max = 12  |
                for (int i = 0; i < Stats.Count(); i++)
                {
                    message += "|" + AddSpaces(DetermineSpaces((Math.Ceiling((Stats[i].StartOp.Op.timestamp - Stats[i].EndOp.Op.timestamp).TotalHours) + " hrs").Length, 17), Math.Ceiling((Stats[i].StartOp.Op.timestamp - Stats[i].EndOp.Op.timestamp).TotalHours) + " hrs") + "|"
                        + AddSpaces(DetermineSpaces((Math.Ceiling((DateTime.UtcNow - Stats[i].EndOp.Op.timestamp).TotalHours).ToString() + " hrs ago.").Length, 17), Math.Ceiling((DateTime.UtcNow - Stats[i].EndOp.Op.timestamp).TotalHours).ToString() + " hrs ago.") + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats[i].netChange).Length, 9), ConvertNumberToDisplay(Stats[i].netChange)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats[i].landChange).Length, 10), ConvertNumberToDisplay(Stats[i].landChange)) + "|"
                        + AddSpaces(DetermineSpaces(Stats[i].percBuilt.ToString("P0").Length, 9), Stats[i].percBuilt.ToString("P0")) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats[i].militaryStrChange).Length, 13), ConvertNumberToDisplay(Stats[i].militaryStrChange)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats[i].totalTech).Length, 12), ConvertNumberToDisplay(Stats[i].totalTech)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats[i].turnsUsed).Length, 12), ConvertNumberToDisplay(Stats[i].turnsUsed)) + "|"
                        + AddSpaces(DetermineSpaces(ConvertNumberToDisplay(Stats[i].turnsLeft).Length, 12), ConvertNumberToDisplay(Stats[i].turnsLeft)) + "|"
                        + Environment.NewLine;
                }
                message += "```";
                await chan.SendMessage(message);
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }
    }
}
