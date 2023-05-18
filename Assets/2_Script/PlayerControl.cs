using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float _jumpForce = 5;
    [SerializeField] float _angleCalc = 10;
    Animator _aniCtrl;
    Rigidbody2D _rdbd2D;
    SpriteRenderer _playerModel;

    const float _velocityX = 3;
    float _currentAngle;
    bool _isSurface;
    bool _isDeath;
    Vector3 _referencePos;
    int _limitJumpCount = 2;
    int _nowJumpCount = 0;

    public bool _isDead
    {
        get { return _isDeath; }
    }

    void Awake()
    {
        _aniCtrl = GetComponent<Animator>();
        _rdbd2D = GetComponent<Rigidbody2D>();
        _playerModel = transform.GetChild(0).GetComponent<SpriteRenderer>();

        _currentAngle = 0.0f;
        _isSurface = false;
        _isDeath = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Jump();
        }
        //Debug.Log(_rdbd2D.velocity.y);

        ApplyAngle();
        BackToPlace();
    }

    void Jump()
    {
        _nowJumpCount++;
        if (_isDeath || _nowJumpCount >= _limitJumpCount)
            return;
        // 위쪽으로 무게와 상관없이 일정한 힘을 가한다
        _rdbd2D.velocity = new Vector2(0, _jumpForce);
        _isSurface = false;
    }

    void ApplyAngle()
    {
        float targetAngle = Mathf.Atan2(_rdbd2D.velocity.y, _velocityX) * Mathf.Rad2Deg;
        _currentAngle = Mathf.Lerp(_currentAngle, targetAngle, Time.deltaTime * _angleCalc);
        _playerModel.transform.localRotation = Quaternion.Euler(0, 0, _currentAngle);

        if (_isDeath)
            return;
        if (_isSurface)
        {
            _nowJumpCount = 0;
            _aniCtrl.SetBool("IsJump", false);
            _aniCtrl.SetBool("IsDown", false);
        }
        else
        {
            if (_rdbd2D.velocity.y > 0)
            {
                _aniCtrl.SetBool("IsJump", true);
                _aniCtrl.SetBool("IsDown", false);
            }
            else
            {
                _aniCtrl.SetBool("IsJump", true);
                _aniCtrl.SetBool("IsDown", true);
            }
        }
    }

    void BackToPlace()
    {
        if (_isDeath)
            return;
        if (_isSurface && (transform.position.x < _referencePos.x))
        {
            Vector3 target = new Vector3(_referencePos.x, transform.position.y, 0);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 3);
        }
    }

    public void InitData(Vector3 rPos)
    {
        _referencePos = rPos;
    }

    void OncollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Surface"))
        {
            _isSurface = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Surface"))
        {
            _isSurface = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CoinObstacle"))
        {
        }

        if (other.CompareTag("BoomObstacle"))
        {
            _isDeath = true;
            _aniCtrl.SetBool("IsDead", _isDeath);
            //GetComponent<CircleCollider2D>().enabled = false;
            //_rdbd2D.gravityScale = 0;
        }
    }

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    _isSurface = false;
    //}

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 0, 300, 90), "Idle"))
    //    {
    //        _aniCtrl.SetBool("IsJump", false);
    //        _aniCtrl.SetBool("IsDown", false);
    //    }
    //    if (GUI.Button(new Rect(0, 100, 300, 90), "JumpUp"))
    //    {
    //        _aniCtrl.SetBool("IsJump", true);
    //        _aniCtrl.SetBool("IsDown", false);
    //    }
    //    if (GUI.Button(new Rect(310, 100, 300, 90), "JumpDown"))
    //    {
    //        _aniCtrl.SetBool("IsJump", false);
    //        _aniCtrl.SetBool("IsDown", true);
    //    }
    //    if (GUI.Button(new Rect(310, 0, 300, 90), "Dead"))
    //    {
    //        _aniCtrl.SetBool("IsDead", true);
    //    }
    //}
}
