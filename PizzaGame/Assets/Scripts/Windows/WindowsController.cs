using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private Zoom cameraControl;
    [SerializeField] private Window cancelWindow;
    public List<Window> windowsList;
    public Window PizzaWindow;
    public Window FullInventoryWindow;
    public Window PauseWindow;
    public Window ShopWindow;
    public Window MenuWindow;
    public List<Window> UpgradeWindows;
    public static WindowsController Instance;

    private void Awake()
    {
        cameraControl.CanMove = true;
        Instance = this;
        for (int i = 0; i < transform.childCount; i++)
            windowsList.Add(transform.GetChild(i).GetComponent<Window>());
        CloseOtherWindows(windowsList[0]);
    }

    private void Update()
    {
        if (windowsList.All(win => !win.gameObject.activeSelf))
            OpenWindow(windowsList[0]);
    }

    public void InventoryButton()
    {
        OpenWindow(FullInventoryWindow);
    }

    public void ShopButton()
    {
        OpenWindow(ShopWindow);
    }

    public void PauseButton()
    {
        OpenWindow(PauseWindow);
    }

    public void UpgradeButton(int floorIndex)
    {
        OpenWindow(UpgradeWindows[floorIndex]);
    }

    public void MenuButton()
    {
        OpenWindow(MenuWindow);
    }

    public void CloseOtherWindows(Window activeWindow)
    {
        foreach (var window in windowsList)
            if (!window.Equals(activeWindow))
                window.CloseWindow();
    }

    public void OpenWindow(Window window, ActionObject actionObject = null)
    {
        if (window.OpenFromOtherWindows)
        {
            window.gameObject.SetActive(true);
            CloseOtherWindows(window);
            if (window.OpenCancelWindow)
                cancelWindow.gameObject.SetActive(true);
            window.StartAction(actionObject);
        }
        else
        {
            if (windowsList
                .Skip(1)
                .Where(win => !win.Equals(window))
                .Any(win => win.gameObject.activeSelf))
                window.CloseWindow();
            else
            {
                window.gameObject.SetActive(true);
                CloseOtherWindows(window);
                if (window.OpenCancelWindow)
                    cancelWindow.gameObject.SetActive(true);
                window.StartAction(actionObject);
            }
        }

        if (window.StopCameraMoving)
            cameraControl.CanMove = false;

    }

    public void CloseWindow(Window window)
    {
        cameraControl.CanMove = true;
        OpenWindow(windowsList[0]);
        window.CloseWindow();
        cancelWindow.gameObject.SetActive(false);
    }
    public void CloseWindow(int windowIndex)
    {
        cameraControl.CanMove = true;
        OpenWindow(windowsList[0]);
        windowsList[windowIndex].CloseWindow();
        cancelWindow.gameObject.SetActive(false);
    }

    public void CancelOtherWindows()
    {
        cameraControl.CanMove = true;
        foreach (var window in windowsList)
            if (!window.Equals(cancelWindow))
                window.CancelWindow();
        OpenWindow(windowsList[0]);
    }

    public void UpdateWindow(Window window)
    {
        window.UpdateWindow();
    }
}
