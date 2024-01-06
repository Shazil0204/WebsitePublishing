using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Microsoft.Data.SqlClient;

namespace simpleaspwithMSSQL.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{

		}

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public int UserAge { get; set; }

        public void OnPost()
		{
            var config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();

            var connectionString = config.GetConnectionString("connectionstring");
            string username = UserName;
            int age = UserAge;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("insertuser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Define parameters
                    command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;
                    command.Parameters.Add("@age", SqlDbType.Int).Value = age;

                    // Execute the procedure
                    int result = (int)command.ExecuteScalar();

                    if (result == 1)
                    {
                        Console.WriteLine("User inserted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Error inserting user.");
                    }
                }
            }
        }
	}
}
