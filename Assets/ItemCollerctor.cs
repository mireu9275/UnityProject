using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private static int cherries = 0;

    [SerializeField] private Text cherriesText;

    private void Start()
    {
        // 시작할 때 현재 개수 표시
        UpdateCherryUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            cherries++;
            UpdateCherryUI();
        }
    }

    private void UpdateCherryUI()
    {
        cherriesText.text = "수집한 체리 갯수: " + cherries;
    }

    public static void ResetCherries()
    {
        cherries = 0;
    }
}