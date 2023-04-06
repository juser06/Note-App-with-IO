using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_App
{
    public class OldNotes : Notes
    {
        public string FinishDate { get; set; }

        public OldNotes(string note,string title, string date, string FinishDate) : base(note,title, date)
        {
            this.FinishDate = FinishDate;
        }
    }
}
