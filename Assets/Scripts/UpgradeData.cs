using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public enum UpgradeType
    {
        PlayerSize,
        PipeFrequency,
        Lives,
        PipeGap,
        expGain,
        ConstantExp,
        LuckyPipes
    }

    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] 
    [ShowIf("upgradeType", UpgradeType.ConstantExp)] 
    private int firstUpgrade;
    [SerializeField] private int minValue;
    [SerializeField] private int maxValue;
    [SerializeField] private bool isPercentage;

    public string Title => title;
    public string Description => description;
    public UpgradeType Type => upgradeType;
    public int MinValue => minValue;
    public int MaxValue => maxValue;
    public bool IsPercentage => isPercentage;


    public int GetRandomValue()
    {
        return Random.Range(minValue, maxValue);
    }
}
