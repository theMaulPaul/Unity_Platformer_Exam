using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum PlayerStates { idle, walking, jumping, duck, falling, ladderIdle, ladderClimbing, ropeIdle, ropeClimbing, climbUp, ledgeGrab }
    private float _horizontal;
    private float _vertical;
    private float _climbVelocity;
    
    public float _climbSpeed;
    public float _gravityValue;
    public bool _onLadder = false;
    public bool _onRope = false;

    public Rigidbody2D _body;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _jumpable;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    
    public Transform ledgePos;
    public float orbRadius = 0.05f;
    public Vector2 xMovement;
    public bool facingRight = true;
    PlayerStates _state;

    private void Start() { _gravityValue = _body.gravityScale; }

    private void Update()
    {
        xMovement.x = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        _body.velocity = new Vector2(xMovement.x * _speed, _body.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) { _body.velocity = new Vector2(_body.velocity.x, _jumpForce); }

        UpdAnimation();
    } 

    private void UpdAnimation()
    {
        //Movement in both directions
        if (xMovement.x > 0 || xMovement.x < 0)
        {
            _state = PlayerStates.walking;
            if ((xMovement.x > 0 && !facingRight) || (xMovement.x < 0 && facingRight))
            {
                transform.localScale *= new Vector2(-1f, 1f);
                facingRight = !facingRight;
            }
        }
        else { _state = PlayerStates.idle; }
        //Ducking/ladder descend
        if (_vertical < 0f && !_onLadder && !_onRope)
        {
            _state = PlayerStates.duck;
            if (Input.GetKeyDown(KeyCode.S))
            {
                _animator.Play("Duck", 0, 0.5f);
            }
        }
        //Jumping/falling animations
        if (_body.velocity.y > .1f) { _state = PlayerStates.jumping; }
        if (_body.velocity.y < -.1f) { _state = PlayerStates.falling; }
        //Ladder trigger and vertical ascension
        if (_onLadder || _onRope)
        {
            _body.gravityScale = 0f;
            _climbVelocity = _climbSpeed * Input.GetAxisRaw("Vertical");
            _body.velocity = new Vector2(_body.velocity.x, _climbVelocity);
            if (_onLadder)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                    _state = PlayerStates.ladderClimbing;
                else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
                    _state = PlayerStates.ladderIdle;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                    _state = PlayerStates.ropeClimbing;
                else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
                    _state = PlayerStates.ropeIdle;
            }
        }
        else
        {
            _body.gravityScale = _gravityValue;
        }
        
        _animator.SetInteger("state", (int)_state);
    }

    private bool IsGrounded() //draws collider box under character for ground collision
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, _jumpable);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(ledgePos.position, orbRadius);
    }

    public void ClimbUp()
    {
        transform.position = new Vector3(ledgePos.position.x, ledgePos.position.y, transform.position.z);
    }

    public void AnimLedge()
    {
        _body.velocity = Vector2.zero;
        _animator.Play("LedgeGrab");
    }
}
