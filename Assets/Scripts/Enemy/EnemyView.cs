using System.Collections;
using Assets.Scripts.Damage;
using Assets.Scripts.Runtime;
using Assets.Scripts.Spells;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class EnemyView: MonoBehaviour, IDamageable
    {
        public NavMeshAgent pathfinder;
        //private LivingEntity m_PlayerLivingEntity;
        private Material m_SkinMaterial;
        private Transform m_Target;
        private Color m_OriginalColor;
        private Color m_AttackColor;
        private float m_UpdatePathFrequency = 0.5f;
        private float m_LastUpdate;
        private EnemyData m_Data;
        private float m_MyCollisionRadius;
        private float m_TargetCollisionRadius;
        private float m_TargetShift;
        private bool m_IsAttacking;
        private bool m_HasTarget;

        public Transform Target => m_Target;
        public float UpdatePathFrequency => m_UpdatePathFrequency;
        public float LastUpdate => m_LastUpdate;
        public float MyCollisionRadius => m_MyCollisionRadius;
        public float TargetCollisionRadius => m_TargetCollisionRadius;
        public bool HasTarget => m_HasTarget;

        public void OnPlayerDeath()
        {
            //Debug.Log("DeathEvent");
            m_HasTarget = false;
            if (pathfinder == null)
            {
                Debug.Log("Destroy pathfinder!!!");
            }
            else
            {
                pathfinder.enabled = false;
            }
        }

        public void Configure()
        {
            pathfinder = GetComponent<NavMeshAgent>();
            m_SkinMaterial = GetComponent<Renderer>().material;
            m_OriginalColor = m_SkinMaterial.color;
            m_AttackColor = Color.red;
            m_MyCollisionRadius = GetComponent<CapsuleCollider>().radius;
            m_LastUpdate = 0f;
            if (!Game.Runner.PlayerController.Data.IsDied)
            {
                m_Target = Game.Runner.PlayerController.Transform;
                //m_PlayerLivingEntity = Game.Runner.PlayerController.Data;
                //m_PlayerLivingEntity.DeathEvent += OnPlayerDeath;
                pathfinder.SetDestination(m_Target.position);
                m_TargetCollisionRadius = m_Target.GetComponent<CapsuleCollider>().radius;
                m_TargetShift = m_MyCollisionRadius + m_TargetCollisionRadius;
                m_HasTarget = true;
            }
            else
            {
                pathfinder.enabled = false;
                m_HasTarget = false;
            }
        }

        public void GetDestination()
        {
            m_LastUpdate += Time.deltaTime;
            if (m_HasTarget)
            {
                if (m_LastUpdate > m_UpdatePathFrequency && !m_IsAttacking)
                {
                    Vector3 direction = (transform.position - m_Target.position).normalized;
                    Vector3 destination = m_Target.position + direction * m_TargetShift;
                    m_LastUpdate = 0f;
                    pathfinder.SetDestination(destination);
                }
            }
        }

        public void AttachData(EnemyData data)
        {
            m_Data = data;
            m_TargetShift = m_MyCollisionRadius + m_TargetCollisionRadius + data.CollidersAttackDistance / 2;
        }

        public IEnumerator Attack()
        {
            //Debug.Log("Attack started");
            m_IsAttacking = true;
            pathfinder.enabled = false;
            m_SkinMaterial.color = m_AttackColor;
            

            Vector3 originalPosition = transform.position;
            Vector3 attackPosition = m_Target.position;
            float attackSpeed = 2;
            float percent = 0;
            
            while (percent <= 1) {
                percent += Time.deltaTime * attackSpeed;
                float interpolation = (1f - percent) * percent * 2;
                transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
                yield return null;
            }
            
            m_SkinMaterial.color = m_OriginalColor;
            pathfinder.enabled = m_HasTarget;
            m_IsAttacking = false;
        }

        public void TakeHit(int damage, RaycastHit hit)
        {
            m_Data.TakeHit(damage, hit);
        }

        public void TakeDamage(int damage)
        {
            m_Data.TakeDamage(damage);
        }
    }
}