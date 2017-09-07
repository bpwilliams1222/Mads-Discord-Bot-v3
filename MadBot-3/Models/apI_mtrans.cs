using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadBot_3.Models
{
    public class api_mtrans
    {
        public int serverid { get; set; }
        public int resetid { get; set; }
        public int transactionid { get; set; }
        public DateTime timestamp { get; set; }
        public int goodid { get; set; }
        public long cost { get; set; }
        public long quantity { get; set; }
        public int standing_order { get; set; }

        public api_mtrans()
        {

        }

        public api_mtrans(string[] columns)
        {
            if(columns.Count() == 8)
            {
                transactionid = Int32.Parse(columns[2]);
                serverid = Int16.Parse(columns[0]);
                resetid = Int16.Parse(columns[1]);
                timestamp = StorageModel.UnixTimeStampToDateTime(double.Parse(columns[3]));
                goodid = Int16.Parse(columns[4]);
                cost = Int32.Parse(columns[5]);
                quantity = Int64.Parse(columns[6]);
                standing_order = Int16.Parse(columns[7]);
            }
        }
    }
}
