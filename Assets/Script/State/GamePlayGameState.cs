using UnityEngine;
using System.Collections;

public class GamePlayGameState : GameStateBase
{
    #region implemented abstract members of _StatesBase

    public override void Activate()
    {
        Time.timeScale = 0;
        LevelManager.Instance.InitLevel();
        GameManager.Instance.IsGameActive = true;
        UIManager.Instance.panel.SetActive(false);
        UIManager.Instance.ActivateUI(Menus.INGAME);
        
        Debug.Log("<color=green>Gameplay State</color> OnActive");
    }

    public override void Deactivate()
    {
        Debug.Log("<color=red>Gameplay State</color> OnDeactivate");
    }

    public override void UpdateState()
    {
        Debug.Log("<color=yellow>Gameplay State</color> OnUpdate");
    }

    #endregion
}