using UnityEngine;

namespace Assets.Scripts.Spells
{
    [CreateAssetMenu(menuName = "Assets/FireballAsset", fileName = "Fireball")]
    public class FireballAsset: ProjectileAssetBase
    {
        public int TargetDamage;
        public int DestructionDamage;

        public override ProjectileDataBase CreateProjectile(Vector3 position, Vector3 direction, Quaternion rotation)
        {
            Fireball fireball = new Fireball(this, direction);
            ProjectileView view = Object.Instantiate(View, position, rotation);
            fireball.AttachView(view);
            return fireball;
        }
    }
}