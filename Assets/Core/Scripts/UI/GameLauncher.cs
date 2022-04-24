using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    public void LaunchGame()
    {
        EventManager.Instance.Emit(GlobalEvent.EnterGame, "Chess");
    }
}
