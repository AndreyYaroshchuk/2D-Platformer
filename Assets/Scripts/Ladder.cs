using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        knight newknight = collision.gameObject.GetComponent<knight>();
        if(newknight != null)
        {
            newknight.OnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        knight newkmight = collision.gameObject.GetComponent<knight>();
        if (newkmight != null)
        {
            newkmight.OnLadder = false;
        }
    }
}
