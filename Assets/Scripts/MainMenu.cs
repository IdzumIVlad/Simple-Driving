using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Globalization;




public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button startButton;
    [SerializeField] private AndroidNotifications AndroidNotifications;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechrgeDuration;

    private int energy;
    private string energyReadyString;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private System.DateTime energyReady;


    private void Start()
    {
        startButton.interactable = true; 

        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt(Score.BestScoreKey).ToString();

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if(energy == 0)
        {
            energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);

            if (energyReadyString == string.Empty) energy = maxEnergy;

            energyReady = System.DateTime.Parse(energyReadyString);

            if (System.DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                startButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - System.DateTime.Now).Seconds);
            }
        }
        if (energy <= 2) energyText.color = new Color(255, 24, 0);
        if (energy > 2) energyText.color = new Color(67, 255, 0);
        energyText.text = "Energy: " + energy.ToString();
    }

    private void EnergyRecharged()
    {
        startButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        if (energy <= 2) energyText.color = new Color(255, 24, 0);
        if (energy > 2) energyText.color = new Color(67, 255, 0);
        energyText.text = "Energy: " + energy.ToString();
    }

   

    public void Play()
    {
        if (energy == 0) return;
        energy--;
        startButton.interactable = false;
        PlayerPrefs.SetInt(EnergyKey, energy);
        if (energy == 0) {
            System.DateTime energyReady = System.DateTime.Now.AddMinutes(energyRechrgeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
#if UNITY_ANDROID
            AndroidNotifications.ScheduleNotification(energyReady);
#endif
        }
        
        SceneManager.LoadScene(1);
    }
}
