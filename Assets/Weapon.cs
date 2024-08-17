﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;

	float timeToFire = 0;
	Transform firePoint;

	void Awake()
    {
		firePoint = transform.Find("FirePoint");
		if (firePoint == null)
        {
			Debug.LogError("No FirePoint? WHAT?!");
        }
    }
	
	// Update is called once per frame
	void Update () {
		// enable the next line when debugging
		// Shoot();

		if (fireRate == 0)
        {
			if (Input.GetButtonDown("Fire1"))
            {
				Shoot();
            }
        }
		else
        {
			if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
				timeToFire = Time.time + 1 / fireRate;
				Shoot();
            }
        }
	}

	void Shoot()
    {
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

		// draws a line from point A to point B. To see the debugging line you need to hit play and then in the top right corner enable Gizmos
		Debug.DrawLine(firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);
		if (hit.collider != null)
        {
			Debug.DrawLine(firePointPosition, hit.point, Color.red);
			Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage");
        }
    }
}
