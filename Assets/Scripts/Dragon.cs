using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : Creature
{
    public CircleCollider2D hitCollider;

    private void Update()
    {
        Vector2 newVelositi = rb.velocity;
        newVelositi.x = Speed * transform.lossyScale.x * -1;
        rb.velocity = newVelositi;
    }

    private void OnTriggerStay2D(Collider2D collision) //OnTriggerStay  постоянно  enter - 1 раз при первом столк exit - 1 раз при последнем стол 
    {
        knight newknight = collision.GetComponent<knight>();
        if (newknight != null)
        {
            animator.SetTrigger("Attack");
            Speed = 0f;

        }
        else
        {
            ChangeDirectiom();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        knight newknigh = collision.gameObject.GetComponent<knight>();
        if (newknigh != null)
        {
            Speed = 2f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<knight>() != null)
        {
            for (int i = 0; i < collision.contacts.Length; i++) // contacts все столкновения с колайдером 
            {
                Vector2 ContactVector = collision.contacts[i].point - (Vector2)transform.position; // временая перемен от высоты до текух позиции
                if (Vector2.Angle(ContactVector, Vector2.up) <= 45) // проверка от высоты к радиусу углу.
                {
                    Die();
                }
            }
        }
    }

    private void ChangeDirectiom()
    {
        //if (gameObject.transform.position.x < point[0].transform.position.x)
        //{
        //    transform.localScale = new Vector3(1, 1, 1);
        //}
        //if (gameObject.transform.position.x > point[1].transform.position.x)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}
        if (transform.localScale.x < 0)
        {
            transform.localScale = Vector3.one;

        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Attack()
    {
        Vector3 hitposition = transform.TransformPoint(hitCollider.offset);
        DoHit(hitposition, hitCollider.radius, Damage);
    }
}
