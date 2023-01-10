using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace CUapp
{
    public partial class Favorites : ContentPage
    {

        public List<recipeModel> searchBarFilteredItems;
        public List<recipeModel> labelSearchFilteredItems;

        public Favorites()
        {
            InitializeComponent();
            BindingContext = this;
            collectionView.ItemsSource = publicVariables.favorites;
        }

        private async void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = e.CurrentSelection.FirstOrDefault() as recipeModel;
            var view = sender as CollectionView;
            if (view.SelectedItem != null)
            {
                Navigation.PushAsync(new recipePage(a));
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

            searchBarFilteredItems = recipeList.favorites.Values.Where(value =>
            value.title.ToLowerInvariant().Contains(searchTerm)).ToList();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                searchBarFilteredItems = recipeList.favorites.Values.ToList();
            }

            if (labelSearchFilteredItems == null)
            {
                foreach (var value in recipeList.favorites.Values)
                {
                    if (searchBarFilteredItems.Contains(value) == false)
                    {
                        publicVariables.favorites.Remove(value);
                    }

                    else if (!publicVariables.favorites.Contains(value))
                    {

                        publicVariables.favorites.Add(value);

                    }
                }
            }
            else
            {
                foreach (var value in recipeList.favorites.Values)
                {
                    if (searchBarFilteredItems.Contains(value) == false)
                    {
                        publicVariables.favorites.Remove(value);
                    }
                    else if (labelSearchFilteredItems.Contains(value) && !publicVariables.favorites.Contains(value))
                    {
                        publicVariables.favorites.Add(value);
                    }

                }

            }

        }

        void labelbtn_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new labelPopup());
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

            labelSearchFilteredItems = recipeList.favorites.Values.Where(value =>
            methods.stringListContains(value.labels, labelsInSearch)).ToList();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                labelSearchFilteredItems = recipeList.favorites.Values.ToList();
            }


            if (searchBarFilteredItems == null)
            {
                foreach (var value in recipeList.favorites.Values)
                {
                    if (!labelSearchFilteredItems.Contains(value))
                    {
                        publicVariables.favorites.Remove(value);
                    }
                    else if (!publicVariables.favorites.Contains(value))
                    {
                        publicVariables.favorites.Add(value);
                    }
                }
            }

            else
            {
                foreach (var value in recipeList.favorites.Values)
                {
                    if (labelSearchFilteredItems.Contains(value) == false)
                    {
                        publicVariables.favorites.Remove(value);
                    }
                    else if (labelSearchFilteredItems.Contains(value) && !publicVariables.favorites.Contains(value))
                    {
                        publicVariables.favorites.Add(value);
                    }
                }
            }

        }

    }
}
