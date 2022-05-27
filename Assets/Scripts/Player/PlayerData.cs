using System;
using Assets.Scripts.Damage;
using Assets.Scripts.Runtime;
using Assets.Scripts.Spells;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Player
{
    public class PlayerData: IDamageable
    {
        
        private PlayerView m_View;
        public readonly PlayerAsset Asset;
        private float m_Speed;
        private bool m_HasView;
        private int m_Health;
        private int m_MaxHealth;
        private bool m_IsDied = false;

        public PlayerView View => m_View;
        public float Speed => m_Speed;
        public bool HasView => m_HasView;
        public int Health => m_Health;
        public int MaxHealth => m_MaxHealth;
        public bool IsDied => m_IsDied;

        public PlayerData(PlayerAsset asset)
        {
            Asset = asset;
            m_MaxHealth = asset.Health;
            m_Health = asset.Health;
            m_Speed = asset.Speed;
        }

        public void AttachView(PlayerView view)
        {
            m_View = view;
            view.AttachData(this);
            m_HasView = true;
        }

        public void DestroyView()
        {
            if (m_HasView)
            {
                Game.Runner.EnemyController.OnPlayerDeath();
                Game.PlayerDeath();
                m_HasView = false;
                //Debug.Log(m_View.gameObject);
                Object.Destroy(m_View.gameObject);
            }
        }
        public void TakeHit(int damage, RaycastHit hit)
        {
            TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            m_Health -= damage;
            if (m_Health <= 0 && !m_IsDied)
            {
                m_Health = 0;
                Die();
            }
            Game.ChangeHealth(m_Health);
        }
        private void Die()
        {
            m_IsDied = true;
        }
    }
}