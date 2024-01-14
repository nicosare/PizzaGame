using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


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
    [SerializeField] private Sprite errorAuthPassword;
    [SerializeField] private Sprite errorEmail;
    [SerializeField] private Sprite errorAge;
    [SerializeField] private TMP_Text loginRegError;
    [SerializeField] private TMP_Text passwordRegError;
    [SerializeField] private TMP_Text emailRegError;
    [SerializeField] private TMP_Text ageRegError;
    [SerializeField] private TMP_Text error401;
    [SerializeField] private TMP_InputField loginAuth;
    [SerializeField] private TMP_InputField passwordAuth;
    [SerializeField] private TMP_InputField loginReg;
    [SerializeField] private TMP_InputField passwordReg;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField ageInput;
    [SerializeField] private TMP_Dropdown gender;

    public void BeginRequestRegistration()
    {
        StartCoroutine(SendRequest(loginReg.text, passwordReg.text, emailInput.text, ageInput.text, gender.value));
    }

    private IEnumerator SendRequest(string login, string password, string email, string age, int gender)
    {
        WWWForm formdata = new WWWForm();

        PlayerRegistration post = new PlayerRegistration()
        {
            login = login,
            password = password,
            email = email,
            age = age == "" ? null : int.Parse(age),
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
            loginRegError.text = "Польхователь с таким логином или паролем уе существует";
            loginReg.image.sprite = errorReg;
            emailInput.image.sprite = errorEmail;
        }
        if (400 == request.responseCode)
        {
            var problemDetails = JsonUtility.FromJson<ResponseErrors>(request.downloadHandler.text);
            if (problemDetails.errors.Login != null)
            {
                loginReg.image.sprite = errorReg;
                loginRegError.text = ArrayErrorsToString(problemDetails.errors.Login);
            }
            if (problemDetails.errors.Password != null)
            {
                passwordReg.image.sprite = errorAuthPassword;
                passwordRegError.text = ArrayErrorsToString(problemDetails.errors.Password);
            }
            if (problemDetails.errors.Email != null)
            {
                emailInput.image.sprite = errorEmail;
                emailRegError.text = ArrayErrorsToString(problemDetails.errors.Email);
            }
            if (problemDetails.errors.Age != null)
            {
                ageInput.image.sprite = errorAge;
                ageRegError.text = ArrayErrorsToString(problemDetails.errors.Age);
            }
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

        SaveToken.Token = request.downloadHandler.text;
        Debug.Log(SaveToken.Token);

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
            passwordAuth.image.sprite = errorAuthPassword;
        }
        Debug.Log(request.responseCode);
        //Debug.Log("UserPassword " + playerRegisteredInfo.password);
        //Debug.Log("UserEmail " + playerRegisteredInfo.email);
        //Debug.Log("UserAge " + playerRegisteredInfo.age);
        //Debug.Log("UserGender " + playerRegisteredInfo.gender);
    }

    private string ArrayErrorsToString(string[] errors)
    {
        var resultString = new StringBuilder();

        foreach (var error in errors)
        {
            resultString.Append(error).Append(", ");
        }

        resultString.Remove(resultString.Length - 2, 2);

        return resultString.ToString();
    }
}
