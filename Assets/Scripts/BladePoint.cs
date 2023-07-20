using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladePoint : MonoBehaviour
{
    public Rigidbody2D rb;
    public float damage;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        Vector2 newVelositi = rb.velocity;
        newVelositi.x = 2 * transform.lossyScale.x * -1;
        rb.velocity = newVelositi;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        knight newKnight = collision.gameObject.GetComponent<knight>();
        if (collision != null)
        {
            ChangeDirectiom();

        }
        if (newKnight != null)
        {
            newKnight.RecieveHit(damage);
        }
    }

    private void ChangeDirectiom()
    {
        if (transform.localScale.x < 0)
        {
            transform.localScale = Vector3.one;

        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
