using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    public Text KillCountText;
    private int KillCount = 0;

    public GameObject DeathScreen;
   
    public void ZombieKilled()
    {
        KillCount += 1;

        string countStr = "";
        if (KillCount < 10) countStr += "0";
        countStr += KillCount;
        KillCountText.text = countStr;
    }

    public void PlayerDied()
    {
        DeathScreen.SetActive(true);
    }
}
