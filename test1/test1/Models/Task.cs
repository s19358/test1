using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test1.Models
{
    public class Task
    {
        public int idTask { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public DateTime deadline { get; set; }

        public string nameoftheproject { get; set; }

        public string tasktype { get; set; }
    }
}
