using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
	public GameObject bullet, barrelL, barrelR;
	private bool rightArm = true;
	public Renderer rend;
	public int numOfShots;
	public float angle;

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
			for (int i = 0; i < numOfShots; i++)
			{
				Quaternion bulletAngle = new Quaternion(transform.rotation.x, transform.rotation.y + Random.Range(-angle, angle), transform.rotation.z, transform.rotation.w);
				clone = Instantiate(bullet, barrelR.transform.position, bulletAngle) as GameObject;
			}
			rightArm = false;
		}
		else
		{
			for (int i = 0; i < numOfShots; i++)
			{
				Quaternion bulletAngle = new Quaternion(transform.rotation.x, transform.rotation.y + Random.Range(-angle, angle), transform.rotation.z, transform.rotation.w);
				clone = Instantiate(bullet, barrelL.transform.position, bulletAngle) as GameObject;
			}
			rightArm = true;
		}

		counter = fireRate;
	}
}
