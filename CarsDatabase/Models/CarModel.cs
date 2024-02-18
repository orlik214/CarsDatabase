 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CarsDatabase.Models
{
    public class CarModel
    {
        //Fields
        private int id;
        private string make;
        private string model;
        private string color;
        private string engine;
        private string year;

        //Properties - Validations
        [DisplayName("ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("Marka")]
        [Required(ErrorMessage ="Podaj Marke samochodu")]
        public string Make
        {
            get { return make; }
            set {  make = value; }
        }

        [DisplayName("Model")]
        [Required(ErrorMessage = "Podaj model samochodu")]
        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        [DisplayName("Kolor")]
        [Required(ErrorMessage = "Podaj kolor samochodu")]
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        [DisplayName("Silnik")]
        [Required(ErrorMessage = "Podaj rodzaj silnika samochodu")]
        public string Engine
        {
            get { return engine; }
            set { engine = value; }
        }

        [DisplayName("Rok produkcji")]
        [Required(ErrorMessage = "Podaj rok produkcji samochodu")]
        public string Year
        {
            get { return year; }
            set { year = value; }
        }
    }
}
