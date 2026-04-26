using System;
using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	public class EquipmentPanel : MonoBehaviour
	{
		[SerializeField] Transform equipmentSlotsParent;
		[SerializeField] EquipmentSlot[] equipmentSlots;

		public GameObject Pistol;

		public event Action<Item> OnItemRightClickedEvent;

		private void Start()
		{
			for (int i = 0; i < equipmentSlots.Length; i++)
			{
				equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
			}
		}

		private void OnValidate()
		{
			equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
		}

		public bool AddItem(EquippableItem item, out EquippableItem previousItem)
		{
			for (int i = 0; i < equipmentSlots.Length; i++)
			{
				if (equipmentSlots[i].EquipmentType == item.EquipmentType)
				{
					previousItem = (EquippableItem)equipmentSlots[i].Item;
					equipmentSlots[i].Item = item;
					return true;
				}
			}
			previousItem = null;
			return false;
		}

		public bool RemoveItem(EquippableItem item)
		{
			for (int i = 0; i < equipmentSlots.Length; i++)
			{
				if (equipmentSlots[i].Item == item)
				{
					equipmentSlots[i].Item = null;
					return true;
				}
			}
			return false;
		}

		void Update()
        {
			DebugFirstSlot();
        }

		public void DebugFirstSlot()
		{
			// Because 'equipmentSlots' is defined at the top of this script,
			// this code can "see" it perfectly.
			Item equippedItem = equipmentSlots[3].Item;

			if (equippedItem != null)
			{
				Debug.Log("Currently wearing: " + equippedItem.name);
				if (equippedItem.name == "Pistol")
                {
					Pistol.SetActive(true);
                }
			}
			else
			{
				Debug.Log("Slot 0 is empty!");
				Pistol.SetActive(false);
			}
		}

	}
}
