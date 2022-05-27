using System.Collections.Generic;
using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using Assets.Scripts.Spells;
using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public class Runner: MonoBehaviour
    {
        private List<IController> m_Controllers;
        private PlayerController m_PlayerController;
        private EnemyController m_EnemyController;
        private EnemySpawnController m_EnemySpawnController;
        private bool m_IsRunning = false;

        public List<IController> Controllers => m_Controllers;
        public bool IsRunning => m_IsRunning;
        public PlayerController PlayerController => m_PlayerController;
        public EnemyController EnemyController => m_EnemyController;
        public EnemySpawnController EnemySpawnController => m_EnemySpawnController;

        void Update()
        {
            if (m_IsRunning)
            {
                TickControllers();
            }
        }

        public void StartRunning()
        {
            CreateControllers();
            StartControllers();
            m_IsRunning = true;
        }

        public void StopRunning()
        {
            m_IsRunning = false;
            StopControllers();
        }

        private void CreateControllers()
        {
            ProjectileController projectileController = new ProjectileController(Game.RootAsset.FireballAsset, Game.RootAsset.LightningAsset);
            m_PlayerController = new PlayerController(Game.PlayerAsset, projectileController);
            m_EnemyController = new EnemyController();
            m_EnemySpawnController = new EnemySpawnController(Game.LevelAsset, m_EnemyController);
            EnemyAttackController enemyAttackController = new EnemyAttackController(m_EnemyController);
            m_Controllers = new List<IController>();
            m_Controllers.Add(m_PlayerController);
            m_Controllers.Add(m_EnemySpawnController);
            m_Controllers.Add(projectileController);
            m_Controllers.Add(m_EnemyController);
            m_Controllers.Add(enemyAttackController);
        }

        private void TickControllers()
        {
            foreach (IController controller in m_Controllers)
            {
                controller.Tick();
            }
        }

        private void StartControllers()
        {
            foreach (IController controller in m_Controllers)
            {
                controller.OnStart();
            }
        }

        private void StopControllers()
        {
            foreach (IController controller in m_Controllers)
            {
                controller.OnStop();
            }
        }
    }
}