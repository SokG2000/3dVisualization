using Assets.Scripts.Runtime;
using Assets.Scripts.Spells;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Player
{
    public class PlayerController: IController
    {
        private PlayerAsset m_Asset;
        private PlayerData m_Data;
        private Transform m_Transform;
        private Camera m_Camera;
        //private float m_MaxX, m_MaxY, m_MinX, m_MinY;
        private ProjectileController m_ProjectileController;

        public PlayerAsset Asset => m_Asset;
        public PlayerData Data => m_Data;
        public Transform Transform => m_Transform;
        public Camera Camera => m_Camera;

        public PlayerController(PlayerAsset asset, ProjectileController projectileController)
        {
            m_Asset = asset;
            m_ProjectileController = projectileController;
        }
        public void OnStart()
        {
            PlayerView view = Object.Instantiate(m_Asset.ViewPrefab);
            m_Data = new PlayerData(m_Asset);
            m_Data.AttachView(view);
            m_Transform = m_Data.View.transform;
            m_Camera = Camera.main;
            //Game.ChangeHealth(m_Data.Health);
            /*m_MaxX = 4;
            m_MaxY = 4;
            m_MinX = -4;
            m_MinY = -4;*/
        }

        public void OnStop()
        {
            
        }

        public void Tick()
        {
            if (!m_Data.IsDied)
            {
                Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                Vector3 moveVelocity = moveInput.normalized * m_Data.Speed;
                //m_Transform.position += moveVelocity * Time.deltaTime;
                Move(moveVelocity);

                Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                Plane ground = new Plane(Vector3.up, Vector3.zero);
                float rayDistance;
                if (ground.Raycast(ray, out rayDistance))
                {
                    Vector3 groundPoint = ray.GetPoint(rayDistance);
                    Vector3 lookPoint = GetLookPoint(groundPoint);
                    //Debug.DrawLine(ray.origin, point, Color.red);
                    LookAt(lookPoint);
                    if (Input.GetMouseButtonDown(0))
                    {
                        CreateFireball(lookPoint);
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        CreateLightning(lookPoint);
                    }
                }
            } else if (m_Data.HasView)
            {
                m_Data.DestroyView();
            }
        }

        private void Move(Vector3 moveVelocity)
        {
            Vector3 newPosition;
            newPosition = m_Transform.position + moveVelocity * Time.deltaTime;
            /*if (newPosition.x > m_MaxX)
            {
                newPosition.x = m_MaxX;
            }
            if (newPosition.x < m_MinX)
            {
                newPosition.x = m_MinX;
            }
            if (newPosition.z > m_MaxY)
            {
                newPosition.z = m_MaxY;
            }
            if (newPosition.z < m_MinY)
            {
                newPosition.z = m_MinY;
            }*/
            m_Transform.position = newPosition;
        }

        private Vector3 GetLookPoint(Vector3 groundPoint)
        {
            return new Vector3(groundPoint.x, m_Transform.position.y, groundPoint.z);
        }

        private void LookAt(Vector3 lookPoint)
        {
            m_Transform.LookAt(lookPoint);
        }

        private void CreateFireball(Vector3 target)
        {
            //Debug.Log("Creating Fireball");
            Vector3 direction = (target - m_Transform.position).normalized;
            m_ProjectileController.CreateFireball(m_Transform.position, direction, m_Transform.rotation);
        }

        private void CreateLightning(Vector3 target)
        {
            //Debug.Log("Creating Lightning");
            Vector3 direction = (target - m_Transform.position).normalized;
            m_ProjectileController.CreateLighthing(m_Transform.position, direction, m_Transform.rotation);
        }
    }
}