using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	public class Inventory : MonoBehaviour
	{
		[SerializeField] List<Item> items;
		[SerializeField] Transform itemsParent;
		[SerializeField] ItemSlot[] itemSlots;
		[SerializeField] List<Item> allPossibleItemsDatabase;

		public event Action<Item> OnItemRightClickedEvent;

		private void Start()
		{
			for (int i = 0; i < itemSlots.Length; i++)
			{
				itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
			}
		}

		private void OnValidate()
		{
			if (itemsParent != null)
				itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

			RefreshUI();
		}

		private void RefreshUI()
		{
			int i = 0;
			for (; i < items.Count && i < itemSlots.Length; i++)
			{
				itemSlots[i].Item = items[i];
			}

			for (; i < itemSlots.Length; i++)
			{
				itemSlots[i].Item = null;
			}
		}

		public bool AddItem(Item item)
		{
			if (IsFull())
				return false;

			items.Add(item);
			RefreshUI();
			return true;
		}

        public bool HasItem(Item item)
        {
            return items.Contains(item);
        }

        public bool RemoveItem(Item item)
		{
			if (items.Remove(item))
			{
				RefreshUI();
				return true;
			}
			return false;
		}

		public bool IsFull()
		{
			return items.Count >= itemSlots.Length;
		}

		public Item FindItemByName(string itemName)
		{
			foreach (Item item in allPossibleItemsDatabase)
			{
				if (item != null && item.name == itemName) return item;
			}
			return null;
		}

		public string[] GetItemNamesForSaving()
		{
			string[] names = new string[items.Count];
			for (int i = 0; i < items.Count; i++)
			{
				names[i] = items[i] != null ? items[i].name : "";
			}
			return names;
		}

		// Allows SaveManager to clear the private list before reloading items
		public void ClearInventoryForLoading()
		{
			items.Clear();
		}

	}
}
