using System;
using System.Collections.Generic;
using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts.Assets
{
    [CreateAssetMenu(menuName = "Assets/LevelAsset", fileName = "LevelAsset")]
    public class LevelAsset : ScriptableObject
    {
        public String SceneName;
        public List<WaveAsset> Waves;
    }
}
