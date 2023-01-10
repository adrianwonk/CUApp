using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace CUapp
{
    public abstract class publicVariables : INotifyPropertyChanged
    {
        public static ObservableCollection<recipeModel> favorites
        {
            get
            {
                return ((App)Application.Current).favorites;
            }
        }

        public static ObservableCollection<recipeModel> recipes
        {
            get
            {
                return ((App)Application.Current).recipes;
            }
        }

        public static ObservableCollection<checkboxBlock> healthCheckBoxes
        {
            get
            {
                return ((App)Application.Current).healthCheckBoxes;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
