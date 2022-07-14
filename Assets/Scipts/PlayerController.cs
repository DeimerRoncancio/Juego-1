using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private float speedPlayer = 5;
	private Animator animPlayer;
	protected Vector2 input;
	[SerializeField] float vidaPlayer = 100;
	[SerializeField] private bool canMove = true;
	private Rigidbody2D rgb;
	[SerializeField] private Vector2 speedRebound;
	[SerializeField] private float loseControl = 1;
	
	void Start ()
	{
		animPlayer = GetComponentInChildren<Animator>();
		rgb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		input.x = Input.GetAxisRaw("Horizontal");
		input.y = Input.GetAxisRaw("Vertical");

		if(canMove == true)
		{
			move();
		}
		
		attack();
		shoot();
	}
	
	void move()
	{
		transform.Translate(input.normalized * speedPlayer * Time.fixedDeltaTime);

		if(input.x != 0 || input.y != 0)
		{
			animPlayer.SetFloat("Horizontal",input.x);
			animPlayer.SetFloat("Vertical",input.y);
			animPlayer.SetFloat("Speed",1);
		}
		else
		{
			animPlayer.SetFloat("Speed",0);
		}
	}
	
	void shoot()
	{
		if(Input.GetMouseButtonDown(0))
		{
			animPlayer.SetBool("Shoot",true);
		}
		else
		{
			animPlayer.SetBool("Shoot",false);
		}
	}

	void attack()
	{
		if(Input.GetMouseButtonDown(1))
		{
			animPlayer.SetBool("Attack",true);
		}
		else
		{
			animPlayer.SetBool("Attack",false);
		}
	}
	
	public void rebound(Vector2 strike)
	{
		rgb.velocity = new Vector2(-speedRebound.x * strike.x,-speedRebound.y * strike.y);
	}

	public void life(float daño,Vector2 position)
	{
		
		vidaPlayer -= daño;
		StartCoroutine(loseControll());
		rebound(position);
		
		if(vidaPlayer <= 0)
		{
			Destroy(this.gameObject,1);
		}
	}
	
	private IEnumerator loseControll()
	{
		canMove = false;
		yield return new WaitForSeconds(loseControl);
		rgb.velocity = new Vector2(0,0);
		canMove = true;
	}
}
