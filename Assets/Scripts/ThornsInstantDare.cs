using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsInstantDare : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        knight newKnight = collision.gameObject.GetComponent<knight>();
        if(newKnight != null )
        {
            newKnight.RecieveHit(newKnight.Health);

        }
    }
}
