using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    //parayi tutmak icin yazilan script 
    public int moneyCount = 0;


    public void OnEnable()
    {
        TriggerEventManager.OnMoneyCollected += IncreaseMoney;

        //yeni desk satin almak icin 
        TriggerEventManager.OnBuyingDesk += BuyArea;
    }

    private void OnDisable()
    {
        TriggerEventManager.OnMoneyCollected -= IncreaseMoney;

        TriggerEventManager.OnBuyingDesk -= BuyArea;
    }

    //desk satin alma islemlerinin kontrol edildigi komut satiri
    void BuyArea()
    {
        if (TriggerEventManager.areaToBuy != null)
        {
            if (moneyCount >= 1) //para varsa
            {
                TriggerEventManager.areaToBuy.Buy(1); 
                moneyCount -= 1; //satin alindiginda paranin eksilmesi icin
            }
        }
    }

    void IncreaseMoney()
    {
        moneyCount += 50; //her toplanan para prefabi icin 50 birim para kazanacagiz.
    }
}