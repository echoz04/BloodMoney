using UnityEngine;

namespace Sources.Runtime.Gameplay.Helpers
{
    public class OpenDoorPopup : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
