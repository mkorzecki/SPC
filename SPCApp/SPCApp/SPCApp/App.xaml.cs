using SPCApp.Data;
using System;
using Xamarin.Forms;
using System.IO;

namespace SPCApp
{
    public partial class App : Application
    {


        static ProductsLocalDatabase database;

        public static ProductsLocalDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ProductsLocalDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Products.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
