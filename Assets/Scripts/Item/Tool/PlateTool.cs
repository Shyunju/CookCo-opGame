using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace CookCo_opGame
{
    public class PlateTool : ToolManager
    {
        [SerializeField] MeshFilter _objectOnPlate;
        public MeshFilter ObjectOnPlate { get { return _objectOnPlate; } set { _objectOnPlate = value; } }


        public override bool CheckToolState(GameObject itemInHand)
        {
            FoodManager fm = itemInHand.GetComponent<FoodManager>();
            ToolManager tm = itemInHand.GetComponent<ToolManager>();
            if (Ingredients.Count < _ingredientsMaxCount)
            {
                if (fm != null)
                {
                    // if (Ingredients.Count == 0)
                    // {
                    //     _objectOnPlate.mesh = fm.MeshFilter.mesh;

                    // }
                    //Ingredients.Add(fm);
                    
                    return true;
                }
                else if (tm != null && tm.Ingredients.Count > 0)
                {
                    InputFromTool(tm);
                    return false;
                }

            }
            return false;
        }

        public override void StartCooking()
        {
            if(Ingredients.Count > 0)
                SettingMeshOnPlate();
        }
        public void InputFromTool(ToolManager tm)
        {
            GrillTool gt = tm.GetComponent<GrillTool>();
            if (gt != null)
                gt.GrillFood.SetActive(false);
            foreach (var item in tm.Ingredients)
                {
                    Ingredients.Add(item);
                }
            FoodManager fm = Ingredients[0];
            SettingMeshOnPlate();
            _objectOnPlate.mesh = Ingredients.First().MeshFilter.mesh;
            Transform[] children = tm.IngredientsTemp.GetComponentsInChildren<Transform>(true);
            for (int i = 1; i < children.Length; i++) // i=0은 부모 자신
            {
                children[i].SetParent(IngredientsTemp.transform, true);
            }
            for (int i = 0; i < tm.Ingredients.Count; i++)
            {
                FoodManager foodIcon = tm.Ingredients[i];
                IngredientUIController.AddIngredientIcon(foodIcon.Icon, i);
            }
            tm.EmptyTool();
        }

        public void SettingMeshOnPlate()
        {
            // 리스트 길이가 1일 때
            
            if (Ingredients.Count == 1)
            {
                _objectOnPlate.mesh = Ingredients.First().MeshFilter.mesh;
                return;
            }

            // 리스트 길이가 2 이상일 때
            int sum = Ingredients.Select(x => x.ItemID).Sum();
            List<int> idList = Ingredients.Select(x => x.ItemID).ToList();
            bool allSame = idList.All(x => x == idList[0]);
            bool contains1106 = idList.Contains(1106);
            bool contains8 = idList.Contains(8);


            // 값들이 모두 같다면 첫번째 요소의 값을 할당
            if (Ingredients.Count > 1 && allSame)
            {
                _objectOnPlate.mesh = Ingredients.First().MeshFilter.mesh;
                return;
            }

            // 값들이 다르고 합이 1000보다 작으면 salad
            if (sum < 1000)
            {
                if (contains8)
                {
                    ChangeMeshInIngredients(6);
                    return;
                }
                ChangeMeshInIngredients(3);
                return;
            }

            // 합이 1000 이상이고, 값 중 1106이 포함되어 있으면 meat ball
            if (sum >= 1000 && contains1106 && !contains8)
            {
                ChangeMeshInIngredients(4);
                return;
            }

            // 합이 1000 이상이고, 값 중 8이 포함되어 있으면 taco
            // if (sum >= 1000 && contains8)
            // {
                
            //     return;
            // }

            // // 합이 10006 이상이면 burger
            // if (sum >= 10006)
            // {
            //     ChangeMeshInIngredients(5);
            //     return;
            // }
            ChangeMeshInIngredients(5);
        }
        public void ChangeMeshInIngredients(int index)
        {
            foreach (var item in Ingredients)
            {
                item.ChangeMesh(index);
            }
            _objectOnPlate.mesh = Ingredients.First().MeshFilter.mesh;
        }

    }
}
