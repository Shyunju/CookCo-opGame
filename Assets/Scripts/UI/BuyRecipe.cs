using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public class BuyRecipe : MonoBehaviour
    {
        [SerializeField] int _recipeID;
        [SerializeField] int _price;
        [SerializeField] int[] _ingredientsTableIndex;
        [SerializeField] TMP_Text _buttonText;
        LobbyUIController _lobbyUIController;
        Button _button;

        void Start()
        {
            _lobbyUIController = GetComponentInParent<LobbyUIController>();
            _button = this.GetComponentInChildren<Button>();
            SetCanBuy();
        }
        public void BuyThisRecipe()
        {
            if (GameManager.Instance.ChangeWalletGold(_price * -1))
            {
                GameManager.Instance.HasRecipes.Add(_recipeID);
                _lobbyUIController.LoadWallet();
                SetCanBuy();
            }
            else
            {
                _lobbyUIController.ShowWarningUI();
            }
        }
        public void SetCanBuy()
        {
            if (GameManager.Instance.HasRecipes.Contains(_recipeID))
            {
                _buttonText.text = "SOLD OUT";
                _button.interactable = false;
                return;
            }

            bool canBuy = true;
            foreach (var i in _ingredientsTableIndex)
            {
                if (!GameManager.Instance.ShopTables[i].isBought)
                {
                    canBuy = false;
                    break;
                }
            }

            _buttonText.text = "-" + _price.ToString();
            // Debug.Log(_button);
            // Debug.Log(canBuy);
            _button.interactable = canBuy;
            //NullReferenceException: Object reference not set to an instance of an object
// CookCo_opGame.BuyRecipe.SetCanBuy () (at Assets/Scripts/UI/BuyRecipe.cs:55)
// CookCo_opGame.LobbyUIController.UpgradeRecipeSet () (at Assets/Scripts/UI/LobbyUIController.cs:27)
// CookCo_opGame.BuyTable.BuyThisTable () (at Assets/Scripts/UI/BuyTable.cs:32)
// UnityEngine.Events.InvokableCall.Invoke () (at /Users/bokken/build/output/unity/unity/Runtime/Export/UnityEvent/UnityEvent.cs:178)
// UnityEngine.Events.UnityEvent.Invoke () (at /Users/bokken/build/output/unity/unity/artifacts/generated/UnityEvent/UnityEvent_0.cs:57)
// UnityEngine.UI.Button.Press () (at ./Library/PackageCache/com.unity.ugui@03407c6d8751/Runtime/UGUI/UI/Core/Button.cs:70)
// UnityEngine.UI.Button.OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData) (at ./Library/PackageCache/com.unity.ugui@03407c6d8751/Runtime/UGUI/UI/Core/Button.cs:114)
// UnityEngine.EventSystems.ExecuteEvents.Execute (UnityEngine.EventSystems.IPointerClickHandler handler, UnityEngine.EventSystems.BaseEventData eventData) (at ./Library/PackageCache/com.unity.ugui@03407c6d8751/Runtime/UGUI/EventSystem/ExecuteEvents.cs:57)
// UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) (at ./Library/PackageCache/com.unity.ugui@03407c6d8751/Runtime/UGUI/EventSystem/ExecuteEvents.cs:272)
// UnityEngine.EventSystems.EventSystem:Update() (at ./Library/PackageCache/com.unity.ugui@03407c6d8751/Runtime/UGUI/EventSystem/EventSystem.cs:530)

        }
    }
}
