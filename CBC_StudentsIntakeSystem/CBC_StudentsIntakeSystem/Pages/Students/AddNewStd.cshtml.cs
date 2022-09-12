using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CBC_StudentsIntakeSystem.Pages.Students.IndexModel;
using System.Data.SqlClient;

namespace CBC_StudentsIntakeSystem.Pages.Students
{
    public class AddNewStdModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            studentInfo.name = Request.Form["name"];
            studentInfo.email = Request.Form["email"];
            studentInfo.phone = Request.Form["phone"];
            if (studentInfo.name.Length == 0)
            {
                errorMessage = "Student Name is required";
                return;
            }
            //save new student into DB
            try
            {
                String connectionString = "Data Source=.\\mssqlserver2;Initial Catalog=mystore;" +
                   "Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "insert into students" +
                       " (name, email, phone) values" +
                        "(@name, @email, @phone)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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
            successMessage = "New student added successfully to the database";
        }


    }
}
