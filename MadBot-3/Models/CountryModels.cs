using Discord;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MadBot_3.Models
{
    public partial class api_spyops
    {
        public Guid entry_id { get; set; }
        public string uploader_api_key { get; set; }
        public int subject_number { get; set; }
        public int uploader_number { get; set; }
        public int serverid { get; set; }
        public string type { get; set; }
        public string json { get; set; }
        public DateTime timestamp { get; set; }

        public api_spyops()
        {
            entry_id = Guid.NewGuid();
            timestamp = DateTime.UtcNow;
        }
    }

    public class SpyOpInfo : StatStylizingCommands
    {
        public SpyOpInfo()
        {

        }

        public SpyOpInfo(string json, string opType, string apiKey)
        {
            JObject jObject = JObject.Parse(json);
            JToken spyOp = jObject;
            if (opType == "selfop")
            {
                this.apiKey = apiKey;
                serverId = (int)spyOp["serverid"];
                resetId = (int)spyOp["resetid"];
                defenseBonus = (double)spyOp["defense_bonus"];
                Military_Net = (long)spyOp["nw_mil"];
                Technology_Net = (long)spyOp["nw_tech"];
                Land_Net = (long)spyOp["nw_land"];
                Other_Net = (long)spyOp["nw_other"];
                Market_Net = (long)spyOp["nw_market"];
                atWar = (int)spyOp["atwar"];
                GDI = (string)spyOp["gdi"];
                TechPerTurn = (int)spyOp["tpt"];
                ExploreRate = (int)spyOp["explore_rate"];
                BuildingRate = (int)spyOp["bpt"];
                SpiesExp = (long)spyOp["expense_spy"];
                TroopsExp = (long)spyOp["expense_tr"];
                JetsExp = (long)spyOp["expense_j"];
                TurretsExp = (long)spyOp["expense_tu"];
                TanksExp = (long)spyOp["expense_ta"];
                TechTotal = (long)spyOp["tech_tot"];
                MedicalPerc = (double)spyOp["pt_med"];
                MilitaryPerc = (double)spyOp["pt_mil"];
                BusinessPerc = (double)spyOp["pt_bus"];
                ResidentialPerc = (double)spyOp["pt_res"];
                AgriculturePerc = (double)spyOp["pt_agri"];
                WarfarePerc = (double)spyOp["pt_war"];
                MilitaryStratPerc = (double)spyOp["pt_ms"];
                WeaponsPerc = (double)spyOp["pt_weap"];
                IndustrialPerc = (double)spyOp["pt_indy"];
                SpyPerc = (double)spyOp["pt_spy"];
                SDIPerc = (double)spyOp["pt_sdi"];
                CorruptionExp = (long)spyOp["corruption"];
                Clan = (string)spyOp["clan"];
                Gov = (string)spyOp["govt"];
                CountryName = (string)spyOp["cname"];
                CountryNumber = (int)spyOp["cnum"];
                turnsLeft = (int)spyOp["turns"];
                turnsTaken = (int)spyOp["turns_played"];
                turnsStored = (int)spyOp["turns_stored"];
                Rank = (int)spyOp["rank"];
                Money = (long)spyOp["money"];
                Networth = (long)spyOp["networth"];
                Population = (int)spyOp["pop"];
                Land = (int)spyOp["land"];
                FoodStore = (long)spyOp["food"];
                FoodProd = (long)spyOp["foodpro"];
                FoodConsumption = (long)spyOp["foodcon"];
                FoodNetChange = (long)spyOp["foodnet"];
                OilStore = (long)spyOp["oil"];
                TaxRevenues = (long)spyOp["revenue"];
                TaxRate = (double)spyOp["taxrate"];
                PerCapita = (double)spyOp["pci"];
                Expenses = (long)spyOp["expenses"];
                MilExpenses = (long)spyOp["expensesmil"];
                AllianceGDIExp = (long)spyOp["expensesally"];
                LandExp = (long)spyOp["expensesland"];
                Expenses = (long)spyOp["expenses"];
                NetIncome = (long)spyOp["netincome"];
                UnusedLands = (int)spyOp["unbuilt"];
                EnterpriseZones = (int)spyOp["b_ent"];
                Residences = (int)spyOp["b_res"];
                IndustrialComplexes = (int)spyOp["b_indy"];
                MilitaryBases = (int)spyOp["b_mb"];
                ResearchLabs = (int)spyOp["b_lab"];
                Farms = (int)spyOp["b_farm"];
                OilRigs = (int)spyOp["b_rig"];
                ConstructionSites = (int)spyOp["b_cs"];
                Spies = (long)spyOp["m_spy"];
                Troops = (long)spyOp["m_tr"];
                Jets = (long)spyOp["m_j"];
                Turrets = (long)spyOp["m_tu"];
                Tanks = (long)spyOp["m_ta"];
                ChemicalMissles = (int)spyOp["m_cm"];
                NuclearMissles = (int)spyOp["m_nm"];
                CruiseMissles = (int)spyOp["m_em"];
                MilitaryPts = (long)spyOp["t_mil"];
                MedicalPts = (long)spyOp["t_med"];
                BusinessPts = (long)spyOp["t_bus"];
                ResidentialPts = (long)spyOp["t_res"];
                AgriculturePts = (long)spyOp["t_agri"];
                WarfarePts = (long)spyOp["t_war"];
                MilitaryStratPts = (long)spyOp["t_ms"];
                WeaponsPts = (long)spyOp["t_weap"];
                IndustrialPts = (long)spyOp["t_indy"];
                SpyPts = (long)spyOp["t_spy"];
                SDIPts = (long)spyOp["t_sdi"];
            }
            else if (opType == "spy")
            {
                this.apiKey = apiKey;
                serverId = (int)spyOp["serverid"];
                resetId = (int)spyOp["resetid"];
                Gov = (string)spyOp["govt"];
                CountryName = (string)spyOp["cname"];
                CountryNumber = (int)spyOp["cnum"];
                turnsLeft = (int)spyOp["turns"];
                turnsTaken = (int)spyOp["turns_played"];
                turnsStored = (int)spyOp["turns_stored"];
                Rank = (int)spyOp["rank"];
                Money = (long)spyOp["money"];
                Networth = (long)spyOp["networth"];
                Population = (int)spyOp["pop"];
                Land = (int)spyOp["land"];
                FoodStore = (long)spyOp["food"];
                FoodProd = (long)spyOp["foodpro"];
                FoodConsumption = (long)spyOp["foodcon"];
                FoodNetChange = (long)spyOp["foodnet"];
                OilStore = (long)spyOp["oil"];
                TaxRevenues = (long)spyOp["revenue"];
                TaxRate = (double)spyOp["taxrate"];
                PerCapita = (double)spyOp["pci"];
                Expenses = (long)spyOp["expenses"];
                MilExpenses = (long)spyOp["expensesmil"];
                AllianceGDIExp = (long)spyOp["expensesally"];
                LandExp = (long)spyOp["expensesland"];
                Expenses = (long)spyOp["expenses"];
                NetIncome = (long)spyOp["netincome"];
                UnusedLands = (int)spyOp["unbuilt"];
                EnterpriseZones = (int)spyOp["b_ent"];
                Residences = (int)spyOp["b_res"];
                IndustrialComplexes = (int)spyOp["b_indy"];
                MilitaryBases = (int)spyOp["b_mb"];
                ResearchLabs = (int)spyOp["b_lab"];
                Farms = (int)spyOp["b_farm"];
                OilRigs = (int)spyOp["b_rig"];
                ConstructionSites = (int)spyOp["b_cs"];
                Spies = (long)spyOp["m_spy"];
                Troops = (long)spyOp["m_tr"];
                Jets = (long)spyOp["m_j"];
                Turrets = (long)spyOp["m_tu"];
                Tanks = (long)spyOp["m_ta"];
                ChemicalMissles = (int)spyOp["m_cm"];
                NuclearMissles = (int)spyOp["m_nm"];
                CruiseMissles = (int)spyOp["m_em"];
                MilitaryPts = (long)spyOp["t_mil"];
                MedicalPts = (long)spyOp["t_med"];
                BusinessPts = (long)spyOp["t_bus"];
                ResidentialPts = (long)spyOp["t_res"];
                AgriculturePts = (long)spyOp["t_agri"];
                WarfarePts = (long)spyOp["t_war"];
                MilitaryStratPts = (long)spyOp["t_ms"];
                WeaponsPts = (long)spyOp["t_weap"];
                IndustrialPts = (long)spyOp["t_indy"];
                SpyPts = (long)spyOp["t_spy"];
                SDIPts = (long)spyOp["t_sdi"];
                MilitaryPerc = CalculateAllTech(MilitaryPts, "t_mil");
                MedicalPerc = CalculateAllTech(MedicalPts, "t_med");
                BusinessPerc = CalculateAllTech(BusinessPts, "t_bus");
                ResidentialPerc = CalculateAllTech(ResidentialPts, "t_res");
                AgriculturePerc = CalculateAllTech(AgriculturePts, "t_agri");
                WarfarePerc = CalculateAllTech(WarfarePts, "t_war");
                MilitaryStratPerc = CalculateAllTech(MilitaryStratPts, "t_ms");
                WeaponsPerc = CalculateAllTech(WeaponsPts, "t_weap");
                IndustrialPerc = CalculateAllTech(IndustrialPts, "t_indy");
                SpyPerc = CalculateAllTech(SpyPts, "t_spy");
                SDIPerc = CalculateAllTech(SDIPts, "t_sdi");
            }
        }

        #region Properties
        [JsonProperty("resetid")]
        public int resetId { get; set; }
        [JsonProperty("serverid")]
        public int serverId { get; set; }
        public string apiKey { get; set; }
        public int Att { get; set; }
        public int Def { get; set; }
        public Nullable<DateTime> LastSpyOp { get; set; }
        public Nullable<DateTime> LastActivity { get; set; }
        public bool Available { get; set; }
        [JsonProperty("govt")]
        public string Gov { get; set; }
        [JsonProperty("clan")]
        public string Clan { get; set; }
        [JsonProperty("cname")]
        public string CountryName { get; set; }
        [JsonProperty("cnum")]
        public int CountryNumber { get; set; }
        public int entry_id { get; set; }        
        [JsonProperty("defense_bonus")]
        public double defenseBonus { get; set; }
        [JsonProperty("turns")]
        public int turnsLeft { get; set; }        
        [JsonProperty("turns_played")]
        public int turnsTaken { get; set; }
        [JsonProperty("turns_stored")]
        public int turnsStored { get; set; }        
        [JsonProperty("rank")]
        public int Rank { get; set; }        
        [JsonProperty("networth")]
        public long Networth { get; set; }        
        [JsonProperty("land")]
        public int Land { get; set; }
        [JsonProperty("money")]        
        public long Money { get; set; }        
        [JsonProperty("pop")]
        public long Population { get; set; }
        [JsonProperty("atwar")]
        public int atWar { get; set; }
        [JsonProperty("gdi")]
        public string GDI { get; set; }        
        [JsonProperty("b_ent")]
        public int EnterpriseZones { get; set; }        
        [JsonProperty("b_res")]
        public int Residences { get; set; }        
        [JsonProperty("b_indy")]
        public int IndustrialComplexes { get; set; }        
        [JsonProperty("b_mb")]
        public int MilitaryBases { get; set; }        
        [JsonProperty("b_lab")]
        public int ResearchLabs { get; set; }        
        [JsonProperty("b_farm")]
        public int Farms { get; set; }        
        [JsonProperty("b_rig")]
        public int OilRigs { get; set; }        
        [JsonProperty("b_cs")]
        public int ConstructionSites { get; set; }        
        [JsonProperty("unbuilt")]
        public int UnusedLands { get; set; }        
        [JsonProperty("t_mil")]
        public long MilitaryPts { get; set; }        
        [JsonProperty("pt_mil")]
        public double MilitaryPerc { get; set; }        
        [JsonProperty("t_med")]
        public long MedicalPts { get; set; }        
        [JsonProperty("pt_med")]
        public double MedicalPerc { get; set; }        
        [JsonProperty("t_bus")]
        public long BusinessPts { get; set; }        
        [JsonProperty("pt_bus")]
        public double BusinessPerc { get; set; }        
        [JsonProperty("t_res")]
        public long ResidentialPts { get; set; }        
        [JsonProperty("pt_res")]
        public double ResidentialPerc { get; set; }        
        [JsonProperty("t_agri")]
        public long AgriculturePts { get; set; }        
        [JsonProperty("pt_agri")]
        public double AgriculturePerc { get; set; }        
        [JsonProperty("t_war")]
        public long WarfarePts { get; set; }        
        [JsonProperty("pt_war")]
        public double WarfarePerc { get; set; }        
        [JsonProperty("t_ms")]
        public long MilitaryStratPts { get; set; }        
        [JsonProperty("pt_ms")]
        public double MilitaryStratPerc { get; set; }
        [JsonProperty("t_weap")]
        public long WeaponsPts { get; set; }        
        [JsonProperty("pt_weap")]
        public double WeaponsPerc { get; set; }        
        [JsonProperty("t_indy")]
        public long IndustrialPts { get; set; }        
        [JsonProperty("pt_indy")]
        public double IndustrialPerc { get; set; }        
        [JsonProperty("t_spy")]
        public long SpyPts { get; set; }        
        [JsonProperty("pt_spy")]
        public double SpyPerc { get; set; }        
        [JsonProperty("t_sdi")]
        public long SDIPts { get; set; }        
        [JsonProperty("pt_sdi")]
        public double SDIPerc { get; set; }        
        [JsonProperty("revenue")]
        public long TaxRevenues { get; set; }
        [JsonProperty("taxrate")]
        public double TaxRate { get; set; }
        [JsonProperty("pci")]
        public double PerCapita { get; set; }        
        [JsonProperty("expenses")]
        public long Expenses { get; set; }        
        [JsonProperty("netincome")]
        public long NetIncome { get; set; }        
        public long Cashing { get; set; }        
        [JsonProperty("food")]
        public long FoodStore { get; set; }        
        [JsonProperty("foodpro")]
        public long FoodProd { get; set; }        
        [JsonProperty("foodcon")]
        public long FoodConsumption { get; set; }        
        [JsonProperty("foodnet")]
        public long FoodNetChange { get; set; }        
        [JsonProperty("oil")]
        public long OilStore { get; set; }        
        [JsonProperty("oilpro")]
        public long OilProduction { get; set; }        
        [JsonProperty("bpt")]
        public double BuildingRate { get; set; }        
        [JsonProperty("explore_rate")]
        public int ExploreRate { get; set; }        
        [JsonProperty("m_spy")]
        public long Spies { get; set; }        
        [JsonProperty("m_tr")]
        public long Troops { get; set; }        
        [JsonProperty("m_j")]
        public long Jets { get; set; }        
        [JsonProperty("m_tu")]
        public long Turrets { get; set; }        
        [JsonProperty("m_ta")]
        public long Tanks { get; set; }        
        [JsonProperty("m_nm")]
        public int NuclearMissles { get; set; }        
        [JsonProperty("m_cm")]
        public int ChemicalMissles { get; set; }        
        [JsonProperty("m_em")]
        public int CruiseMissles { get; set; }        
        [JsonProperty("expensesmil")]
        public long MilExpenses { get; set; }        
        [JsonProperty("expense_spy")]
        public long SpiesExp { get; set; }        
        [JsonProperty("expense_tr")]
        public long TroopsExp { get; set; }        
        [JsonProperty("expense_j")]
        public long JetsExp { get; set; }        
        [JsonProperty("expense_tu")]
        public long TurretsExp { get; set; }        
        [JsonProperty("expense_ta")]
        public long TanksExp { get; set; }        
        [JsonProperty("expensesally")]
        public long AllianceGDIExp { get; set; }        
        [JsonProperty("expensesland")]
        public long LandExp { get; set; }        
        [JsonProperty("corruption")]
        public long CorruptionExp { get; set; }        
        [JsonProperty("nw_mil")]
        public long Military_Net { get; set; }        
        [JsonProperty("nw_tech")]
        public long Technology_Net { get; set; }        
        [JsonProperty("nw_land")]
        public long Land_Net { get; set; }        
        [JsonProperty("nw_other")]
        public long Other_Net { get; set; }        
        [JsonProperty("nw_market")]
        public long Market_Net { get; set; }        
        [JsonProperty("tpt")]
        public int TechPerTurn { get; set; }        
        public long TechTotal { get; set; }
        public long SpyStr { get; set; }
        public double SPAL { get; set; }
        #endregion

        /*
         * Calculate Tech Percent Method
         * 
         * Purpose:
         * Returns a double representing the tech % for a given technology and points invested in that technology
         * 
         */
        public double CalculateAllTech(long points, string tech)
        {
            double max = 0;
            double govT = 1;
            double govEff = 1;
            double BaseTech = 1.00;
            int c1 = 192;
            double c2 = 6.795;
            switch (tech)
            {
                case "t_mil":
                    c1 = 780;
                    c2 = 5.75;
                    switch (Gov)
                    {
                        case "D":
                            max = 0.8166;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 0.8916;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 0.8333;
                            govEff = 1.2;
                            break;
                        default:
                            max = 0.8333;
                            break;
                    }
                    break;
                case "t_med":
                    c1 = 1650;
                    c2 = 4.62;
                    switch (Gov)
                    {
                        case "D":
                            max = 0.6333;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 0.7833;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 0.6666;
                            govEff = 1.2;
                            break;
                        default:
                            max = 0.6666;
                            break;
                    }
                    break;
                case "t_bus":
                    switch (Gov)
                    {
                        case "D":
                            max = 1.88;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 1.52;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 1.80;
                            govEff = 1.2;
                            break;
                        default:
                            max = 1.80;
                            break;
                    }
                    break;
                case "t_res":
                    switch (Gov)
                    {
                        case "D":
                            max = 1.88;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 1.52;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 1.80;
                            govEff = 1.2;
                            break;
                        default:
                            max = 1.80;
                            break;
                    }
                    break;
                case "t_agri":
                    switch (Gov)
                    {
                        case "D":
                            max = 2.43;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 1.845;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 2.30;
                            govEff = 1.2;
                            break;
                        default:
                            max = 2.30;
                            break;
                    }
                    break;
                case "t_war":
                    BaseTech = 0.002;
                    switch (Gov)
                    {
                        case "D":
                            max = 0.055;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 0.0332;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 0.050;
                            govEff = 1.2;
                            break;
                        default:
                            max = 0.050;
                            break;
                    }
                    break;
                case "t_ms":
                    switch (Gov)
                    {
                        case "D":
                            max = 1.44;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 1.26;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 1.40;
                            govEff = 1.2;
                            break;
                        default:
                            max = 1.40;
                            break;
                    }
                    break;
                case "t_weap":
                    switch (Gov)
                    {
                        case "D":
                            max = 1.55;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 1.325;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 1.5;
                            govEff = 1.2;
                            break;
                        default:
                            max = 1.5;
                            break;
                    }
                    break;
                case "t_indy":
                    switch (Gov)
                    {
                        case "D":
                            max = 1.66;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 1.39;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 1.60;
                            govEff = 1.2;
                            break;
                        default:
                            max = 1.60;
                            break;
                    }
                    break;
                case "t_spy":
                    switch (Gov)
                    {
                        case "D":
                            max = 1.55;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 1.325;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 1.5;
                            govEff = 1.2;
                            break;
                        default:
                            max = 1.5;
                            break;
                    }
                    break;
                case "t_sdi":
                    BaseTech = 0.01;
                    switch (Gov)
                    {
                        case "D":
                            max = 0.989;
                            govT = 1.1;
                            break;
                        case "H":
                            max = 0.5885;
                            govT = 0.65;
                            break;
                        case "C":
                            max = 0.90;
                            govEff = 1.2;
                            break;
                        default:
                            max = 0.90;
                            break;
                    }
                    break;
            }
            //BaseTech%+(MaxTech%-BaseTech%)*GvtTech*(1-EXP(-GvtEff*TechPts/(C1+C2*Land)))
            var test = BaseTech + (max - BaseTech) * govT * (1 - Math.Exp(-govEff * points / (c1 + c2 * Land)));
            return test;
        }

        /*
         * Calculate Spy Strength
         * 
         * Purpose:
         * Returns a long representing the strength of a users spy defense
         * 
         */
        public long CalculateSpyStr(long spies, double spyTech, bool dict)
        {
            long spyStr = 0;
            if (spyTech < 1)
                spyTech = 1;
            else if (spyTech >= 2)
                spyTech = 1;
            if (dict)
            {
                spyStr = spies * Convert.ToInt64(spyTech) * Convert.ToInt64(1.3);
            }
            else
            {
                spyStr = spies * Convert.ToInt64(spyTech);
            }

            return spyStr;
        }

        /*
         * Calculate SPAL(Spies Per Acre of Land)
         * 
         * Purpose:
         * Returns a double representing a countries SPAL given the amount of spy strength and land
         * 
         */
        public double CalculateSPAL(long spyStr, long land)
        {
            double spal = spyStr / land;
            return spal;
        }

        /*
         * Convert To Display Break Method
         * 
         * Purpose:
         * Returns a string representing an abbreviated defensive strength provided the amount of units, tech percent and bonus
         * 
         */
        public string ConvertToDisplayBreak(long unit, double tech, double bonus)
        {
            if (tech == 0)
                tech = 1;
            else if ((tech / 100) >= 2)
                tech = 1;
            else if (tech > 100)
                tech = tech / 100;
            if (bonus == 0)
                bonus = 1;
            else if (bonus >= 2)
                bonus = 1;
            long str = Convert.ToInt64(Convert.ToDouble(tech) * Convert.ToDouble(bonus) * Convert.ToDouble(unit));
            string displayBreak;
            if (str > 1000000)
            {
                var temp = Math.Round(Convert.ToDouble(str) / Convert.ToDouble(1000000), 1);
                displayBreak = temp + "m";
            }
            else if (str > 1000)
            {
                var temp = Math.Round(Convert.ToDouble(str) / Convert.ToDouble(1000), 1);
                displayBreak = temp.ToString("N1") + "k";
            }
            else
            {
                displayBreak = str.ToString("N0");
            }
            return displayBreak;
        }

        /*
         * Get Spy Op Message Method
         * 
         * Purpose:
         * Returns a string parsed from an SpyOpInfo object
         * 
         */
        public string GetSpyOpMessage()
        {
            string population = ConvertNumberToDisplay(Population),
                    cash = ConvertNumberToDisplay(Money),
                    oil = ConvertNumberToDisplay(OilStore),
                    food = ConvertNumberToDisplay(FoodStore),
                    networth = ConvertNumberToDisplay(Networth),
                    username = frmDiscordBot.Storage.UsersStorage.Get(apiKey),
                    message = "";

            bool dict = false;
            if (Gov == "I")
                dict = true;
            SpyStr = CalculateSpyStr(Spies, SpyPerc, dict);
            SPAL = CalculateSPAL(SpyStr, Land);
            // Cname, Tag, Pop, Cash, Oil, Bushels, Troops, Jets, Turrets, Tanks, SPAL, SDI, Weap

            // TODO Need to save API CODES so that I can display username uploading op / advisor
            message = "``` {" + CountryName + "(#" + CountryNumber + ")" + " [" + Clan + "] - Net:" + networth + "  Land: " + Land + "  Cash:" + cash + "  SPAL:" + SPAL
                + "  Troops: " + ConvertToDisplayBreak(Troops, WeaponsPerc, defenseBonus)
                + "  Jets: " + ConvertToDisplayBreak(Jets, WeaponsPerc, 1)
                + "  Turrets:" + ConvertToDisplayBreak(Turrets, WeaponsPerc, defenseBonus)
                + "  Tanks:" + ConvertToDisplayBreak(Tanks, WeaponsPerc, defenseBonus)
                + "  Missiles:" + (NuclearMissles + ChemicalMissles + CruiseMissles).ToString("N0")
                + "  Turns:" + turnsLeft + "(" + turnsStored + ")  Civs:" + population
                + "  Raw SS Break:" + ConvertToDisplayBreak((Convert.ToInt64(Troops * 0.5) + Turrets + (Tanks * 2)), WeaponsPerc, defenseBonus) + " jets } Uploaded By - " + username + "```";
            return message;
        }
    }

    public partial class TagChange
    {
        public TagChange()
        {
            TagChangeId = Guid.NewGuid();
            timestamp = DateTime.UtcNow;
        }
        public Guid TagChangeId { get; set; }
        public DateTime timestamp { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string FromTag { get; set; }
        public string ToTag { get; set; }
        public int resetId { get; set; }        
    }

    public class countryLogin
    {        
        public countryLogin()
        {
            loginId = Guid.NewGuid();
            DetectedBy = new List<detected>();
        }
        public Guid loginId { get; set; }
        public int countryNum { get; set; }
        public string countryName { get; set; }
        public string tag { get; set; }
        public List<detected> DetectedBy { get; set; }
    }

    public class detected
    {
        public detected()
        {
            DetectedId = Guid.NewGuid();
        }
        public Guid DetectedId { get; set; }
        public DetectionMethod DetectedBy { get; set; }
        public DateTime timeDetected { get; set; }
        public Guid loginId { get; set; }
    }

    public enum DetectionMethod
    {
        Networth,
        Land,
        News
    }

    public enum CountryStatus
    {
        Alive,
        Dead,
        Deleted,
        Protection,
        Vacation
    }

    public class api_currentranks : StatStylizingCommands
    {
        public int Number { get; set; }
        public DateTime Timestamp { get; set; }
        public int serverId { get; set; }
        public int resetId { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public int Land { get; set; }
        public long Networth { get; set; }
        public string Tag { get; set; }
        public string Gov { get; set; }
        public bool GDI { get; set; }
        public CountryStatus Status { get; set; }
        public string User { get; set; }
        public int KillList { get; set; }

        public api_currentranks()
        {

        }

        public api_currentranks(string[] columns)
        {
            Number = Convert.ToInt32(columns[3]);
            Timestamp = DateTime.UtcNow;
            serverId = Convert.ToInt32(columns[0]);
            resetId = Convert.ToInt32(columns[1]);
            Rank = Convert.ToInt32(columns[2]);
            Name = columns[4];
            Land = Convert.ToInt32(columns[5]);
            Networth = Convert.ToInt64(columns[6]);
            Tag = columns[7];
            Gov = columns[8];
            GDI = Convert.ToBoolean(Int16.Parse(columns[9]));
            Status = CountryStorage.DetermineCountryStatus(Convert.ToBoolean(Int16.Parse(columns[10])), Convert.ToBoolean(Int16.Parse(columns[11])), Convert.ToBoolean(Int16.Parse(columns[12])), Convert.ToBoolean(Int16.Parse(columns[13])));
            User = "";
            KillList = 0;
        }

        /*
         * Determine DR(Deminishing Return) Method
         * 
         * Purpose:
         * Returns an int representing a countries DR based on the news provided
         * 
         */
        public int DetermineDR(List<api_news> News)
        {
            int dr = 0;

            foreach (var newsEvent in News)
            {
                if (newsEvent.defender_num == this.Number)
                    dr++;

                if (newsEvent.attacker_num == this.Number)
                    dr--;
            }
            if (dr < 0)
                dr = 0;
            return dr;
        }

        /*
         * Add To Kill List Method
         * 
         * Purpose:
         * Modifies the api_currentrank object for this country to flag it as a kill list country
         * 
         */
        public bool AddToKillList()
        {
            try
            {
                KillList = 1;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*
         * Remove From Kill List Method
         * 
         * Purpose:
         * Modifies the api_currentrank object for this country to remove the flag labelling it as a kill list country
         * 
         */
        public bool RemoveFromKillList()
        {
            try
            {
                KillList = 0;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class api_news
    {
        public int newsid { get; set; }
        public int resetid { get; set; }
        public int serverid { get; set; }
        public DateTime timestamp { get; set; }
        public string Type { get; set; }
        public int win { get; set; }
        public string attacker_name { get; set; }
        public int attacker_num { get; set; }
        public string a_tag { get; set; }
        public string defender_name { get; set; }
        public int defender_num { get; set; }
        public string d_tag { get; set; }
        public long result1 { get; set; }
        public long result2 { get; set; }
        public int killhit { get; set; }

        public api_news()
        {

        }

        public api_news(string[] columns)
        {
            if (columns.Count() == 15)
            {
                serverid = Int16.Parse(columns[0]);
                resetid = Int16.Parse(columns[1]);
                newsid = Int32.Parse(columns[2]);
                timestamp = StorageModel.UnixTimeStampToDateTime(double.Parse(columns[3]));
                Type = columns[4];
                win = Int16.Parse(columns[5]);
                attacker_num = Int16.Parse(columns[6]);
                attacker_name = columns[7];
                defender_num = Int16.Parse(columns[8]);
                defender_name = columns[9];
                result1 = Convert.ToInt32(columns[10]);
                result2 = (columns[11] != "") ? Convert.ToInt32(columns[11]) : 0;
                a_tag = columns[12];
                d_tag = columns[13];
                killhit = Int16.Parse(columns[14]);
            }
        }
    }
}
