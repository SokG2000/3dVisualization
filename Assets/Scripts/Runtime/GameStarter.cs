using Assets.Scripts.Assets;
using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private RootAsset m_RootAsset;
        void Awake()
        {
            Game.SetRootAsset(m_RootAsset);
        }

        public void StartLevel1()
        {
            Game.StartLevel(m_RootAsset.Levels[0], m_RootAsset.PlayerAsset);
        }
    }
}
