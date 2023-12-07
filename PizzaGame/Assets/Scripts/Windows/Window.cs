using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField]
    protected GameObject windowField;
    public ActionObject ActionObjectCallBack;
    [SerializeField]
    private bool stopTime;
    [SerializeField]
    private bool closeByEscape;
    public bool OpenFromOtherWindows;
    public bool OpenCancelWindow;
    public bool StopCameraMoving;

    private void Update()
    {
        if (closeByEscape && Input.GetKeyDown(KeyCode.Escape))
            CancelWindow();
    }

    private void BasicParameters()
    {
        Time.timeScale = 1f;
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }

    public abstract void StartAction(ActionObject actionObject);
    public abstract void UpdateWindow();
    public virtual void CloseWindow()
    {
        gameObject.SetActive(false);
    }
    public virtual void CancelWindow()
    {
        WindowsController.Instance.CloseWindow(1);
        if (ActionObjectCallBack != null)
            ActionObjectCallBack.CancelAction();
        CloseWindow();
    }
}
