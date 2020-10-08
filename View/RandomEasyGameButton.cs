using System;
using UnityEngine;
using UnityEngine.UI;

namespace Realm
{
    public class RandomEasyGameButton : MonoBehaviour
    {
        #region Fields

        public Action OActionClick = () => { };
        
        private Button _button;

        #endregion


        #region UnityMethods
        
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        #endregion


        #region Methods

        private void OnClick()
        {
            OActionClick.Invoke();
        }

        #endregion
    }
}