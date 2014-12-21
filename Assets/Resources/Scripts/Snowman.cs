﻿using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour
{
	public float speed = 0.02f;

	private bool flipped;

    private GameObject ground;
    private Vector2 patrolPoint;

    private bool ground_collision = false;
	void Start ()
	{
		flipped = false;
	}

	void Update ()
	{
		/*if (Input.GetKey (KeyCode.A))
		{
			moveLeft (speed);
		}
		if (Input.GetKey (KeyCode.D))
		{
			moveRight(speed);
		}*/

        if (ground_collision && Mathf.Round(patrolPoint.x) == Mathf.Round(gameObject.transform.position.x))
            RandomPoint();
        if (ground_collision && patrolPoint.x < gameObject.transform.position.x)
            moveLeft(speed);
        if(ground_collision && patrolPoint.x > gameObject.transform.position.x)
            moveRight(speed);

        //print(Mathf.RoundToInt(gameObject.transform.position.x).ToString() + " patrol:" + Mathf.RoundToInt(patrolPoint.x).ToString()  );
	}

    void RandomPoint()
    {
        //če je bil prejšnji patrol point manjši od od polovice
        float randomX = 0;
        if(patrolPoint.x < ground.transform.position.x)
            randomX = Random.Range(ground.transform.localScale.x/2, ground.transform.localScale.x);
        else
            randomX = Random.Range(0, ground.transform.localScale.x / 2);
        float randomXpos = (ground.transform.position.x - ground.transform.localScale.x / 2) + randomX ;
        patrolPoint = new Vector2(randomXpos , transform.position.y);
    }

	void moveLeft(float speed)
	{
		transform.position += new Vector3(-speed, 0, 0);
		if (flipped)
		{
			flipped = false;
			Vector3 curScale = transform.localScale;
			curScale.x *= -1.0f;
			transform.localScale = curScale;
		}
	}

	void moveRight(float speed)
	{
		transform.position += new Vector3(speed, 0, 0);
		if (!flipped)
		{
			flipped = true;
			Vector3 curScale = transform.localScale;
			curScale.x *= -1.0f;
			transform.localScale = curScale;
		}
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            ground_collision = true;
            ground = coll.gameObject;
            RandomPoint();
        }
    }
}
