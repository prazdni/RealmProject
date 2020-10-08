using UnityEngine;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
    #region Fields

    private Button _button;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(
            () =>
            {
                Application.Quit();
            });
    }

    #endregion
}
