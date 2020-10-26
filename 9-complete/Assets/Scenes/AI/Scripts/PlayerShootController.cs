namespace Scenes.AI
{
    using KPU.Manager;
    using UnityEngine;

    public class PlayerShootController : MonoBehaviour
    {
        private Player _player;

        [SerializeField] private string bulletName;
        [SerializeField] private LayerMask groundLayer;
        private float _timer;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            // Time
            _timer += Time.deltaTime;
            
            if (_timer <= _player.Stat.AttackRate) return; // Shoot Rate 타이머가 안되었을 때.

            if (_player.State == PlayerState.Dead) return; // Dead 체크 
            if (!Input.GetMouseButton(0)) return; // 마우스 클릭 여부 확인.

            var bulletGameObject = ObjectPoolManager.Instance.Spawn(bulletName);
            bulletGameObject.SetActive(true);
            bulletGameObject.transform.position = transform.position;

            var bullet = bulletGameObject.GetComponent<Bullet>();
            if (!(bullet is null) &&
                Physics.Raycast(_player.Cam.ScreenPointToRay(Input.mousePosition), out var raycastHit, groundLayer))
            {
                var dir = raycastHit.point - transform.position;
                var normalizedDir = new Vector3(dir.x, 0, dir.z).normalized;

                bullet.Shoot(normalizedDir, _player.Stat.ShootSpeed, _player.Stat.AttackPower);

                _timer = 0f; // timer reset
            }
        }
    }
}