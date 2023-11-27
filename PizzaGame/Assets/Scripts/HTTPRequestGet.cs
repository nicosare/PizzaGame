using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequestGet : MonoBehaviour
{
    [SerializeField] private string url;

    private void Start()
    {
        StartCoroutine(SendRequest());
    }

    private IEnumerator SendRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(this.url);

        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
    }
}
