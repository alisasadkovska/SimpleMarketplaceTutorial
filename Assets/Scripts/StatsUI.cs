using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI helmetText;
    public TextMeshProUGUI swordText;
    public TextMeshProUGUI shieldText;
    

   
    void Update()
    {
        moneyText.text = PlayerStats.Money.ToString();
        foodText.text = PlayerStats.Food.ToString();
        helmetText.text = PlayerStats.Helmets.ToString();
        swordText.text = PlayerStats.Swords.ToString();
        shieldText.text = PlayerStats.Shields.ToString();
    }
}
