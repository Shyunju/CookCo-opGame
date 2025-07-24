using System.Collections;
using UnityEngine;

namespace CookCo_opGame
{
    public class WaterTable : TableManager
    {
        [SerializeField] Transform _spawnTablePosition;
        [SerializeField] GameObject _waterInSink;
        [SerializeField] GameObject _plate;
        [SerializeField] GameObject _tempItem;
        [SerializeField] bool _hasPlate = false;

        private TableManager _spawnTable;
        private float _washingDuration = 4f;
        public PlayerManager PlayerManager { get; set; }
        public bool HasPlate {get { return _hasPlate; } set { _hasPlate = value;}}
        public GameObject WaterInSink { get { return _waterInSink; } set { _waterInSink = value; } }
        void Start()
        {
            _purpose = TablePurpose.Wash;
            _spawnTable = _spawnTablePosition.gameObject.GetComponent<TableManager>();
        }
        public override void ChangeState(GameObject item)
        {
            ItemManager im = CurrentItem.GetComponent<ItemManager>();
            PlayerManager.StateMachine.ChangeState(PlayerManager.StateMachine.IdleState);
            if (!_spawnTable.IsFull)
            {
                SpawnPlate();
                WaterInSink.SetActive(false);
                HasPlate = false;
                IsFull = false;
                PlayerManager = null;
                im.StopNextStep = false;
                im.Elapsed = 0f;
            }
            else
            {
                im.StopNextStep = true;
            }

        }

        public override bool PerformPurpose()
        {
            if (HasPlate)
            {
                CurrentItem = _tempItem;
                StartCoroutine(WashPlateCo());
                return true;
            }
            return false;
        }
        public void SpawnPlate()
        {
            CurrentItem = Instantiate(_plate, _spawnTablePosition.position, Quaternion.identity) as GameObject;
            CurrentItem.GetComponent<ItemManager>().PickedUp(_spawnTablePosition.gameObject);
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
