using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    private GameObject towerTransform;
    private int targetID = 0;
    private GameObject targetTroop;

    public float projectileSpeed = 20;

    // Use this for initialization
    void Start () {

        

    }
	
	// Update is called once per frame
	void Update () {


        towerTransform = GameObject.Find("Tower");
        TowerShoot towerScript = towerTransform.GetComponent<TowerShoot>();
        targetID = towerScript.targetID;

        GameObject[] troopList = GameObject.FindGameObjectsWithTag("Troop");

        foreach (GameObject troop in troopList)
        {
            int troopID = troop.GetInstanceID();

            if (troopID == targetID)
            {
                targetTroop = troop;
            }
        }

        Vector2 displacement = targetTroop.transform.position - transform.position;
        Vector2 directionToTroop = displacement.normalized;
        Vector2 projectileVelocity = directionToTroop * projectileSpeed;

        transform.Translate(projectileVelocity * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Troop")
        {
            Destroy(gameObject);
        }

    }
}
