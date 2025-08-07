using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.AI;

namespace CookCo_opGame
{
    public class MouseMove : MonoBehaviour
    {
        [SerializeField] GameObject _plateOfMouse;
        [SerializeField] Transform _mouseHouse;
        //Transform _mouseHouse;
        [SerializeField] ItemManager _currentItem;
        public GameObject PlateOfMouse { get { return _plateOfMouse; } }
        public Transform Target { get; private set; }  // 이동할 목표 위치(음식 위치 등)
        public GameObject ItemOnHead { get; set; }
        public Transform MouseHouse { get { return _mouseHouse; } set { _mouseHouse = value; } }
        public ItemManager CurrentItem { get { return _currentItem; } set { _currentItem = value; } }
        public bool HasItem { get; set; }
        public bool IsMoving { get; set; }
        public Collider MouseCollider { get; set; }

        private NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>(); 
            HasItem = false;
            IsMoving = false;
            SetTarget(MouseHouse);
        }
        public void SetTarget(Transform target)
        {
            Target = target;
            if (target != null)
                agent.SetDestination(target.position);
        }
        
    }
}
