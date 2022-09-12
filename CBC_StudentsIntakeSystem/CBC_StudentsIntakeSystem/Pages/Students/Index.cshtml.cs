using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CBC_StudentsIntakeSystem.Pages.Students
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo>studentsList=new List<StudentInfo>();   
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\mssqlserver2;Initial Catalog=mystore;" +
                    "Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from students";
                    using(SqlCommand command=new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader=command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo studentInfo = new StudentInfo();
                                studentInfo.id = "" + reader.GetInt32(0);
                                studentInfo.name = reader.GetString(1);
                                studentInfo.email = reader.GetString(2);
                                studentInfo.phone = reader.GetString(3);
                                // add studentInfo obj to the studenlist
                                studentsList.Add(studentInfo);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }

        public class StudentInfo
        {
            public String id;
            public String name;
            public String email;
            public String phone;

        }
    }
}
