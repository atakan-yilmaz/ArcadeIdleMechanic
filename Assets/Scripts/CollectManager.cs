using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public List<GameObject> paperList = new List<GameObject>();
    public GameObject paperPrefab;
    public Transform collectPoint;

    int paperLimit = 10; //oyuncu eline maks 10 kagit parcasi alabilecek

    private void OnEnable()
    {
        TriggerEventManager.OnPaperCollect += GetPaper;
        TriggerEventManager.OnPaperGive += GivePaper;
    }

    private void OnDisable()
    {
        TriggerEventManager.OnPaperCollect -= GetPaper;
        TriggerEventManager.OnPaperGive -= GivePaper;
    }

    void GetPaper()
    {
        if (paperList.Count <= paperLimit) //instantiate optimizasyon sorunu cikarabilir object pooling mantigiyla yapildiginda daha saglikli olur
        {
            GameObject temp = Instantiate(paperPrefab, collectPoint); //kagitlar da karakterle beraber hareket edecek
            temp.transform.position = new Vector3(collectPoint.position.x, 0.5f+((float)paperList.Count / 20), collectPoint.position.z);
            paperList.Add(temp);

            //her eklenen kagit icin listeden silinme komutu
            if (TriggerEventManager.printerManager != null)
            {
                TriggerEventManager.printerManager.RemoveLast();
            }
        }
    }

    //kagidin masaya birakildiginda triggerlanan fonk.
    void GivePaper()
    {
        if (paperList.Count>0)
        {
            TriggerEventManager.workManager.GetPaper();
            RemoveLast();
        }
    }

    public void RemoveLast()
    {
        if (paperList.Count >0)
        {
            Destroy(paperList[paperList.Count - 1]);

            paperList.RemoveAt(paperList.Count - 1);
        }
    }
}