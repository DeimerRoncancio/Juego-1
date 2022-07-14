using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	private Rigidbody2D rgb;
	public float speed;
	[SerializeField] private float daño;
	
	void Start()
	{
		rgb = GetComponent<Rigidbody2D>();
	}
	
	void Update()
	{
		shoot();
	}

	void shoot()
	{
		rgb.AddForce(transform.up * speed * Time.deltaTime,ForceMode2D.Impulse);
		Destroy(this.gameObject,3);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("EnemyMole"))
		{
			other.GetComponent<EnemyMoleController>().deathEnemy(daño);
			Destroy(this.gameObject);
		}
		if(other.CompareTag("EnemyTrunk"))
		{
			other.GetComponent<EnemyTrunkController>().deathEnemy2(daño);
			Destroy(this.gameObject);
		}
	}
}
