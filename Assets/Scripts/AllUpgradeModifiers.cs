using UnityEngine;

[CreateAssetMenu(fileName = "AllUpgradeModifiers", menuName = "Scriptable Objects/AllUpgradeModifiers")]
public class AllUpgradeModifiers : ScriptableObject
{
    public float PlayerScaleModifier;
    public float PipeFrequencyModifier;
    public float PipeGapModifier;
    public int LivesModifier;
    public float expGainModifier;
    public float ConstantExpModifier;
    public float LuckyPipesModifier;
}
