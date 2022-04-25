using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    public void LaunchGame()
    {
        MessageManager.Dispatch<string>("EnterGame", "Chess");
    }
}
