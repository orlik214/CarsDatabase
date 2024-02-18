using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarsDatabase.Models;
using CarsDatabase.Presenters;
using CarsDatabase._Repositories;
using CarsDatabase.Views;
using System.Configuration;
using System.Data.SqlClient;

namespace CarsDatabase
{
    internal static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            ICarView view = new CarView();
            ICarRepository repository = new CarRepository(sqlConnectionString);
            new CarPresenter(view, repository);
            Application.Run((Form)view);
        }
    }
}
