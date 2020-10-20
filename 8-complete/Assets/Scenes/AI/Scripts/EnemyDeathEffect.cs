namespace Scenes.AI
{
    using UnityEngine;
    using System.Collections;

    public class EnemyDeathEffect : MonoBehaviour
    {
        private Coroutine _deathEffectRoutine;

        private void OnEnable()
        {
            _deathEffectRoutine = StartCoroutine(DeathRoutine());
        }

        private void OnDisable()
        {
            StopCoroutine(_deathEffectRoutine);
        }

        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}