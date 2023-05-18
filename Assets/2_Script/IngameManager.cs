using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct stScoreCount
{
    public int _coinCount;
    public int _crystalCount;
    public int _coinPoint;
    public int _crystalPoint;

    public stScoreCount(int coinPoint, int cryPoint)
    {
        _coinCount = 0;
        _crystalCount = 0;
        _coinPoint = coinPoint;
        _crystalPoint = cryPoint;
    }
}

public class IngameManager : MonoBehaviour
{
    public enum eGameState
    {
        Load                = 0,            // 맵과 플레이어를 세팅한다.
        Ready,                              // 화면에 UI룰 띄우고 화면 스크롤만 한다.
        Play,                               // 실질적 게임 플레이를 한다.
        End,                                // Over와 Clear를 출력하여 결과단계의 준비를 한다.
        Result                              // 게임 결과를 출력하고 다음을 뭘할지 결정하게 한다.
    }

    static IngameManager _uniqueInstance;

    [SerializeField] GameObject _preFabPlayerObj;
    [SerializeField] GameObject[] _maps;
    [SerializeField] GameObject _prefabTitleMessageWnd;
    [SerializeField] GameObject _prefabResultWnd;
    [SerializeField] GameObject _prefabCloseWnd;
    [SerializeField] float _endTime = 60;

    const float _limitReadyTime = 3;
    float _checkTime = 0;

    // 사용 참조 함수
    Transform _posPlayerSpawn;
    Transform _posReference;
    TitleMessageWnd _wndTMessage;
    TimerObj _timer;
    ScoreBoardObj _scoreBoard;
    PlayerControl _player;
    ExitWnd _wndExit;
    

    // 사용 정보 함수
    eGameState _gameState;
    int _nowMapIndex;
    float _nowTime;
    int _scoreType;
    stScoreCount _score;
    bool _isClear;

    public eGameState _eStateGame
    {
        get { return _gameState; }
    }

    static public IngameManager _instance
    {
        get { return _uniqueInstance; }
    }

    void Awake()
    {
        _uniqueInstance = this;
    }

    void Start()
    {
        // 임시.
        _scoreType = 1;
        LoadGame(1);
    }

