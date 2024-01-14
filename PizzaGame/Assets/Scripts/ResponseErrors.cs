using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResponseErrors
{
    public Error errors;
}

[System.Serializable]
public class Error
{
    public string[] Login;

    public string[] Password;

    public string[] Email;

    public string[] Age;
}
