using TMPro;
using UnityEngine;

namespace Sources.Runtime.Game
{
    public class DisplayFPS : MonoBehaviour
    {
        [SerializeField] private bool _showFPS = false;
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField] private float _hudRefreshRate = 0.1f;

        private float _timer;

        public void HandleWorkingState(bool state)
        {
            if (state == true)
            {
                _showFPS = true;
                _fpsText.gameObject.SetActive(true);
            }
            else
            {
                _showFPS = false;
                _fpsText.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            _showFPS = false;
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (_showFPS == false)
                return;
            
            if(_fpsText.gameObject.activeSelf == false)
                _fpsText.gameObject.SetActive(true);

            if (Time.unscaledTime > _timer)
            {
                int fps = (int)(1f / Time.unscaledDeltaTime);
                _fpsText.text = "FPS: " + fps;
                _timer = Time.unscaledTime + _hudRefreshRate;
            }
        }
    }
}