using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace CUapp
{
    public partial class Recipes : ContentPage
    {

        public List<recipeModel> searchBarFilteredItems;
        public List<recipeModel> labelSearchFilteredItems;

        public Recipes()
        {
            InitializeComponent();

            collectionView.ItemsSource = publicVariables.recipes;
            BindingContext = this;

        }

        private async void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = e.CurrentSelection.FirstOrDefault() as recipeModel;
            var view = sender as CollectionView;
            if (view.SelectedItem != null)
            {
                await Navigation.PushAsync(new recipePage(a));
                view.SelectedItem = null;
            }
        }

        void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = string.Empty;
            }

            searchTerm = searchTerm.ToLowerInvariant();

            searchBarFilteredItems = recipeList.list.Where(value =>
            value.title.ToLowerInvariant().Contains(searchTerm)).ToList();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                searchBarFilteredItems = recipeList.list.ToList();
            }

            if (labelSearchFilteredItems == null)
            {
                foreach (var value in recipeList.list)
                {
                    if (searchBarFilteredItems.Contains(value) == false)
                    {
                        publicVariables.recipes.Remove(value);
                    }
                    
                    else if (!publicVariables.recipes.Contains(value))
                    {
                        
                         publicVariables.recipes.Add(value);
                        
                    }
                }
            }
            else
            {
                foreach (var value in recipeList.list)
                {
                    if (searchBarFilteredItems.Contains(value) == false)
                    {
                        publicVariables.recipes.Remove(value);
                    }
                    else if (labelSearchFilteredItems.Contains(value) && !publicVariables.recipes.Contains(value))
                    {
                        publicVariables.recipes.Add(value);
                    }

                }
        
            }
            
        }

        

        void labelSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = string.Empty;
            }

            searchTerm = searchTerm.ToLowerInvariant();

            char[] seperator = new char[] { ' ', ',' };

            var labelsInSearch = searchTerm.ToString().Split(seperator, StringSplitOptions.RemoveEmptyEntries).ToList();

            labelSearchFilteredItems = recipeList.list.Where(value =>
            methods.stringListContains(value.labels,labelsInSearch)).ToList();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                labelSearchFilteredItems = recipeList.list.ToList();
            }


            
            foreach (var value in recipeList.list)
            {
                if (!labelSearchFilteredItems.Contains(value))
                {
                    publicVariables.recipes.Remove(value);
                }
                else if (!publicVariables.recipes.Contains(value) && !publicVariables.recipes.Contains(value))
                {
                    publicVariables.recipes.Add(value);
                }
            }
        }

        void labelbtn_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new labelPopup());
        }
        
    }
}
