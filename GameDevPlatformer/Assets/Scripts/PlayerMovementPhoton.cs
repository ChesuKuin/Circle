using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent (typeof (Controller2D))]
public class PlayerMovementPhoton : MonoBehaviour {

	PhotonView view;

	public float jumpHeight = 2;
	public float timeToGroundDecelerate;
	public float timeToJumpApex = 0.3f;
	public float timeToWallUnstick;
	public float wallSlideSpeedMax = 0.1f;
	public float wallStickTime = 0;
	public float minYPosition;
	public float moveSpeed;

	public bool wallSliding = false;

	public Vector2 spawnPoint;
	public Vector2 velocity;
	public Vector2 wallClimb;
	public Vector2 wallHop;
	public Vector2 wallLeap;
	public Vector2 input;

	float accelerationTimeGrounded = 0.1f;
	float accelerationTimeLanding = 0.3f;
	float minMoveSpeed = 10;
	float gravity;
	float jumpVelocity;
	float velocityXSmoothing;

	public TrailRenderer trail;
	
	Controller2D controller;
	GameObject player;
	LevelAdvanceScript levelAdvance;
	PauseScript pause;
	SpriteRenderer renderer;
	SlipScript slip;

	void Start() {
		controller = GetComponent<Controller2D>();
		levelAdvance = GameObject.FindObjectOfType<LevelAdvanceScript>();
		pause = GameObject.FindObjectOfType<PauseScript>();
		renderer = GetComponent<SpriteRenderer>();
		trail = GetComponent<TrailRenderer>();
		slip = GameObject.FindObjectOfType<SlipScript>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		spawnPoint = new Vector2(spawnPoint.x, spawnPoint.y);
		moveSpeed = 10;

		view = GetComponent<PhotonView>();
	}
		
	void Update () {
		
			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			int wallDirectionX = (controller.collisions.left) ? -1 : 1;
			float targetVelocityXGround = input.x * moveSpeed;
			if (controller.collisions.below)
			{
				if (input.x == 1)
				{
					velocity.x += (100 * Mathf.Pow(Time.deltaTime, 2));
				}
				else if (input.x == -1)
				{
					velocity.x -= (100 * Mathf.Pow(Time.deltaTime, 2));
				}
				if (timeToGroundDecelerate < 0.25f)
				{
					if (Mathf.Sign(input.x) != Mathf.Sign(velocity.x))
					{
						velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityXGround, ref velocityXSmoothing, accelerationTimeLanding);
					}
					else
					{
						timeToGroundDecelerate += Time.deltaTime;
					}
				}
				else if (timeToGroundDecelerate >= 0.25f)
				{
					velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityXGround, ref velocityXSmoothing, accelerationTimeGrounded);
				}
			}
			else if (!controller.collisions.below)
			{
				timeToGroundDecelerate = 0;
				if (velocity.x > 0)
				{
					if (input.x == 1)
					{
						velocity.x += 20 * Time.deltaTime;
					}
					else if (input.x == -1)
					{
						velocity.x -= 40 * Time.deltaTime;
					}
				}
				else if (velocity.x < 0)
				{
					if (input.x == 1)
					{
						velocity.x += 40 * Time.deltaTime;
					}
					else if (input.x == -1)
					{
						velocity.x -= 20 * Time.deltaTime;
					}
				}
			}

			if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
				&& (controller.collisions.left || controller.collisions.right)
				&& !controller.collisions.below && velocity.y < 0
				&& !slip.slipping)
			{
				wallSliding = true;

				if (velocity.y < -wallSlideSpeedMax)
				{
					velocity.y = wallSlideSpeedMax;
				}
			}
			else
			{
				wallSliding = false;
			}

			if (controller.collisions.above || controller.collisions.below)
			{
				velocity.y = 0;
				if (velocity.x < -minMoveSpeed)
				{
					velocity.x += 5 * Time.deltaTime;
				}
				else if (velocity.x > minMoveSpeed)
				{
					velocity.x -= 5 * Time.deltaTime;
				}
			}

			if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below)
			{
				velocity.x = 0;
			}

			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) && controller.collisions.below)
			{
				if (wallSliding)
				{
					if (input.x == 0)
					{
						velocity.x = -wallDirectionX * wallHop.x;
						velocity.y = wallHop.y;
					}
					else if (input.x != wallDirectionX)
					{
						velocity.x = -wallDirectionX * wallLeap.x;
						velocity.y = wallLeap.y;
					}
					else if (input.x == wallDirectionX)
					{
						velocity.x = -wallDirectionX * wallClimb.x;
						velocity.y = wallClimb.y;
					}
				}

				if (controller.collisions.below)
				{
					velocity.y = jumpVelocity;
				}
			}

			velocity.y += gravity * Time.deltaTime;
			controller.Move(velocity * Time.deltaTime);

			if ((this.transform.position.y < (minYPosition)) || Input.GetKeyDown(KeyCode.R))
			{
				this.transform.position = spawnPoint;
				velocity.x = 0;
				velocity.y = 0;
				trail.time = 0;
				this.renderer.enabled = true;
			}

			if (velocity.x > 20 || velocity.x < -20 || velocity.y > 20 || velocity.y < -20)
			{
				trail.enabled = true;
				if (trail.time < 0.25f)
				{
					trail.time += 0.01f;
				}
				else
				{
					trail.time = 0.25f;
				}
			}
			else
			{
				if (trail.time > 0)
				{
					trail.time -= 0.01f;
				}
				else
				{
					trail.enabled = false;
				}
			}

			if (!pause.isPaused && !levelAdvance.isWon)
			{
				Time.timeScale = 1;
			}
			else
			{
				Time.timeScale = 0;
			}
		}
	}
