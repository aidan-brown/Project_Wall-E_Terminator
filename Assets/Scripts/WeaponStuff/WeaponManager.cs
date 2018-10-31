using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class WeaponManager : MonoBehaviour
{
	public Weapon[] weapons;
	public int activeWeapon;

	// makes it easier to look through inputs
	private KeyCode[] keyCodes =
	{
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9
	};

	// Use this for initialization
	void Start ()
	{
		activeWeapon = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			weapons[activeWeapon].fire();
		}

		for (int i = 0; i < weapons.Length; i++)
		{
			if (Input.GetKeyDown(keyCodes[i]))
			{
				activeWeapon = i;
				break;
				// does all the stuff with hiding the old weapon and showing the new one
			}
		}
	}
}
