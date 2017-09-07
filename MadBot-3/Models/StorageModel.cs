using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MadBot_3.Models
{
    /*
     * RELATIONS STORAGE
     * 
     * Stores Relation objects tracking current relations with other clans
     * 
     */
    public class RelationsStorage
    {
        List<Relation> Relations = new List<Relation>();

        // Basic constructor, also loads the data initially
        public RelationsStorage()
        {
            string path = @"C:\Data\Relations";

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            foreach(var file in Directory.GetFiles(path))
            {
                var temp = new Relation();
                Relations.Add(temp.LoadRelationFromXML(file));
            }
        }

        // Method used to add relations and save them
        public bool Add(Relation relation)
        {
            try
            {
                Relations.Add(relation);
                relation.SaveRelationToXML();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // method used to remove relations and save changes
        public bool Remove(string clanTag)
        {
            try
            {
                Relations.RemoveAll(c => c.clanTag == clanTag);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // method of removing all relations
        public bool RemoveAll()
        {
            try
            {
                foreach(var relation in Relations)
                {
                    relation.Remove();
                }
                Relations.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // method returning relation object that matches the tag provided
        public Relation Get(string clanTag)
        {
            return Relations.SingleOrDefault(c => c.clanTag == clanTag);
        }

        // method returning all current relations
        public List<Relation> Get()
        {
            return Relations;
        }
    }

    /*
     * INSULT STORAGE
     * 
     * Stored built in Insults for peer to peer interaction and fun
     * 
     */
    public class InsultStorage : IDisposable
    {
        List<string> Insults = new List<string>();

        // Basic Constructor
        public InsultStorage()
        {
            // Check for proper storage, or create Generic list
            Initialize();
        }

        //Creates a standard generic list of insults
        public List<string> Generic()
        {
            var Insults = new List<string>();
            Insults.Add("I'm not saying I hate you, but I would unplug your life support to charge my phone.");
            Insults.Add("Is your ass jealous of the amount of shit that just came out of your mouth?");
            Insults.Add("You're so ugly, when your mom dropped you off at school she got a fine for littering.");
            Insults.Add("Roses are red, violets are blue, I have 5 fingers, the 3rd ones for you.");
            Insults.Add("Your birth certificate is an apology letter from the condom factory.");
            Insults.Add("You're the reason they invented double doors!");
            Insults.Add("I wasn't born with enough middle fingers to let you know how I feel about you.");
            Insults.Add("I bet your brain feels as good as new, seeing that you never use it.");
            Insults.Add("You bring everyone a lot of joy, when you leave the room.");
            Insults.Add("You must have been born on a highway because that's where most accidents happen.");
            Insults.Add("What's the difference between you and eggs? Eggs get laid and you don't.");
            Insults.Add("If laughter is the best medicine, your face must be curing the world.");
            Insults.Add("I'm jealous of all the people that haven't met you!");
            Insults.Add("I'd like to see things from your point of view but I can't seem to get my head that far up my ass.");
            Insults.Add("I could eat a bowl of alphabet soup and shit out a smarter statement than that.");
            Insults.Add("You're so ugly, when you popped out the doctor said 'Aww what a treasure' and your mom said 'Yeah, lets bury it.'");
            Insults.Add("Two wrongs don't make a right, take your parents as an example.");
            Insults.Add("There's only one problem with your face, I can see it.");
            Insults.Add("If I wanted to kill myself I'd climb your ego and jump to your IQ.");
            Insults.Add("You're so ugly you scare the shit back into people.");
            Insults.Add("You shouldn't play hide and seek, no one would look for you.");
            Insults.Add("Your family tree must be a cactus because everybody on it is a prick.");
            Insults.Add("If you really want to know about mistakes, you should ask your parents.");
            Insults.Add("At least when I do a handstand my stomach doesn't hit me in the face.");
            Insults.Add("It's better to let someone think you are an Idiot than to open your mouth and prove it.");
            Insults.Add("Somewhere out there is a tree, tirelessly producing oxygen so you can breathe. I think you owe it an apology.");
            Insults.Add("If you're gonna be a smartass, first you have to be smart. Otherwise you're just an ass.");
            Insults.Add("I don't exactly hate you, but if you were on fire and I had water, I'd drink it.");
            Insults.Add("Hey, you have somthing on your chin... no, the 3rd one down");
            Insults.Add("You're so stupid that you had to call 411 to get the number for 911.");
            Insults.Add("My middle finger gets a boner every time I see you.");
            Insults.Add("I have neither the time nor the crayons to explain this to you.");
            Insults.Add("When God put teeth in your mouth he ruined a perfectly good asshole.");
            Insults.Add("I'd slap you, but shit stains.");
            Insults.Add("You're dumber than snake mittens.");
            Insults.Add("Let me guess... you're the first person in your family without a tail?");
            Insults.Add("The smartest thing that ever came out of your mouth is a penis.");
            Insults.Add("Keep rolling your eyes... maybe you'll find a brain back there.");
            Insults.Add("Your family's gene pool could use a little chlorine.");
            Insults.Add("I could eat a bowl of alphabet soup and shit a better line than that.");
            Insults.Add("If your brain exploded it wouldn't even mess up your hair.");
            Insults.Add("I don't know what your problem is, but i bet it's hard to pronounce.");
            Insults.Add("I was going to give you a nasty look, but I see you already have one.");
            Insults.Add("I hope that one day soon you choke on all that shit you talk.");
            Insults.Add("I wasn't insulting you, I was describing you.");
            Insults.Add("I'm no cactus expert, but I know a prick when I see one.");
            Insults.Add("I'd call you a pussy, but you don't have the depth or the warmth to live up to it.");
            Insults.Add("I'm not saying I hate you, but I'd unplug your life support to charge my phone.");
            Insults.Add("I'm not saying you're fat, but it looks like you were poured into your clothes and forgot to say 'when'.");
            Insults.Add("You look like you just ran a hundred yard dash in a ninety yard gym.");
            Insults.Add("I hope the rest of your day is as pleasant as you are.");
            Insults.Add("It looks like your face was on fire and someone put it out with a wet brick.");
            Insults.Add("You're the personification of comic sans.");
            Insults.Add("It must be difficult for you, exhausting your entire vocabulary in one sentence.");
            Insults.Add("Letting you live was medical malpractice.");
            Insults.Add("A douche of your magnitude could cleanse the vagina of a whale.");
            Insults.Add("Your IQ doesn't make a respectable earthquake.");
            Insults.Add("You are a walking advertisement for the benefits of birth control.");
            Insults.Add("You're so ugly your birth certificate is an apology letter from Durex.");
            Insults.Add("If you were any more inbred, you'd be a sandwich.");
            Insults.Add("If my dogs face was as ugly as yours, i'd shave his ass and teach him to walk backwards.");
            Insults.Add("Save your breath, you'll need to to blow up your girlfriend later.");

            return Insults;
        }

        //handles saving insults to xml
        public void SaveInsultToXML(List<string> insults)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<string>));
            string path = @"C:\Data\InsultStorage";

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            using (TextWriter tw = new StreamWriter(path + @"\" + Guid.NewGuid() + ".xml"))
            {
                xs.Serialize(tw, insults);
                tw.Close();
            }
        }

        //Initializes Data, ensures all the 'ducks' are in a row
        public void Initialize()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<string>));
            if (Directory.Exists(@"C:\Data\InsultStorage"))
            {
                foreach (var file in Directory.GetFiles(@"C:\Data\InsultStorage"))
                {
                    using (var sr = new StreamReader(file))
                    {
                        Insults.AddRange((List<string>)xs.Deserialize(sr));
                        sr.Close();
                    }
                }
            }
            else
            {
                SaveInsultToXML(Generic());
            }
        }

        /*
         * Insult Method
         * 
         * Purpose:
         * Resturns a random insult from the insult list
         * 
         */
        public string Insult()
        {
            XmlSerializer xs = new XmlSerializer(typeof(api_spyops));
            try
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                return Insults[rnd.Next(Insults.Count)];
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
            return "";
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
                Insults.Clear();
            }
            disposed = true;
        }
    }

    /*
     * Spy Op Storage
     * 
     * Handles storage, load & saving of ops, Background workers to check for new ops and process queue of ops added
     * 
     */ 
    public class SpyOpsStorage
    {
        private List<api_spyops> SpyOpStorage = new List<api_spyops>();
        private static List<SpyOpInfo> OpQueue = new List<SpyOpInfo>();
        private int currentReset = 1392;
        private BackgroundWorker NewOpDetectionService;
        private BackgroundWorker ProcessOpQueueService;
        private System.Timers.Timer NewOpDetectionServiceTimer;
        private System.Timers.Timer ProcessOpQueueServiceTimer;

        //loads saved data
        private void Initialize()
        {
            XmlSerializer xs = new XmlSerializer(typeof(api_spyops));
            string path = @"C:\Data\ResetSpecifcData\SpyOps";
            if (Directory.Exists(path))
            {
                string serverPath = path + @"\12";
                if (Directory.Exists(serverPath))
                {
                    var dirs = Directory.GetDirectories(serverPath).OrderByDescending(c => c).ToList();
                    if (dirs.Count() > 0)
                    {
                        //C:\Data\ResetSpecifcData\SpyOps\12\1353
                        var x = dirs[0].Split('\\');
                        currentReset = Int32.Parse(dirs[0].Split('\\')[dirs[0].Split('\\').Count() - 1]);
                        foreach (var countryDir in Directory.GetDirectories(dirs[0]))
                        {
                            if (Directory.GetFiles(countryDir).Count() > 0)
                            {
                                var file = Directory.GetFiles(countryDir).OrderByDescending(c => c).FirstOrDefault();

                                using (var sr = new StreamReader(file))
                                {
                                    SpyOpStorage.Add((api_spyops)xs.Deserialize(sr));
                                }
                            }
                        }
                    }
                }
            }
        }

        // basic constructor, also starts background processes
        public SpyOpsStorage()
        {
            Initialize();
            using (NewOpDetectionService = new BackgroundWorker())
            {
                NewOpDetectionService.DoWork += CheckForNewOps;
                TimeSpan x = new TimeSpan(0, 0, 0, 0, 500);
                NewOpDetectionServiceTimer = new System.Timers.Timer(x.TotalMilliseconds);
                NewOpDetectionServiceTimer.Elapsed += CheckCheckForNewOpsProcess;
                NewOpDetectionServiceTimer.Start();
                x = new TimeSpan(0, 0, 1);
                ProcessOpQueueServiceTimer = new System.Timers.Timer(x.TotalMilliseconds);
                ProcessOpQueueServiceTimer.Elapsed += CheckQueueServiceProcess;
                ProcessOpQueueServiceTimer.Start();
                using (ProcessOpQueueService = new BackgroundWorker())
                {
                    ProcessOpQueueService.DoWork += ProcessQueue;
                }
            }
        }

        // save single op to xml
        private void SaveSpyOpsToXML(api_spyops op)
        {
            string basePath = @"C:\Data\ResetSpecifcData\SpyOps";
            int tempCurrentReset = new SpyOpInfo(op.json, op.type, op.uploader_api_key).resetId;

            if (Directory.Exists(basePath) == false)
                Directory.CreateDirectory(basePath);

            string serverPath = basePath + @"\" + op.serverid;

            if (Directory.Exists(serverPath) == false)
                Directory.CreateDirectory(serverPath);

            string resetPath = serverPath + @"\" + tempCurrentReset;

            if (Directory.Exists(resetPath) == false)
                Directory.CreateDirectory(resetPath);

            XmlSerializer xs = new XmlSerializer(typeof(api_spyops));
            
            string countryPath = resetPath + @"\" + op.subject_number;

            if (Directory.Exists(countryPath) == false)
                Directory.CreateDirectory(countryPath);

            using (TextWriter tw = new StreamWriter(countryPath + @"\" + StorageModel.ConvertToUnixTime(op.timestamp) + ".xml"))
            {
                xs.Serialize(tw, op);
                tw.Close();
            }            
        }

        // checks Background process to see if is is running, if not executes CheckForNewOps
        private void CheckCheckForNewOpsProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!NewOpDetectionService.IsBusy)
                NewOpDetectionService.RunWorkerAsync();
        }

        // cheks Background process to see if it is running, if not executes ProcessQueue
        private void CheckQueueServiceProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!ProcessOpQueueService.IsBusy)
                ProcessOpQueueService.RunWorkerAsync();
        }

        // Checks for New Ops, if Found processes them saving them to xml and adding to queue if necessary
        private void CheckForNewOps(object sender, DoWorkEventArgs e)
        {
            try
            {
                string[] files = Directory.GetFiles(@"C:\Data\newSpyOps");
                foreach (var file in files)
                {
                    XmlSerializer xs = new XmlSerializer(typeof(api_spyops));
                    using (var sr = new StreamReader(file))
                    {
                        var op = (api_spyops)xs.Deserialize(sr);
                        if (SpyOpStorage.Where(c => c.subject_number == op.subject_number).Count() > 0)
                        {
                            var x = SpyOpStorage.Where(c => c.subject_number == op.subject_number).OrderByDescending(c => c.timestamp).FirstOrDefault().timestamp;
                            if (SpyOpStorage.Where(c => c.subject_number == op.subject_number).OrderByDescending(c => c.timestamp).FirstOrDefault().timestamp < op.timestamp)
                            {                                
                                SpyOpStorage.RemoveAll(c => c.subject_number == op.subject_number);
                                SpyOpStorage.Add(op);
                                OpQueue.Add(new SpyOpInfo(op.json, op.type, op.uploader_api_key));                                
                                SaveSpyOpsToXML(op);
                            }
                        }
                        else
                        {
                            if (op.serverid == 12)
                            {
                                SpyOpStorage.Add(op);
                                OpQueue.Add(new SpyOpInfo(op.json, op.type, op.uploader_api_key));
                            }
                            SaveSpyOpsToXML(op);
                        }
                        sr.Close();
                    }
                    File.Delete(file);
                }
            }
            catch(Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }        

        // Determines if there are Ops in the Queue and sends them to the channel
        private void ProcessQueue(object sender, DoWorkEventArgs e)
        {
            if (OpQueue.Count() >= 1)
            {
                List<SpyOpInfo> tempQueue = new List<SpyOpInfo>();
                tempQueue = OpQueue;
                foreach (var op in tempQueue)
                {
                    // Send to Bot
                    frmDiscordBot.Bot.channels.SendSpyOpToChannel(op, null);
                }
                OpQueue.Clear();
            }
        }

        // Grab Op from Storage and return it
        public api_spyops Get(int num)
        {
            return SpyOpStorage.SingleOrDefault(c => c.subject_number == num);
        }

        // Grab multiple ops from File Storage and return them
        public List<api_spyops> Get(int subject_num, int numOps)
        {
            XmlSerializer xs = new XmlSerializer(typeof(api_spyops));
            List<api_spyops> Ops = new List<api_spyops>();
            string directory = @"C:\Data\ResetSpecifcData\SpyOps\12\" + currentReset+ @"\" + subject_num;
            var files = Directory.GetFiles(directory).OrderByDescending(c => c).ToArray();
            for (int i =0; i <= numOps; numOps++)
            {
                if (files.Count() > i)
                {
                    var file = files[i];
                    using (var sr = new StreamReader(file))
                    {
                        Ops.Add((api_spyops)xs.Deserialize(sr));
                    }
                }
            }
            return Ops;
        }

        // Stops all background process
        public void Stop()
        {
            NewOpDetectionServiceTimer.Stop();
            NewOpDetectionServiceTimer.Dispose();
            ProcessOpQueueServiceTimer.Stop();
            ProcessOpQueueServiceTimer.Dispose();
            NewOpDetectionService.Dispose();
            ProcessOpQueueService.Dispose();
        }
    }

    /*
     * User Storage
     * 
     * Handles storage, loading & saving of users, and methods to support bot commands
     * 
     */ 
    public class UserStorage
    {
        private List<User> UsersStorage = new List<User>();

        //Basic Constructor
        public UserStorage()
        {
            Initialize();
        }

        //Loads data on Construction
        private void Initialize()
        {
            string basePath = @"C:\Data\Users\Users.xml";
            if (Directory.Exists(@"C:\Data\Users"))
            {
                if (File.Exists(basePath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<User>));
                    using (var sr = new StreamReader(basePath))
                    {
                        UsersStorage = (List<User>)xs.Deserialize(sr);
                    }
                }
            }
        }

        //saves data to xml
        private void SaveUserDataToXML()
        {
            string basePath = @"C:\Data\Users\";

            if (Directory.Exists(basePath) == false)
                Directory.CreateDirectory(basePath);

            XmlSerializer xs = new XmlSerializer(typeof(List<User>));
            if (File.Exists(basePath + "Users.xml"))
                File.Delete(basePath + "Users.xml");
            using (TextWriter tw = new StreamWriter(basePath + "Users.xml"))
            {
                xs.Serialize(tw, UsersStorage);
                tw.Close();
            }
        }

        // add user method for use with bot commands
        public string AddUser(string username, ulong discordId)
        {
            try
            {                
                return AddUser(username, discordId, null);
            }
            catch
            {
                return "There was a problem adding the user provided.";
            }
        }

        // add user method for use with bot commands
        public string AddUser(string username, ulong discordId, string APIKey)
        {
            try
            {
                var user = new User();
                user.DiscordUserId = discordId;
                if (username.Contains(' '))
                {
                    string tempUsername = "";
                    foreach (var CHAR in username)
                    {
                        if (CHAR != ' ')
                            tempUsername += CHAR;
                    }
                    username = tempUsername;
                }
                user.Username = username;
                user.APIKey = APIKey;
                UsersStorage.Add(user);
                SaveUserDataToXML();
                return user.Username + ", has been added successfully.";
            }
            catch
            {
                return "There was a problem adding the user provided.";
            }
        }

        // remove user method for use with bot commands
        public string RemoveUser(ulong discordId)
        {
            try
            {
                var user = UsersStorage.SingleOrDefault(c => c.DiscordUserId == discordId);
                UsersStorage.Remove(user);
                return user.Username + ", was removed successfully";
            }
            catch
            {
                return "There was a problem removing the user.";
            }
        }

        // update apikey method for use with bot commands
        public string UpdateAPIKey(ulong discordId, string apikey)
        {
            try
            {
                if (UsersStorage.SingleOrDefault(c => c.DiscordUserId == discordId) != null)
                {
                    var user = UsersStorage.SingleOrDefault(c => c.DiscordUserId == discordId);
                    user.APIKey = apikey;
                    SaveUserDataToXML();
                    return user.Username + "'s APIKey was updated successfully.";
                }
                else
                    return "There was not a user found by that id, please check the Discord UserID you are using.";
            }
            catch
            {
                return "There was a problem updating the user.";
            }
        }

        // returns the user object for the id provided
        public User Get(ulong id)
        {
            return UsersStorage.SingleOrDefault(c => c.DiscordUserId == id);
        }

        // returns the user object for the apiKey provided
        public string Get(string apiKey)
        {
            if (apiKey == null || apiKey == "")
                return "Unknown";
            var tempUser = UsersStorage.FirstOrDefault(c => c.APIKey == apiKey);
            if (tempUser != null)
                return tempUser.Username;
            else
                return "Unknown";
        }

        // method that supports a bot command for updating 
        public string UpdateUsersName(ulong discordId, string newUsername)
        {
            string message;

            if (UsersStorage.SingleOrDefault(C => C.DiscordUserId == discordId) != null)
            {
                UsersStorage.SingleOrDefault(C => C.DiscordUserId == discordId).Username = newUsername;
                message = "Username successfully changed, user will now be known as " + newUsername;
            }
            else
                message = "A user was not found by the id provided, please verify the user has already been added or that you are using the right ID.";

            return message;
        }
    }

    /*
     * Country Storage
     * 
     * Handles storage, loading & saving of countries, background process to update storage every 5 minutes
     *      methods for determining if a country is online, or changed tags, and methods for bot commands
     */ 
    public class CountryStorage
    {
        private List<api_currentranks> Countries = new List<api_currentranks>();
        private List<api_currentranks> CountriesDayOld = new List<api_currentranks>();
        private List<api_currentranks> CountriesTwoDaysOld = new List<api_currentranks>();
        private List<api_currentranks> CountriesThreeDaysOld = new List<api_currentranks>();
        private List<countryLogin> LoginData = new List<countryLogin>();
        private List<TagChange> TagChanges = new List<TagChange>();

        private int currentReset = 1392;
        private BackgroundWorker CountriesUpdateService;
        private System.Timers.Timer CountriesUpdateServiceTimer;

        //Basic Constructor, also starts background process
        public CountryStorage()
        {
            try
            {
                Initialize();
                using (CountriesUpdateService = new BackgroundWorker())
                {
                    CountriesUpdateService.DoWork += ReceiveCountryData;
                    TimeSpan x = new TimeSpan(0, 5, 0);
                    CountriesUpdateServiceTimer = new System.Timers.Timer(x.TotalMilliseconds);
                    CountriesUpdateServiceTimer.Elapsed += CheckUpdateCountryProcess;
                    CountriesUpdateServiceTimer.Start();
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        //save country data to xml
        public void SaveCountryDataToXML()
        {
            try
            {
                if (Countries.Count() > 0)
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<api_currentranks>));
                    List<api_currentranks> NewSet = new List<api_currentranks>();
                    currentReset = Countries.OrderBy(c => c.resetId).FirstOrDefault().resetId;
                    if (Countries.All(c => c.resetId == currentReset) == false)
                    {
                        NewSet = Countries.Where(c => c.resetId != currentReset).ToList();
                        Countries.RemoveAll(c => c.resetId != currentReset);
                    }
                    string basePath = @"C:\Data\ResetSpecifcData\" + currentReset;

                    if (Directory.Exists(basePath) == false)
                        Directory.CreateDirectory(basePath);

                    string path = basePath + @"\Countries\Current";

                    if (Directory.Exists(path) == false)
                        Directory.CreateDirectory(path);

                    if (Directory.Exists(basePath + @"\Countries\1DayAgo\") == false)
                        Directory.CreateDirectory(basePath + @"\Countries\1DayAgo\");
                    if (Directory.Exists(basePath + @"\Countries\2DaysAgo\") == false)
                        Directory.CreateDirectory(basePath + @"\Countries\2DaysAgo\");
                    if (Directory.Exists(basePath + @"\Countries\3DaysAgo\") == false)
                        Directory.CreateDirectory(basePath + @"\Countries\3DaysAgo\");

                    var tempFiles = Directory.GetFiles(path).OrderBy(c => c).ToArray();
                    if (tempFiles.Count() > 0)
                    {
                        var previousUpdateFilePath = tempFiles[0];
                        if (double.Parse(previousUpdateFilePath.Split('\\')[6].Split('.')[0]) <= StorageModel.ConvertToUnixTime(DateTime.UtcNow.AddDays(-1)))
                        {
                            tempFiles = Directory.GetFiles(basePath + @"\Countries\1DayAgo\");
                            if (tempFiles.Count() > 0)
                            {
                                var dayOldFile = tempFiles[0];
                                tempFiles = Directory.GetFiles(basePath + @"\Countries\2DaysAgo\");
                                if (tempFiles.Count() > 0)
                                {
                                    var twoDayOldFile = tempFiles[0];
                                    tempFiles = Directory.GetFiles(basePath + @"\Countries\3DaysAgo\");
                                    if (tempFiles.Count() > 0)
                                    {
                                        var threeDayOldFile = tempFiles[0];
                                        File.Delete(threeDayOldFile);
                                    }
                                    File.Move(twoDayOldFile, basePath + @"\Countries\3DaysAgo\" + twoDayOldFile.Split('\\')[6]);
                                }
                                File.Move(dayOldFile, basePath + @"\Countries\2DaysAgo\" + dayOldFile.Split('\\')[6]);
                            }
                            File.Move(previousUpdateFilePath, basePath + @"\Countries\1DayAgo\" + previousUpdateFilePath.Split('\\')[6]);
                        }
                        if(tempFiles.Count() == 2)
                        {
                            File.Delete(tempFiles[1]);
                        }
                    }
                    using (TextWriter tw = new StreamWriter(path + @"\" + StorageModel.ConvertToUnixTime(DateTime.UtcNow) + ".xml"))
                    {
                        xs.Serialize(tw, Countries);
                        tw.Close();
                    }
                    

                    if (NewSet.Count() > 0)
                    {
                        currentReset = NewSet.FirstOrDefault().resetId;
                        basePath = @"C:\Data\ResetSpecifcData\" + currentReset;

                        if (Directory.Exists(basePath) == false)
                            Directory.CreateDirectory(basePath);

                        if (Directory.Exists(basePath + @"Countries\1DayAgo\") == false)
                            Directory.CreateDirectory(basePath + @"Countries\1DayAgo\");
                        if (Directory.Exists(basePath + @"Countries\2DaysAgo\") == false)
                            Directory.CreateDirectory(basePath + @"Countries\2DaysAgo\");
                        if (Directory.Exists(basePath + @"Countries\3DaysAgo\") == false)
                            Directory.CreateDirectory(basePath + @"Countries\3DaysAgo\");

                        path = basePath + @"\Countries\Current";

                        if (Directory.Exists(path) == false)
                            Directory.CreateDirectory(path);

                        using (TextWriter tw = new StreamWriter(path + @"\" + StorageModel.ConvertToUnixTime(DateTime.UtcNow) + ".xml"))
                        {
                            xs.Serialize(tw, NewSet);
                            tw.Close();
                        }
                        NewSet.Clear();
                    }
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        //load data on start
        private void Initialize()
        {
            string basePath = @"C:\Data\ResetSpecifcData";
            if (Directory.GetDirectories(basePath).Count() > 0)
            {
                currentReset = Int32.Parse(Directory.GetDirectories(basePath).OrderByDescending(c => c).ToList()[1].Split('\\')[Directory.GetDirectories(basePath).OrderByDescending(c => c).ToList()[1].Split('\\').Count() - 1]);
                if(Directory.Exists(basePath + @"\" + currentReset + @"\Countries"))
                {
                    foreach (var directory in Directory.GetDirectories(basePath + @"\" + currentReset + @"\Countries"))
                    {
                        if (Directory.GetFiles(directory).Count() > 0)
                        {
                            var tempFile = Directory.GetFiles(directory).ToList().OrderByDescending(c => c).ToArray();
                            if (tempFile.Count() > 0)
                            {
                                switch (directory.Split('\\')[5])
                                {
                                    case "Current":
                                        Countries = LoadDataFromXML(tempFile[0]);
                                        break;
                                    case "1DayAgo":
                                        CountriesDayOld = LoadDataFromXML(tempFile[0]);
                                        break;
                                    case "2DaysAgo":
                                        CountriesTwoDaysOld = LoadDataFromXML(tempFile[0]);
                                        break;
                                    case "3DaysAgo":
                                        CountriesThreeDaysOld = LoadDataFromXML(tempFile[0]);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        //Load file into storage
        private List<api_currentranks> LoadDataFromXML(string file)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<api_currentranks>));
                using (var sr = new StreamReader(file))
                {
                    return (List<api_currentranks>)xs.Deserialize(sr);
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
                return new List<api_currentranks>();
            }
        }

        //checks update process to determine if it is running, if not runs it
        private void CheckUpdateCountryProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!CountriesUpdateService.IsBusy)
                CountriesUpdateService.RunWorkerAsync();
        }

        //makes a call to EE server for csv data, parses it and stores it
        private void ReceiveCountryData(object sender, DoWorkEventArgs e)
        {
            List<api_currentranks> NewRanks = new List<api_currentranks>();
            string url = "http://www.earthempires.com/ranks_feed?apicode=838a8f8296991745d36d3b32f5845b6e&serverid=12";
            //GET CSV, PARSE and Build Model
            HttpClientHandler handler = new HttpClientHandler() // Use GZip compression
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            string download = "";
            using (var client = new HttpClient(handler)) // Contact EE's API and store response in a string variable
            {
                HttpResponseMessage response;
                client.DefaultRequestHeaders.Add("Encoding", "gzip");
                response = client.GetAsync(url).Result;
                download = response.Content.ReadAsStringAsync().Result;
            }
            // Check to ensure a status message was not receievd so that the csv string can be seperated into proper objects
            if (download != "You already received this update" && download != "You are being throttled for not using gzip to 1 out of 4 updates.")
            {
                NewRanks = (from line in download.Split(Convert.ToChar(10))
                             let columns = line.Split(',')
                             where columns.Count().Equals(14)
                             select new api_currentranks(columns)).ToList();
                SaveCountryDataToXML();
                CheckForOnlineCountries(NewRanks);
            }
        }

        /*
         * Online Country Detection Service
         * 
         * Purpose:
         * Passed data by Ranks Update Service to determine if a country logged in or is online currently
         * 
         */
        public void CheckForOnlineCountries(List<api_currentranks> NewRanks)
        {
            var tagChanges = new List<TagChange>();
            foreach (var cty in NewRanks)
            {
                // Check DB to ensure country exists
                var temp = Countries.Where(c => c.Number == cty.Number).FirstOrDefault();
                if (temp != null)
                {
                    Countries.Remove(temp);
                    //check for net change && same reset
                    if (((cty.Networth / temp.Networth) > 1.01) && temp.resetId == cty.resetId)
                    {
                        // Look for Login Record, if it doesn't exist create it
                        if (LoginData.Where(c => c.countryNum == temp.Number).Count() > 0)
                        {
                            var login = LoginData.SingleOrDefault(c => c.countryNum == temp.Number);
                            // Add new detection to login data
                            var detected = new detected();
                            detected.DetectedBy = DetectionMethod.Networth;
                            detected.timeDetected = DateTime.UtcNow;
                            detected.loginId = login.loginId;
                            login.DetectedBy.Add(detected);
                            login.tag = temp.Tag;
                            //If warring tag detected display online country detection method in spam channel

                            /*if (frmMainMenu.StatBot.AtWar)
                            {
                                frmMainMenu.StatBot.Storage.Logins.SendOnlineCountryDetails(login, detected);
                            }*/

                        }
                        else
                        {
                            // Create new login and add detection to it
                            var login = new countryLogin();
                            login.countryNum = temp.Number;
                            login.countryName = temp.Name;
                            var detected = new detected();
                            detected.DetectedBy = DetectionMethod.Networth;
                            detected.timeDetected = DateTime.UtcNow;
                            detected.loginId = login.loginId;
                            login.DetectedBy = new List<detected>();
                            login.DetectedBy.Add(detected);
                            login.tag = temp.Tag;
                            LoginData.Add(login);
                            //If warring tag detected display online country detection method in spam channel
                            /*if (frmMainMenu.StatBot.AtWar)
                            {
                                frmMainMenu.StatBot.Storage.Logins.SendOnlineCountryDetails(login, detected);
                            }*/
                        }
                    }
                    // check for land changes
                    if (cty.Land == (temp.Land + 20) && temp.resetId == cty.resetId)
                    {
                        // If Login record is detected add detection to it, else create new login and detection record
                        if (LoginData.Where(c => c.countryNum == temp.Number).Count() > 0)
                        {
                            var login = LoginData.SingleOrDefault(c => c.countryNum == temp.Number);
                            var detected = new detected();
                            detected.DetectedBy = DetectionMethod.Land;
                            detected.timeDetected = DateTime.UtcNow;
                            detected.loginId = login.loginId;
                            login.DetectedBy.Add(detected);
                            login.tag = temp.Tag;
                            //If warring tag detected display online country detection method in spam channel
                            /*if (frmMainMenu.StatBot.AtWar)
                            {
                                frmMainMenu.StatBot.Storage.Logins.SendOnlineCountryDetails(login, detected);
                            }*/
                        }
                        else
                        {
                            var login = new countryLogin();
                            login.countryNum = temp.Number;
                            var detected = new detected();
                            detected.DetectedBy = DetectionMethod.Land;
                            detected.timeDetected = DateTime.UtcNow;
                            detected.loginId = login.loginId;
                            login.DetectedBy = new List<detected>();
                            login.DetectedBy.Add(detected);
                            login.tag = temp.Tag;
                            LoginData.Add(login);
                            //If warring tag detected display online country detection method in spam channel
                            /*if (frmMainMenu.StatBot.AtWar)
                            {
                                frmMainMenu.StatBot.Storage.Logins.SendOnlineCountryDetails(login, detected);
                            }*/
                        }
                    }
                    else
                    {
                        var tempLogin = LoginData.Where(c => c.countryNum == cty.Number).FirstOrDefault();
                        if (tempLogin != null && tempLogin.DetectedBy.Count() > 0)
                        {
                            // if land changed and last detection was over 4 hours ago
                            if (tempLogin.DetectedBy.OrderByDescending(c => c.timeDetected).FirstOrDefault().timeDetected > DateTime.UtcNow.AddHours(-4) && cty.Land > (temp.Land + 20) && cty.resetId == temp.resetId)
                            {
                                var detected = new detected();
                                detected.DetectedBy = DetectionMethod.Land;
                                detected.timeDetected = DateTime.UtcNow;
                                detected.loginId = tempLogin.loginId;
                                tempLogin.DetectedBy.Add(detected);
                                tempLogin.tag = temp.Tag;
                                //If warring tag detected display online country detection method in spam channel
                                /*if (frmMainMenu.StatBot.AtWar)
                                {
                                    frmMainMenu.StatBot.Storage.Logins.SendOnlineCountryDetails(tempLogin, detected);
                                }*/
                            }
                        }
                    }

                    //check for tag change && same reset
                    if (temp.Tag != cty.Tag && temp.resetId == cty.resetId)
                    {
                        tagChanges.Add(new TagChange { FromTag = temp.Tag, ToTag = cty.Tag, Number = cty.Number, Name = cty.Name, resetId = temp.resetId });
                    }
                    //update model for user, killlist
                    cty.User = temp.User;
                    cty.Timestamp = temp.Timestamp;
                    cty.KillList = temp.KillList;
                    if (temp != null)
                        Countries.Add(cty);
                }
                else
                {
                    cty.Timestamp = DateTime.UtcNow;
                    Countries.Add(cty);
                }
            }
            if (tagChanges.Count() > 0)
            {
                TagChanges.AddRange(tagChanges);
            }
            SaveLoginAndTagChangeDataToXML();
        }

        // handles saving login and tag change data found when updating ranks
        public void SaveLoginAndTagChangeDataToXML()
        {
            string basePath = @"C:\Data\ResetSpecifcData\" + currentReset;

            if (Directory.Exists(basePath) == false)
                Directory.CreateDirectory(basePath);

            string path;// = basePath + @"\Countries\Login";

            
            if (LoginData.Count() > 0)
            {
                path = basePath + @"\Countries\Logins";

                if (Directory.Exists(path) == false)
                    Directory.CreateDirectory(path);

                XmlSerializer xs = new XmlSerializer(typeof(List<countryLogin>));
                using (TextWriter tw = new StreamWriter(path + @"\" + Guid.NewGuid() + ".xml"))
                {
                    xs.Serialize(tw, LoginData);
                    tw.Close();
                }
            }
            if(TagChanges.Count() > 0)
            {
                path = basePath + @"\Countries\TagChanges";

                if (Directory.Exists(path) == false)
                    Directory.CreateDirectory(path);

                XmlSerializer xs = new XmlSerializer(typeof(List<TagChange>));
                using (TextWriter tw = new StreamWriter(path + @"\" + Guid.NewGuid() + ".xml"))
                {
                    xs.Serialize(tw, TagChanges);
                    tw.Close();
                }
            }
        }

        // method returns a countries status based on the boolean variables received from EE
        public static CountryStatus DetermineCountryStatus(bool Prot, bool Vac, bool Alive, bool Deleted)
        {
            if (Prot == true && Deleted == false)
                return CountryStatus.Protection;
            else if (Vac == true && Deleted == false)
                return CountryStatus.Vacation;
            else if (Alive == true && Deleted == false)
                return CountryStatus.Alive;
            else if (Alive == false && Deleted == false)
                return CountryStatus.Dead;
            else
                return CountryStatus.Deleted;
        }

        // method support bot command !own
        public void ClaimCountry(int[] nbrs, string user)
        {
            foreach(var nbr in nbrs)
            {
                if(nbr > 0)
                    Countries.SingleOrDefault(c => c.Number == nbr).User = user;
            }
            SaveCountryDataToXML();
        }

        // method support bot command !disown
        public bool UnClaimCountry(int[] nbrs)
        {
            try
            {
                foreach (var nbr in nbrs)
                {
                    if(nbr > 0)
                        Countries.SingleOrDefault(C => C.Number == nbr).User = "";
                }
                SaveCountryDataToXML();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // method support bot command !whois
        public string WhoIs(int nbr)
        {
            try
            {
                return Countries.SingleOrDefault(c => c.Number == nbr).User;
            }
            catch
            {
                return null;
            }
        }

        // method support bot command !lookup
        public string[] LookupUser(string username)
        {
            var model = new string[2];
            try
            {
                foreach (var country in Countries.Where(c => c.User == username).OrderBy(c => c.Number).ToList())
                {
                    if (country.Status == CountryStatus.Dead)
                        model[1] += country.Number + ", ";
                    else
                        model[0] += country.Number + ", ";
                }
                if (model[0] != null)
                {
                    model[0] = model[0].TrimEnd(' ');
                    model[0] = model[0].TrimEnd(',');
                }
                if (model[1] != null)
                {
                    model[1] = model[1].TrimEnd(' ');
                    model[1] = model[1].TrimEnd(',');
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
            return model;
        }

        // returns claimed countries from storage
        public List<api_currentranks> ClaimedCountries()
        {
            return Countries.Where(c => c.User != null && c.User != "" && c.Tag == "LoC").ToList();
        }   

        // returns country data, for stats
        public List<api_currentranks>[] CountryData()
        {
            List<api_currentranks>[] CountryData = new List<api_currentranks>[3];
            CountryData[0] = new List<api_currentranks>();
            CountryData[0] = CountriesDayOld;
            CountryData[1] = new List<api_currentranks>();
            CountryData[1] = CountriesTwoDaysOld;
            CountryData[2] = new List<api_currentranks>();
            CountryData[2] = CountriesThreeDaysOld;
            return CountryData;
        }     

        // return a single countries data from storage
        public api_currentranks Get(int nbr)
        {
            return Countries.SingleOrDefault(c => c.Number == nbr);
        }

        public List<api_currentranks> Get()
        {
            return Countries;
        }

        // returns tag change data from storage
        public List<TagChange> GetTagChangeData()
        {
            List<TagChange> TagChangeData = new List<TagChange>();
            string basePath = @"C:\Data\ResetSpecifcData\" + currentReset;
            string path = basePath + @"\Countries\TagChanges";
            XmlSerializer xs = new XmlSerializer(typeof(List<TagChange>));
            foreach (var file in Directory.GetFiles(path))
            {                
                using (var sr = new StreamReader(file))
                {
                    TagChangeData.AddRange((List<TagChange>)xs.Deserialize(sr));
                }
            }
            return TagChangeData;
        }

        // returns login data for a single country
        public List<countryLogin> GetLoginData(int nbr)
        {
            List<countryLogin> Logins = new List<countryLogin>();
            
            string path = @"C:\Data\ResetSpecifcData\"+currentReset+@"\Countries\Logins";

            XmlSerializer xs = new XmlSerializer(typeof(List<countryLogin>));

            foreach (var file in Directory.GetFiles(path))
            {
                List<countryLogin> temp = new List<countryLogin>();
                using (var sr = new StreamReader(file))
                {
                    temp = ((List<countryLogin>)xs.Deserialize(sr)).Where(c => c.countryNum == nbr).ToList();
                }
                foreach(var login in temp)
                {
                    if(Logins.SingleOrDefault(c => c.countryNum == login.countryNum) != null)
                    {
                        Logins.SingleOrDefault(c => c.countryNum == login.countryNum).DetectedBy.AddRange(login.DetectedBy);
                    }
                    else
                    {
                        Logins.Add(login);
                    }
                }
            }

            return Logins;
        }

        //returns login data for a tag
        public List<countryLogin> GetLoginData(string tag)
        {
            List<countryLogin> Logins = new List<countryLogin>();

            string path = @"C:\Data\ResetSpecifcData\" + currentReset + @"\Countries\Logins";

            XmlSerializer xs = new XmlSerializer(typeof(List<countryLogin>));

            foreach (var file in Directory.GetFiles(path))
            {
                List<countryLogin> temp = new List<countryLogin>();
                using (var sr = new StreamReader(file))
                {
                    temp = ((List<countryLogin>)xs.Deserialize(sr)).Where(c => c.tag == tag).ToList();
                }
                foreach (var login in temp)
                {
                    if (Logins.SingleOrDefault(c => c.countryNum == login.countryNum) != null)
                    {
                        Logins.SingleOrDefault(c => c.countryNum == login.countryNum).DetectedBy.AddRange(login.DetectedBy);
                    }
                    else
                    {
                        Logins.Add(login);
                    }
                }
            }

            return Logins;
        }

        // method supporting a bot command to assign countries to a specific user
        public void AssignCountries(int[] nbrs, ulong userId)
        {
            var user = frmDiscordBot.Storage.UsersStorage.Get(userId);
            ClaimCountry(nbrs, user.Username);
        }
        
        // method returning an array of country numbers parsed from the messaage array received
        public int[] GetNumbersFromInput(string[] messageParts)
        {
            try
            {
                int[] countryNbrs = new int[16];
                for (int i = 1; i < messageParts.Count(); i++)
                {
                    if (messageParts[i].Contains(','))
                    {
                        if (messageParts[i].Split(',').Count() > 2)
                        {
                            int nbrsFlag = 0;
                            foreach (var part in messageParts[i].Split(','))
                            {
                                if (Int32.TryParse(part, out countryNbrs[nbrsFlag]))
                                {
                                    nbrsFlag++;
                                }
                            }
                            break;
                        }
                        else
                        {
                            if (Int32.TryParse(messageParts[i].Trim(','), out countryNbrs[i - 1]))
                            {

                            }
                        }
                    }
                    else
                    {
                        if (Int32.TryParse(messageParts[i].Trim(','), out countryNbrs[i - 1]))
                        {

                        }
                    }
                }
                return countryNbrs;
            }
            catch
            {
                return null;
            }
        }

        // method support a bot command to add a country to the kill list
        public bool AddCountryToKillList(int nbr)
        {
            if (Countries.FirstOrDefault(c => c.Number == nbr).AddToKillList())
                return true;
            else
                return false;
        }

        // method supporting a bot command to remove a country form the kill list
        public bool RemoveCountryFromKillList(int nbr)
        {
            if (Countries.FirstOrDefault(c => c.Number == nbr).RemoveFromKillList())
                return true;
            else
                return false;
        }

        // stops all background process
        public void Stop()
        {
            CountriesUpdateServiceTimer.Stop();
            CountriesUpdateServiceTimer.Dispose();
            CountriesUpdateService.Dispose();
        }
    }

    /*
     * Error Logs
     * 
     * For Hanlding of the all the bad things that happen
     * 
     */
    public class ErrorLogs
    {
        // Saves Error to XML
        public static void SaveErrorToXML(BotError Error)
        {
            string path = @"C:\Data\Errors";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            XmlSerializer xs = new XmlSerializer(typeof(BotError));
            using (TextWriter tw = new StreamWriter(path + @"\" + StorageModel.ConvertToUnixTime(DateTime.UtcNow) + ".xml"))
            {
                xs.Serialize(tw, Error);
                tw.Close();
            }
        }
    }

    /*
     * Market Data Storage
     * 
     * Handles storage, loading & saving of market data, background process to update storage every 5 seconds
     *          
     */
    public class MarketDataStorage
    {
        private List<api_mtrans> Transactions = new List<api_mtrans>();
        private int lastTransactionIdRecorded = 36134883;
        private int curResetNbr = 1392;
        private BackgroundWorker MarketUpdateService;
        private System.Timers.Timer MarketUpdateServiceTimer;

        //Basic constructor, also starts the Background process
        public MarketDataStorage()
        {
            try
            {
                Initialize();
                using (MarketUpdateService = new BackgroundWorker())
                {
                    MarketUpdateService.DoWork += ReceiveMarketTransactions;
                    MarketUpdateServiceTimer = new System.Timers.Timer(5000);
                    MarketUpdateServiceTimer.Elapsed += CheckMarketUpdateProcess;
                    MarketUpdateServiceTimer.Start();
                }
            }
            catch(Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        //Saves storage data to XML
        private void SaveTransactionsDataToXML()
        {
            try
            {
                if (Transactions.Count() > 0)
                {
                    List<api_mtrans> NewSet = new List<api_mtrans>();
                    curResetNbr = Transactions.OrderBy(c => c.resetid).FirstOrDefault().resetid;
                    if (Transactions.All(c => c.resetid == curResetNbr) == false)
                    {
                        NewSet = Transactions.Where(c => c.resetid != curResetNbr).ToList();
                        Transactions.RemoveAll(c => c.resetid != curResetNbr);
                    }
                    string basePath = @"C:\Data\ResetSpecifcData\" + curResetNbr;

                    if (Directory.Exists(basePath) == false)
                        Directory.CreateDirectory(basePath);

                    string path = basePath + @"\MarketTransactions";

                    if (Directory.Exists(path) == false)
                        Directory.CreateDirectory(path);

                    XmlSerializer xs = new XmlSerializer(typeof(List<api_mtrans>));
                    using (TextWriter tw = new StreamWriter(path + @"\" + Guid.NewGuid().ToString() + ".xml"))
                    {
                        xs.Serialize(tw, Transactions);
                        tw.Close();
                    }

                    if (NewSet.Count() > 0)
                    {
                        curResetNbr = NewSet.FirstOrDefault().resetid;
                        basePath = @"C:\Data\ResetSpecifcData\" + curResetNbr;

                        if (Directory.Exists(basePath) == false)
                            Directory.CreateDirectory(basePath);

                        path = basePath + @"\MarketTransactions";

                        if (Directory.Exists(path) == false)
                            Directory.CreateDirectory(path);

                        using (TextWriter tw = new StreamWriter(path + @"\" + Guid.NewGuid().ToString() + ".xml"))
                        {
                            xs.Serialize(tw, NewSet);
                            tw.Close();
                        }
                        lastTransactionIdRecorded = NewSet.Select(c => c.transactionid).Max();
                        NewSet.Clear();
                    }
                    else
                        lastTransactionIdRecorded = Transactions.Select(c => c.transactionid).Max();
                    Transactions.Clear();
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        //Checks Background process to determine if it is running, if not exectures ReceiveMarketTransactions method
        private void CheckMarketUpdateProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!MarketUpdateService.IsBusy)
                MarketUpdateService.RunWorkerAsync();
        }

        //makes a call to EE server obtaining market csv data, parses it and stores it
        private void ReceiveMarketTransactions(object sender, DoWorkEventArgs e)
        {
            try
            {
                // transactionId for 2nd to last transaction of 1353 = 36134883
                // transactionId for 1st transaction for 1392 = 36150175
                // GET CSV from URL
                string url = "http://www.earthempires.com/mtrans_feed?apicode=838a8f8296991745d36d3b32f5845b6e&serverid=12&startid=" + lastTransactionIdRecorded;
                HttpClientHandler handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
                string download = "";
                using (var client = new HttpClient(handler))
                {
                    HttpResponseMessage response;
                    client.DefaultRequestHeaders.Add("Encoding", "gzip");
                    response = client.GetAsync(url).Result;
                    download = response.Content.ReadAsStringAsync().Result;
                }
                // PARSE CSV and Create Model to save to DB
                if (download.ToString() != "" && download.ToString() != "You already received this update" && download.ToString() != "You are being throttled for not using gzip to 1 out of 4 updates.")
                {
                    Transactions = (from line in download.Split(Convert.ToChar(10))
                                    let columns = line.Split(',')
                                    where columns.Count().Equals(8)
                                    select new api_mtrans(columns)).ToList();
                    SaveTransactionsDataToXML();
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        //initial load on start
        private void Initialize()
        {
            try
            {
                List<api_mtrans> AllData = new List<api_mtrans>();
                string basePath = @"C:\Data\ResetSpecifcData";
                if (Directory.GetDirectories(basePath).Count() > 0)
                {
                    curResetNbr = Int32.Parse(Directory.GetDirectories(basePath).OrderByDescending(c => c).ToList()[1].Split('\\')[Directory.GetDirectories(basePath).OrderByDescending(c => c).ToList()[1].Split('\\').Count() - 1]);
                    string path = basePath + @"\" + curResetNbr + @"\MarketTransactions";
                    foreach (var file in Directory.GetFiles(path))
                    {
                        AllData.AddRange(LoadDataFromXML(file));
                    }
                    if (AllData != null && AllData.Count() > 0)
                        lastTransactionIdRecorded = AllData.Select(C => C.transactionid).Max();
                    AllData.Clear();
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        //returns market data from file
        private List<api_mtrans> LoadDataFromXML(string file)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<api_mtrans>));
                using (var sr = new StreamReader(file))
                {
                    return (List<api_mtrans>)xs.Deserialize(sr);
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
                return new List<api_mtrans>();
            }
        }
        
        // returns market data from a specific timeperiod
        public List<api_mtrans> LoadDataFromXML(DateTime loadFrom)
        {
            try
            {
                List<api_mtrans> AllData = new List<api_mtrans>();
                string basePath = @"C:\Data\ResetSpecifcData";
                if (curResetNbr == 0)
                {
                    var x = Directory.GetDirectories(basePath).OrderByDescending(c => c).ToList()[1].Split('\\');
                    curResetNbr = Int32.Parse(Directory.GetDirectories(basePath).OrderByDescending(c => c).ToList()[1].Split('\\')[Directory.GetDirectories(basePath).OrderByDescending(c => c).ToList()[1].Split('\\').Count() - 1]);
                }
                string path = basePath + @"\" + curResetNbr + @"\MarketTransactions";
                foreach (var file in Directory.GetFiles(path))
                {
                    AllData.AddRange(LoadDataFromXML(file));
                }
                AllData.RemoveAll(C => C.timestamp < loadFrom);
                return AllData;
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
                return null;
            }
        }

        // stops all background processes
        public void Stop()
        {
            MarketUpdateServiceTimer.Stop();
            MarketUpdateServiceTimer.Dispose();
            MarketUpdateService.Dispose();
        }
    }

    /*
     * News Storage
     * 
     * Handles storage, loading & saving of news data, background process to update news ever 5 seconds
     * 
     */
    public class NewsStorage
    {
        private List<api_news> News = new List<api_news>();
        private int lastNewsIdRecorded = 31054833;
        private int curResetNbr = 1392;
        private BackgroundWorker NewsUpdateService;
        private System.Timers.Timer NewsUpdateServiceTimer;

        //Basic constructor, also starts background process to update news data
        public NewsStorage()
        {
            try
            {
                Initialize();
                using (NewsUpdateService = new BackgroundWorker())
                {
                    NewsUpdateService.DoWork += ReceiveNews;
                    NewsUpdateServiceTimer = new System.Timers.Timer(5000);
                    NewsUpdateServiceTimer.Elapsed += CheckNewsUpdateProcess;
                    NewsUpdateServiceTimer.Start();
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        // initially loads data, and puts all 'ducks' in a row
        private void Initialize()
        {
            try
            {
                List<api_news> AllData = new List<api_news>();
                string basePath = @"C:\Data\ResetSpecifcData";
                if (Directory.GetDirectories(basePath).Count() > 0)
                {
                    curResetNbr = Int32.Parse(Directory.GetDirectories(basePath).OrderByDescending(c => c).ToList()[1].Split('\\')[Directory.GetDirectories(basePath).OrderByDescending(c => c).ToList()[1].Split('\\').Count() - 1]);
                    string path = basePath + @"\" + curResetNbr + @"\News";
                    foreach (var file in Directory.GetFiles(path))
                    {
                        AllData.AddRange(LoadDataFromXML(file));
                    }
                    if (AllData != null)
                        lastNewsIdRecorded = AllData.Select(C => C.newsid).Max();
                    AllData.Clear();
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        // returns a list of news data from a file
        private List<api_news> LoadDataFromXML(string file)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<api_news>));
                using (var sr = new StreamReader(file))
                {
                    return (List<api_news>)xs.Deserialize(sr);
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
                return new List<api_news>();
            }
        }

        // returns a list of news data from a timeperiod forward
        public List<api_news> LoadDataFromXML(DateTime loadFrom)
        {
            try
            {
                List<api_news> AllData = new List<api_news>();
                string basePath = @"C:\Data\ResetSpecifcData";
                if(curResetNbr == 0)
                    curResetNbr = int.Parse(Directory.GetDirectories(basePath).OrderByDescending(c => c).FirstOrDefault().Split('\\')[3]);
                string path = basePath + @"\" + curResetNbr + @"\News";
                foreach (var file in Directory.GetFiles(path))
                {
                    AllData.AddRange(LoadDataFromXML(file));
                }
                return AllData.Where(c => c.timestamp > loadFrom).ToList();
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
                return null;
            }
        }

        // returns a list of nes data from a timerpiod forward for a specific country
        public List<api_news> LoadDataFromXML(DateTime loadFrom, int num)
        {
            try
            {
                List<api_news> AllData = new List<api_news>();
                string basePath = @"C:\Data\ResetSpecifcData";
                if(curResetNbr == 0)
                    curResetNbr = int.Parse(Directory.GetDirectories(basePath).OrderByDescending(c => c).FirstOrDefault().Split('\\')[3]);
                string path = basePath + @"\" + curResetNbr + @"\News";
                if (Directory.Exists(path))
                {
                    foreach (var file in Directory.GetFiles(path))
                    {
                        AllData.AddRange(LoadDataFromXML(file).Where(c => c.attacker_num == num || c.defender_num == num).Where(c => c.timestamp > loadFrom).ToList());
                    }
                }
                return AllData;
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
                return null;
            }
        }

        //saves news data to xml
        private void SaveNewsDataToXML()
        {
            try
            {
                if (News.Count() > 0)
                {
                    List<api_news> NewSet = new List<api_news>();
                    curResetNbr = News.OrderBy(c => c.resetid).FirstOrDefault().resetid;
                    if (News.All(c => c.resetid == curResetNbr) == false)
                    {
                        NewSet = News.Where(c => c.resetid != curResetNbr).ToList();
                        News.RemoveAll(c => c.resetid != curResetNbr);
                    }
                    string basePath = @"C:\Data\ResetSpecifcData\" + curResetNbr;

                    if (Directory.Exists(basePath) == false)
                        Directory.CreateDirectory(basePath);

                    string path = basePath + @"\News";

                    if (Directory.Exists(path) == false)
                        Directory.CreateDirectory(path);

                    XmlSerializer xs = new XmlSerializer(typeof(List<api_news>));
                    using (TextWriter tw = new StreamWriter(path + @"\" + Guid.NewGuid().ToString() + ".xml"))
                    {
                        xs.Serialize(tw, News);
                        tw.Close();
                    }

                    if (NewSet.Count() > 0)
                    {
                        curResetNbr = NewSet.FirstOrDefault().resetid;
                        basePath = @"C:\Data\ResetSpecifcData\" + curResetNbr;

                        if (Directory.Exists(basePath) == false)
                            Directory.CreateDirectory(basePath);

                        path = basePath + @"\News";

                        if (Directory.Exists(path) == false)
                            Directory.CreateDirectory(path);

                        using (TextWriter tw = new StreamWriter(path + @"\" + Guid.NewGuid().ToString() + ".xml"))
                        {
                            xs.Serialize(tw, NewSet);
                            tw.Close();
                        }
                        lastNewsIdRecorded = NewSet.Select(c => c.newsid).Max();
                        NewSet.Clear();
                    }
                    else
                        lastNewsIdRecorded = News.Select(c => c.newsid).Max();
                    News.Clear();
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        // checks Background process to determine if it is running, if not executes ReceiveNews method
        private void CheckNewsUpdateProcess(object sender, System.Timers.ElapsedEventArgs e)
        {                       
            if (!NewsUpdateService.IsBusy)
                NewsUpdateService.RunWorkerAsync();
        }

        // makes a call to EE server obtaining news csv, parsing it and saving it to xml
        private void ReceiveNews(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Start of 1392 set = 31066010
                // Second to last news event in 1353 = 31054833
                string url = "http://www.earthempires.com/news_feed?apicode=267feaebe1d88d8ee7954dddf740b45e&serverid=12&startid=" + lastNewsIdRecorded;
                HttpClientHandler handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
                string download = "";
                using (var client = new HttpClient(handler))
                {
                    HttpResponseMessage response;
                    client.DefaultRequestHeaders.Add("Encoding", "gzip");
                    response = client.GetAsync(url).Result;
                    download = response.Content.ReadAsStringAsync().Result;
                }
                // PARSE CSV and Create Model to save to DB
                if (download.ToString() != "" && download.ToString() != "You already received this update" && download.ToString() != "You are being throttled for not using gzip to 1 out of 4 updates.")
                {
                    News = (from line in download.Split(Convert.ToChar(10))
                            let columns = line.Split(',')
                            where columns.Count().Equals(15)
                            select new api_news(columns)).ToList();

                    SaveNewsDataToXML();

                    //Thread checkNewsThread = new Thread(() => CheckNewsEvents(model));
                    //checkNewsThread.Start();
                }
            }
            catch (Exception c)
            {
                ErrorLogs.SaveErrorToXML(new BotError(c));
            }
        }

        // stops all background process
        public void Stop()
        {
            NewsUpdateServiceTimer.Stop();
            NewsUpdateServiceTimer.Dispose();
            NewsUpdateService.Dispose();
        }
    }

    /*
     * Storage
     * 
     * Allows for communication between different storage types
     * 
     */
    public class StorageModel
    {
        public StorageModel()
        {

        }

        public SpyOpsStorage SpyOpsStorage = new SpyOpsStorage();
        public NewsStorage NewsStorage = new NewsStorage();
        public MarketDataStorage MarketStorage = new MarketDataStorage();
        public CountryStorage CountryStorage = new CountryStorage();
        public UserStorage UsersStorage = new UserStorage();
        public InsultStorage InsultStorage = new InsultStorage();
        public RelationsStorage RelationsStorage = new RelationsStorage();

        /*
         * Convert Unix Timestamp to DateTime Method
         * 
         * Purpose:
         * Convert a double Unix Timestamp to a DateTime object
         */
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        /*
         * Convert Unix DateTime to Unix Timestamp Method
         * 
         * Purpose:
         * Convert a DateTime object to a long Unix timestamp
         */
        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }

        /*
         * Stop Background Processes Method
         * 
         * Purpose:
         * Stops all process handling storage
         */
        public void StopProcesses()
        {
            SpyOpsStorage.Stop();
            NewsStorage.Stop();
            MarketStorage.Stop();
            CountryStorage.Stop();
        }
    }
}
