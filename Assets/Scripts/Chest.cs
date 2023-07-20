using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrystallType { Random, Red, Green, Blue }
public class Chest : MonoBehaviour
{
    public InventoriItem itemDate = new InventoriItem();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        knight newKnight = collision.gameObject.GetComponent<knight>();
        if (newKnight != null)
        {
            if (itemDate.CrystallType == CrystallType.Random)
            {
                itemDate.CrystallType = (CrystallType)Random.Range(1, 4);
            }
            if (itemDate.Count == 0)
            {
                itemDate.Count = Random.Range(1, 6);
            }
            GameController.Instance.AddNewInventoryItem(itemDate);
            Destroy(gameObject);
        }
    }
}
