using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CUapp
{
    public class recipeModel
    {  
        public string title { get; set; }
        public int difficulty { get; set; }
        public string foodType { get; set; }
        public string foodAmount { get; set; }
        public List<string> labels = new List<string>();

        public int i { get; set; }
        public string buttonImg { get; set; }

        public string prepTime { get; set; }
        public string cookTime { get; set; }
        public string creator { get; set; }
        
        public List<ingredientBlock> ingredients = new List<ingredientBlock>();
        public List<ingredientBlock> howto = new List<ingredientBlock>();
        public List<ingredientBlock> nutrTips = new List<ingredientBlock>();
        public List<ingredientBlock> cookTips = new List<ingredientBlock>();
    }
}
