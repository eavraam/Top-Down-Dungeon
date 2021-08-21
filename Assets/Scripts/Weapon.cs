using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{

    //Damage structure
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D col)
    {

        if(col.tag == "Fighter")
        {
            if (col.name == "Player")
                return;

            //Create a new Damage Object, then send it to the fighter we 've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            col.SendMessage("ReceiveDamage", dmg);


            Debug.Log(col.name);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }
}
