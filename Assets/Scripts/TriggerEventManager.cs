using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventManager : MonoBehaviour
{
    public delegate void OnCollectArea();
    public static event OnCollectArea OnPaperCollect;
    public static PrinterManager printerManager; //printerManager icerisindeki methoda ulasmak icin static olarak yazildi

    public delegate void OnDeskArea(); //kagidin masaya birakildiktan sonra yok olmasi
    public static event OnDeskArea OnPaperGive;

    public static WorkManager workManager;


    //paralarin toplanip alinmasi icin gerekli 
    public delegate void OnMoneyArea();
    public static event OnMoneyArea OnMoneyCollected;

    //buyarea icin gerekli kod
    public delegate void OnBuyArea();
    public static event OnBuyArea OnBuyingDesk;

    //satin alinan deskin tanimlanmasi icin
    public static BuyArea areaToBuy; //ayni mantikla yazici icin de yapilabilir.

    bool isCollecting, isGiving;

    private void Start()
    {
        StartCoroutine(CollectEnum());
    }

    //toplama islemini kontrol edecek
    IEnumerator CollectEnum()
    {
        while (true)
        {
            if (isCollecting == true)
            {
                OnPaperCollect();
            }
            if (isGiving == true)
            {
                OnPaperGive();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("money")) //yazilmadigi takdirde desk 
        {
            OnMoneyCollected();
            Destroy(other.gameObject);
        }
    }

    //box collidera girdigi zaman toplama islemini yapip yapmamayi kontrol eder
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BuyArea"))
        {
            OnBuyingDesk();

            areaToBuy = other.gameObject.GetComponent<BuyArea>(); //satin alinan yeni desk icin 
        }

        if (other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = true;

            printerManager = other.gameObject.GetComponent<PrinterManager>();
        }
        //calisma masasina kagit birakildigi anda kagitlarin birakilmasi 
        if (other.gameObject.CompareTag("WorkArea"))
        {
            isGiving = true;

            workManager = other.gameObject.GetComponent<WorkManager>();
        }
    }
 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = false;

            printerManager = null; //islem bittiyse burada bitir, donguye sokma
        }
        if (other.gameObject.CompareTag("WorkArea"))
        {
            isGiving = false;
            workManager = null;
        }
        if (other.gameObject.CompareTag("BuyArea"))
        {
            areaToBuy = null; //satin alinan yeni desk icin 
        }
    }
}