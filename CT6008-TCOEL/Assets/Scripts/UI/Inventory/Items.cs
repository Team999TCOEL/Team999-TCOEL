using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Items
{
	public enum ItemType {
		SMG,
		Health,
	}

	public ItemType itemType;
	public int iItemAmmount;

	public Sprite GetSprite() {
		switch (itemType) {
			default:
			case ItemType.SMG:
				return ItemAssets.Instance.SMGSprite;
			case ItemType.Health:
				return ItemAssets.Instance.healthSprite;

		}
	}

	public bool IsStackable() {
		switch (itemType) {
			default:
			case ItemType.Health:
				return true;
			case ItemType.SMG:
				return false;
		}
	}
}
