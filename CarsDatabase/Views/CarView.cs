using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarsDatabase.Views
{
    public partial class CarView : Form, ICarView
    {
        //Fields
        private string message;
        private bool isSuccesful;
        private bool isEdit;

        //Constructor
        public CarView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(Detail);
        }

        private void AssociateAndRaiseViewEvents()
        {
            //Search
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };
            //Add
            btnAddNew.Click += delegate 
            { 
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(CarList);
                tabControl1.TabPages.Add(Detail);
                CarList.Text = "Dodaj nowy samochód";
            };

            //Edit
            btnEdit.Click += delegate 
            { 
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(CarList);
                tabControl1.TabPages.Add(Detail);
                CarList.Text = "Edytuj samochód";
            };
            //Save
            btnSave.Click += delegate 
            { 
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccesful)
                {
                    tabControl1.TabPages.Remove(Detail);
                    tabControl1.TabPages.Add(CarList);
                }
                MessageBox.Show(Message);
            };
            //Cancel
            btnCancel.Click += delegate 
            { 
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(Detail);
                tabControl1.TabPages.Add(CarList);
            };
            //Delete
            btnDelete.Click += delegate 
            { 
                DeleteEvent?.Invoke(this, EventArgs.Empty);
                MessageBox.Show(Message);
            };
        }

        public string CarId
        {
            get 
            {
                return txtCarId.Text;
            }
            set 
            { 
                txtCarId.Text = value;
            }
        }
        public string CarMake
        {
            get 
            {
                return txtCarMake.Text; 
            }
            set 
            { 
                txtCarMake.Text = value;
            }
        }
        public string CarModel 
        { 
            get 
            {
                return txtCarModel.Text;
            } 
            set 
            { 
                txtCarModel.Text = value;
            } 
        }
        public string CarColor 
        { 
            get 
            {
                return txtCarColor.Text; 
            } 
            set 
            { 
                txtCarColor.Text = value;
            }
        }
        public string CarEngine 
        { 
            get 
            {
                return txtCarEngine.Text;
            } 
            set 
            {
                txtCarEngine.Text = value;
            } 
        }
        public string CarYear 
        { 
            get 
            {
                return txtCarYear.Text;
            } 
            set 
            {
                txtCarYear.Text = value;
            } 
        }
        public string SearchValue 
        { 
            get 
            {
                return txtSearch.Text;
            } 
            set 
            {
                txtSearch.Text = value;
            } 
        }
        public bool IsEdit 
        { 
            get 
            {
                return isEdit;
            } 
            set 
            {
                isEdit = value;
            } 
        }
        public bool IsSuccesful 
        { 
            get 
            {
                return isSuccesful;
            } 
            set 
            {
                isSuccesful = value;
            } 
        }
        public string Message 
        { 
            get 
            {
                return message;
            } 
            set 
            {
                message = value;
            } 
        }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public void SetCarListBindingSource(BindingSource carList)
        {
            dataGridView.DataSource = carList;
        }

       
    }
}
