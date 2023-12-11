using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : Window
{
    public override void StartAction(ActionObject actionObject)
    {
        GameManager.Instance.PauseGame();
    }

    public override void UpdateWindow()
    {
        throw new System.NotImplementedException();
    }
}
