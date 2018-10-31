using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public int fireRate;
	protected int counter = 0;

	public bool canFire { get { return (counter == 0); } }

	// does fire rate
	private void Update()
	{
		if (counter > 0)
			counter--;
	}

	// should set counter = fireRate at the end and swap between right and left arm
	public abstract void fire();
}
