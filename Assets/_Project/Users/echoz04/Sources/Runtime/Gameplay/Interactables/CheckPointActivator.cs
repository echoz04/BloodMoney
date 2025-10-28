using Sources.Runtime.Gameplay.Character;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Interactables
{
    public class CheckPointActivator : MonoBehaviour
    {
        [SerializeField] private Animator _checkPointAnimator;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<CharacterRoot>())
            {
                _checkPointAnimator.enabled = true;
            }
        }
    }
}
