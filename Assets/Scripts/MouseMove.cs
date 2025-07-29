using UnityEngine;
using UnityEngine.AI;

namespace CookCo_opGame
{
    public class MouseMove : MonoBehaviour
    {
        public Transform target;  // 이동할 목표 위치(음식 위치 등)

        private NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트 가져오기
            if (target != null)
            {
                agent.SetDestination(target.position); // 목적지 설정
            }
        }

        void Update()
        {
            if (target != null && agent.destination != target.position)
            {
                agent.SetDestination(target.position); // 타겟 위치가 변하면 계속 업데이트 가능
            }
        }
    }
}
