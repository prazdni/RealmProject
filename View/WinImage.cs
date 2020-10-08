using UnityEngine;
using UnityEngine.UI;

namespace Realm
{
    public class WinImage : MonoBehaviour, IExecute
    {
        private Image _image;
        private void Start()
        {
            _image = GetComponent<Image>();
        }

        public void Execute()
        {
            _image.fillAmount += Time.deltaTime * 10;
                
            if (Mathf.Approximately(_image.fillAmount, 1.0f))
            {
                _image.fillAmount = 1.0f;
                Time.timeScale = 0.0f;
            }
        }
    }
}