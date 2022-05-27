using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    public class Fireball: ProjectileDataBase
    {
        private int m_TargetDamage;
        private float m_DestructionDamage;
        public Fireball(FireballAsset asset, Vector3 direction)
        {
            m_TargetDamage = asset.TargetDamage;
            m_DestructionDamage = asset.DestructionDamage;
            m_CollisionMask = asset.CollisionMask;
            m_Speed = asset.Speed;
            m_Direction = direction;
        }
        
        public override void OnCollide(RaycastHit hit)
        {
            //Debug.Log("Collision with " + hit.collider.gameObject.name);
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                //Debug.Log("Make damage");
                damageable.TakeHit(m_TargetDamage, hit);
            }
            m_IsCollided = true;
            m_ToDestroy = true;
        }

        public override void OnDestroy()
        {
            // TODO Make destruction damage
        }
    }
}
