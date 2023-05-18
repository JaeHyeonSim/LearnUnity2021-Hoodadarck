using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // 스크립트 사용에 필요

public class TitleMessageWnd : MonoBehaviour
{
    [SerializeField] Text _txtMessage;

    public void OpenedTMessageWnd(bool isOpen = false, string message = "")
    {
        if (isOpen)
            _txtMessage.text = message;

        gameObject.SetActive(isOpen);
    }
}
