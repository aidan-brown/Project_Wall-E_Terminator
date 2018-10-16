﻿using UnityEngine;

public class weapon_switching : MonoBehaviour {

    //index of weapon
    public int selectedWeapon = 0;

	// Use this for initialization
	void Start () {
        SelectWeapon();
	}
	
	// Update is called once per frame
	void Update () {

        int previousSelectedWeapon = selectedWeapon;

        //using scroll where to change weapons
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        //uses the function to change weapon shown on screen when changing weapons
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        //using buttons to change weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

    }

    //set which weapon to be shown on screen
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
