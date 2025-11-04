using UnityEngine;

namespace Sources.Runtime.Menu
{
    public class QuitGameButton : MonoBehaviour
    {
        public void Do()
        {
            Application.Quit();
        }
    }
}