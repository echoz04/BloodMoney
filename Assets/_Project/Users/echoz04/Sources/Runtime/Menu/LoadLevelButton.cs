using Sources.Runtime.Services.Loaders.Scene;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Sources.Runtime.Menu
{
    public class LoadLevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Scene _sceneToLoad;
        
        private void OnValidate() =>
            _button = GetComponent<Button>();

        [Inject]
        private void Construct(MenuLogic menuLogic) =>
            _button.onClick.AddListener(() => menuLogic.LoadLevel(_sceneToLoad));
    }
}
