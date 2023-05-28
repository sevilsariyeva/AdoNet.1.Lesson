using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet._1.Lesson.ViewModels
{
    
    public class HomeUCViewModel
    {
        
        public HomeUCViewModel()
        {
            using (var conn = new SqlConnection())
            {
                var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                conn.ConnectionString = connectionString;
                conn.Open();


                string query = $@" INSERT INTO Authors(Id,FirstName,LastName)
                    VALUES(@id,@firstName,@lastName)";

                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = 1111;

                var paramName = new SqlParameter();
                paramName.ParameterName = "@firstName";
                paramName.SqlDbType = SqlDbType.NVarChar;
                paramName.Value = "Elchin";

                var paramSurname = new SqlParameter();
                paramSurname.ParameterName = "@lastName";
                paramSurname.SqlDbType = SqlDbType.NVarChar;
                paramSurname.Value = "Quliyev";

                using (var command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add(paramId);
                    command.Parameters.Add(paramName);
                    command.Parameters.Add(paramSurname);

                    var result = command.ExecuteNonQuery();
                    Console.WriteLine($"{result} row affected");
                }

            }
        }
    }
    
}
