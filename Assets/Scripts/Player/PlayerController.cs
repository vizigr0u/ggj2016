﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    public Transform groundCheck;
    public LayerMask groundLayers;
    public bool CanMove = true;

    private bool _facingRight = true;
    private bool _isGrounded = false;
    private float _groundRadius = 0.2f;
    private bool _isJumping = false;
    [HideInInspector]
    public bool _isCrouching = false;

    Transform _transform;
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    BoxCollider2D _boxCollider2D;

	void Awake () {
        _transform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update() {
        //inputs
        if (_isGrounded && CanMove) {
            if (Input.GetButtonDown("Jump")) {
                _isJumping = true;
                _rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }

            if (Input.GetAxis("Vertical") <= -0.9f) {
                _isCrouching = true;
            }
            else {
                _isCrouching = false;
            }
        }
        
        //modify player's boxcollider depending on his state
        if (_isJumping) {
            _boxCollider2D.offset = new Vector2(0.05f, 0.04f);
        }
        //else if (_isCrouching) {
            
        //}
        else {
            _boxCollider2D.offset = new Vector2(0.001f, -0.15f);
        }
    }
	
	void FixedUpdate () {
        if (!CanMove)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _animator.SetFloat("Speed", 0f);
            return;
        }
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, _groundRadius, groundLayers);
        _animator.SetBool("Grounded", _isGrounded);
        _animator.SetBool("Jumping", _isJumping);

        _animator.SetFloat("vSpeed", _rigidbody2D.velocity.y);

        float _move = Input.GetAxis("Horizontal");

        _animator.SetFloat("Speed", Mathf.Abs(_move));

        if (!_isCrouching) {
            _rigidbody2D.velocity = new Vector2(_move * maxSpeed, _rigidbody2D.velocity.y);
        }

        if (_move > 0f && !_facingRight) {
            Flip();
        }
        else if (_move < 0f && _facingRight) {
            Flip();
        }
	}
    
    void Flip() {
        _facingRight = !_facingRight;
        Vector3 _scale = _transform.localScale;
        _scale.x *= -1;
        _transform.localScale = _scale;
    }

    public void JumpingFalse() {
        _isJumping = false;
    }
}
