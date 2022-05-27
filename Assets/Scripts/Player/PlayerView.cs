using Assets.Scripts.Damage;
using Assets.Scripts.Spells;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerView : MonoBehaviour, IDamageable
    {
        private PlayerData m_Data;

        public void AttachData(PlayerData data)
        {
            m_Data = data;
        }

        /*public void Move(Vector3 moveInput)
        {
        }*/
        public void TakeHit(int damage, RaycastHit hit)
        {
            m_Data.TakeHit(damage, hit);
        }

        public void TakeDamage(int damage)
        {
            m_Data.TakeDamage(damage);
        }
    }
}
