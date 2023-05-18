using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitWnd : MonoBehaviour
{
    public void ClickButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ClickXButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
