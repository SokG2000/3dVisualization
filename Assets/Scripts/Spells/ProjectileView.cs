using UnityEngine;

namespace Assets.Scripts.Spells
{
    public class ProjectileView: MonoBehaviour
    {
        //private float m_LifeTime = 5f;
        private ProjectileDataBase m_Data;

        //public void Start()
        //{
        //    Object.Destroy(gameObject, m_LifeTime);
        //}
        public void AttachData(ProjectileDataBase data)
        {
            m_Data = data;
        }
    }
}