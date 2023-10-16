using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    private IInteractable parent;

    private void Awake()
    {
        CheckParent();
    }

    private void CheckParent()
    {
        if (transform.parent.TryGetComponent(out parent)) return;
        Debug.Log($"{transform.parent.name} is not IInteractable!");
        transform.GetChild(0).GetComponent<Button>().enabled = false;
    }

    public void CallAction()
    {
        parent.Interact();
    }

    public void CloseButton()
    {
        Destroy(gameObject);
    }
}