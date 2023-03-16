using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SizaApplication.Models;
using System.Data.SqlClient;

namespace SizaApplication.Controllers
{
    public class FunctionController : Controller

    {
        private readonly Sqlcontrol _context;
        private readonly string _connectionString;

        public FunctionController(Sqlcontrol context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("VehicleDbConnection");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateVehcile(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Register(vehicle);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(vehicle);
            }
        }


        [HttpPost]
        public IActionResult CreateContractor(Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                _context.AddContractor(contractor);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(contractor);
            }
        }



        public ActionResult DisplayVechciles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Vehicles", connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle();
                    vehicle.Id = (int)reader["VehicleId"];
                    vehicle.Type = (string)reader["Type"];
                    vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                    vehicle.Model = (string)reader["Model"];
                    vehicle.WeightInTons = (decimal)reader["WeightInTons"];

                    vehicles.Add(vehicle);
                }

                reader.Close();
            }

            return View(vehicles);
        }

        public ActionResult Edit(int id)
        {

            Vehicle vehicle = new Vehicle();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Vehicles WHERE VehicleId = @VehicleId", connection);
                command.Parameters.AddWithValue("@VehicleId", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    vehicle.Id = (int)reader["VehicleId"];
                    vehicle.Type = (string)reader["Type"];
                    vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                    vehicle.Model = (string)reader["Model"];
                    vehicle.WeightInTons = (decimal)reader["WeightInTons"];
                }

                reader.Close();
            }

            return View(vehicle);
        }
        [HttpPost]
        public ActionResult Update(Vehicle vehicle)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UPDATE Vehicles SET Type = @Type, RegistrationNumber = @RegistrationNumber, Model = @Model, WeightInTons = @WeightInTons WHERE VehicleId = @VehicleId", connection);
                command.Parameters.AddWithValue("@VehicleId", vehicle.Id);
                command.Parameters.AddWithValue("@Type", vehicle.Type);
                command.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
                command.Parameters.AddWithValue("@Model", vehicle.Model);
                command.Parameters.AddWithValue("@WeightInTons", vehicle.WeightInTons);

                command.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Vehicle vehicle = new Vehicle();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Vehicles WHERE VehicleId = @VehicleId", connection);
                command.Parameters.AddWithValue("@VehicleId", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    vehicle.Id = (int)reader["VehicleId"];
                    vehicle.Type = (string)reader["Type"];
                    vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                    vehicle.Model = (string)reader["Model"];
                    vehicle.WeightInTons = (decimal)reader["WeightInTons"];
                }

                reader.Close();
            }

            return View(vehicle);
        }



        [HttpPost]
        public ActionResult ComfirmDelete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM Vehicles WHERE VehicleId = @VehicleId", connection);
                command.Parameters.AddWithValue("@VehicleId", id);

                command.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }



        public ActionResult ViewVehicles()
        {
            List<Contractor> contractors = new List<Contractor>();
            List<Vehicle> vehicles = new List<Vehicle>();

            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Contractors", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Contractor contractor = new Contractor();
                    contractor.Id = (int)reader["ContractorId"];
                    contractor.Name = (string)reader["Contractor Name"];
                    contractors.Add(contractor);
                }

                reader.Close();

                command = new SqlCommand("SELECT * FROM Vehicles", connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle();
                    vehicle.Id = (int)reader["VehicleId"];
                    vehicle.contractorId = (int)reader["ContractorId"];
                    vehicle.Type = (string)reader["VehicleType"];
                    vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                    vehicle.Model = (string)reader["Model"];
                    vehicle.WeightInTons = (decimal)reader["Weight"];
                    vehicles.Add(vehicle);
                }

                reader.Close();
            }

            ViewBag.Contractors = contractors;
            ViewBag.Vehicles = vehicles;

            return View();
        }

        public ActionResult SummarizeContractors()
        {
            List<Contractor> contractors = new List<Contractor>();
            List<Vehicle> vehicles = new List<Vehicle>();

           
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Contractors", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Contractor contractor = new Contractor();
                    contractor.Id = (int)reader["ContractorId"];
                    contractor.Name = (string)reader["Contractor Name"];
                    contractors.Add(contractor);
                }

                reader.Close();

                command = new SqlCommand("SELECT * FROM Vehicles", connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle();
                    vehicle.Id = (int)reader["VehicleId"];
                    vehicle.contractorId = (int)reader["ContractorId"];
                    vehicle.Type = (string)reader["VehicleType"];
                    vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                    vehicle.Model = (string)reader["Model"];
                    vehicle.WeightInTons = (decimal)reader["Weight"];
                    vehicles.Add(vehicle);
                }

                reader.Close();
            }

            var summarizedData = from c in contractors
                                 join v in vehicles on c.Id equals v.contractorId into g
                                 select new
                                 {
                                     ContractorName = c.Name,
                                     NumberOfVehicles = g.Count(),
                                     TotalTons = g.Sum(x => x.WeightInTons)
                                 };

            ViewBag.SummarizedData = summarizedData;

            return View();
        }
    }
}
