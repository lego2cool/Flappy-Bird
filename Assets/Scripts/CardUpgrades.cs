using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardUpgrades : MonoBehaviour
{
    [Serializable]
    public class UpgradeButton
    {
        public Button button;
        public TMP_Text title;
        public TMP_Text description;

        [NonSerialized] public UpgradeData upgrade;
        [NonSerialized] public float value;
        [NonSerialized] public UnityAction listener;

        public void SetButtonText(UpgradeData upgradeData, float value)
        {
            SetButtonText(upgradeData, value.ToString());
            this.value = value;
        }

        public void SetButtonText(UpgradeData upgradeData, string valueText)
        {
            upgrade = upgradeData;

            if (title != null)
            {
                title.text = upgradeData != null ? upgradeData.Title : string.Empty;
            }

            if (description != null)
            {
                description.text = upgradeData != null
                    ? upgradeData.Description.Replace("{value}", valueText)
                    : string.Empty;
            }
        }
    }

    [SerializeField] private List<UpgradeData> upgrades = new List<UpgradeData>();
    [SerializeField] private List<UpgradeButton> upgradeButtons = new List<UpgradeButton>(3);
    [SerializeField] private AllUpgradeModifiers allUpgradeModifiers;
    [SerializeField] private Canvas upgradeCanvas;

    public void ShowUpgradeMenu()
    {
        if (upgradeCanvas != null)
        {
            upgradeCanvas.gameObject.SetActive(true);
        }

        Time.timeScale = 0f;
        RefreshButtons();
    }

    public void RefreshButtons()
    {
        List<UpgradeData> availableUpgrades = new List<UpgradeData>(upgrades);

        for (int index = 0; index < upgradeButtons.Count; index++)
        {
            UpgradeButton upgradeButton = upgradeButtons[index];

            if (upgradeButton == null || upgradeButton.button == null)
            {
                continue;
            }

            if (upgradeButton.listener != null)
            {
                upgradeButton.button.onClick.RemoveListener(upgradeButton.listener);
            }

            int buttonIndex = index;
            upgradeButton.listener = () => ApplyUpgrade(upgradeButtons[buttonIndex]);
            upgradeButton.button.onClick.AddListener(upgradeButton.listener);

            if (availableUpgrades.Count == 0)
            {
                upgradeButton.SetButtonText(null, 0f);
                upgradeButton.button.interactable = false;
                continue;
            }

            int randomIndex = UnityEngine.Random.Range(0, availableUpgrades.Count);
            UpgradeData upgrade = availableUpgrades[randomIndex];
            availableUpgrades.RemoveAt(randomIndex);
            upgradeButton.SetButtonText(upgrade, upgrade.GetRandomValue());
            upgradeButton.button.interactable = true;
        }
    }

    private void ApplyUpgrade(UpgradeButton upgradeButton)
    {
        if (upgradeButton == null || upgradeButton.upgrade == null)
        {
            return;
        }

        if (allUpgradeModifiers != null)
        {
            float currentValue = GetModifierValue(upgradeButton.upgrade.Type);
            float value = upgradeButton.upgrade.IsPercentage
                ? upgradeButton.value / 100f
                : upgradeButton.value;

            switch (upgradeButton.upgrade.Type)
            {
                case UpgradeData.UpgradeType.PlayerSize:
                    allUpgradeModifiers.PlayerScaleModifier += value;
                    break;
                case UpgradeData.UpgradeType.PipeFrequency:
                    allUpgradeModifiers.PipeFrequencyModifier += value;
                    break;
                case UpgradeData.UpgradeType.Lives:
                    allUpgradeModifiers.LivesCount += Mathf.RoundToInt(value);
                    break;
                case UpgradeData.UpgradeType.PipeGap:
                    allUpgradeModifiers.PipeGapModifier += value;
                    break;
                case UpgradeData.UpgradeType.expGain:
                    allUpgradeModifiers.expGainModifier += value;
                    break;
                case UpgradeData.UpgradeType.ConstantExp:
                    if (allUpgradeModifiers.ConstantExpModifier <= 0f)
                    {
                        allUpgradeModifiers.ConstantExpModifier = upgradeButton.upgrade.FirstUpgrade > 0
                            ? upgradeButton.upgrade.FirstUpgrade
                            : 5f;
                    }
                    else
                    {
                        allUpgradeModifiers.ConstantExpModifier = Mathf.Max(
                            0.1f,
                            allUpgradeModifiers.ConstantExpModifier * (1f - value));
                    }
                    break;
                case UpgradeData.UpgradeType.LuckyPipes:
                    allUpgradeModifiers.LuckyPipesModifier += value;
                    break;
            }

            float newValue = GetModifierValue(upgradeButton.upgrade.Type);
            upgradeButton.SetButtonText(
                upgradeButton.upgrade,
                $"{currentValue:0.##} -> {newValue:0.##}");
        }

        if (upgradeCanvas != null)
        {
            upgradeCanvas.gameObject.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    private float GetModifierValue(UpgradeData.UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeData.UpgradeType.PlayerSize:
                return allUpgradeModifiers.PlayerScaleModifier;
            case UpgradeData.UpgradeType.PipeFrequency:
                return allUpgradeModifiers.PipeFrequencyModifier;
            case UpgradeData.UpgradeType.Lives:
                return allUpgradeModifiers.LivesCount;
            case UpgradeData.UpgradeType.PipeGap:
                return allUpgradeModifiers.PipeGapModifier;
            case UpgradeData.UpgradeType.expGain:
                return allUpgradeModifiers.expGainModifier;
            case UpgradeData.UpgradeType.ConstantExp:
                return allUpgradeModifiers.ConstantExpModifier;
            case UpgradeData.UpgradeType.LuckyPipes:
                return allUpgradeModifiers.LuckyPipesModifier;
            default:
                return 0f;
        }
    }
    
}
