using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public class GameQuitter : MonoBehaviour
    {
        // Start is called before the first frame update
        public void OnQuitPressed()
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }
}
