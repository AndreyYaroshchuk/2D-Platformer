using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Dragon : MonoBehaviour
{
    public Slider slider_Health;
    public Creature owner;
    public GameObject target;
    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (target != null)
        {
            slider_Health.value = owner.Health;
        }
    }
}
