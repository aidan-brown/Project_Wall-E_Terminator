using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
	public GameObject flame, barrelL, barrelR;
	private bool rightArm = true;
	public Renderer rend;

	public override void fire()
	{
		if (!canFire)
			return;

		GameObject clone;
		clone = Instantiate(flame, barrelR.transform.position, transform.rotation) as GameObject;
		clone = Instantiate(flame, barrelL.transform.position, transform.rotation) as GameObject;
		
		counter = fireRate;
	}

	public override void hide()
	{
		rend.enabled = false;
	}

	public override void show()
	{
		rend.enabled = true;
	}
}
