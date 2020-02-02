using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas menuInGame;

    [Header("End")]
    [SerializeField] private Gauge selfEsteemGauge;
    [SerializeField] private Gauge attentionGauge;
    [SerializeField] private Text selfEsteemPercent;
    [SerializeField] private Text attentionPercent;
    [SerializeField] private Image selfEsteemSlider;
    [SerializeField] private Image attentionSlider;
    [SerializeField] private Canvas game;
    [SerializeField] private Canvas endGame;

    public void End()
    {
        selfEsteemPercent.text = ((selfEsteemGauge.Value / (float)selfEsteemGauge.Max) * 100f).ToString();
        attentionPercent.text = ((attentionGauge.Value / (float)attentionGauge.Max) * 100f).ToString();
        selfEsteemSlider.fillAmount = (selfEsteemGauge.Value / (float)selfEsteemGauge.Max);
        attentionSlider.fillAmount = (attentionGauge.Value / (float)attentionGauge.Max);

        if (game)
        {
            game.gameObject.SetActive(false);
        }

        if (endGame)
        {
            endGame.gameObject.SetActive(true);
        }
    }

    public void Relaunch()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToMenuInGame()
    {
        menuInGame.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        menuInGame.gameObject.SetActive(false);
    }
}
