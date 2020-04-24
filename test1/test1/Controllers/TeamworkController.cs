using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test1.Models;
using test1.Services;

namespace test1.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TeamworkController : ControllerBase
    {

        private string ConnString = "Data Source=db-mssql;Initial Catalog=s19358;Integrated Security=True";

        
        IDbService service;

        //constructor injection

        public TeamworkController(IDbService ser)
        {
            service = ser;
        }

    
        [HttpGet("{id}")]
        public IActionResult getTeamMember(int id)
        {

            TeamMember team = service.getTeamMember(id);
            return Ok(team);


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {

            string delete = service.DeleteProject(id);
            return Ok(delete);


        }
    }
}