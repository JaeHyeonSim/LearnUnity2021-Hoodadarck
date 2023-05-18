using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerObj : MonoBehaviour
{
    [SerializeField] Text _txtSec;
    [SerializeField] Text _txtMilSec;

    public void ShowTimer(float nowTime)
    {
        int sec = (int)nowTime;                         // 초
        int milSec = (int)((nowTime - sec) * 100);      // 백분초

        _txtSec.text = sec.ToString();
        if (milSec < 10)
            _txtMilSec.text = "0" + milSec.ToString();
        else
            _txtMilSec.text = milSec.ToString();
    }
}
