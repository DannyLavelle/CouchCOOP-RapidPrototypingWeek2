using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    bool startdrip = false;
    public void DropWeapon()
    {
        if (startdrip == false)
        {
            Instantiate(gameObject);
            startdrip = true;
            Destroy(gameObject);
        }
        
        
    }
  
}
