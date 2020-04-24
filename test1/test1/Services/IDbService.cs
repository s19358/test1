using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test1.Models;

namespace test1.Services
{
    public interface IDbService
    {
        TeamMember getTeamMember(int id);
        string DeleteProject(int id);

    }
}
