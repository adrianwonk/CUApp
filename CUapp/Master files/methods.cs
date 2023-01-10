using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;


namespace CUapp
{
    public class methods
    {
        public static bool stringListContains(List<string> list1, List<string> list2)
        {
            int result = 0;
            foreach (string value in list1)
            {
                if (list2.Contains(value))
                {
                    result += 1;
                }
            }
            if (result == list1.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // inputs a str[] and returns a ingredientBlock list
        // line could be empty
        // elements within lineArr could be empty
        public static List<ingredientBlock> stringArrToIngredientBlockList(string[] stringArr, bool split)
        {
            List<ingredientBlock> result = new List<ingredientBlock>();
            string value_ = "";
            string step_ = "";
            string[] stepAndLine;

            foreach (var line in stringArr)
            {
                if (!(String.IsNullOrEmpty(line) || String.IsNullOrWhiteSpace(line)))
                {

                    // seperates the step #. with the content
                    if (split)
                    {
                        stepAndLine = line.Split(' ');
                        step_ = stepAndLine[0];

                        // loops through until finds an non-empty/whitespace element
                        if (stepAndLine.Length >= 2)
                        {
                            for (int i = 1; i < stepAndLine.Length; i++)
                            {
                                if (!(String.IsNullOrEmpty(stepAndLine[i]) || String.IsNullOrWhiteSpace(stepAndLine[i])))
                                {
                                    value_ = stepAndLine[i];
                                    break;
                                }
                            }
                        }

                        result.Add(new ingredientBlock { step = step_, text = value_ });
                    }
                    else
                    {
                        result.Add(new ingredientBlock { text = line });
                    }
                    
                }
            }

            return result;
        }

        public static void updateList()
        {
            //loop through recipeList.list
            foreach (recipeModel value in recipeList.list)
            {
                //if value.difficulty is in diff.Array (if the difficulty is right)
                if (labelPopup.difficultyTag.Contains(value.difficulty))
                {
                    //checks if label array is in value.labels
                    if (stringListContains(labelPopup.labels, value.labels))
                    {
                        //adds value to the collection
                        if (!publicVariables.recipes.Contains(value))
                        {
                            publicVariables.recipes.Add(value);
                        }

                        // adds value to the favorites col.
                        if (!publicVariables.favorites.Contains(value) && recipeList.favorites.ContainsKey(value.i))
                        {
                            publicVariables.favorites.Add(value);
                        }
                    }
                    else
                    {
                        // checks if the recipeModel is in recipes and removes it
                        if (publicVariables.recipes.Contains(value))
                        {
                            publicVariables.recipes.Remove(value);
                        }

                        //checks if the recipeModel is in favorites and removes it
                        if (publicVariables.favorites.Contains(value) && recipeList.favorites.ContainsKey(value.i))
                        {
                            publicVariables.favorites.Remove(value);
                        }
                    }
                }

                else
                {
                    if (publicVariables.recipes.Contains(value))
                    {
                        publicVariables.recipes.Remove(value);
                    }
                    if (publicVariables.favorites.Contains(value) && recipeList.favorites.ContainsKey(value.i))
                    {
                        publicVariables.favorites.Remove(value);
                    }
                }
            }                

        }

        public static string diff(int x)
        {
            switch(x){
                case 1:
                    return "新手";
                case 2:
                    return "容易";
                case 3:
                    return "平常";
                case 4:
                    return "困難";
                case 5:
                    return "專業";
                default:
                    return "還沒有難度分別";
            }
        }
    }
}
