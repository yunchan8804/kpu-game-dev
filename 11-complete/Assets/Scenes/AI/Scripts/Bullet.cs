namespace Scenes.AI
{
    using System.Collections;
    using KPU.Manager;
    using UnityEngine;

    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private float _power;
        private float _speed = 5;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private LayerMask hitLayer;
        private Coroutine _bulletRoutine;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if ((1 << other.gameObject.layer & hitLayer) != 0)
            {
                var damagable = other.gameObject.GetComponent<IDamagable>();
                damagable?.Damage(_power);
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
           _bulletRoutine =  StartCoroutine(BulletLifeRoutine());
        }

        private void OnDisable()
        {
            if (_bulletRoutine == null) return;

            StopCoroutine(_bulletRoutine);
            _bulletRoutine = null;
        }

        private IEnumerator BulletLifeRoutine()
        {
            yield return new WaitForSeconds(lifeTime);
            var smokeGameObject = ObjectPoolManager.Instance.Spawn("effect_dust");
            smokeGameObject.SetActive(true);
            smokeGameObject.transform.position = transform.position;
            gameObject.SetActive(false);
        }

        public void Shoot(Vector3 vec, float speed, float power)
        {
            _power = power;
            _speed = speed;
            
            _rigidbody.AddForce(vec * _speed, ForceMode.Impulse);
        }
    }
}