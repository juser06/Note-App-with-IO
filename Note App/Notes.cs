using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_App
{
    public class Notes
    {
        public string note { get; set; }
        public string title { get; set; }
        public string date { get; set; }

        public Notes(string note,string title, string date)
        {
            this.note = note;
            this.title = title;
            this.date = date;
        }
    }
}
