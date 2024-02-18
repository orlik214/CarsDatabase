using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarsDatabase.Views
{
    public interface ICarView
    {
        //properties
        string CarId { get; set; }
        string CarMake { get; set; }
        string CarModel { get; set; }
        string CarColor { get; set; }
        string CarEngine { get; set; }
        string CarYear { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccesful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        //Methods
        void SetCarListBindingSource(BindingSource carList);
        void Show();

    }
}
