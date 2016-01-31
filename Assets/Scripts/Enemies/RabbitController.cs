using UnityEngine;
using System.Collections;

public class RabbitController : MonoBehaviour {

    public float maxSpeed = 5f;
    public float jumpForce = 300f;
    public Transform groundCheck;
    public LayerMask groundLayers;
    public Transform startPosition;
    public Transform endPosition;
    public RectTransform healthBar;

    [HideInInspector]
    public bool _facingRight = true;
    private bool _isGrounded = false;
    private float _groundRadius = 0.2f;
    private bool _hasTouched = false;
    private bool _isGoingToEnd = true;
    private bool _isFollowingPlayer = false;

    Rigidbody2D _rigidbody2D;
    Animator _animator;
    BoxCollider2D _boxCollider2D;
    GameObject _player;

    void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _player = GameObject.Find("Player");
    }

    void Update() {
        if (GetComponent<EnemyData>()._actualHealth <= 0f) {
            Touched();
        }

        if (_facingRight) {
            healthBar.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (!_facingRight) {
            healthBar.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void FixedUpdate() {
        if (Vector3.Distance(transform.position, _player.transform.position) <= 4.0f && !_hasTouched) {
            _isFollowingPlayer = true;

            Vector3 _direction = (_player.transform.position - transform.position).normalized * (GetComponent<EnemyData>().config.MoveSpeed * 1.05f);

            _rigidbody2D.velocity = _direction;
        }

        if (Vector3.Distance(transform.position, _player.transform.position) >= 5.0f && _isFollowingPlayer) {
            _isFollowingPlayer = false;
        }

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, _groundRadius, groundLayers);

        _animator.SetBool("Touched", _hasTouched);

        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));

        if (_isGoingToEnd && !_hasTouched && !_isFollowingPlayer) {
            Vector3 _direction = (endPosition.position - transform.position).normalized * GetComponent<EnemyData>().config.MoveSpeed;

            _rigidbody2D.velocity = _direction;
        }
        else if (!_isGoingToEnd && !_hasTouched && !_isFollowingPlayer) {
            Vector3 _direction = (startPosition.position - transform.position).normalized * GetComponent<EnemyData>().config.MoveSpeed;

            _rigidbody2D.velocity = _direction;
        }

        if (Vector3.Distance(transform.position, endPosition.position) <= 1f && _isGoingToEnd) {
            _isGoingToEnd = false;
        }
        else if (Vector3.Distance(transform.position, startPosition.position) <= 1f && !_isGoingToEnd) {
            _isGoingToEnd = true;
        }

        if (!_isFollowingPlayer) {
            if (_isGoingToEnd && !_facingRight) {
                Flip();
            }
            else if (!_isGoingToEnd && _facingRight) {
                Flip();
            }
        }
        else if (_isFollowingPlayer) {
            if (transform.position.x - _player.transform.position.x <= 0f && !_facingRight) {
                Flip();
            }
            else if (transform.position.x - _player.transform.position.x >= 0f && _facingRight) {
                Flip();
            }
        }
    }

    void Flip() {
        _facingRight = !_facingRight;
        Vector3 _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
    }

    void OnTriggerEnter2D(Collider2D _col) {
        if (_col.gameObject.tag.Equals("Player") && PlayerManager.Instance.canBeTouched) {
            PlayerManager.Instance.UpdateLife(GetComponent<EnemyData>().config.Damage);
            PlayerManager.Instance.canBeTouched = false;

            Touched();
        }
    }

    public void Touched() {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.isKinematic = true;
        _hasTouched = true;
    }

    public void CanBeTouched() {
        _rigidbody2D.isKinematic = false;
        _hasTouched = false;
    }
}
