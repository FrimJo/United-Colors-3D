using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;

public class BallSpawner : MonoBehaviour {

	public Transform smallBallPrefab;
	public float spawnInterval;

	private System.Random random;
	private float InstantiationTimer;

	// Use this for initialization
	void Start () {
		this.InstantiationTimer = this.spawnInterval;
		this.random = new System.Random();
	
	}
	
	// Update is called once per frame
	void Update () {

		InstantiationTimer -= Time.deltaTime;
		if (InstantiationTimer <= 0) {
			SpawnBall();
			InstantiationTimer = this.spawnInterval;
		}

	}

	private void SpawnBall()
	{

		// Get the with, hight and depth of the arena
		var scale = transform.lossyScale;

		// Get random value between 0 and width for x
		var x = (float) random.NextDouble() * scale.x;

		// Get random value between 0 and height for y
		var y = (float) random.NextDouble() * scale.y;

		// Set depth position using arena z-position pluss the depth of the ball
		var z = scale.y/2.0f + smallBallPrefab.lossyScale.z;

		// Create a small ball with no rotaion
		Transform ball = Instantiate(smallBallPrefab, new Vector3(x, y, z), Quaternion.identity);
		Rigidbody body = ball.GetComponent<Rigidbody>();
		ball.parent = this.transform.parent;

		// Add force to make the ball travel towards the middle
		body.AddForce((transform.position - ball.position).normalized);
	}
}
