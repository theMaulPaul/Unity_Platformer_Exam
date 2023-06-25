using UnityEngine;

public class TriggerClimb : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    private BoxCollider2D _box;
    void Start()
    {
        _box = GetComponents<BoxCollider2D>()[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _box.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _box.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && _player._body.velocity.y <= 0)
            _player.AnimLedge();
    }
}
