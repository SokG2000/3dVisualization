using System.Collections.Generic;
using Assets.Scripts.Runtime;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyController: IController
    {
        private List<EnemyData> m_Enemies;

        public List<EnemyData> Enemies => m_Enemies;

        public void OnStart()
        {
            m_Enemies = new List<EnemyData>();
        }

        public void OnStop()
        {
            
        }

        public void Tick()
        {
            ProcessDeath();
            foreach (EnemyData enemy in m_Enemies)
            {
                enemy.Tick();
            }
        }

        public void OnPlayerDeath()
        {
            foreach (EnemyData enemy in m_Enemies)
            {
                if (!enemy.IsDestroyed)
                {
                    enemy.View.OnPlayerDeath();
                }
            }
        }

        private void ProcessDeath()
        {
            foreach (EnemyData enemy in m_Enemies)
            {
                if (enemy.IsDied)
                {
                    enemy.OnDeath();
                }
            }
            m_Enemies.RemoveAll(data => data.IsDestroyed);
            TryEndGame();
        }

        private void TryEndGame()
        {
            if (m_Enemies.Count == 0 && Game.Runner.EnemySpawnController.HasFinishedSpawn())
            {
                Game.FinishLevel();
            }
        }

        public void CreateEnemy(EnemyAsset asset, Vector3 position, Quaternion rotation)
        {
            EnemyData data = new EnemyData(asset);
            EnemyView view = Object.Instantiate(asset.View, position, rotation);
            view.Configure();
            data.AttachView(view);
            m_Enemies.Add(data);
        }
    }
}