using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using CarsDatabase.Models;
using CarsDatabase.Views;

namespace CarsDatabase.Presenters
{
    internal class CarPresenter
    {
        //Fields
        private ICarView view;
        private ICarRepository repository;
        private BindingSource carsBindingSource;
        private IEnumerable<CarModel> carList;

        //Constructor
        public CarPresenter(ICarView view, ICarRepository repository)
        {
            this.carsBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            this.view.SearchEvent += SearchCar;
            this.view.AddNewEvent += AddNewCar;
            this.view.EditEvent += LoadSelectedCarToEdit;
            this.view.DeleteEvent += DeleteSelectedCar;
            this.view.SaveEvent += SaveCar;
            this.view.CancelEvent += CancelAction;
            //Set cars binding source
            this.view.SetCarListBindingSource(carsBindingSource);
            //Load car list view
            LoadAllCarList();
            this.view.Show();
        }

        //Methods
        private void LoadAllCarList()
        {
            carList = repository.GetAll();
            carsBindingSource.DataSource = carList;
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveCar(object sender, EventArgs e)
        {
            var model = new CarModel();
            model.Id = Convert.ToInt32(view.CarId);
            model.Make = view.CarMake;
            model.Model = view.CarModel;
            model.Color = view.CarColor;
            model.Engine = view.CarEngine;
            model.Year = view.CarYear;
            try 
            {
                new Common.ModelDataValidation().Validate(model);
                if(view.IsEdit)
                {
                    repository.Edit(model);
                    view.Message = "Samochód został zedytowany";
                }
                else
                {
                    repository.Add(model);
                    view.Message = "Samochód został dodany";
                }
                view.IsSuccesful = true;
                LoadAllCarList();
                CleanViewFields();
            }
            catch (Exception ex) 
            {
                view.IsSuccesful = false;
                view.Message = ex.Message;
            }
        }

        private void CleanViewFields()
        {
            view.CarId = "0";
            view.CarMake = "";
            view.CarModel = "";
            view.CarColor = "";
            view.CarEngine = "";
            view.CarYear = "";
        }

        private void DeleteSelectedCar(object sender, EventArgs e)
        {
            try
            {
                var car = (CarModel)carsBindingSource.Current;
                repository.Delete(car.Id);
                view.IsSuccesful = true;
                view.Message = "Samochód został usunięty";
                LoadAllCarList();
            }
            catch (Exception ex)
            {
                view.IsSuccesful = false;
                view.Message = "Pojawił się problem, samochód nie zostanie usunięty";
            }
        }

        private void LoadSelectedCarToEdit(object sender, EventArgs e)
        {
            var car = (CarModel)carsBindingSource.Current;
            view.CarId= car.Id.ToString();
            view.CarMake = car.Make;
            view.CarModel = car.Model;
            view.CarColor = car.Color;
            view.CarEngine = car.Engine;
            view.CarYear = car.Year;
            view.IsEdit = true;
        }

        private void AddNewCar(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        private void SearchCar(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                carList = repository.getByValue(this.view.SearchValue);
            else carList = repository.GetAll();
            carsBindingSource.DataSource = carList;
        }
    }
}
