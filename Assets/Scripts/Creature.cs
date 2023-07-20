using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, Idestractebl 
{
    //[SerializeField] открывает видим полей 
    [SerializeField] protected float speed =4 ;
    [SerializeField] protected float damage = 10;
    [SerializeField] protected float health = 50;
    protected Animator animator;
    protected Rigidbody2D rb;

    public float Health { get => health; set => health = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Damage { get => damage; set => damage = value; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // найти компонент да дочерном объекте 
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void RecieveHit(float damage)
    {
        Health -= damage;
        GameController.Instance.Hit(this);
        if(Health <= 0 )
        {
            Die();
        }
    }
    protected void DoHit(Vector3 hitPosition, float hitRadius, float hitDamage)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitPosition, hitRadius);
        for (int i = 0; i < hits.Length; i++)
        {
            if (!GameObject.Equals(hits[i].gameObject, gameObject))
            {
                Idestractebl destractebl = hits[i].gameObject.GetComponent<Idestractebl>();
                if (destractebl != null)
                {
                    destractebl.RecieveHit(hitDamage);
                }
                break;
            }
        }
    }
    public void Die()
    {
        GameController.Instance.Killed(this);
    }
   

}
