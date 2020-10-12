
namespace Scenes.AI
{
    using System;
    using System.Collections;
    using UnityEngine;
    public class Dust : MonoBehaviour
    {
        private Coroutine _dustRoutine;

        private void OnEnable()
        {
            _dustRoutine = StartCoroutine(DustRoutine());
        }

        private void OnDisable()
        {
            if (_dustRoutine != null) StopCoroutine(_dustRoutine);
        }
        private IEnumerator DustRoutine()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}
