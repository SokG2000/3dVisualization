using System.Collections.Generic;
using Assets.Scripts.Runtime;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    public class ProjectileController: IController
    {
        private List<ProjectileDataBase> m_Projectiles;
        private FireballAsset m_FireballAsset;
        private LightningAsset m_LightningAsset;

        private float m_CollisionDist = 0.05f;

        public ProjectileController(FireballAsset fireballAsset, LightningAsset lightningAsset)
        {
            m_FireballAsset = fireballAsset;
            m_LightningAsset = lightningAsset;
        }
        public void OnStart()
        {
            m_Projectiles = new List<ProjectileDataBase>();
        }

        public void OnStop()
        {
            
        }

        public void Tick()
        {
            foreach (ProjectileDataBase projectile in m_Projectiles)
            {
                projectile.Move(Time.deltaTime);
            }
            foreach (ProjectileDataBase projectile in m_Projectiles)
            {
                //Debug.Log(projectile.Speed * Time.deltaTime);
                projectile.CheckCollisions(projectile.Speed * Time.deltaTime + m_CollisionDist);
            }
            ProcessDestruction();
        }

        private void ProcessDestruction()
        {
            foreach (ProjectileDataBase projectile in m_Projectiles)
            {
                if (projectile.ToDestroy)
                {
                    //Debug.Log("Controller destruction command");
                    projectile.OnDestroy();
                    projectile.DestroyView();
                }
            }

            m_Projectiles.RemoveAll(projectile => projectile.IsDestroyed);
        }

        private void CreateProjectile(ProjectileAssetBase asset, Vector3 position, Vector3 direction, Quaternion rotation)
        {
            ProjectileDataBase data = asset.CreateProjectile(position, direction, rotation);
            m_Projectiles.Add(data);
        }

        public void CreateFireball(Vector3 position, Vector3 direction, Quaternion rotation)
        {
            CreateProjectile(m_FireballAsset, position, direction, rotation);
        }

        public void CreateLighthing(Vector3 position, Vector3 direction, Quaternion rotation)
        {
            CreateProjectile(m_LightningAsset, position, direction, rotation);
        }
    }
}