using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class PostRequest : MonoBehaviour
{
    private int registrationPostResponseCodeSucces = 201;
    private int authorizationPostResponseCodeSucces = 200;
    private string urlReg = "https://a22421-7263.c.d-f.pw/api/v1/player/registration";
    private string urlAuth = "https://a22421-7263.c.d-f.pw/api/v1/player/authorization";
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject regMenuPanel;
    [SerializeField] GameObject authMenuPanel;
    [SerializeField] private Sprite errorAuth;
    [SerializeField] private Sprite errorReg;
    [SerializeField] private TMP_Text error409;
    [SerializeField] private TMP_Text error401;
    [SerializeField] private TMP_InputField loginAuth;
    [SerializeField] private TMP_InputField passwordAuth;
    [SerializeField] private TMP_InputField loginReg;
    [SerializeField] private TMP_InputField passwordReg;
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField age;
    [SerializeField] private TMP_Dropdown gender;

    public void BeginRequestRegistration()
    {
        StartCoroutine(SendRequest(loginReg.text, passwordReg.text, email.text, age.text, gender.value));
    }

    private IEnumerator SendRequest(string login, string password, string email, string age, int gender)
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
        UnityWebRequest request = UnityWebRequest.Post(this.urlReg, formdata);
        byte[] postBytes = Encoding.UTF8.GetBytes(json);
        UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);

        request.uploadHandler = uploadHandler;
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return request.SendWebRequest();

        if (registrationPostResponseCodeSucces == request.responseCode)
        {
            mainMenuPanel.gameObject.SetActive(true);
            regMenuPanel.gameObject.SetActive(false);
        }
        if (409 == request.responseCode)
        {
            error409.gameObject.SetActive(true);
            loginReg.image.sprite = errorReg;
        }
        Debug.Log(request.responseCode);
    }

    public void BeginRequestAuthorization()
    {
        StartCoroutine(SendRequest(loginAuth.text, passwordAuth.text));
    }

    private IEnumerator SendRequest(string login, string password)
    {
        WWWForm formdata = new WWWForm();

        PlayerAuthorization post = new PlayerAuthorization()
        {
            login = login,
            password = password,
        };

        string json = JsonUtility.ToJson(post);
        UnityWebRequest request = UnityWebRequest.Post(this.urlAuth, formdata);
        byte[] postBytes = Encoding.UTF8.GetBytes(json);
        UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);

        request.uploadHandler = uploadHandler;
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return request.SendWebRequest();

        Token.token = request.downloadHandler.text;
        Debug.Log(Token.token);

        //PlayerRegistration playerRegisteredInfo = JsonUtility.FromJson<PlayerRegistration>(request.downloadHandler.text);

        if (authorizationPostResponseCodeSucces == request.responseCode)
        {
            mainMenuPanel.gameObject.SetActive(true);
            authMenuPanel.gameObject.SetActive(false);
        }
        if (401 == request.responseCode)
        {
            error401.gameObject.SetActive(true);
            loginAuth.image.sprite = errorAuth;
        }
        Debug.Log(request.responseCode);
        //Debug.Log("UserPassword " + playerRegisteredInfo.password);
        //Debug.Log("UserEmail " + playerRegisteredInfo.email);
        //Debug.Log("UserAge " + playerRegisteredInfo.age);
        //Debug.Log("UserGender " + playerRegisteredInfo.gender);
    }
}
