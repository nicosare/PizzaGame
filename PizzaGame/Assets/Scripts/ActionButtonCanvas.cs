using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ActionButtonCanvas : MonoBehaviour
{
    private IInteractable interactableParent;
    public Button ActionButton;

    private void Awake()
    {
        const int buttonIndex = 0;
        ActionButton = transform.GetChild(buttonIndex).GetComponent<Button>();
        interactableParent = transform.parent.GetComponent<IInteractable>();
    }

    public void CallAction()
    {
        interactableParent.Interact();
    }

    public void CloseButton()
    {
        Destroy(gameObject);
    }
}