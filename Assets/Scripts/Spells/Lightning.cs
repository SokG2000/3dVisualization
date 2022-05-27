using System.Collections.Generic;
using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    public class Lightning: ProjectileDataBase
    {
        private int m_Damage;
        private HashSet<IDamageable> m_Damaged;

        public Lightning(LightningAsset asset, Vector3 direction)
        {
            m_Damage = asset.Damage;
            m_Speed = asset.Speed;
            m_Direction = direction;
            m_CollisionMask = asset.CollisionMask;
            m_Damaged = new HashSet<IDamageable>();
        }

        public override void OnCollide(RaycastHit hit)
        {
            //Debug.Log(hit.collider.gameObject.name);
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                if (!m_Damaged.Contains(damageable))
                {
                    m_Damaged.Add(damageable);
                    //Debug.Log("Make damage");
                    damageable.TakeHit(m_Damage, hit);
                }
            }
            m_IsCollided = true;
        }

        public override void OnDestroy()
        {
            throw new System.NotImplementedException();
        }
    }
}