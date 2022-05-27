using UnityEngine;

namespace Assets.Scripts.Spells
{
    public abstract class ProjectileDataBase
    {
        protected ProjectileView m_View;
        protected readonly ProjectileAssetBase m_Asset;
        protected Vector3 m_Direction;
        protected float m_Speed;
        protected LayerMask m_CollisionMask;
        protected bool m_IsDestroyed = false;
        protected bool m_IsCollided = false;
        protected bool m_ToDestroy = false;

        public ProjectileView View => m_View;
        public Vector3 Direction => m_Direction;
        public float Speed => m_Speed;
        public LayerMask CollisionMask => m_CollisionMask;
        public bool IsDestroyed => m_IsDestroyed;
        public bool IsCollided => m_IsCollided;
        public bool ToDestroy => m_ToDestroy;

        public void Move(float deltaTime)
        {
            m_View.transform.position += m_Direction.normalized * (m_Speed * deltaTime);
        }

        public void AttachView(ProjectileView view)
        {
            m_View = view;
            view.AttachData(this);
        }

        public void DestroyView()
        {
            //Debug.Log("Destroying projectile");
            m_IsDestroyed = true;
            Object.Destroy(m_View.gameObject);
        }

        public void CheckCollisions(float collisionDistance)
        {
            Ray ray = new Ray(m_View.transform.position, m_View.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, collisionDistance, m_CollisionMask, QueryTriggerInteraction.Collide))
            {
                OnCollide(hit);
            }
        }
        
        public abstract void OnCollide(RaycastHit hit);

        public abstract void OnDestroy();
    }
}