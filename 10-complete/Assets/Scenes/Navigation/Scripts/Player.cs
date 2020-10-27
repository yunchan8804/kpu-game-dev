namespace Scenes.Navigation
{
    using UnityEngine;
    using UnityEngine.AI;

    public class Player : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Camera _camera;
        private Vector3 _targetPosition;
        [SerializeField] private float speed = 10f;
        [SerializeField] private LayerMask layerToCast;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _camera = FindObjectOfType<Camera>();
            _targetPosition = transform.position;
            _agent.speed = speed;
        }

        private void Start()
        {
            _agent.ActivateCurrentOffMeshLink(true);
        }

        private void Update()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var raycastHit, Mathf.Infinity, layerToCast)) _targetPosition = raycastHit.point;

            _agent.SetDestination(_targetPosition);
        }
    }
}