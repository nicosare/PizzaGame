using System.Collections;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI outputText;
    public static Message Instance;

    private void Awake()
    {
        Instance = this;
        messageText.gameObject.SetActive(false);
    }
    public void LoadMessage(string message, float seconds = 1)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessage(message, seconds));
    }

    IEnumerator ShowMessage(string message, float seconds)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        outputText.text = message;
        yield return new WaitForSeconds(seconds);
        messageText.gameObject.SetActive(false);
    }
}
