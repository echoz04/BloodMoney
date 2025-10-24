using UnityEngine;
using VContainer.Unity;

namespace Sources.Runtime.Gameplay.Character
{
    public sealed class CursorLocker
    {
        public void Lock()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Unlock()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
