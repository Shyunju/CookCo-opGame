using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace CookCo_opGame
{
    public class GameManager : Singleton<GameManager>
    {
        ItemDataManager _itemDataManager;
        RecipeDataManager _recipeDataManager;
        [SerializeField] ScoreAndTimerUIController _scoreAndTimerUIController;
        List<List<int>> _orders = new List<List<int>>();
        public List<ItemData> ItemDataList { get; private set; }
        public List<RecipeData> RecipeDataList { get;  private set; }
        public List<List<int>> Orders { get { return _orders; } }
        public int Score { get; private set; }
        void Awake()
        {
            _itemDataManager = new ItemDataManager();
            _recipeDataManager = new RecipeDataManager();
        }
        private void Start()
        {
            ItemDataList = _itemDataManager.GetAllItems();
            RecipeDataList = _recipeDataManager.GetAllRecipes();
            //레벨별 범위 안에 레시피 아이디 번호를 랜덤으로 가져와 오더에 추가
            //제출시 비교는 해당 아이디를 가진 레시피의 리스트와 현재재료들 값을 리스트한것을 비교
            List<int> test = new List<int>();
            test.Add(101);
            _orders.Add(test);
        }
        public void ChangeScore(int mount)
        {
            Score += mount;
            _scoreAndTimerUIController.UpdateScoreText();
        }
    }
}
