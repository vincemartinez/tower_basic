using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerShoot : MonoBehaviour {

    private Vector2 projectileHome;
    private GameObject targetTroop;
    private int targetInstanceID = 0;
    private bool shotFired = false;
    private bool targetLocked = false;
    private bool troopInRange = false;
    private float shotTime = 0;
    private float offset = 0.2f;
    CircleCollider2D towerColliderRange;


    public GameObject projectilePrefab;
    public GameObject closestTroop = null;
    public List<Transform> troopList;
    public float shotsPerSecond = 2;
    private float towerRange;
    public int targetID = 0;

    // Use this for initialization
    void Start () {
        projectileHome = new Vector2(transform.position.x, transform.position.y);
        towerColliderRange = gameObject.GetComponent<CircleCollider2D>();
        towerRange = towerColliderRange.radius;
    }
	
	// Update is called once per frame
	void Update () {

        if (targetTroop == null)
        {
            targetLocked = false;
        }

        if (shotFired == true)
        {
            shotTime += Time.deltaTime;



            if (shotTime >= 1 / shotsPerSecond)
            {
                shotFired = false;
                shotTime = 0;
            }
        }

        if (targetLocked == true)
        {

            Vector3 directionToTroop = targetTroop.transform.position - transform.position;
            float angle = Mathf.Atan2(directionToTroop.y, directionToTroop.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (shotFired == false)
            {
                Instantiate(projectilePrefab, projectileHome, Quaternion.identity);

                if(Vector2.Distance(transform.position, targetTroop.transform.position) <= towerRange + offset)
                {
                    shotFired = true;
                }
                else
                {
                    targetLocked = false;
                    print("target out of range");
                }
                
            }
            
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }

    }

    void OnTriggerEnter2D(Collider2D collisionEnter)
    {
        if (collisionEnter.tag == "Troop")
        {

            if (targetLocked == false)
            {
                targetLocked = true;
                print("new target locked (enter)");
                FindClosestTarget();
            }

            
        }

    }

    void OnTriggerStay2D(Collider2D collisionStay)
    {
        if (collisionStay.tag == "Troop")
        {
            if (targetLocked == false)
            {
                targetLocked = true;
                print("new target locked (stay)");
                FindClosestTarget();
            }
        }
    }



    void FindClosestTarget()
    {
        GameObject[] troopList = GameObject.FindGameObjectsWithTag("Troop");

        float closestTroopDistance = Mathf.Infinity;

        foreach(GameObject troop in troopList)
        {
            float distanceToTroop = Vector2.Distance(transform.position, troop.transform.position);

            if (distanceToTroop < closestTroopDistance)
            {
                closestTroopDistance = distanceToTroop;
                closestTroop = troop;
            }
        }



        targetTroop = closestTroop;
        targetID = targetTroop.GetInstanceID();
        print("closest target found ID# " + targetID);

    }


}
