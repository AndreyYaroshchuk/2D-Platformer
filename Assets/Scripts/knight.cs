using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class knight : Creature
{
    public int jumpForce;
    public int laderSpeed;
    public Transform groundCheck;
    public Transform attackPoint;
    public float attackReng = 0.5f;
    public float hitDeley = 0.4f;
    public bool onGround = true;
  //  private float maxHeath;
    private bool onLadder;

    public bool OnLadder 
    {
        get
        {
            return onLadder;
        }
        set
        {
            if (value == true)
            {
                rb.gravityScale = 0f;
            }
            else
            {
                rb.gravityScale = 1f;
            }
            onLadder = value;
        }
    }

  //  public float MaxHeath { get => maxHeath; set => maxHeath = value; }

    private void Start()
    {
       // GameController.Instance.NewKnight = this;
        GameController.Instance.OnUpdateHeroParameters += GameController_OnUpdateHeroParameters;
    }

    private void GameController_OnUpdateHeroParameters(HeroParameters parameters)
    {
        Health = parameters.MaxHealth;
        Damage = parameters.Damage;
        Speed = parameters.Speed;
    }

    private void Update()
    {
        ChangeDirection();
        animator.SetFloat("Speed",Mathf.Abs(Input.GetAxis("Horizontal")));
        Vector2 newVelositi = rb.velocity;
        newVelositi.x = Input.GetAxis("Horizontal") * Speed;
        rb.velocity= newVelositi;


        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
            Invoke("Attack", hitDeley); // вызов функция спустя времени определн
        }

        onGround = CheckGround();

        animator.SetBool("Jump", !onGround);
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(Vector2.up * jumpForce);
           
        }

        if (OnLadder)
        {
            newVelositi = rb.velocity;
            newVelositi.y = Input.GetAxis("Vertical") * laderSpeed;
            rb.velocity = newVelositi;
        }
    }

    private bool CheckGround()
    {
        RaycastHit2D[] hit = Physics2D.LinecastAll(transform.position, groundCheck.position);

        for (int i = 0; i < hit.Length; i++)
        {
            if (!GameObject.Equals(hit[i].collider.gameObject, gameObject))
            { 
                return true;
            }
        }
        return false;
    }

    private void ChangeDirection()
    {
        if (transform.localScale.x < 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = Vector3.one;

            }

        }
        else
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }
        }

    }
   
    public void Attack()
    {
        DoHit(attackPoint.position, attackReng, Damage);
    }

    //public override void Die()
    //{
    //    GameController.Instance.GameOwer();
    //}
    private void OnDestroy()
    {
        GameController.Instance.OnUpdateHeroParameters -= GameController_OnUpdateHeroParameters;
    }
}
