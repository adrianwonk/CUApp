using System;

using Xamarin.Forms;

namespace CUapp
{
    public partial class recipePage : ContentPage
    {
        recipeModel recipe;

        string favoriteMessage = "Favorited :D";
        string nonFavMessage = "Click me to favorite!";

        bool favorited_ = false;
    

        public recipePage(recipeModel recipe_)
        {
            InitializeComponent();
            recipe = recipe_;

            impTags.Text = "重要🏷️:";
            difficulty.Text = $"難度: {methods.diff(recipe.difficulty)}";
            foodType.Text = $"餐類: {recipe.foodType}";
            foodAmount.Text = $"份量: {recipe.foodAmount}";
            prepTime.Text = $"準備時間: {recipe.prepTime}";
            cookingTime.Text = $"煮時間: {recipe.cookTime}";
            creator.Text = $"{recipe.creator}";

            int tagNum = recipe.labels.Count;

            for (var i = 0; i < tagNum; i++)
            {
                if (i != tagNum - 1)
                {
                    impTags.Text += $" {recipe.labels[i]},";
                }
                else
                {
                    impTags.Text += $" {recipe.labels[i]}";
                }
            }

            ingredients.ItemsSource = recipe.ingredients;
            pic.Source = recipe.buttonImg;

            if (ingredients.Height >= 500)
            {
                ingredients.HeightRequest = 1000;
            }
            else
            {
                ingredients.HeightRequest = 300;
            }

            howto.ItemsSource = recipe.howto;

            Title = recipe.title;

            if (recipeList.favorites.ContainsKey(recipe.i))
            {
                favorited_ = true;
                rectangle.Text = favoriteMessage;
            }

            else
            {
                favorited_ = false;
                rectangle.Text = nonFavMessage;

            }

            nutrTips.ItemsSource = recipe.nutrTips;

            cookTips.ItemsSource = recipe.cookTips;

            double w = Application.Current.MainPage.Width;
            pic.WidthRequest = w;

            BindingContext = this;
        }

        void favorited(object sender, EventArgs e)
        {

            if (!favorited_)
            {
                favorited_ = true;
                publicVariables.favorites.Add(recipe);

                recipeList.favorites[recipe.i] = recipe;
                rectangle.Text = favoriteMessage;
            }
            else
            {
                favorited_ = false;
                publicVariables.favorites.Remove(recipe);
                recipeList.favorites.Remove(recipe.i);
                rectangle.Text = nonFavMessage;
            }
        }
    }
}
