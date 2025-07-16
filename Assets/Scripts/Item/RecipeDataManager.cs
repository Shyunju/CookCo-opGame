using UnityEngine;
using System.Collections.Generic;
namespace CookCo_opGame
{
    public class RecipeDataManager
    {
        private RecipeDataList _recipeList = new RecipeDataList();
        private string _jsonPath;
        TextAsset jsonAsset;

        public RecipeDataManager()
        {
            jsonAsset = Resources.Load<TextAsset>("Recipes");

            LoadItems();
        }


        //아이템 리스트 로드
        public void LoadItems()
        {

            if (jsonAsset != null)
            {
                string json = jsonAsset.text;
                _recipeList = JsonUtility.FromJson<RecipeDataList>(json);
            }
            else
            {
                _recipeList = new RecipeDataList();
            }
        }

        public RecipeData GetItemByID(int id)
        {
            
            RecipeData it = _recipeList.Recipes.Find((item) => item.RecipeID == id);
            return it;
        }

        public List<RecipeData> GetAllRecipes()
        {
            return _recipeList.Recipes;
        }
    }
}
