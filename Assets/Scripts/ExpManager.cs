using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpManager : MonoBehaviour
{
    public float currentExp;
    public int expToUpgrade = 20;
    public float expGrowthMultiplier = 1.2f;
    public Slider expSlider;
    public TMP_Text expText;
    public bool enableDebugText = true;
    [SerializeField] private AllUpgradeModifiers allUpgradeModifiers;

    private void Start()
    {
        UpdateExpUI();
        if (enableDebugText)
            expText.gameObject.SetActive(true);
        else
            expText.gameObject.SetActive(false);
    } 
    public void AddExp(int amount)
    {
        currentExp += amount * (1f + allUpgradeModifiers.expGainModifier);
        Debug.Log("Current Experience: " + currentExp + "/" + expToUpgrade);
        if (currentExp >= expToUpgrade)
        {
            //OpenMenuUpgrade();
            currentExp -= expToUpgrade;
            expToUpgrade = Mathf.RoundToInt(expToUpgrade * expGrowthMultiplier); // Increase the experience needed for the next upgrade
        }
        UpdateExpUI();
    }

    public void UpdateExpUI()
    {
        if (expSlider != null)
        {
            expSlider.maxValue = expToUpgrade;
            expSlider.value = currentExp;
        }

        if (expText != null)
        {
            expText.text = "EXP " + currentExp + "/" + expToUpgrade;
        }
    }
}
