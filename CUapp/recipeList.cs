using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CUapp
{
    public class recipeList
    {
        public static List<recipeModel> list = new List<recipeModel>();

        public static Dictionary<int, recipeModel> favorites = new Dictionary<int, recipeModel>();

    }
}
