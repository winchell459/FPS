using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayerController : MonoBehaviour {

    public int Health = 100;
    public int ZombieDamage = 10;

    public int ZombieKills = 0;

    public Text EnemyKillsCount;
    public Text HealthText;

    public void ZombieAttack()
    {
        Health -= ZombieDamage;
        Health = Mathf.Clamp(Health, 0, 100);
        HealthText.text = "Health: " + Health;
    }

    public void ZombieKill()
    {
        ZombieKills += 1;
        EnemyKillsCount.text = "Zombie Kills: " + ZombieKills;
    }
}
