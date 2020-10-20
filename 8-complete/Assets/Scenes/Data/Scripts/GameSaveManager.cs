namespace Scenes.Data
{
    using System.IO;
    using System.Text;
    using KPU;
    using UnityEngine;

    public class GameSaveManager : SingletonBehaviour<GameSaveManager>
    {
        [SerializeField] private Transform player;

        private void Start()
        {
            var path = Path.Combine(Application.persistentDataPath, "player_transform.json");
            if (!File.Exists(path)) return;

            var gameStateSave = JsonUtility.FromJson<GameStateSave>(File.ReadAllText(path));

            player.transform.position = gameStateSave.position;
            player.transform.rotation = gameStateSave.rotation;
        }

        private void OnApplicationQuit()
        {
            var transformData = new GameStateSave()
            {
                position = player.transform.position, rotation = player.transform.rotation
            };
            var jsonText = JsonUtility.ToJson(transformData);

            File.WriteAllText(Path.Combine(Application.persistentDataPath, "player_transform.json"), jsonText,
                Encoding.UTF8);
        }
    }
}