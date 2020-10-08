using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Realm
{
    public class RestartButton : MonoBehaviour
    {
        #region Fields

        private Button _button;
        private Text _text;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(
                () =>
                {
                    SceneManager.LoadScene(0);
                    Time.timeScale = 1.0f;
                });
            
            _text = _button.GetComponentInChildren<Text>();
            _text.text = String.Empty;
        }

        #endregion


        #region Methods

        public void ActivateButton(bool isActive)
        {
            _button.interactable = isActive;
            
            _text.text = String.Empty;
            
            if (isActive)
            {
                _text.text = "Restart";
            }
        }

        #endregion
    }
}