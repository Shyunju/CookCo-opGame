using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace CookCo_opGame
{
    public class PlateTool : ToolManager
    {
        [SerializeField] MeshFilter _objectOnPlate;

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
                Ingredients = tm.Ingredients.ToList();
                FoodManager first = Ingredients.First().GetComponent<FoodManager>();
                _objectOnPlate.mesh = first.MeshFilter.mesh;
                tm.EmptyTool();
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
    }
}
