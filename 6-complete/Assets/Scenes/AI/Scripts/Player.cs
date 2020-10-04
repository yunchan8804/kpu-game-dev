namespace Scenes.AI
{
    using UnityEngine;
    using UnityEngine.AI;

    public class Player : MonoBehaviour, IDamagable
    {
        private NavMeshAgent _agent;
        private PlayerState _state;
        [SerializeField] private float speed;
        [SerializeField] private Camera cam;
        [SerializeField] private Stat stat;

        public PlayerState State => _state;

        public Stat Stat => stat;

        public Camera Cam => cam;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var camTowardVector = (transform.position - cam.transform.position).normalized;
            var finalVector = (camTowardVector * Input.GetAxis("Vertical") +
                               cam.transform.right * Input.GetAxis("Horizontal")) * (speed * Time.deltaTime);
            var yRemovedVector = new Vector3(finalVector.x, 0, finalVector.z);
            _agent.Move(yRemovedVector);

            if (finalVector.magnitude >= Mathf.Epsilon)
                transform.rotation = Quaternion.LookRotation(yRemovedVector);
        }

        public void Damage(float damageAmount)
        {
            stat.AddHp(damageAmount);
        }
    }
}