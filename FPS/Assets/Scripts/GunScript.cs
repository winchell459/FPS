using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetTrigger("FireGun");

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)){
                if(hit.transform.CompareTag("Enemy")){
                    hit.transform.SendMessage("TakeDamage");
                }
            }
        }
    }
}
