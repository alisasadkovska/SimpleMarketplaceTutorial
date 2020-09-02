using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    private string sellResource;
    private string buyResource;
    private readonly int step = 5;
    private int ratio = 0;
    private int sellAmount = 0;
    private int buyAmount = 0;
    
    private bool isMarketOpen = false;
     private Animator animator;
    private Sprite openMarketIcon;

     [Header("UI elements")]
    public GameObject marketPanel;
    public Image marketButtonIcon;
    public Sprite closeMarketIcon;
    public Button swipeButton;
    public Button confirmDealButton;
    public Image sellIcon;
    public Image buyIcon;
    public TextMeshProUGUI sellAmountText;
    public TextMeshProUGUI buyAmountText;
    public TextMeshProUGUI ratioText;
    void Start()
    {
        if(marketPanel!=null)
        animator = marketPanel.GetComponent<Animator>();
        if(marketButtonIcon!=null)
        openMarketIcon = marketButtonIcon.sprite;
        SetupStartResources();
        InvokeRepeating("CalculateRatio",0f,5f);
    }
    
    public void ToogleMarket(){
        if(isMarketOpen){
            animator.SetTrigger("fade_out");
            marketButtonIcon.sprite = openMarketIcon;
        }else{
            animator.SetTrigger("fade_in");
              marketButtonIcon.sprite = closeMarketIcon;
        }

         isMarketOpen = !isMarketOpen;
    }
    

      private void SetupStartResources()
    {
        sellResource = CommonData.money;
        buyResource = CommonData.shield;
        //CalculateRatio();
    }

     private void CalculateRatio()
    {
        if (sellResource == buyResource)
        {
            ratio = 1;
            return;
        }

        switch (sellResource)
        {
            case CommonData.money:
                ratio = Random.Range(1,4);
                break;
            case CommonData.food:
                ratio = Random.Range(5,11);
                break;
            case CommonData.helmet:
                ratio = Random.Range(3,5);
                break;
            case CommonData.sword:
                ratio = Random.Range(1,3);
                break;
            case CommonData.shield:
                ratio = Random.Range(3,6);
                break;
        }

         CalculateBuyRatio();
         CalculateSellRatio();
    }
       

     


    public void IncreaseSellAmount()
    {
        int maxSellAmount = GetMaxSellAmount();

        if ((sellAmount + step) > maxSellAmount)
        {
            return;
        }
        else
        {
            sellAmount += step;
            buyAmount = sellAmount / ratio;
        }

        UpdateUI();
    }

      public void DescreaseSellAmount()
    {
        if ((sellAmount - step) < 0)
        {
            sellAmount = 0;
            if (buyAmount < step) buyAmount = 0;
        }
        else
        {
            sellAmount -= step;
            buyAmount = sellAmount / ratio;
        }
        UpdateUI();
    }

       public void IncreaseBuyAmount()
    {
        int maxBuyAmount = GetMaxBuyAmount();

        if ((buyAmount + step) > maxBuyAmount)
        {
            return;
        }
        else
        {
            buyAmount += step;
            sellAmount += (step * ratio);
        }

        UpdateUI();
    }

        public void DecreaseBuyAmount()
    {
        if ((buyAmount - step) < 0)
        {
            sellAmount -= (buyAmount * ratio);
            if (sellAmount < step) sellAmount = 0;
            buyAmount = 0;
        }
        else
        {
            buyAmount -= step;
            sellAmount -= (step * ratio);
        }
           

        UpdateUI();
    }


       public void MaxAmount()
    {
        buyAmount = GetMaxBuyAmount();
        sellAmount = GetMaxSellAmount();
        UpdateUI();
    }

     public void MinAmount()
    {
        int minBuyAmount = 0;

        switch (sellResource)
        {
            case CommonData.money:
                if (PlayerStats.Money >= step) minBuyAmount = step; else minBuyAmount = PlayerStats.Money;
                break;
            case CommonData.food:
                if (PlayerStats.Food >= step) minBuyAmount = step; else minBuyAmount = PlayerStats.Food;
                break;
            case CommonData.helmet:
                if (PlayerStats.Helmets >= step) minBuyAmount = step; else minBuyAmount = PlayerStats.Helmets;
                break;
            case CommonData.sword:
                if (PlayerStats.Swords >= step) minBuyAmount = step; else minBuyAmount = PlayerStats.Swords;
                break;
            case CommonData.shield:
                if (PlayerStats.Shields >= step) minBuyAmount = step; else minBuyAmount = PlayerStats.Shields;
                break;
        }

        sellAmount = minBuyAmount;
        buyAmount = sellAmount / ratio;

        UpdateUI();
    }

       public void PickSellItem()
    {
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (clickedButton != null)
        {
            sellIcon.sprite = clickedButton.transform.GetChild(0).GetComponent<Image>().sprite;
            sellResource = clickedButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.ToString();
        }
       
       CalculateRatio();
       CalculateSellRatio();
    }

    private void CalculateSellRatio(){
        if (sellAmount > GetMaxSellAmount())
        {
            sellAmount = GetMaxSellAmount();
        }
        buyAmount = sellAmount / ratio;
        UpdateUI();
    }

    public void PickBuyItem()
    {
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (clickedButton != null)
        {
            buyIcon.sprite = clickedButton.transform.GetChild(0).GetComponent<Image>().sprite;
            buyResource = clickedButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.ToString();
        }
        
        CalculateRatio();
        CalculateBuyRatio();
    }

    private void CalculateBuyRatio(){
        buyAmount = 0;
        buyAmount = sellAmount / ratio;
        UpdateUI();
    }

    public void SwipeItems(){
        Sprite sellIconSprite = sellIcon.sprite;
        Sprite buyIconSprite = buyIcon.sprite;

        sellIcon.sprite = buyIconSprite;
        buyIcon.sprite = sellIconSprite;

        string _buyResource = buyResource;
        string _sellResource = sellResource;

        buyResource = _sellResource;
        sellResource = _buyResource;

        CalculateRatio();
    }

      public void ConfirmDeal()
    {
        if (sellResource == buyResource)
            return;

        if (sellAmount == 0 || buyAmount == 0)
            return;

        switch (sellResource)
        {
            case CommonData.money:
                PlayerStats.Money -= sellAmount;
                break;
            case CommonData.food:
                PlayerStats.Food -= sellAmount;
                break;
            case CommonData.helmet:
                PlayerStats.Helmets -= sellAmount;
                break;
            case CommonData.sword:
                PlayerStats.Swords -= sellAmount;
                break;
            case CommonData.shield:
                PlayerStats.Shields -= sellAmount;
                break;
        }

        switch (buyResource)
        {
            case CommonData.money:
                PlayerStats.Money += buyAmount;
                break;
            case CommonData.food:
                PlayerStats.Food += buyAmount;
                break;
            case CommonData.helmet:
                PlayerStats.Helmets += buyAmount;
                break;
            case CommonData.sword:
                PlayerStats.Swords += buyAmount;
                break;
            case CommonData.shield:
                PlayerStats.Shields += buyAmount;
                break;
        }

       

        sellAmount = 0;
        buyAmount = 0;
        UpdateUI();
    }

        private int GetMaxSellAmount()
    {
        int maxSellAmount = 0;

        switch (sellResource)
        {
            case CommonData.money:
                maxSellAmount = PlayerStats.Money;
                break;
            case CommonData.food:
                maxSellAmount = PlayerStats.Food;
                break;
            case CommonData.helmet:
                maxSellAmount = PlayerStats.Helmets;
                break;
            case CommonData.sword:
                maxSellAmount = PlayerStats.Swords;
                break;
            case CommonData.shield:
                maxSellAmount = PlayerStats.Shields;
                break;
        }

        return maxSellAmount;
    }

       private int GetMaxBuyAmount()
    {
        int maxBuyAmount = 0;

        switch (sellResource)
        {
            case CommonData.money:
                maxBuyAmount = PlayerStats.Money / ratio;
                break;
            case CommonData.food:
                maxBuyAmount = PlayerStats.Food / ratio;
                break;
            case CommonData.helmet:
                maxBuyAmount = PlayerStats.Helmets / ratio;
                break;
            case CommonData.sword:
                maxBuyAmount = PlayerStats.Swords / ratio;
                break;
            case CommonData.shield:
                maxBuyAmount = PlayerStats.Shields / ratio;
                break;
        }

        return maxBuyAmount;
    }

    
    void Update()
    {
         if (sellAmount == 0 || buyAmount == 0 || sellResource == buyResource)
        {
            confirmDealButton.interactable = false;
        }
        else
        {
            confirmDealButton.interactable = true;
        }

        swipeButton.interactable = sellResource!=buyResource;
    }

     private void UpdateUI()
    {
        sellAmountText.text = sellAmount.ToString();
        buyAmountText.text = buyAmount.ToString();
         ratioText.text = ratio.ToString();
    }
}
