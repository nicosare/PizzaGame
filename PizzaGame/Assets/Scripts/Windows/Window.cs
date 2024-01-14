using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField]
    protected GameObject windowField;
    public ActionObject ActionObjectCallBack;
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
