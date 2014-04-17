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
    public class DBCompanyAccessor : IEntityAccessor<Company>
    {
        private SqlCeConnection connection;
        private string connectionString;

        public DBCompanyAccessor()
        {
            connectionString = ConfigurationManager.ConnectionStrings["AppDBConnectionString"].ConnectionString;
 
        }

        ~DBCompanyAccessor()
        {
            connection.Close();
        }


        public List<Company> GetAll()
        {
            using (connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                SqlCeCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Company";
                SqlCeDataReader reader = cmd.ExecuteReader();


                List<Company> result = new List<Company>();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string address = reader.GetString(2);
                    string phone = reader.GetString(3);

                    Company company = new Company(name, address, phone, id);
                    result.Add(company);
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

                using (SqlCeCommand cmd = new SqlCeCommand("DELETE FROM Company WHERE CompanyId = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }


        public void Insert(Company item)
        {
            using (connection = new SqlCeConnection(connectionString))
            {

                using (SqlCeCommand cmd = new SqlCeCommand("INSERT INTO Company (Name, Address, Phone) VALUES (@Name, @Address, @Phone)", connection))
                {
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@Address", item.Address);
                    cmd.Parameters.AddWithValue("@Phone", item.Phone);
                    

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }


        public Company GetById(int id)
        {
            using (connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                SqlCeCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Company WHERE CompanyId = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                SqlCeDataReader reader = cmd.ExecuteReader();

                Company resultCompany = null;
                
                if (reader.Read())
                {
                    string name = reader.GetString(1);
                    string address = reader.GetString(2);
                    string phone = reader.GetString(3);

                    resultCompany = new Company(name, address, phone, id);
                }

                cmd.ExecuteNonQuery();
                connection.Close();
                reader.Close();

                return resultCompany;
            }
        }

    }
}
	
 
