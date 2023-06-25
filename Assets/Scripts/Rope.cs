using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            _player._onRope = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player") 
            _player._onRope = false;
    }
}
