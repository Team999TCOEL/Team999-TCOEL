﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // allows use to save into a file
public class PlayerData
{
	public float[] afPlayerPositions; // float array that contains the position of our player
	public int iPlayerFuelAmmount;
	public float fPlayerMaxHealthAmmount;
	public int iHealAmmount;
	public float fMaxStamina;
	public List<Items> itemList;

	public PlayerData(PlayerController playerController) {
		afPlayerPositions = new float[2];
		afPlayerPositions[0] = playerController.transform.position.x;
		afPlayerPositions[1] = playerController.transform.position.y;
		iPlayerFuelAmmount = playerController.iFuelAmmount;
		fPlayerMaxHealthAmmount = playerController.fPlayerMaxHealth;
		iHealAmmount = playerController.iHealAmmount;
		fMaxStamina = playerController.fMaxStamina;
		itemList = playerController.itemList;
	}
}
