using UnityEngine;
using System.Collections.Generic;
namespace CookCo_opGame
{
    public class RecipeData
    {
        public int RecipeID;
        public string RecipeName;
        public List<int> RecipeList;
        public string UIPath;
        [System.NonSerialized]
        public Sprite UISprite;


    }
    [System.Serializable]
    public class RecipeDataList
    {
        public List<RecipeData> Recipes = new List<RecipeData>();
    }
}
