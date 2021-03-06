﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainEaterBehavior : MonoBehaviour {
    public float speed;
    private Rigidbody2D rb;
    
    private Animator ani;
    public float EatDelay;
    private float lastEat;
    public bool eating;
    private AudioManager audioManager;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        audioManager = AudioManager.instance;
        ani = GetComponent<Animator>();
        GetComponent<EnemyLifeController>().mname = "braineater";
        rb.velocity = new Vector2(-speed, 0);
        lastEat = Time.time;
        
	}

    // Update is called once per frame
    void Update() {
        if (Time.time - lastEat > EatDelay) {
            ani.SetTrigger("Eat");
            lastEat = Time.time;
        }
        eating = ani.GetCurrentAnimatorStateInfo(0).IsName("Eating");
        if (eating && !GetComponent<EnemyLifeController>().dying) {
            audioManager.PlaySound("braineater bite");
            rb.velocity = new Vector2(-2*speed, 0);
        }

	}
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" && eating) {
            collision.gameObject.GetComponent<Player>().TakeDamage(2);
        }
    }

}
