using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100;
    public RawImage HealthBar;
    private float defaultLength;


	private void Start()
	{
        defaultLength = HealthBar.rectTransform.sizeDelta.x;

	}

	public void TakeDamage(float attackPower){
        health = Mathf.Clamp(health - attackPower, 0, 100);
        float newLength = defaultLength * health / 100;
        HealthBar.rectTransform.sizeDelta = new Vector2( newLength, HealthBar.rectTransform.sizeDelta.y);
        if (health <= 0)
        {
            FindObjectOfType<LevelHandler>().PlayerDied();
            gameObject.tag = "Untagged";
            gameObject.GetComponent<CharacterController>().enabled = false;
        }
    }
}
