using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CookCo_opGame
{
    public class CookingPlayManager : Singleton<CookingPlayManager>
    {
        [SerializeField] MouseHouse _mouseHouse;
        [SerializeField] ScoreUIController _scoreUIController;
        [SerializeField] GameObject[] RecipeUI;  //레시피 별 이미지 프리팹 배열, 아이디와 인덱스 맞출것
        [SerializeField] GameObject _orderUICanvas;  // 주문 캔버스
        List<(List<int>, int)> _orders = new List<(List<int>,int)>(); //주문 레시피가 담겨있는 리스트
        public List<(List<int>, int)> Orders { get { return _orders; } }
        public List<GameObject> OrdersUI { get; set; } //주문 이미지 프리팹 리스트
        private float _addOrderTime = 25f;
        private int _orderLimit = 1;
        private int _startLife = 3;
        public int LifeCount { get; set; }
        public int Score { get; private set; }

        void Awake()
        {
            base.Awake();
            OrdersUI = new List<GameObject>();
        }
        void Start()
        {
            StartCoroutine(OrderNewMenuCo());
            LifeCount = _startLife;
        }
        IEnumerator OrderNewMenuCo()
        {
            if (Orders.Count < 5)
            {
                //레벨별 범위 안에 레시피 아이디 번호를 랜덤으로 가져와 오더에 추가
                //제출시 비교는 해당 아이디를 가진 레시피의 리스트와 현재재료들 값을 리스트한것을 비교
                int randomIndex = UnityEngine.Random.Range(0, GameManager.Instance.HasRecipes.Count-1);  //인덱스 랜덤 추출
                int recipeID = GameManager.Instance.HasRecipes[randomIndex]; //해당 인덱스의 레시피 아이디
                List<int> test = GameManager.Instance.RecipeDataList.Find(x => x.RecipeID == recipeID).RecipeList;
                int price = GameManager.Instance.RecipeDataList.Find(x => x.RecipeID == recipeID).Price;


                //레시피 아이디의 레시피를 이미지로 하는 유아이 오브젝트 생성
                //유아이 리스트에 추가
                _orders.Add((test, price));
                GameObject newOrder = Instantiate(RecipeUI[recipeID]);
                newOrder.transform.SetParent(_orderUICanvas.transform, true);
                newOrder.transform.SetAsFirstSibling();
                OrdersUI.Add(newOrder);
            }
            yield return new WaitForSeconds(_addOrderTime);
            StartCoroutine(OrderNewMenuCo());
        }

        public void GiveTargetToMouse(Transform transform)
        {
            _mouseHouse.ExportMouse(transform);
        }

        public void ChangeScore(int mount)
        {
            if (Score + mount < 0)
            {
                Score = 0;
            }
            else
            {
                Score += mount;                
            }
            _scoreUIController.UpdateScoreText();
        }
        public void CompleteMenu(int index)
        {
            GameObject ds = OrdersUI[index];
            OrdersUI.RemoveAt(index);
            Destroy(ds);
        }
        public void ChangeLife(int amount)
        {
            LifeCount += amount;
            if (LifeCount < 0)
            {
                //GameOver
                GameManager.Instance.GoToLobby();
            }
            else
            {
                _scoreUIController.ChangeLifeUI();
            }
        }
        
    }
}