    void Update()
    {
        _checkTime -= Time.deltaTime;

        switch (_gameState)
        {
            case eGameState.Ready:
                if (_checkTime <= 1.2)
                    _wndTMessage.OpenedTMessageWnd(false, "");
                if (_checkTime <= 1 && _checkTime > 0)
                    _wndTMessage.OpenedTMessageWnd(true, "Play!");
                if (_checkTime <= 0)
                    PlayGame();
                break;

            case eGameState.Play:
                _nowTime -= Time.deltaTime;
                if (_nowTime <= 0)
                {
                    _nowTime = 0;
                    EndGame(true);
                }
                else
                {
                    if (_player._isDead)
                        EndGame(false);
                }
                _timer.ShowTimer(_nowTime);
                break;

            case eGameState.End:
                _checkTime += Time.deltaTime;
                if (_checkTime >= 2.5f)
                    ResultGame(_isClear);
                break;
        }

        if (Input.GetKeyDown("escape"))
        {
            if (_wndExit == null)
            {
                GameObject go = Instantiate(_prefabCloseWnd);
                _wndExit = go.GetComponent<ExitWnd>();
                Time.timeScale = 0;
            }
            else
            {
                if (_wndExit.gameObject.activeSelf)
                {
                    _wndExit.gameObject.SetActive(false);
                    Time.timeScale = 1;
                }
                else
                {
                    _wndExit.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }

    }

    /// <summary>
    ///  Player 프리펩을 게임상에 지정한 위치에 등장시키는 함수.
    /// </summary>
    void SpawnPlayerObject()
    {
        GameObject go = Instantiate(_preFabPlayerObj, _posPlayerSpawn.position, _posPlayerSpawn.rotation);
        _player = go.GetComponent<PlayerControl>();
        _player.InitData(_posReference.position);
    }

    void MapLoad()
    {
        Instantiate(_maps[_nowMapIndex], Vector3.zero, Quaternion.identity);
    }

    public void LoadGame(int mapNumber)
    {
        _gameState = eGameState.Load;

        _nowMapIndex = mapNumber - 1;
        MapLoad();

        //GameObject go = GameObject.Find("PlayerSpawnPos");              // 이름을 이용해서 게임오브젝트를 찾아오는 방법
        GameObject go = GameObject.FindGameObjectWithTag("PlayerSpawn");  // Tag를 이용해서 게임오브젝트를 찾아오는 방법
        _posPlayerSpawn = go.transform;
        go = GameObject.FindGameObjectWithTag("ReferencePos");
        _posReference = go.transform;
        go = GameObject.FindGameObjectWithTag("TimerUI");
        _timer = go.GetComponent<TimerObj>();
        go = GameObject.FindGameObjectWithTag("ScoreBoardUI");
        _scoreBoard = go.GetComponent<ScoreBoardObj>();
        go = Instantiate(_prefabTitleMessageWnd);
        _wndTMessage = go.GetComponent<TitleMessageWnd>();

        _wndTMessage.OpenedTMessageWnd(true, "Ready~");

        SpawnPlayerObject();

        _nowTime = _endTime;
        _timer.ShowTimer(_nowTime);
        switch (_scoreType)
        {
            case 1:
                _score = new stScoreCount(5, 0);
                break;
            case 2:
                _score = new stScoreCount(5, 10);
                break;
        }
        _scoreBoard.InitItemCount(_scoreType);
        // ^^^^ 여기까지 로드에 필요한 내용
        ReadyGame();
    }

    public void ReadyGame()
    {
        _gameState = eGameState.Ready;

        _checkTime = _limitReadyTime;
    }

    public void PlayGame()
    {
        _gameState = eGameState.Play;

        //_wndTMessage.OpenedTMessageWnd();
    }

    public void EndGame(bool isSuccess)
    {
        _gameState = eGameState.End;

        _checkTime = 0;
        _isClear = isSuccess;

        if (isSuccess)
            _wndTMessage.OpenedTMessageWnd(true, "Game Clear!!");
        else
            _wndTMessage.OpenedTMessageWnd(true, "Game Over...");

        ObstacleObj[] objs1 = GameObject.FindObjectsOfType<ObstacleObj>();
        for (int n = 0; n < objs1.Length; n++)
            objs1[n].enabled = false;

        MoveMapObj[] objs2 = GameObject.FindObjectsOfType<MoveMapObj>();
        for (int n = 0; n < objs2.Length; n++)
            objs2[n].enabled = false;

        ObstacleSpawnManagerObject mng = GameObject.FindObjectOfType<ObstacleSpawnManagerObject>();
        mng.enabled = false;

        ResultGame(_isClear);
    }

    public void ResultGame(bool isSuccess)
    {
        _gameState = eGameState.Result;

        _wndTMessage.OpenedTMessageWnd();
        // 결과창을 연다.
        GameObject go = Instantiate(_prefabResultWnd);
        ResultWnd wndR = go.GetComponent<ResultWnd>();
        wndR.OpenWindow(isSuccess, _score._coinCount, _score._coinPoint, _score._crystalCount, _score._crystalPoint);
    }

    public int _myScoreType
    {
        get { return _scoreType; }
    }

    public void GetPoint(CoinObj.eCoinType type)
    {
        // 타입에 따른 카운트
        switch (type)
        {
            case CoinObj.eCoinType.Coin:
                _score._coinCount += 1;
                _scoreBoard.ShowCoinCount(_score._coinCount);
                break;
            case CoinObj.eCoinType.Crystal:
                _score._crystalCount += 1;
                _scoreBoard.ShowCrystalCount(_score._crystalCount);
                break;
        }
    }
}
