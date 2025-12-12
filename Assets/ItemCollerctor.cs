using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private Text cherriesText;

    private void Start()
    {
        cherriesText.text = "수집한 체리 갯수: " + cherries;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);

            cherries++;

            cherriesText.text = "수집한 체리 갯수: " + cherries;
        }
    }
}
