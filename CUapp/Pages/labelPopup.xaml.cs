using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace CUapp
{
    public partial class labelPopup : PopupPage
    {
        public static List<int> difficultyTag = new List<int> { };
        public static List<string> labels = new List<string> { };

        public static bool diff1 = true;
        public static bool diff2 = true;
        public static bool diff3 = true;
        public static bool diff4 = true;
        public static bool diff5 = true;

        public labelPopup()
        {
            InitializeComponent();

            //collectionView.ItemsSource = publicVariables.recipes;
            healthTagCol.ItemsSource = publicVariables.healthCheckBoxes;

            BindingContext = this;
            d1.IsChecked = diff1;
            d2.IsChecked = diff2;
            d3.IsChecked = diff3;
            d4.IsChecked = diff4;
            d5.IsChecked = diff5;
        }

        void difficultyChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var classId = Convert.ToInt16(checkbox.ClassId);
            switch (classId)
            {
                case 1:
                    diff1 = e.Value;
                    break;
                case 2:
                    diff2 = e.Value;
                    break;
                case 3:
                    diff3 = e.Value;
                    break;
                case 4:
                    diff4 = e.Value;
                    break;
                case 5:
                    diff5 = e.Value;
                    break;
            }

            // Add difficulty to difficulty tag and update recipeList
            if (e.Value == true)
            {
                
                if (!difficultyTag.Contains(classId))
                {
                    difficultyTag.Add(classId);
                    methods.updateList();
                }
            }

            // Remove difficulty from difficulty tag and update recipeList
            else
            {
                if (difficultyTag.Contains(classId))
                {
                    difficultyTag.Remove(classId);
                    methods.updateList();
                }
            }
        }

        void labelChanged(object sender, CheckedChangedEventArgs e)
        {
            //check value
            var checkbox = (CheckBox)sender;
            var classId = checkbox.ClassId;

            if (e.Value)
            {
                if (!labels.Contains(classId) && !String.IsNullOrWhiteSpace(classId))
                {
                    labels.Add(classId);
                }
            }

            else if (labels.Contains(classId))
            {
                labels.Remove(classId);          
            }

            methods.updateList();
        }

    }
}
