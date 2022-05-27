using UnityEngine;

namespace Assets.Scripts.Spells
{
    public abstract class ProjectileAssetBase: ScriptableObject
    {
        public float Speed;
        public ProjectileView View;
        public LayerMask CollisionMask;
        public abstract ProjectileDataBase CreateProjectile(Vector3 position, Vector3 direction, Quaternion rotation);
    }
}