using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum PlayerStates { idle, walking, jumping, duck, falling, ladderIdle, ladderClimbing }
    private float _horizontal;
    private float _vertical;
    public bool _onLadder = false;
    public float _climbSpeed;
    private float _climbVelocity;
    public float _gravityValue;
    
    
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _jumpable;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    private void Start()
    {
        _gravityValue = _body.gravityScale;
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        _body.velocity = new Vector2(_horizontal * _speed, _body.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            _body.velocity = new Vector2(_body.velocity.x, _jumpForce);
        }

        UpdAnimation();
    } 

    private void UpdAnimation()
    {
        PlayerStates _state;
        
        if (_horizontal > 0f)
        {
            _state = PlayerStates.walking;
            _sprite.flipX = false;
        }
        else if (_horizontal < 0f)
        {
            _state = PlayerStates.walking;
            _sprite.flipX = true;
        }
        else
        {
            _state = PlayerStates.idle;
        }

        if (_vertical < 0f)
        {
            if (!_onLadder)
            {
                _state = PlayerStates.duck;
                if (Input.GetKeyDown(KeyCode.S))
                {
                    _animator.Play("Duck", 0, 0.5f);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.S))
                    _state = PlayerStates.ladderClimbing;
                else if (Input.GetKeyUp(KeyCode.S))
                    _state = PlayerStates.ladderIdle;
            }
        }

        if (_body.velocity.y > .1f)
        {
            _state = PlayerStates.jumping;
        }
        if (_body.velocity.y < -.1f)
        {
            _state = PlayerStates.falling;
        }

        if (_onLadder)
        {
            _body.gravityScale = 0f;
            _climbVelocity = _climbSpeed * Input.GetAxisRaw("Vertical");
            _body.velocity = new Vector2(_body.velocity.x, _climbVelocity);
            if(Input.GetKeyDown(KeyCode.W))
                _state = PlayerStates.ladderClimbing;
            else if (Input.GetKeyUp(KeyCode.W))
                _state = PlayerStates.ladderIdle;
        }
        if(!_onLadder)
        {
            _body.gravityScale = _gravityValue;
        }
        
        _animator.SetInteger("state", (int)_state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, _jumpable);
    }
}
