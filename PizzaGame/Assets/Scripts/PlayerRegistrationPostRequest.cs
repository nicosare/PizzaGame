using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class PlayerRegistrationPostRequest : MonoBehaviour
{
    [SerializeField] private string url;
    [SerializeField] private string login;
    [SerializeField] private string password;
    [SerializeField] private string email;
    [SerializeField] private int age;
    [SerializeField] private int gender;

    private void Start()
    {
        StartCoroutine(SendRequest());
    }

    private IEnumerator SendRequest()
    {
        WWWForm formdata = new WWWForm();

        PlayerRegistration post = new PlayerRegistration()
        {
            login = login,
            password = password,
            email = email,
            age = age,
            gender = gender
        };

        string json = JsonUtility.ToJson(post);

        UnityWebRequest request = UnityWebRequest.Post(this.url, formdata);

        byte[] postBytes = Encoding.UTF8.GetBytes(json);

        UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);

        request.uploadHandler = uploadHandler;

        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return request.SendWebRequest();

        //PlayerRegistration playerRegisteredInfo = JsonUtility.FromJson<PlayerRegistration>(request.downloadHandler.text);

        Debug.Log(request.responseCode);
        //Debug.Log("UserPassword " + playerRegisteredInfo.password);
        //Debug.Log("UserEmail " + playerRegisteredInfo.email);
        //Debug.Log("UserAge " + playerRegisteredInfo.age);
        //Debug.Log("UserGender " + playerRegisteredInfo.gender);
    }
}
