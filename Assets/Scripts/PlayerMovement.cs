using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum PlayerStates { idle, walking, jumping, duck, falling }
    private float _horizontal;
    private float _vertical;
    
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _jumpable;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    
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
            _state = PlayerStates.duck;
        }

        if (_body.velocity.y > .1f)
        {
            _state = PlayerStates.jumping;
        }
        else if (_body.velocity.y < -.1f)
        {
            _state = PlayerStates.falling;
        }
        
        _animator.SetInteger("state", (int)_state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, _jumpable);
    }
}
