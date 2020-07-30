using SPCApp.Data;
using System;
using Xamarin.Forms;
using System.IO;
using SPCApp.Models;

namespace SPCApp
{
    public partial class App : Application
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        static ProductsDatabase database;

        public static ProductsDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ProductsDatabase("Server=db4free.net;Port=3306;database=spcdatabase;User Id=spcuser;Password=spcpassword;charset=utf8");                   
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

        protected async override void OnSleep()
        {
            await firebaseHelper.CreateProductsBackUp();
        }

        protected override void OnResume()
        {
        }
    }
}
