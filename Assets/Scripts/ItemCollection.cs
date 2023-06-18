using TMPro;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    private int _goldCnt = 0;
    private int _gBars = 1500;
    private int _gBar = 500;
    private int _emer = 800;
    private int _saph = 1200;
    private int _ruby = 1600;

    [SerializeField] private TMP_Text _text;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Gbar"))
        {
            Destroy(other.gameObject);
            _goldCnt += _gBar;
            _text.text = "Gold: " + _goldCnt;
        }
        else if (other.gameObject.CompareTag("Gbars"))
        {
            Destroy(other.gameObject);
            _goldCnt += _gBars;
            _text.text = "Gold: " + _goldCnt;
        }
        if (other.gameObject.CompareTag("Emerald"))
        {
            Destroy(other.gameObject);
            _goldCnt += _emer;
            _text.text = "Gold: " + _goldCnt;
        }
        else if (other.gameObject.CompareTag("Sapphire"))
        {
            Destroy(other.gameObject);
            _goldCnt += _saph;
            _text.text = "Gold: " + _goldCnt;
        }
        if (other.gameObject.CompareTag("Ruby"))
        {
            Destroy(other.gameObject);
            _goldCnt += _ruby;
            _text.text = "Gold: " + _goldCnt;
        }
    }
}
