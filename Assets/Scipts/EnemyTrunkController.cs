using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrunkController : PlayerController
{
	private float speedEnemy = 1;
	[SerializeField] GameObject player;
	private bool deathTrunk = false;
	private Animator animEnemyTrunk;
	[SerializeField] private float vidaTrunk = 30;

	void Awake ()
	{
		animEnemyTrunk = GetComponentInChildren<Animator>();
		vidaTrunk = Mathf.Clamp(vidaTrunk,0,100);
	}
	
	void Update ()
	{
		input = (player.transform.position - transform.position).normalized;
		animEnemyTrunk.SetFloat("Horizontal",input.x);
		animEnemyTrunk.SetFloat("Vertical",input.y);
	}
	
	void FixedUpdate ()
	{
		transform.position = Vector2.MoveTowards
		(
			transform.position,
			player.transform.position,
			speedEnemy * Time.fixedDeltaTime
		);
	}

	public void deathEnemy2(float daño)
	{
		vidaTrunk -= daño;
		animEnemyTrunk.SetTrigger("Demage");
		
		if(vidaTrunk <= 0)
		{
			GetComponent<Collider2D>().enabled = false;
			deathTrunk = true;
			speedEnemy = 0;
			animEnemyTrunk.SetBool("DeathTrunk",deathTrunk);
			Destroy(this.gameObject,1);
		}
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerController>().life(20,other.GetContact(0).normal);
		}
	}
}
