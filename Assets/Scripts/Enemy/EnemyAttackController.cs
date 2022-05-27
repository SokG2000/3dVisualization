using Assets.Scripts.Damage;
using Assets.Scripts.Player;
using Assets.Scripts.Runtime;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAttackController: IController
    {
        private EnemyController m_EnemyController;
        private Transform target;
        private PlayerData m_TargetData;

        public EnemyAttackController(EnemyController enemyController)
        {
            m_EnemyController = enemyController;
        }
        public void OnStart()
        {
            target = Game.Runner.PlayerController.Transform;
            m_TargetData = Game.Runner.PlayerController.Data;
        }

        public void OnStop()
        {
            
        }

        public void Tick()
        {
            if (Game.Runner.PlayerController.Data.IsDied)
            {
                return;
            }
            foreach (EnemyData enemy in m_EnemyController.Enemies)
            {
                float sqrDistance = (target.position - enemy.View.transform.position).sqrMagnitude;
                //Debug.Log(sqrDistance + " " + enemy.SqrCentersAttackDistance);
                if (sqrDistance < enemy.SqrCentersAttackDistance)
                {
                    //Debug.Log("Try attack");
                    if (enemy.TryAttack())
                    {
                        m_TargetData.TakeDamage(enemy.Damage);
                        enemy.View.StartCoroutine(enemy.View.Attack());
                    }
                }
            }
        }
    }
}