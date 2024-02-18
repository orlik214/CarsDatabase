using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CarsDatabase.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CarsDatabase._Repositories
{
    public class CarRepository : BaseRepository, ICarRepository
    {
        //Constructor
        public CarRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //Methods
        public void Add(CarModel carModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Car values (@make, @model, @color, @engine, @year)";
                command.Parameters.Add("@make", SqlDbType.NVarChar).Value = carModel.Make;
                command.Parameters.Add("@model", SqlDbType.NVarChar).Value = carModel.Model;
                command.Parameters.Add("@color", SqlDbType.NVarChar).Value = carModel.Color;
                command.Parameters.Add("@engine", SqlDbType.NVarChar).Value = carModel.Engine;
                command.Parameters.Add("@year", SqlDbType.NVarChar).Value = carModel.Year;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {

                connection.Open();
                command.Connection = connection;
                command.CommandText = "delete from Car where Car_Id=@id";
                command.Parameters.Add("@id",SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(CarModel carModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update Car set Car_Make=@make,Car_Model=@model, Car_Color=@color, Car_Engine=@engine,Car_Year=@year where Car_Id=@id";
                command.Parameters.Add("@make", SqlDbType.NVarChar).Value = carModel.Make;
                command.Parameters.Add("@model", SqlDbType.NVarChar).Value = carModel.Model;
                command.Parameters.Add("@color", SqlDbType.NVarChar).Value = carModel.Color;
                command.Parameters.Add("@engine", SqlDbType.NVarChar).Value = carModel.Engine;
                command.Parameters.Add("@year", SqlDbType.NVarChar).Value = carModel.Year;
                command.Parameters.Add("id",SqlDbType.Int).Value = carModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<CarModel> GetAll()
        {
            var carList = new List<CarModel>();
            using (var connection= new SqlConnection(connectionString))
                using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Car order by Car_Id desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var carModel = new CarModel();
                        carModel.Id = (int)reader[0];
                        carModel.Make = reader[1].ToString();
                        carModel.Model = reader[2].ToString();
                        carModel.Color = reader[3].ToString();
                        carModel.Engine = reader[4].ToString();
                        carModel.Year = reader[5].ToString();
                        carList.Add(carModel);
                    }
                }
            }
            return carList;
        }

        public IEnumerable<CarModel> getByValue(string value)
        {
            var carList = new List<CarModel>();
            int carId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string carMake = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Select * from Car
                                         where Car_Id=@id or Car_Make like @make+'%'
                                         order by Car_Id desc";
                command.Parameters.Add("@id", SqlDbType.Int).Value=carId;
                command.Parameters.Add("@make", SqlDbType.NVarChar).Value = carMake;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var carModel = new CarModel();
                        carModel.Id = (int)reader[0];
                        carModel.Make = reader[1].ToString();
                        carModel.Model = reader[2].ToString();
                        carModel.Color = reader[3].ToString();
                        carModel.Engine = reader[4].ToString();
                        carModel.Year = reader[5].ToString();
                        carList.Add(carModel);
                    }
                }
            }
            return carList;
        }
    }
}
