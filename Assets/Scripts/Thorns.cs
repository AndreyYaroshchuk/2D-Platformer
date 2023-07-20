using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        knight newKnight = collision.gameObject.GetComponent<knight>();   // не понятно 
        if (newKnight != null)
        {
            newKnight.RecieveHit(damage);
                
        }
        //if (collision.gameObject.tag == "knight")
        //{

        //    Idestractebl destractebl = collision.gameObject.GetComponent<Idestractebl>();
        //    destractebl.Hit(damage);

        //}

    }
 
}
