using UnityEngine;

namespace Assets.Scripts.Spells
{
    [CreateAssetMenu(menuName = "Assets/LightningAsset", fileName = "Lightning")]
    public class LightningAsset: ProjectileAssetBase
    {
        public int Damage;
        public override ProjectileDataBase CreateProjectile(Vector3 position, Vector3 direction, Quaternion rotation)
        {
            Lightning lightning = new Lightning(this, direction);
            ProjectileView view = Object.Instantiate(View, position, rotation);
            lightning.AttachView(view);
            return lightning;
        }
    }
}