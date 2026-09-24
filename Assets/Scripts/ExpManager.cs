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
    [SerializeField] private CardUpgrades upgradeMenu;
    private float constantExpTimer;

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
        float expMultiplier = allUpgradeModifiers != null ? 1f + allUpgradeModifiers.expGainModifier : 1f;
        currentExp += amount * expMultiplier;
        Debug.Log("Current Experience: " + currentExp + "/" + expToUpgrade);
        if (currentExp >= expToUpgrade)
        {
            //upgradeMenu.ShowUpgradeMenu();
            currentExp -= expToUpgrade;
            expToUpgrade = Mathf.RoundToInt(expToUpgrade * expGrowthMultiplier); // Increase the experience needed for the next upgrade
        }
        UpdateExpUI();
    }

    private void Update()
    {
        if (allUpgradeModifiers != null && allUpgradeModifiers.ConstantExpModifier > 0f)
        {
            constantExpTimer += Time.deltaTime;
            if (constantExpTimer >= allUpgradeModifiers.ConstantExpModifier)
            {
                constantExpTimer = 0f;
                AddExp(1);
            }
        }
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
