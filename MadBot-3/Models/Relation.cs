using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MadBot_3.Models
{
    public class Relation
    {
        public string clanTag { get; set; }
        public DateTime timeStamp { get; set; }
        public RelationTypes RelationType { get; set; }

        /*
         * Load Relation From XML Method
         * 
         * Purpose:
         * Parses the XML file provided and returns a Relation object
         * 
         */
        public Relation LoadRelationFromXML(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Relation));
            using (var sr = new StreamReader(file))
            {                
                var temp = (Relation)xs.Deserialize(sr);
                sr.Close();
                return temp;            
            }            
        }

        /*
         * Save Relation to XML
         * 
         * Purpose:
         * Saves the Relation to XML
         * 
         */
        public void SaveRelationToXML()
        {
            XmlSerializer xs = new XmlSerializer(typeof(Relation));
            string path = @"C:\Data\Relations";

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            if(File.Exists(path + @"\" + clanTag + ".xml"))
            {
                File.Delete(path + @"\" + clanTag + ".xml");
            }
            using (TextWriter tw = new StreamWriter(path + @"\" + clanTag + ".xml"))
            {
                xs.Serialize(tw, this);
                tw.Close();
            }
        }

        /*
         * Remove Method
         * 
         * Purpose:
         * Deletes the Relation objects' XML file provided
         * 
         */
        public void Remove()
        {
            string path = @"C:\Data\Relations";

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            if (File.Exists(path + @"\" + clanTag + ".xml"))
            {
                File.Delete(path + @"\" + clanTag + ".xml");
            }
        }

        /*
         * Determine Relation Type Method
         * 
         * Purpose:
         * Returns the RelationType based on the string provided
         * 
         */
        public RelationTypes DetermineRelationType(string input)
        {
            switch(input)
            {
                case "FDP":
                    return RelationTypes.FDP;
                case "LDP":
                    return RelationTypes.LDP;
                case "uNAP":
                    return RelationTypes.uNAP;
                case "NAP":
                    return RelationTypes.NAP;
                default:
                    return RelationTypes.NULL;
            }
        }
    }
    public enum RelationTypes
    {
        FDP,
        LDP,
        uNAP,
        NAP,
        NULL
    }
}
