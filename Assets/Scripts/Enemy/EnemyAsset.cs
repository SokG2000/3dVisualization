using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [CreateAssetMenu(menuName = "Assets/EnemyAsset", fileName = "EnemyAsset")]
    public class EnemyAsset: ScriptableObject
    {
        public int Damage;
        public int Health;
        public int Score;
        public EnemyView View;
        public float AttackDistance;
        public float TimeBetweenAttack;
    }
}