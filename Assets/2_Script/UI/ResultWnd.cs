using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultWnd : MonoBehaviour
{
    [SerializeField] Text _txtTitle;
    [SerializeField] Text _txtCoin1;
    [SerializeField] Text _txtCoin2;
    [SerializeField] Text _txtScore;
    [SerializeField] GameObject _rootSuccess;
    [SerializeField] GameObject _rootFailed;

    public void OpenWindow(bool isSuccess, int coinCount1, int coin1PerPoint, int coinCount2, int coin2PerPoint)
    {
        if (isSuccess)
        {
            _txtTitle.text = "Clear !!";
            _rootSuccess.SetActive(true);
            _rootFailed.SetActive(false);
        }
        else
        {
            _txtTitle.text = "Failed";
            _rootSuccess.SetActive(false);
            _rootFailed.SetActive(true);
        }
        _txtCoin1.text = coinCount1.ToString();
        _txtCoin2.text = coinCount2.ToString();
        _txtScore.text = (coinCount1 * coin1PerPoint + coinCount2 * coin2PerPoint).ToString();

    }

    public void ClickHomeButton()
    {

    }

    public void ClickRestartButton()
    {
        SceneManager.LoadScene("IngameScene");
    }

    public void ClickNextButton()
    {

    }
}
