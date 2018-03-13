using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopMove : MonoBehaviour {

    private Rigidbody2D Troop;
    public float totalHealth = 100;
    private float remainingHealth = 100;

    public float speed = 7;
    public Image healthBar;
    public float damageFromProjectile = 20;

	// Use this for initialization
	void Start () {
        Troop = GetComponent<Rigidbody2D>();   
	}
	
	// Update is called once per frame
	void Update () {
        MoveTroop();
	}

    private void MoveTroop()
    {
        Troop.velocity = Vector2.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish Line")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Projectile")
        {
            remainingHealth -= damageFromProjectile;

            if (remainingHealth > 0)
            {
                healthBar.fillAmount = remainingHealth / totalHealth;
            }
            else
            {
                Destroy(gameObject);
            }



        }

    }

}
