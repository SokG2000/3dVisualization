using System;
using Assets.Scripts.Assets;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Runtime
{
    public static class Game
    {
        public static RootAsset RootAsset;
        public static PlayerAsset PlayerAsset;
        public static LevelAsset LevelAsset;
        public static int Score = 0;

        private static Runner s_Runner;
        public static Runner Runner => s_Runner;

        public static event Action<int> GetLifes;
        public static event Action<int> GetScore;
        public static event Action<bool> EndGame;

        private static String UISceneName = "UI";

        public static void FinishLevel()
        {
            Runner.StopRunning();
            EndGame?.Invoke(true);
            //Debug.Log("Конец");
        }

        public static void PlayerDeath()
        {
            Debug.Log("Dead!");
            Runner.StopRunning();
            EndGame?.Invoke(false);
        }

        public static void UpdateHealth()
        {
            ChangeHealth(Runner.PlayerController.Data.Health);
        }

        public static void ChangeHealth(int newHealth)
        {
            GetLifes?.Invoke(newHealth);
            Debug.Log("Changed health");
        }

        public static void AddScore(int newScore)
        {
            Score += newScore;
            GetScore?.Invoke(Score);
            //Debug.Log("Changed health");
        }
        public static void SetRootAsset(RootAsset rootAsset)
        {
            RootAsset = rootAsset;
        }

        public static void StartLevel(LevelAsset levelAsset, PlayerAsset playerAsset)
        {
            LevelAsset = levelAsset;
            PlayerAsset = playerAsset;
            AsyncOperation operation = SceneManager.LoadSceneAsync(levelAsset.SceneName);
            operation.completed += ConfigureScene;
        }

        public static void ConfigureScene(AsyncOperation operation)
        {
            if (!operation.isDone)
            {
                throw new Exception("Can't load scene");
            }
            s_Runner = Object.FindObjectOfType<Runner>();
            s_Runner.StartRunning();
            
            SceneManager.LoadScene(UISceneName, LoadSceneMode.Additive);
        }
    }
}
