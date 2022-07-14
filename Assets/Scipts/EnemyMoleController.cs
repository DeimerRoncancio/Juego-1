using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoleController : PlayerController
{
	[SerializeField] GameObject player;
	private Animator animEnemyMole;
	private float speedEnemy2 = 1;
	private bool death = false;
	[SerializeField] private float vidaMole = 30;
	
	void Awake ()
	{
		animEnemyMole = GetComponentInChildren<Animator>();
		vidaMole = Mathf.Clamp(vidaMole,0,100);
	}
	
	void Update()
	{
		animEnemy();
	}

	void FixedUpdate ()
	{
		toPursuePlayer();
	}
	
	void animEnemy()
	{
		input = (player.transform.position - transform.position).normalized;
		animEnemyMole.SetFloat("Horizontal",input.x);
		animEnemyMole.SetFloat("Vertical",input.y);
	}
	
	void toPursuePlayer()
	{
		transform.position = Vector2.MoveTowards
		(
			transform.position,
			player.transform.position,
			speedEnemy2 * Time.fixedDeltaTime
		);
	}
	
	public void deathEnemy(float daño)
	{
		vidaMole -= daño;
		animEnemyMole.SetTrigger("Demage");
		
		if(vidaMole <= 0)
		{
			GetComponent<Collider2D>().enabled = false;
			death = true;
			speedEnemy2 = 0;
			animEnemyMole.SetBool("Death",death);
			Destroy(this.gameObject,1);
		}
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerController>().life(20,other.GetContact(0).normal);
		}
	}
}
