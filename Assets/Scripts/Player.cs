﻿/*
* (Matt Kirchoff)
* (File Name)
* (Assignment1)
* (Describe, in general, the code contained.)
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 4;
    public float shootDistance;

    private Rigidbody2D rb2d;
    private Vector2 moveSpeed;

    public int ammo = 20;
    public bool fire = false;

    public int score = 0;

    public Text ammoUI;
    public Text scoreUI;

    // Start is called before the first frame update
    void Start()
    {
        ammoUI.text = ("Ammo: " + ammo.ToString());
        scoreUI.text = ("Score: " + score.ToString());
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveSpeed = move.normalized * speed;

        scoreUI.text = ("Score: " + score.ToString());


        if (Input.GetMouseButtonDown(0) && !fire && ammo > 0)
        {
            fire = true;
            ammo--;
            Shoot();
            fire = false;
            ammoUI.text = ("Ammo: " + ammo.ToString());
        }
        if (ammo == 0 || Input.GetKeyDown(KeyCode.R))
        {
            ammoUI.text = ("Reloading...");
            Invoke("Reload", 1);

        }
    }
    void OnTrigerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "enemy")
        {
            Debug.Log("You lose");
        }
    }
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveSpeed * Time.fixedDeltaTime);
    }
    void Reload()
    {
        ammo = 20;
        ammoUI.text = ("Ammo: " + ammo.ToString());

    }
    void Shoot()
    {
        Vector2 startPos = this.transform.position;
        Vector2 endPos = this.transform.right * shootDistance;
        Debug.DrawRay(startPos, endPos);
        RaycastHit2D hit = Physics2D.Raycast(startPos, endPos, shootDistance);
        if (hit)
        {
            score++;
            Debug.Log(hit.transform.name + " was hit");
            Destroy(hit.collider.gameObject);
        }
    }
}
