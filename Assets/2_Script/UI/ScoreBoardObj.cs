using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardObj : MonoBehaviour
{

    [SerializeField] RectTransform _bg;
    [SerializeField] Text _txtCoinCount;
    [SerializeField] Text _txtCrystalCount;

    const float _itemHeight = 60;

    public void InitItemCount(int itemCnt = 1)
    {
        _bg.sizeDelta = new Vector2(_bg.sizeDelta.x, _itemHeight * itemCnt);
        ShowCoinCount(0);
        ShowCrystalCount(0);
    }

    public void ShowCoinCount(int count)
    {
        _txtCoinCount.text = count.ToString();
    }
    public void ShowCrystalCount(int count)
    {
        _txtCrystalCount.text = count.ToString();
    }
}
