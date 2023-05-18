using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObj : MonoBehaviour
{
    [SerializeField] ObstacleSpawnManagerObject.eObstacleType _type;
    [SerializeField] float _movSpeed = 3;
    float _endX = -15;
    int _limitW = 0;
    int _limitH = 0;

    SpriteRenderer _model;
    CoinObj _coin;

    public void InitData(Sprite img = null, CoinObj.eCoinType type = CoinObj.eCoinType.Coin)
    {
        _model = transform.GetChild(0).GetComponent<SpriteRenderer>();
        switch (_type)
        {
            case ObstacleSpawnManagerObject.eObstacleType.BOOM:
                InitBoom(img, type);
                break;
            case ObstacleSpawnManagerObject.eObstacleType.STAIRS:
                // 크기와 위치 연산해서 자리를 잡도록 한다.
                InitStairs();
                break;
            }
    }

    void InitBoom(Sprite img = null, CoinObj.eCoinType type = CoinObj.eCoinType.Coin)
    {
        if (img != null)
        {
            _coin = transform.GetComponentInChildren<CoinObj>();
            _coin.InitCoin(type, img);
        }
    }

    void InitStairs()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        int sx = Random.Range(1, _limitW + 1);
        int sy = Random.Range(1, _limitH + 1);

        col.size = _model.size = new Vector2(sx, sy);
        _model.transform.localPosition = new Vector2(0, (_model.size.y / 2) - 0.25f);
        col.offset = new Vector2(0, (_model.size.y / 2) - 0.25f);
    }

    void Update()
    {
        if (transform.position.x <= _endX)
            Destroy(gameObject);

        transform.Translate(Vector3.left * _movSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CoinObstacle"))
        {
            Destroy(gameObject);
        }
    }
}
