using System.Collections.Generic;
using Assets.Scripts.Assets;
using Assets.Scripts.Runtime;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawnController: IController
    {
        private EnemyController m_EnemyController;
        private LevelAsset m_LevelAsset;
        private float m_Time;
        private List<int> m_EnemyIds;

        public EnemySpawnController(LevelAsset levelAsset, EnemyController enemyController)
        {
            m_EnemyController = enemyController;
            m_LevelAsset = levelAsset;
        }
        public void OnStart()
        {
            m_Time = 0f;
            m_EnemyIds = new List<int>(m_LevelAsset.Waves.Count);
            for (int i = 0; i < m_LevelAsset.Waves.Count; i++)
            {
                m_EnemyIds.Add(0);
            }
        }
        public void OnStop()
        {
            
        }

        public bool HasFinishedSpawn()
        {
            for (int i = 0; i < m_LevelAsset.Waves.Count; i++)
            {
                WaveAsset waveAsset = m_LevelAsset.Waves[i];
                if (m_EnemyIds[i] < waveAsset.EnemiesNumber)
                {
                    return false;
                }
            }
            return true;
        }

        public void Tick()
        {
            float newTime = m_Time + Time.deltaTime;
            for (int i = 0; i < m_LevelAsset.Waves.Count; i++)
            {
                WaveAsset waveAsset = m_LevelAsset.Waves[i];
                if (m_EnemyIds[i] >= waveAsset.EnemiesNumber)
                {
                    continue;
                }
                float nextSpawn = waveAsset.StartTime + waveAsset.TimeBetweenSpawns * m_EnemyIds[i];
                if (nextSpawn < newTime)
                {
                    m_EnemyController.CreateEnemy(waveAsset.EnemyAsset, waveAsset.SpawnPosition, Quaternion.identity);
                    ++m_EnemyIds[i];
                }
            }
            m_Time = newTime;
        }
    }
}