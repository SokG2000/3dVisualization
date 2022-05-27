using System.Collections.Generic;
using Assets.Scripts.Player;
using Assets.Scripts.Spells;
using UnityEngine;

namespace Assets.Scripts.Assets
{
    [CreateAssetMenu(menuName = "Assets/Asset Root", fileName = "Asset Root")]
    public class RootAsset : ScriptableObject
    {
        public List<LevelAsset> Levels;
        public PlayerAsset PlayerAsset;
        public FireballAsset FireballAsset;
        public LightningAsset LightningAsset;
    }
}
