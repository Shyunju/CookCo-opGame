using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace CookCo_opGame
{
    public class GameManager : Singleton<GameManager>
    {
        ItemDataManager _itemDataManager;
        RecipeDataManager _recipeDataManager;
        [SerializeField] ScoreAndTimerUIController _scoreAndTimerUIController;
        [SerializeField] GameObject[] RecipeUI;  //레시피 별 이미지 프리팹 배열, 아이디와 인덱스 맞출것
        [SerializeField] GameObject _orderUICanvas;  // 주문 캔버스
        //List<(int, GameObject)> _ordersUI;
        List<List<int>> _orders = new List<List<int>>(); //주문 레시피가 담겨있는 리스트
        public List<ItemData> ItemDataList { get; private set; }
        public List<RecipeData> RecipeDataList { get; private set; }
        public List<List<int>> Orders { get { return _orders; } }
        public List<GameObject> OrdersUI { get; set; } //주문 이미지 프리팹 리스트
        public int Score { get; private set; }
        void Awake()
        {
            _itemDataManager = new ItemDataManager();
            _recipeDataManager = new RecipeDataManager();
            OrdersUI = new List<GameObject>();
        }
        private void Start()
        {
            ItemDataList = _itemDataManager.GetAllItems();
            RecipeDataList = _recipeDataManager.GetAllRecipes();
            StartCoroutine(OrderNewMenuCo());

        }
        public void ChangeScore(int mount)
        {
            Score += mount;
            _scoreAndTimerUIController.UpdateScoreText();
        }
        IEnumerator OrderNewMenuCo()
        {
            if (Orders.Count < 5)
            {
                //레벨별 범위 안에 레시피 아이디 번호를 랜덤으로 가져와 오더에 추가
                //제출시 비교는 해당 아이디를 가진 레시피의 리스트와 현재재료들 값을 리스트한것을 비교
                int orderNumber = Random.Range(1, 9);
                //int orderNumber = 1;
                List<int> test = RecipeDataList.Find(x => x.RecipeID == orderNumber).RecipeList;
                Debug.Log(RecipeDataList.Find(x => x.RecipeID == orderNumber).RecipeName);


                //레시피 아이디의 레시피를 이미지로 하는 유아이 오브젝트 생성
                //유아이 리스트에 추가
                _orders.Add(test);
                GameObject a = Instantiate(RecipeUI[orderNumber]) as GameObject;
                a.transform.SetParent(_orderUICanvas.transform, true);
                a.transform.SetAsFirstSibling();
                OrdersUI.Add(a);
            }
            yield return new WaitForSeconds(20f);
            StartCoroutine(OrderNewMenuCo());
        }

        public void CompleteMenu(int index)
        {
            GameObject ds = OrdersUI[index];
            OrdersUI.RemoveAt(index);
            Destroy(ds);
        }
    }
}
