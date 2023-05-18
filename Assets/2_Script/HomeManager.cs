using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] GameObject _wndTitle;
    [SerializeField] GameObject _wndSelect;
    int _selectStageNum = -1;

    static HomeManager _uniqueInstance;

    public static HomeManager _instance
    {
        get { return _uniqueInstance; }
    }

    void Awake()
    {
        _uniqueInstance = this;
        ChangeTab(false);
    }

    void Update()
    {
        
    }

    public void ChangeTab(bool isSelect)
    {
        _wndTitle.SetActive(!isSelect);
        _wndSelect.SetActive(isSelect);
    }

    public void SetSelectNum(int num)
    {
        _selectStageNum = num;
    }

    public void ClickGameStartButton()
    {

    }
}
