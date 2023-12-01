using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearFields : MonoBehaviour
{
    [SerializeField] private TMP_Text error409;
    [SerializeField] private TMP_Text error401;
    [SerializeField] private TMP_InputField loginAuth;
    [SerializeField] private TMP_InputField passwordAuth;
    [SerializeField] private TMP_InputField loginReg;
    [SerializeField] private TMP_InputField passwordReg;
    [SerializeField] private TMP_InputField passwordConfReg;
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField age;
    [SerializeField] private TMP_Dropdown gender;

    public void ClearReg()
    {
        error409.gameObject.SetActive(false);
        loginReg.text = null;
        passwordConfReg.text = null;
        passwordReg.text = null;
        email.text = null;
        age.text = null;
        gender.value = 0;
    }

    public void ClearAuth()
    {
        error401.gameObject.SetActive(false);
        loginAuth.text = null;
        passwordAuth.text = null;
    }
}
