﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadScript : MonoBehaviour
{
    public GameObject player;
    public GameMaster master;
    public float radius = 1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject masterObj = GameObject.Find("Scripts in the scene");
        master = masterObj.GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position - player.transform.position).magnitude < radius)
        {
            Debug.Log("HIT BAD OJECT!");
            master.changeScore(-2);
            master.createNewScorePoint();
            Destroy(gameObject);
        }
    }
}