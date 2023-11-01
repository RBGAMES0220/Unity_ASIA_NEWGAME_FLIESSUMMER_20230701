using UnityEngine;

public class Mood : MonoBehaviour
{
    [SerializeField] private GameObject _mood;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _mood?.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _mood?.SetActive(false);
        }
    }
}