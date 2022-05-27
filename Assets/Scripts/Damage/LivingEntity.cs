using UnityEngine;

namespace Assets.Scripts.Damage
{
    public class LivingEntity: IDamageable
    {
        protected int m_Health;
        protected int m_MaxHealth;
        protected bool m_IsDied = false;

        public int Health => m_Health;
        public int MaxHealth => m_MaxHealth;
        public bool IsDied => m_IsDied;

        public void TakeHit(int damage, RaycastHit hit)
        {
            TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            m_Health -= damage;
            if (m_Health <= 0 && !m_IsDied)
            {
                Die();
            }
        }

        private void Die()
        {
            m_IsDied = true;
        }
    }
}
