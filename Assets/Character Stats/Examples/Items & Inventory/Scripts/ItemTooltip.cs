using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace Kryz.CharacterStats.Examples
{
	public class ItemTooltip : MonoBehaviour
	{
		public static ItemTooltip Instance;

		[SerializeField] Text descriptionText;
		[SerializeField] Text nameText;
		[SerializeField] Text slotTypeText;
		[SerializeField] Text statsText;

		private StringBuilder sb = new StringBuilder();

		private void Awake()
		{
			if (Instance == null) {
				Instance = this;
			} else {
				Destroy(this);
			}
			gameObject.SetActive(false);
		}

		public void ShowTooltip(Item itemToShow)
		{
			// 1. Turn on the UI for EVERY item
			gameObject.SetActive(true);

			// 2. Show the basic stuff that every Item has
			nameText.text = itemToShow.ItemName;
			descriptionText.text = itemToShow.Description;

			// 3. NOW check if it's equippable to show stats
			if (itemToShow is EquippableItem)
			{
				EquippableItem equippable = (EquippableItem)itemToShow;
				slotTypeText.text = equippable.EquipmentType.ToString();

				sb.Length = 0;
				AddStatText(equippable.StrengthBonus, " Strength");
				// ... add the rest of your AddStatText lines here ...
				statsText.text = sb.ToString();
			}
			else
			{
				// 4. If it's just a Key, clear the stat text and type text
				slotTypeText.text = "";
				statsText.text = "";
			}
		}

		public void HideTooltip()
		{
			gameObject.SetActive(false);
		}

		private void AddStatText(float statBonus, string statName)
		{
			if (statBonus != 0)
			{
				if (sb.Length > 0)
					sb.AppendLine();

				if (statBonus > 0)
					sb.Append("+");

				sb.Append(statBonus);
				sb.Append(statName);
			}
		}
	}
}
