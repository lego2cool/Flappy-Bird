using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUpgrades : MonoBehaviour
{
    [Serializable]
    public class UpgradeButton
    {
        public Button button;
        public TMP_Text title;
        public TMP_Text description;

        public void SetButtonText(UpgradeData upgradeData, float value)
        {
            if (title != null)
            {
                title.text = upgradeData.Title;
            }

            if (description != null)
            {
                description.text = upgradeData.Description.Replace("{value}", value.ToString());
            }
        }
    }

    [SerializeField] private List<UpgradeData> upgrades = new List<UpgradeData>();
    [SerializeField] private List<UpgradeButton> upgradeButtons = new List<UpgradeButton>(3);
    [SerializeField] private AllUpgradeModifiers allUpgradeModifiers;

    public void RefreshButtons()
    {
        for (int index = 0; index < upgradeButtons.Count; index++)
        {
            UpgradeButton upgradeButton = upgradeButtons[index];
            UpgradeData upgrade = upgrades[UnityEngine.Random.Range(0, upgrades.Count)];

            if (upgradeButton == null)
            {
                continue;
            }

            upgradeButton.SetButtonText(upgrade, upgrade.GetRandomValue());
        }
    }
    
}
