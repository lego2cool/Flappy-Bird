using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public int currentExp;
    public int expToUpgrade = 2;
    public float expGrowthMultiplier = 1.2f;

    private void Update()
    {
        // For testing purposes, you can add experience by pressing the space key
        
    } 
    public void AddExp(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToUpgrade)
        {
            //OpenMenuUpgrade();
            currentExp -= expToUpgrade;
            expToUpgrade = Mathf.RoundToInt(expToUpgrade * expGrowthMultiplier); // Increase the experience needed for the next upgrade
        }
    }
}
