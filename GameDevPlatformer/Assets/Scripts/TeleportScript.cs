using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour {

	public GameObject player;

	public Vector2 destination;

	public float countdownTimer;

	PlayerMovement velocity;

	void Start () {
		velocity = GameObject.FindObjectOfType<PlayerMovement>();
		countdownTimer = 0.05f;
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.name == player.name) {
			// Begin timer
			if (countdownTimer > 0) {
				countdownTimer -= Time.deltaTime;
			} else if (countdownTimer <= 0) {
				player.transform.position = new Vector2(destination.x, destination.y);
				velocity.velocity.x = 0;
				velocity.velocity.y = 0;
				velocity.trail.time = 0;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == player.name) {
			countdownTimer = 0.05f;
		}
	}
}
