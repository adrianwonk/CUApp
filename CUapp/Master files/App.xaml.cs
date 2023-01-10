 using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CUapp
{
    public partial class App : Application
    {
        public ObservableCollection<recipeModel> favorites { get; set; }
        public ObservableCollection<recipeModel> recipes { get; set; }
        public ObservableCollection<checkboxBlock> healthCheckBoxes { get; set; }

        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());
            MainPage = new MainPage();
            
        }

        protected override void OnStart()
        {
            healthCheckBoxes = new ObservableCollection<checkboxBlock>();
            GoogleSheets sheets = new GoogleSheets();
            favorites = new ObservableCollection<recipeModel>(recipeList.favorites.Values);
            recipes = new ObservableCollection<recipeModel>(recipeList.list);
            
            // inside healthCheckBoxes(), put the healthcheckbox list.

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
