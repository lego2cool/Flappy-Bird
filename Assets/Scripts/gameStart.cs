using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class gameStart : MonoBehaviour
{
    [Serializable]
    public class ButtonScene
    {
        public Button button;
        public string sceneName;

        [NonSerialized]
        public UnityAction listener;
    }

    public List<ButtonScene> buttonScenes = new List<ButtonScene>();
    [SerializeField] private AllUpgradeModifiers allUpgradeModifiers;

    void Start()
    {
        foreach (ButtonScene buttonScene in buttonScenes)
        {
            if (buttonScene.button == null || string.IsNullOrWhiteSpace(buttonScene.sceneName))
            {
                continue;
            }

            ButtonScene currentButtonScene = buttonScene;
            currentButtonScene.listener = () => StartGame(currentButtonScene.sceneName);
            currentButtonScene.button.onClick.AddListener(currentButtonScene.listener);
        }
    }

    void OnDestroy()
    {
        foreach (ButtonScene buttonScene in buttonScenes)
        {
            if (buttonScene.button != null && buttonScene.listener != null)
            {
                buttonScene.button.onClick.RemoveListener(buttonScene.listener);
            }
        }
    }

    void StartGame(string sceneName)
    {
        ResetUpgradeModifiers();
        SceneManager.LoadScene(sceneName);
    }

    private void ResetUpgradeModifiers()
    {
        if (allUpgradeModifiers == null)
        {
            return;
        }

        allUpgradeModifiers.PlayerScaleModifier = 0f;
        allUpgradeModifiers.PipeFrequencyModifier = 0f;
        allUpgradeModifiers.PipeGapModifier = 0f;
        allUpgradeModifiers.LivesCount = 1;
        allUpgradeModifiers.expGainModifier = 0f;
        allUpgradeModifiers.ConstantExpModifier = 0f;
        allUpgradeModifiers.LuckyPipesModifier = 0f;
    }
}
