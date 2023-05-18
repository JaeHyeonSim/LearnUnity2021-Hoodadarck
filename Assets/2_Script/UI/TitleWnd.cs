using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleWnd : MonoBehaviour
{
    [SerializeField] Text _txtStart;

    float _timeCheck = 0;
    float _timeColor = 0;
    int _colorCount = 0;
    float _timeStd = 2;

    void Awake()
    {
        _txtStart.CrossFadeColor(Color.yellow, 0.05f, true, true);
    }

    void Update()
    {
        _timeCheck += Time.deltaTime;
        _timeColor += Time.deltaTime;
        if (_timeCheck >= _timeStd)
        {
            _timeCheck = 0;
            _txtStart.enabled = !_txtStart.enabled;
            if (_txtStart.enabled)
                _timeStd = 2;
            else
                _timeStd = 0.3f;
        }
        if (_timeColor >= 1.5f)
        {
            _timeColor = 0;
            _colorCount++;

            if (_colorCount >= 3)
                _colorCount = 0;
            switch (_colorCount)
            {
                case 0:
                    _txtStart.CrossFadeColor(Color.yellow, 1.0f, true, true);
                    break;
                case 1:
                    _txtStart.CrossFadeColor(Color.green, 1.0f, true, true);
                    break;
                case 2:
                    _txtStart.CrossFadeColor(Color.red, 1.0f, true, true);
                    break;
            }
        }

        ClickAction();
    }

    void ClickAction()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            HomeManager._instance.ChangeTab(true);
        }
    }
}
