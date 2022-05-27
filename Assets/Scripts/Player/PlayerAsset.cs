using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(menuName = "Assets/PlayerAsset", fileName = "PlayerAsset")]
    public class PlayerAsset: ScriptableObject
    {
        public float Speed;
        public int Health;
        public PlayerView ViewPrefab;
    }
}