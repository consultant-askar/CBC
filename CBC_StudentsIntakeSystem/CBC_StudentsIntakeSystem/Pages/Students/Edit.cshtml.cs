using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static CBC_StudentsIntakeSystem.Pages.Students.IndexModel;

namespace CBC_StudentsIntakeSystem.Pages.Students
{
    public class EditModel : PageModel
    {
        public List<StudentInfo> studentsList = new List<StudentInfo>();
        public StudentInfo studentInfo = new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\mssqlserver2;Initial Catalog=mystore;" +
                    "Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from students where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                
                                studentInfo.id = "" + reader.GetInt32(0);
                                studentInfo.name = reader.GetString(1);
                                studentInfo.email = reader.GetString(2);
                                studentInfo.phone = reader.GetString(3);
                                
                               
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            studentInfo.id = Request.Form["id"];
            studentInfo.name = Request.Form["name"];
            studentInfo.email = Request.Form["email"];
            studentInfo.phone = Request.Form["phone"];
            if (studentInfo.name.Length == 0)
            {
                errorMessage = "Student Name field is required";
                return;
            }
            try
            {

                String connectionString = "Data Source=.\\mssqlserver2;Initial Catalog=mystore;" +
                   "Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "update students set name=@name, email=@email, phone=@phone where id=@id";
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", studentInfo.id);
                        command.Parameters.AddWithValue("@name", studentInfo.name);
                        command.Parameters.AddWithValue("@email", studentInfo.email);
                        command.Parameters.AddWithValue("@phone", studentInfo.phone);
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            //clear old fields
            studentInfo.name = "";
            studentInfo.email = "";
            studentInfo.phone = "";
            successMessage = "Student info updated successfully to the database";
        }
    }
}
