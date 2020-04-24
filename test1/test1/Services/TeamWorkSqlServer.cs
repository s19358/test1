using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using test1.Models;

namespace test1.Services
{
    public class TeamWorkSqlServer : IDbService
    {

        private string ConnString = "Data Source=db-mssql;Initial Catalog=s19358;Integrated Security=True";
        public string DeleteProject(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from project where idProject=@id;";
                com.Parameters.AddWithValue("id", id);
                con.Open();

                var transaction = con.BeginTransaction();
                com.Transaction = transaction;

                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();

                    com.CommandText = "DELETE FROM task WHERE idProject=@id;";

                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        //return BadRequest("error detected.");
                        throw new Exception("error detected.");
                    }

                    com.CommandText = "DELETE FROM project WHERE idProject=@id;";

                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        // return BadRequest("error detected.");
                        throw new Exception("error detected.");
                    }

                }
                else
                {

                    //return NotFound("there is no project with this id!");
                    throw new Exception("there is no project with this id!");
                }

                transaction.Commit();

                return "succesfully deleted.";

            }
        }

        public TeamMember getTeamMember(int id)
        {
            var listassignedTo = new List<Models.Task>();
            var listcreatedby = new List<Models.Task>();


            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from TeamMember where idTeamMember=@teammember;";   //check such person exists in db
                com.Parameters.AddWithValue("teammember", id);
                con.Open();
                var dr = com.ExecuteReader();
                if (dr.Read())
                {

                    dr.Close();
                    com.CommandText = "select t.idTask ,t.Name, t.description ,t.deadline,p.name 'projectname' ,a.name 'tasktype' from Task t " +
                        " join project p on p.IdProject = t.IdProject join TaskType a on a.idTaskType=t.idTaskType where t.idAssignedTo=@teammember order by t.deadline desc;";
                    dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        var task = new Models.Task();
                        task.idTask = (int)dr["idTask"];
                        task.name = dr["Name"].ToString();
                        task.description = dr["description"].ToString();
                        task.deadline = DateTime.Parse(dr["Deadline"].ToString());
                        task.nameoftheproject = dr["projectname"].ToString();
                        task.tasktype = dr["tasktype"].ToString();

                        listassignedTo.Add(task);

                    }


                    dr.Close();
                    com.CommandText = "select t.idTask ,t.Name, t.description ,t.deadline,p.name 'projectname' ,a.name 'tasktype' from Task t " +
                        " join project p on p.IdProject = t.IdProject join TaskType a on a.idTaskType=t.idTaskType where t.idCreator=@teammember order by t.deadline desc;";
                    dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        var task = new Models.Task();
                        task.idTask = (int)dr["idTask"];
                        task.name = dr["Name"].ToString();
                        task.description = dr["description"].ToString();
                        task.deadline = DateTime.Parse(dr["Deadline"].ToString());
                        task.nameoftheproject = dr["projectname"].ToString();
                        task.tasktype = dr["tasktype"].ToString();

                        listcreatedby.Add(task);

                    }


                    var teammember = new TeamMember();
                    teammember.listofTasksAssignedto = listassignedTo;
                    teammember.listofTasksCreatedBy = listcreatedby;

                    dr.Close();
                    com.CommandText = "select * from TeamMember where idTeamMember =@teammember;";
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        teammember.idTeamMember = (int)dr["idTeamMember"];
                        teammember.FirstName = dr["FirstName"].ToString();
                        teammember.LastName = dr["LastName"].ToString();
                        teammember.email = dr["Email"].ToString();
                    }

                    //return Ok(teammember);
                    return teammember;

                }
                else
                {
                    //return BadRequest("There is no such record !");
                    throw new Exception("There is no such record !");


                }
            }


        }
    }
}
