﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public int awarenessRadius = 2;

    public bool isAwared = false;
    public float personalSpaceRadius = 1.5f;
    public float speed = 0.01f;
    GameObject player;

    public Animator anim;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("walking", false);

        if (player != null)
        {
            isAwared = Vector3.Magnitude(transform.position - player.transform.position) < awarenessRadius;
            if (isAwared)
            {
                Vector3 lookAt = (player.transform.position - transform.position);
                lookAt.y = 0.0f;    

                // Apply force here
                if (Vector3.Magnitude(player.transform.position - transform.position) > personalSpaceRadius)
                {
                    // Apply force
                    transform.position = transform.position + lookAt * speed;
                    anim.SetBool("walking", true);
                }

                lookAt = transform.forward + lookAt * 0.1f;
                transform.forward = Vector3.Normalize(lookAt);


            }
        }


	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, awarenessRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, personalSpaceRadius);


        if (isAwared) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, player.transform.position);

        }
    }
}