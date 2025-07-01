using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace CookCo_opGame
{
    public class PlateTool : ToolManager
    {
        [SerializeField] MeshFilter _objectOnPlate;
        public MeshFilter ObjectOnPlate { get { return _objectOnPlate;} set { _objectOnPlate = value;}}

        public override bool CheckToolState(GameObject itemInHand)
        {
            FoodManager fm = itemInHand.GetComponent<FoodManager>();
            ToolManager tm = itemInHand.GetComponent<ToolManager>();
            if (fm != null)
            {
                _objectOnPlate.mesh = fm.MeshFilter.mesh;
                return true;
            }
            else if (tm != null && tm.Ingredients.Count > 0)
            {
                InputFromTool(tm);
                
                return false;
            }
            return false;
        }

        public override void StartCooking()
        {
            if (Ingredients.Count > 0 && CurrentTable.Purpose == TableManager.TablePurpose.None) //제출 테이블로 수정 필요
            {
                //제출시 이루어져야할 것들
            }
            else
            {
                return;
            }
        }
        public void InputFromTool(ToolManager tm)
        {
            Ingredients = tm.Ingredients.ToList();
            //FoodManager fm = Ingredients[0].GetComponent<FoodManager>();
            //_objectOnPlate.mesh = fm.MeshFilter.mesh;  메쉬 바꾸기
            Transform[] children = tm.IngredientsTemp.GetComponentsInChildren<Transform>(true);
            for (int i = 1; i < children.Length; i++) // i=0은 부모 자신
            {
                children[i].SetParent(IngredientsTemp.transform, true);
            }
            for (int i = 0; i < tm.Ingredients.Count; i++)
            {
                //FoodManager foodIcon = tm.Ingredients[i].GetComponent<FoodManager>();
                //IngredientUIController.AddIngredientIcon(foodIcon.Icon, i);
            }
            tm.EmptyTool();
        }
    }
}
