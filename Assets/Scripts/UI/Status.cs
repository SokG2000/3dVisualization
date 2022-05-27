using Assets.Scripts.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Status: MonoBehaviour
    {
        [SerializeField] private Text m_Health;
        [SerializeField] private Text m_Score;

        void OnEnable()
        {
            Game.GetLifes += ChangeHealth;
            Game.GetScore += ChangeScore;
            Game.UpdateHealth();
        }

        void OnDisable()
        {
            Game.GetLifes -= ChangeHealth;
            Game.GetScore -= ChangeScore;
        }

        private void ChangeHealth(int newHealth)
        {
            m_Health.text = "Health: " + newHealth;
        }

        private void ChangeScore(int newScore)
        {
            m_Score.text = "Score: " + newScore;
        }
    }
}