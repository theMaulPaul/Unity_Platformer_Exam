using System;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            _player._onLadder = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
            _player._onLadder = false;
    }
}
