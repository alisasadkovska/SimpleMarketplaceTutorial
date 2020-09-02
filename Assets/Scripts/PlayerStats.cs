using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    private void Awale()
    {
        if(instance != null)
        return;

        instance = this;
    }
     
    public static int Money;
    public static int Food;
    public static int Helmets;
    public static int Swords;
    public static int Shields;
    
    [Header("Setup start resources")]
    public int startMoney = 100;
    public int startFood = 15000;
    public int startHelmets = 50;
    public int startSwords = 200;
    public int startShields = 250;


    void Start()
    {
        Money = startMoney;
        Food = startFood;
        Helmets = startHelmets;
        Swords = startSwords;
        Shields = startShields;
    }
}
