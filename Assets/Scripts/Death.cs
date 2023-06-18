using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] Sprite _sprite;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            Die();
        }
    }

    private void Die()
    {
        _animator.SetTrigger("death");
        _body.bodyType = RigidbodyType2D.Static;
        _renderer.sprite = _sprite;
    }
    
    private void RestartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
