using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    internal class StatusLog
    {
        public string Status { get; set; }
        public string Time { get; set; }
        public string Alias { get; set; }

        public StatusLog(string status, string time, string alias)
        {
            Status = status;
            Time = time;
            Alias = alias;
        }

        public override string ToString()
        {
            return $"{Time} - {Alias}: {Status}";
        }
    }
}
