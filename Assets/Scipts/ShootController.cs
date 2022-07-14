using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
	private Transform dirController;
	[SerializeField] private GameObject bullet;
	protected Vector2 input;
	private float range = 0.7f;

	void Start ()
	{
		dirController = GetComponent<Transform>();
		dirController.rotation = Quaternion.Euler(0,0,180);
	}
	
	void Update ()
	{
		input.x = Input.GetAxisRaw("Horizontal");
		input.y = Input.GetAxisRaw("Vertical");

		range -= Time.deltaTime;

		if(input.x != 0 || input.y != 0)
		{
			if(input.x == 1)
			{
				dirController.rotation = Quaternion.Euler(0,0,0-90);
			}
			else if(input.x == -1)
			{
				dirController.rotation = Quaternion.Euler(0,0,180-90);
			}
			else if(input.y == 1)
			{
				dirController.rotation = Quaternion.Euler(0,0,90-90);
			}
			else if(input.y == -1)
			{
				dirController.rotation = Quaternion.Euler(0,0,270-90);
			}
		}

		if(Input.GetButtonDown("Fire1") && range < 0)
		{
			Instantiate(bullet,transform.position,Quaternion.Euler(0,0,transform.rotation.eulerAngles.z));
			range = 0.5f;
		}
	}
}
