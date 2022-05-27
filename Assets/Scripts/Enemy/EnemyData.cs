using Assets.Scripts.Damage;
using Assets.Scripts.Runtime;
using Assets.Scripts.Spells;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyData: LivingEntity
    {
        private EnemyView m_View;
        public readonly EnemyAsset Asset;
        private int m_Damage;
        private int m_Score;
        private float m_CentersAttackDistance;
        private float m_CollidersAttackDistance;
        private float m_SqrCollidersAttackDistance;
        private float m_SqrCentersAttackDistance;
        private bool m_IsCollided = false;
        private bool m_IsDestroyed = false;
        private float m_TimeBetweenAttack;
        private float m_LastAttack;

        public EnemyView View => m_View;
        public int Damage => m_Damage;
        public float SqrCentersAttackDistance => m_SqrCentersAttackDistance;
        public float SqrCollidersAttackDistance => m_SqrCollidersAttackDistance;
        public float CollidersAttackDistance => m_CollidersAttackDistance;

        public bool IsCollided => m_IsCollided;
        public bool IsDestroyed => m_IsDestroyed;

        public EnemyData(EnemyAsset asset)
        {
            
            Asset = asset;
            m_MaxHealth = asset.Health;
            m_Health = asset.Health;
            m_Damage = asset.Damage;
            m_CollidersAttackDistance = asset.AttackDistance;
            m_SqrCollidersAttackDistance = m_CollidersAttackDistance * m_CollidersAttackDistance;
            m_TimeBetweenAttack = asset.TimeBetweenAttack;
            m_LastAttack = asset.TimeBetweenAttack;
            m_Score = asset.Score;
        }

        public void Tick()
        {
            m_View.GetDestination();
            m_LastAttack += Time.deltaTime;
        }

        public bool TryAttack()
        {
            if (!m_View.HasTarget)
            {
                return false;
            }
            if (m_TimeBetweenAttack <= m_LastAttack)
            {
                m_LastAttack = 0f;
                return true;
            }
            return false;
        }

        public void AttachView(EnemyView view)
        {
            m_View = view;
            m_CentersAttackDistance = view.TargetCollisionRadius + view.MyCollisionRadius + m_CollidersAttackDistance;
            m_SqrCentersAttackDistance = m_CentersAttackDistance * m_CentersAttackDistance;
            view.AttachData(this);
        }

        public void OnDeath()
        {
            Game.AddScore(m_Score);
            DestroyView();
        }

        private void DestroyView()
        {
            m_IsDestroyed = true;
            Object.Destroy(m_View.gameObject);
        }

    }
}