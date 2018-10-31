using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGun : Weapon
{
	public GameObject bullet, barrelL, barrelR;
	private bool rightArm = true;
	public Renderer rend;

	public override void hide()
	{
		rend.enabled = false;
	}

	public override void show()
	{
		rend.enabled = true;
	}

	public override void fire()
	{
		if (!canFire)
			return;

		GameObject clone;

		if (rightArm)
		{
			clone = Instantiate(bullet, barrelR.transform.position, transform.rotation) as GameObject;
			rightArm = false;
		}
		else
		{
			clone = Instantiate(bullet, barrelL.transform.position, transform.rotation) as GameObject;
			rightArm = true;
		}

		counter = fireRate;
	}
}
