using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopSpawn : MonoBehaviour {

    public GameObject troopPrefab;

    Vector2 troopHome;

	// Use this for initialization
	void Start () {
        troopHome = new Vector2(0, -Camera.main.orthographicSize);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(troopPrefab, troopHome, Quaternion.identity);
        }
	}
}
