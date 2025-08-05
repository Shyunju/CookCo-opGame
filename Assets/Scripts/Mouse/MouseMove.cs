using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.AI;

namespace CookCo_opGame
{
    public class MouseMove : MonoBehaviour
    {
        [SerializeField] Transform _house;
        [SerializeField] GameObject _plateOfMouse;
        public GameObject PlateOfMouse { get { return _plateOfMouse; } }
        public Transform House { get { return _house; } }
        public Transform Target { get; private set; }  // 이동할 목표 위치(음식 위치 등)
        public GameObject ItemOnHead { get; set;}
        public Transform TestTransform;

        private NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트 가져오기
            Target = TestTransform;
            SetTarget(Target);
        }
        public void SetTarget(Transform target)
        {
            Target = target;
            agent.SetDestination(target.position); //테이블 앞으로 설정해주기
                                                   //도중에 플레이어에게 뺏겨도 목적지 집으로 설정

        }
        
    }
}
