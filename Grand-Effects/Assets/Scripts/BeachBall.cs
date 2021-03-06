﻿using UnityEngine;
using System.Collections;

public class BeachBall : MonoBehaviour {

    Animator animator;
    bool ballIsPopped = false;
    public Transform levelComplete;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Kitty" && !ballIsPopped)
        {
            PopBeachBall();
        }
    }

    void PopBeachBall()
    {
        animator.SetBool("isPopped", true);
        Debug.Log("Ball popped");
        ballIsPopped = true;
        audio.Play();
        Instantiate(levelComplete, new Vector3(0, 0, -7), Quaternion.identity);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
