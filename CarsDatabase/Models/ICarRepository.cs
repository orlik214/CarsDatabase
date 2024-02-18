using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsDatabase.Models
{
    public interface ICarRepository
    {
        void Add(CarModel carModel);
        void Edit(CarModel carModel);
        void Delete(int id);
        IEnumerable<CarModel> GetAll();
        IEnumerable<CarModel> getByValue(string value); //search
    }
}
