using System;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Google.Apis.Services;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Google.Apis.Util.Store;
using System.Threading;

using System.Collections.Generic;
using Xamarin.Forms;

namespace CUapp
{
    public class GoogleSheets
    {
        static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static readonly string SpreadsheetId = "1Me8q2o54xphoO_L1l-aK6_qzwIX2AAmmZzCCxvmGbl8";
        static readonly string ApplicationName = "CUapp";
        static readonly string updatedSheet = "Sheet3";
        static readonly string tagSheet = "popupTags";
        static SheetsService service;

        public GoogleSheets()
        {
            GoogleCredential credential;

            var assembly = typeof(GoogleSheets).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("CUapp.client_secret.json");
            using (var reader = new StreamReader(stream))
            {
               
                credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
            }

            // Create Google Sheets API service.
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential, ApplicationName = ApplicationName                
            });
            ReadRecipeEntries(false);
            ReadHealthTagEntries();
        }

        static public void ReadRecipeEntries(bool updated)
        {
            var count = 0;
            var countRange = $"{updatedSheet}!D1";
            var countRequest = service.Spreadsheets.Values.Get(SpreadsheetId, countRange);
            Google.Apis.Sheets.v4.Data.ValueRange countResponse = new Google.Apis.Sheets.v4.Data.ValueRange();
            countResponse = countRequest.Execute();
            count = Convert.ToInt32(countResponse.Values[0][0]);
            int endIndex = 3 + count;



            var range = $"{updatedSheet}!B4:O{endIndex}";
            var request = service.Spreadsheets.Values.Get(SpreadsheetId, range);
            Google.Apis.Sheets.v4.Data.ValueRange response = new Google.Apis.Sheets.v4.Data.ValueRange();
            IList<IList<object>> values;
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.ROWS;
            response = request.Execute();
            values = response.Values;
            
            if (values != null && values.Count > 0)
            {
                for (int recipeIndex = 0; recipeIndex < values.Count; recipeIndex++)
                {
                    IList<object> recipe = values[recipeIndex];

                    // title
                    string title = (string)recipe[0];

                    //difficulty
                    int difficulty = Convert.ToInt32(recipe[4]);

                    //food type
                    string foodType = (string)recipe[11];

                    //food amount
                    string foodAmount = (string)recipe[1];

                    //labels
                    char[] seperator = new char[] { ' ', '#' };
                    string labels_ = (string)recipe[12];
                    List<string> labels = labels_.Split(seperator, StringSplitOptions.RemoveEmptyEntries).ToList();


                    //index
                    int index = recipeIndex;

                    //image route
                    string buttonImg = $"{title}.jpeg";


                    // prep time
                    string prepTime = (string)recipe[3];

                    // cook time
                    string cookingTime = (string)recipe[2];

                    //creator
                    string creator = (string)recipe[13];


                    //ingredients
                    string ingredients_raw = (string)recipe[5];
                    string[] ingredients_str = ingredients_raw.Split('\n');

                    string amount_raw = (string)recipe[6];
                    string[] amount = amount_raw.Split('\n');

                    List<ingredientBlock> ingredients = new List<ingredientBlock>();

                    // looping through the ingredient texts in the cell
                    for (int i = 0; i < ingredients_str.Length; i++)
                    {
                        // if there is an amount for that ingredient
                        if (i < amount.Length)
                        {
                            ingredientBlock ingredient = new ingredientBlock { text = ingredients_str[i], step = amount[i] };
                            ingredients.Add(ingredient);
                        }
                        else
                        {
                            ingredientBlock ingredient = new ingredientBlock { text = ingredients_str[i], step = "" };
                            ingredients.Add(ingredient);
                        }
                    }

                    //howto
                    string howToRaw = (string)recipe[7];
                    string[] howToSeperatedWithLine = howToRaw.Split('\n');
                    List<ingredientBlock> howto = methods.stringArrToIngredientBlockList(howToSeperatedWithLine, true);

                    //nutritionTips
                    string nutrTipsRaw = (string)recipe[9];
                    string[] nutrTipsSeperatedWithLine = nutrTipsRaw.Split('\n');
                    List<ingredientBlock> nutrTips = methods.stringArrToIngredientBlockList(nutrTipsSeperatedWithLine, false);

                    //cookingTips_ 
                    string cookTipsRaw = (string)recipe[10];
                    string[] cookTipsSeperatedWithLine = cookTipsRaw.Split('\n');
                    List<ingredientBlock> cookTips = methods.stringArrToIngredientBlockList(cookTipsSeperatedWithLine, false);

                    recipeList.list.Add(new recipeModel { creator = creator, foodType = foodType, title = title, difficulty = difficulty, foodAmount = foodAmount, howto = howto, nutrTips = nutrTips, cookTips = cookTips, i = index, ingredients = ingredients, labels = labels, buttonImg = buttonImg, prepTime = prepTime, cookTime = cookingTime });


                    // create new recipemodel
                    // title = 0, index = 1, buttonImg = 2, labels = 3
                    // ingredients_num = 4, methods_num = 5, nutritionTips_num = 6
                    // cookingTips_num = 7, ingredients = 8:8+ingredients_num-1, 
                }
            }
        }

        public static void ReadHealthTagEntries()
        {

            var numRange = $"{tagSheet}!A2";
            var numRequest = service.Spreadsheets.Values.Get(SpreadsheetId, numRange);
            var numValues = numRequest.Execute().Values;
            short num = Convert.ToInt16(numValues[0][0]);
            int lastCell = 2 + num;

            var range = $"{tagSheet}!A3:A{lastCell}";
            var request = service.Spreadsheets.Values.Get(SpreadsheetId, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;

            var response = request.Execute();
            var values1 = response.Values;
            var values = values1[0];

            if (values != null && values.Count > 0)
            {
                for (short i = 0; i < values.Count; i++)
                {
                    if (values[i] != null)
                    {
                        bool isChecked = false;
                        string name = (string)values[i];
                        short id = i;

                        publicVariables.healthCheckBoxes.Add(
                            new checkboxBlock { isChecked = isChecked, id = id, name = name }
                            );
                    }
                }
            }
        }
    }
}
