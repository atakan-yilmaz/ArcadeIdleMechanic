using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterManager : MonoBehaviour
{
    public List<GameObject> paperList = new List<GameObject>();
    public GameObject paperPrefab;
    public Transform exitPoint;
    bool isWorking;

    int stackCount = 10; //bir sirada maks 10 tane kagidin olmasini calistiran int
    

    void Start()
    {
        StartCoroutine(PrintPaper());
    }

    public void RemoveLast()
    {
        Destroy(paperList[paperList.Count - 1]);

        paperList.RemoveAt(paperList.Count - 1);
    }

    IEnumerator PrintPaper()
    {
        while (true) //kagitlarin surekli atmasini istedigim icin 
        {

            float paperCount = paperList.Count; //paperlistten degil de counttan almasini saglamak icin

            //kagitlarin 10arli 3 sira olmasi icin
            int rowCount = (int)paperCount / stackCount;


            if (isWorking == true)
            {
                GameObject temp = Instantiate(paperPrefab);
                temp.transform.position = new Vector3(exitPoint.position.x+((float) rowCount/3), (paperCount%stackCount) / 20, exitPoint.position.z);

                paperList.Add(temp); //paperlistten otomatik cekmesi icin

                if (paperList.Count > 29)
                {
                    isWorking = false;
                }
            }
            else if(paperList.Count<29)
            {
                isWorking = true;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}