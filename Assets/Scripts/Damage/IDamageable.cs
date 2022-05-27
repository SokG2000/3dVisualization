using UnityEngine;

namespace Assets.Scripts.Damage
{
    public interface IDamageable
    {
        void TakeHit(int damage, RaycastHit hit);

        void TakeDamage(int damage);
    }
}