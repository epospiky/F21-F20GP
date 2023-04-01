using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickups : MonoBehaviour
{
    public string theGun;
    private bool collected;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !collected)
        {
            Player_Cont.instance.addGun(theGun);
            Destroy(gameObject);
            collected = true;
        }
    }
}
