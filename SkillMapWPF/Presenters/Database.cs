using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SkillMapWPF.Presenters
{
    class Database
    {
        private readonly string _connectionString =
    @"Data Source=LAPTOP-91IHV11B;Initial Catalog=SkillMapDataBase;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        //private readonly string _connectionString =
        //    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public DataTable GetData(string query)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }
        public DataTable GetUsers()
        {
            string query = @"
                SELECT FirstName, LastName
                FROM [User]
                ORDER BY NEWID()";

            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(table);
            }

            return table;
        }
        public DataRow LoginAndGetProfile(string email, string password)
        {
            // Используем ваше представление для получения ID и Роли
            string query = "SELECT UserID, FirstName, RoleName, RoleId FROM UserProfile WHERE Email = @email AND PasswordHash = @pass";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pass", password); // В идеале здесь должен быть хэш

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }
        public int RegisterUser(string firstName, string lastName, string email, string phone, string password, string roleCode)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Указываем имя вашей процедуры
                using (SqlCommand command = new SqlCommand("AddNewUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Используем SqlDbType.NVarChar, чтобы кириллица не превратилась в вопросы
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = firstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = lastName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = email;
                    command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 15).Value = phone;
                    command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 200).Value = password;
                    command.Parameters.Add("@RoleCode", SqlDbType.NVarChar, 20).Value = roleCode;

                    connection.Open();
                    // Выполняем процедуру и получаем результат SELECT SCOPE_IDENTITY()
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }
        public DataTable GetResumesPaged(int skip, int take)
        {
            // Используем ваше представление ResumeDetails
            string query = @"
        SELECT ResumeId, Specialty, DesiredSalary, UserExperience 
        FROM ResumeDetails 
        ORDER BY CreatedDate DESC 
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(table);
                    return table;
                }
            }
        }
        public DataTable GetVacanciesPaged(int skip, int take)
        {
            // Запрос к вашему представлению VacancyDetails
            string query = @"
     SELECT VacancyId, VacancyTitle, CompanyName, SalaryRange, Location, WorkFormat 
    FROM VacancyDetails 
    ORDER BY VacancyId 
    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        public int CreateResume(int userId, string specialty, string description, int experience, int salary)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("CreateResume", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    command.Parameters.Add("@Specialty", SqlDbType.NVarChar, 100).Value = specialty;
                    command.Parameters.Add("@ResumeDescription", SqlDbType.NVarChar, 1000).Value = description;
                    command.Parameters.Add("@UserExperience", SqlDbType.Int).Value = experience;
                    command.Parameters.Add("@DesiredSalary", SqlDbType.Int).Value = salary;
                    // Статус по умолчанию 'Активное' прописан в самой процедуре

                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }
    }
}
