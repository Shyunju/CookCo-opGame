using System.Collections;
using UnityEngine;

namespace CookCo_opGame
{
    public class WaterTable : TableManager
    {
        [SerializeField] Transform _spawnTable;
        [SerializeField] GameObject _waterInSink;
        [SerializeField] GameObject _plate;
        [SerializeField] GameObject _tempItem;
        [SerializeField] bool _hasPlate = false;

        private float _washingDuration = 4f;
        public PlayerManager PlayerManager { get; set; }
        public bool HasPlate {get { return _hasPlate; } set { _hasPlate = value;}}
        public GameObject WaterInSink { get { return _waterInSink; } set { _waterInSink = value; } }
        void Start()
        {
            _purpose = TablePurpose.Wash;
        }
        public override void ChaingeState(GameObject item)
        {
            SpawnPlate();
            WaterInSink.SetActive(false);
            HasPlate = false;
            PlayerManager.StateMachine.ChaingeState(PlayerManager.StateMachine.IdleState);
            PlayerManager = null;
        }

        public override bool PerformPurpose()
        {
            CurrentItem = _tempItem;
            StartCoroutine(WashPlateCo());
            return true;
            // if (CurrentItem != null)
            // {
            //     ToolManager tm = CurrentItem.GetComponent<ToolManager>();
            //     if (tm != null && tm.CurrentState == ItemState.Used)
            //     {
            //     }
            // }
            // return false;
        }
        public void SpawnPlate()
        {
            CurrentItem = Instantiate(_plate, _spawnTable.position, Quaternion.identity) as GameObject;
            CurrentItem.GetComponent<ItemManager>().PickedUp(_spawnTable.gameObject);
        }
        
        IEnumerator WashPlateCo()
        {
            yield return new WaitForSeconds(.2f);
            ItemManager itemManager = CurrentItem.GetComponent<ItemManager>();
            itemManager.Duration = _washingDuration;
            itemManager.IsCooking = true;
        }
    }
}
