using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


public class PostRequest : MonoBehaviour
{
    private string urlPlayerData = "https://a22421-7263.c.d-f.pw/api/v1/player/data";
    private string urlPlayersPlaceInTop = "https://a22421-7263.c.d-f.pw/api/v1/leaderboard/player";
    private string urlReg = "https://a22421-7263.c.d-f.pw/api/v1/player/registration";
    private string urlAuth = "https://a22421-7263.c.d-f.pw/api/v1/player/authorization";
    private string urlauthorizationToken = "https://a22421-7263.c.d-f.pw/api/v1/player/authorization/refresh_token";
    private string urlIsPlayerPlayingNow = "https://a22421-7263.c.d-f.pw/api/v1/player/play?isPlaying=";
    private string urlLeaderboard = "https://a22421-7263.c.d-f.pw/api/v1/leaderboard?countPlayer=";
    [SerializeField] private ScenesController SceneManager;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject regMenuPanel;
    [SerializeField] GameObject authMenuPanel;
    [SerializeField] GameObject[] leaderboardList;
    [SerializeField] GameObject playerPlaceInLeaderboard;
    [SerializeField] GameObject gameIsPlayingOnOthetDeviceError;
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

    private void Awake()
    {
        StartCoroutine(SendRequest());
    }

    public void BeginRequestAuthorization()
    {
        StartCoroutine(SendRequest(loginAuth.text, passwordAuth.text));
    }

    public void BeginRequestRegistration()
    {
        StartCoroutine(SendRequest(loginReg.text, passwordReg.text, emailInput.text, ageInput.text, gender.value));
    }

    public void GetLeaderboard()
    {
        StartCoroutine(SendRequest(10));
        StartCoroutine(GetPlayerPlaceRequest());
    }

    public void SetPlayerIsActive(bool isPlaying)
    {
        StartCoroutine(SendRequest(isPlaying));
    }

    private IEnumerator SendRequest(bool isPlaying)
    {
        var url = urlIsPlayerPlayingNow + isPlaying;

        UnityWebRequest request = UnityWebRequest.Get(url);

        request.SetRequestHeader("Authorization", "Bearer " + SaveToken.Token);

        yield return request.SendWebRequest();

        if (request.responseCode == 200 && isPlaying == true)
            SceneManager.ChangeScene(0);

        if (request.responseCode == 409)
            gameIsPlayingOnOthetDeviceError.gameObject.SetActive(true);

        if (isPlaying == false)
            SceneManager.Exit();
    }

    private IEnumerator SendRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(this.urlauthorizationToken);

        yield return request.SendWebRequest();

        if (request.responseCode == 200)
        {
            SaveToken.Token = request.downloadHandler.text;
            mainMenuPanel.gameObject.SetActive(true);
        }
    }

    private IEnumerator GetPlayerData()
    {

        UnityWebRequest request = UnityWebRequest.Get(this.urlPlayerData);

        request.SetRequestHeader("Authorization", "Bearer " + SaveToken.Token);

        yield return request.SendWebRequest();

        var playerGameData = JsonUtility.FromJson<PlayerGameData>(request.downloadHandler.text);
    }

    private IEnumerator SavePlayerData(PlayerGameData gameData)
    {
        WWWForm formdata = new WWWForm();

        string gameDataJson = JsonUtility.ToJson(gameData);
        UnityWebRequest request = UnityWebRequest.Post(this.urlPlayerData, formdata);
        byte[] postBytes = Encoding.UTF8.GetBytes(gameDataJson);
        UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);

        request.uploadHandler = uploadHandler;
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        request.SetRequestHeader("Authorization", "Bearer " + SaveToken.Token);

        yield return request.SendWebRequest();
    }

    private IEnumerator GetPlayerPlaceRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(this.urlPlayersPlaceInTop);

        request.SetRequestHeader("Authorization", "Bearer " + SaveToken.Token);

        yield return request.SendWebRequest();

        var playerInLeaderboard = JsonUtility.FromJson<PlayersLeaderboard>(request.downloadHandler.text);

        var components = playerPlaceInLeaderboard.transform.GetChilds();
        components[0].GetComponent<TextMeshProUGUI>().text = playerInLeaderboard.placeInTop.ToString();
        components[1].GetComponent<TextMeshProUGUI>().text = playerInLeaderboard.login.ToString();
        components[2].GetComponent<TextMeshProUGUI>().text = playerInLeaderboard.rating.ToString();
    }

    private IEnumerator SendRequest(int countPlayer = 10)
    {
        UnityWebRequest request = UnityWebRequest.Get(this.urlLeaderboard + countPlayer);

        yield return request.SendWebRequest();

        string json = "{\"leaderboard\":" + request.downloadHandler.text + "}";

        var leaderboardPlayers = JsonUtility.FromJson<Leaderboards>(json);

        for (var i = 0; i < 10; i++)
        {
            var leaderboardTransforms = leaderboardList[i].gameObject.transform.GetChilds();

            leaderboardTransforms[0].GetComponent<TextMeshProUGUI>().text = leaderboardPlayers.leaderboard[i].placeInTop.ToString();
            leaderboardTransforms[1].GetComponent<TextMeshProUGUI>().text = leaderboardPlayers.leaderboard[i].login.ToString();
            leaderboardTransforms[2].GetComponent<TextMeshProUGUI>().text = leaderboardPlayers.leaderboard[i].rating.ToString();
        }
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

        if (201 == request.responseCode)
        {
            mainMenuPanel.gameObject.SetActive(true);
            regMenuPanel.gameObject.SetActive(false);
            StartCoroutine(SendRequest(login, password));
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

        if (200 == request.responseCode)
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
