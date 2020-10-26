namespace Scenes.Data
{
    using System;
    using System.Collections;
    using KPU.Manager;
    using UnityEngine;
    using UnityEngine.AI;

    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnWaveData SpawnWaveData;

        private void Start()
        {
            EventManager.On("game_started", OnGameStarted);
        }

        private void OnGameStarted(object obj)
        {
            StartCoroutine(SpawnStart());
        }

        private IEnumerator SpawnStart()
        {
            foreach (var waveData in SpawnWaveData.WaveDataset)
            {
                for (int i = 0; i < waveData.count; i++)
                {
                    NavMesh.SamplePosition(transform.position, out var hit, 1f, NavMesh.AllAreas);
                    var spawnGameObject = ObjectPoolManager.Instance.Spawn(waveData.spawnTargetName, hit.position);
                    
                    yield return new WaitForSeconds(waveData.spawnInterval);
                }

                yield return new WaitForSeconds(waveData.spawnInterval);
            }
        }
    }
}