namespace Scenes.Navigation.Npc
{
    using UnityEngine;
    using UnityEngine.AI;

    public class Npc : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public float speed = 1f;
        public Transform target;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = speed;
            target = FindObjectOfType<Player>().transform;
        }

        private void Update()
        {
            if (target != null) _agent.SetDestination(target.position);
        }
    }
}
