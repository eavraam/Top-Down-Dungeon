using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTextPerson : Collidable
{
    public string message;
    private float cooldown = 4.0f;
    private float lastShout = -4.0f;

    protected override void OnCollide(Collider2D col)
    {
        if(Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, 0.18f, 0), Vector3.zero, 4.0f);
        }

    }
}
