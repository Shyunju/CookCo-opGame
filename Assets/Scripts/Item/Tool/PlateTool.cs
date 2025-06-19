using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace CookCo_opGame
{
    public class PlateTool : ToolManager
    {
        [SerializeField] MeshFilter _objectOnPlate;
        public FoodManager FoodMesh { get; set; }

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
            if (Ingredients.Count > 0 && CurrentTable.purpose == TableManager.TablePurpose.None) //제출 테이블로 수정 필요
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
            Debug.Log(Ingredients.Count);   //자꾸 0이 되어버리는 상황
            FoodManager fm = Ingredients[0].GetComponent<FoodManager>();
            Ingredients = tm.Ingredients.ToList();
            _objectOnPlate.mesh = fm.MeshFilter.mesh;
            for(int i = 0; i < tm.Ingredients.Count; i++)
            {
                FoodManager foodIcon = tm.Ingredients[i].GetComponent<FoodManager>();
                IngredientUIController.AddIngredientIcon(foodIcon.Icon, i);                
            }
            tm.EmptyTool();
        }
    }
}
