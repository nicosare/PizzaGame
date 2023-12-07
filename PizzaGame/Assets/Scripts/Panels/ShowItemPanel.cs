using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemPanel : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private Color upColor;
    [SerializeField] private Color downColor;
    private int action;
    private Animator animator;
    public bool WasShown;

    private void Awake()
    {
        WasShown = false;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.SetInteger("Action", action);
        StartCoroutine(ClosePanel());
    }
    private IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(1);
        WasShown = true;
        gameObject.SetActive(false);
    }
    public void LoadTakeData(Sprite icon, string name, int amount)
    {
        itemIcon.sprite = icon;
        textField.text = $"+ {name} ×{amount}";
        textField.color = upColor;
        action = 1;
    }
    public void LoadGiveData(Sprite icon, string name, int amount)
    {
        itemIcon.sprite = icon;
        textField.text = $"- {name} ×{amount}";
        textField.color = downColor;
        action = -1;
    }
}
