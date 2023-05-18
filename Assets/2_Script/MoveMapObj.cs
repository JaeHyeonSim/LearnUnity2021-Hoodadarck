using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMapObj : MonoBehaviour
{
    [SerializeField] float _movSpeed = 3;
    [SerializeField] float _startX = 10;
    [SerializeField] float _endX = -10;

    void Start()
    {
    }

    void Update()
    {
        if (transform.position.x <= _endX)
            transform.Translate(_startX - _endX, 0, 0);
        transform.Translate(Vector3.left * _movSpeed * Time.deltaTime);
    }
}
