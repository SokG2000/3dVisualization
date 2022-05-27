using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts.Assets
{
    [CreateAssetMenu(menuName = "Assets/WaveAsset", fileName = "WaveAsset")]
    public class WaveAsset: ScriptableObject
    {
        public EnemyAsset EnemyAsset;
        public int EnemiesNumber;
        public float TimeBetweenSpawns;
        public float StartTime;
        public Vector3 SpawnPosition;
    }
}