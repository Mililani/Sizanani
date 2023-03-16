using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using Microsoft.Extensions.Configuration;

namespace SizaApplication.Models
{
    public class Sqlcontrol
    {
        private readonly string _connectionString
            ;
        public Sqlcontrol(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("VehicleDbConnection");

        }

        public void Register(Vehicle vehicle)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Vehicles (Type, RegistrationNumber, Model, WeightInTons) VALUES (@Type, @RegistrationNumber, @Model, @WeightInTons)", connection);
                command.Parameters.AddWithValue("@Type", vehicle.Type);
                command.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
                command.Parameters.AddWithValue("@Model", vehicle.Model);
                command.Parameters.AddWithValue("@WeightInTons", vehicle.WeightInTons);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void AddContractor(Contractor contractor)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Contractors (Name, Email, Phone) VALUES (@Name, @Email, @Phone)", connection);
                command.Parameters.AddWithValue("@Name", contractor.Name);
                command.Parameters.AddWithValue("@Email", contractor.Email);
                command.Parameters.AddWithValue("@Phone", contractor.Phone);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


       
    }
}
