using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class WindowsController : MonoBehaviour
{
    public List<Window> windowsList;
    public Window PizzaWindow;
    public Window FullInventoryWindow;
    public static WindowsController Instance;
    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < transform.childCount; i++)
            windowsList.Add(transform.GetChild(i).GetComponent<Window>());
        CloseOtherWindows(windowsList[0]);
    }

    private void Update()
    {
        if (windowsList.All(win => !win.gameObject.activeSelf))
            OpenWindow(windowsList[0]);

        if (Input.GetKeyDown(KeyCode.E))
            OpenWindow(FullInventoryWindow);
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
                windowsList[1].gameObject.SetActive(true);
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
                    windowsList[1].gameObject.SetActive(true);
                window.StartAction(actionObject);
            }
        }
    }

    public void CloseWindow(Window window)
    {
        OpenWindow(windowsList[0]);
        window.CloseWindow();
        windowsList[1].gameObject.SetActive(false);
    }
    public void CloseWindow(int windowIndex)
    {
        OpenWindow(windowsList[0]);
        windowsList[windowIndex].CloseWindow();
        windowsList[1].gameObject.SetActive(false);
    }

    public void CancelOtherWindows()
    {
        foreach (var window in windowsList)
            if (!window.Equals(windowsList[1]))
                window.CancelWindow();
        OpenWindow(windowsList[0]);
    }

    public void UpdateWindow(Window window)
    {
        window.UpdateWindow();
    }
}
