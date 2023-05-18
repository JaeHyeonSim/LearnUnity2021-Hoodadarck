using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObj : MonoBehaviour
{
    public enum eCoinType
    {
        Coin            = 0,
        Crystal,

        count
    }

    [SerializeField] GameObject _prefabEffectObject;
    [SerializeField] float _limitUp = 1;
    [SerializeField] float _limitDown = -1;

    bool _isUp;
    Vector3 _stdPos;
    eCoinType _myType;

    SpriteRenderer _model;

    void Awake()
    {
        _stdPos = transform.localPosition;
    }
    
    void Update()
    {
        if (transform.localPosition.y > (_stdPos.y + _limitUp))
            _isUp = false;
        if (transform.localPosition.y < (_stdPos.y + _limitDown))
            _isUp = true;

        if (_isUp)
            transform.Translate(Vector3.up * Time.deltaTime * 2);
        else
            transform.Translate(Vector3.down * Time.deltaTime * 2);
    }

    public void InitCoin(eCoinType type, Sprite icon)
    {
        _model = GetComponent<SpriteRenderer>();
        _model.sprite = icon;
        _myType = type;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(_prefabEffectObject, transform.position, _prefabEffectObject.transform.rotation);
            IngameManager._instance.GetPoint(_myType);
            Destroy(gameObject);
        }
    }
}
