namespace Scenes.AI
{
    using KPU.Manager;
    using UnityEngine;

    public class PlayerShootController : MonoBehaviour
    {
        private Player _player;

        [SerializeField] private string bulletName;
        [SerializeField] private LayerMask groundLayer;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (_player.State == PlayerState.Dead || !Input.GetMouseButtonDown(0))
                return;

            var bulletGameObject = ObjectPoolManager.Instance.Spawn(bulletName);
            bulletGameObject.SetActive(true);
            bulletGameObject.transform.position = transform.position;
            var bullet = bulletGameObject.GetComponent<Bullet>();

            var ray = _player.Cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var raycastHit, groundLayer))
            {
                var dir = raycastHit.point - transform.position;
                var normalizedDir = new Vector3(dir.x, 0, dir.z).normalized;

                bullet.Shoot(normalizedDir, _player.Stat.ShootSpeed, _player.Stat.AttackPower);
            }
        }
    }
}