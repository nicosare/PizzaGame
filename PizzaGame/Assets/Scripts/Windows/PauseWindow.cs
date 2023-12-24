using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : Window
{
    public override void StartAction(ActionObject actionObject)
    {
        PauseGame();
    }

    private void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    public void ContinueGame()
    {
        GameManager.Instance.ContinueGame();
    }

    public override void UpdateWindow()
    {
        throw new System.NotImplementedException();
    }
}
