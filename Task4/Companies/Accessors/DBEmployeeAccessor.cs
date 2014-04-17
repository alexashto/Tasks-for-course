using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.Data;
using Companies.DataEntity;
using System.Configuration;

namespace Companies.Accessors
{
    public class DBEmployeeAccessor : IEntityAccessor<Employee>
    {
        private SqlCeConnection connection;
        private string connectionString;

        public DBEmployeeAccessor()
        {
            connectionString = ConfigurationManager.ConnectionStrings["AppDBConnectionString"].ConnectionString;
 
        }

        ~DBEmployeeAccessor()
        {
            connection.Close();
        }


        public List<Employee> GetAll()
        {
            using (connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                SqlCeCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Employee";
                SqlCeDataReader reader = cmd.ExecuteReader();


                List<Employee> result = new List<Employee>();
                while (reader.Read())
                {
                    int id = reader.GetInt32(6);
                    string firstName = reader.GetString(0);
                    string lastName = reader.GetString(1);
                    DateTime birthDate = reader.GetDateTime(2);
                    int companyId = reader.GetInt32(3);
                    string position = reader.GetString(4);
                    DateTime employmentDate = reader.GetDateTime(5);

                    Employee emp = new Employee(firstName, lastName, birthDate, companyId, position, employmentDate, id);
                    result.Add(emp);
                }
                
                cmd.ExecuteNonQuery();
                connection.Close();
                reader.Close();
                

                return result;
            }
        }


        public void DeleteById(int id) 
        {
            using (connection = new SqlCeConnection(connectionString))
            {

                using (SqlCeCommand cmd = new SqlCeCommand("DELETE FROM Employee WHERE EmployeeId = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }


        public void Insert(Employee item)
        {
            using (connection = new SqlCeConnection(connectionString))
            {

                using (SqlCeCommand cmd = new SqlCeCommand("INSERT INTO Employee (FirstName, LastName, BirthDate, CompanyId, Position, EmploymentDate)"+
                                                            " VALUES (@FirstName, @LastName, @BirthDate, @CompanyId, @Position, @EmploymentDate)", connection))
                {
                    cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", item.LastName);
                    cmd.Parameters.AddWithValue("@BirthDate", item.BirthDate);
                    cmd.Parameters.AddWithValue("@CompanyId", item.CompanyId);
                    cmd.Parameters.AddWithValue("@Position", item.Position);
                    cmd.Parameters.AddWithValue("@EmploymentDate", item.EmploymentDate);
                    

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }


        public Employee GetById(int id)
        {
            using (connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                SqlCeCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Employee WHERE EmployeeId = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                SqlCeDataReader reader = cmd.ExecuteReader();

                Employee resultEmp = null;
                
                if (reader.Read())
                {
                    string firstName = reader.GetString(0);
                    string lastName = reader.GetString(1);
                    DateTime birthDate = reader.GetDateTime(2);
                    int companyId = reader.GetInt32(3);
                    string position = reader.GetString(4);
                    DateTime employmentDate = reader.GetDateTime(5);

                    resultEmp = new Employee(firstName, lastName, birthDate, companyId, position, employmentDate, id);
                }

                cmd.ExecuteNonQuery();
                connection.Close();
                reader.Close();

                return resultEmp;
            }
        }

    }
}
	
 
