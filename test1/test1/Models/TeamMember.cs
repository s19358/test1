using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test1.Models
{
    public class TeamMember
    {
        public static IEnumerable<Task> tasks;

        public int idTeamMember { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  email { get; set; }
        public List<Task> listofTasksAssignedto { get; set; }
        public List<Task> listofTasksCreatedBy { get; set; }


        public IEnumerable<Task> getTasks()
        {
            return tasks;
        }
    }
}
