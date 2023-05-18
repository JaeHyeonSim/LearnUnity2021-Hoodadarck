using UnityEngine;

public class ObstacleSpawnManagerObject : MonoBehaviour
{
    public enum eObstacleType
    {
        BOOM                    = 0,
        STAIRS,
        ROCK,
        SHIELD
    }

    [SerializeField] GameObject[] _prefabObstacles;
    [SerializeField] float _intervalTime = 2;
    [SerializeField] float _standardTime = 2;
    [SerializeField] Sprite[] _pointImages;

    float _nowDelayTime = 0;

    void Start()
    {
    }

    void Update()
    {
        if (IngameManager._instance._eStateGame == IngameManager.eGameState.Play)
        {
            _nowDelayTime -= Time.deltaTime;
            if (_nowDelayTime <= 0)
                CreateObstacleObject();
        }
    }

    void CreateObstacleObject()
    {
        int idx = 0;
        GameObject go = SelectPrefabObstacleObject(out idx);
        Instantiate(go, transform.position, Quaternion.identity);
        ObstacleObj obj = go.GetComponent<ObstacleObj>();
        if (idx < 3)
        { // 점수가 있는 장애물
            switch (IngameManager._instance._myScoreType)
            {
                case 1:
                    obj.InitData(_pointImages[0], (CoinObj.eCoinType.Coin));
                    break;
                case 2:
                    int type = Random.Range(0, (int)(CoinObj.eCoinType.count));
                    obj.InitData(_pointImages[type], (CoinObj.eCoinType)type);
                    break;
            }
        }
        else
        { // 점수가 없는 장애물
            obj.InitData();
        }
    }

    /// <summary>
    /// 가지고 있는 프리펩 중 하나를 외부로 반환하는 함수.
    /// </summary>
    /// <returns>프리펩을 반환한다.</returns>
    GameObject SelectPrefabObstacleObject(out int idx)
    {
        idx = Random.Range(0, _prefabObstacles.Length);
        _nowDelayTime = _standardTime + Random.Range(0.0f, _intervalTime);
        return _prefabObstacles[idx];
    }
}
