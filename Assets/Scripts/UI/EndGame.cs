using System;
using Assets.Scripts.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class EndGame: MonoBehaviour
    {
        [SerializeField] private Text m_Result;
        [SerializeField] private GameObject m_EndPanel;
        
        [SerializeField] private String m_MenuScene;

        private String m_WinText = "You won!";
        private String m_LoseText = "You died!";

        void OnEnable()
        {
            Game.EndGame += OnGameEnd;
        }

        void OnDisable()
        {
            Game.EndGame -= OnGameEnd;
        }

        private void OnGameEnd(bool hasWinned)
        {
            m_Result.text = hasWinned ? m_WinText : m_LoseText;
            m_EndPanel.SetActive(true);
        }

        public void OnMenuPressed()
        {
            SceneManager.LoadScene(m_MenuScene);
        }

    }
}