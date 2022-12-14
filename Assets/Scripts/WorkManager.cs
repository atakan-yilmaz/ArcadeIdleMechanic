using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkManager : MonoBehaviour
{
    public List<GameObject> paperList = new List<GameObject>();

    List<GameObject> moneyList = new List<GameObject>(); //paralarin droplanmasi icin

    public Transform paperPoint, moneyDropPoint;
    public GameObject paperPrefab, moneyPrefab;

    private void Start()
    {
        StartCoroutine(GenerateMoney());
    }

    IEnumerator GenerateMoney()
    {
        while (true)
        {
            if (paperList.Count > 0)
            {
                GameObject temp = Instantiate(moneyPrefab);
                temp.transform.position = new Vector3(moneyDropPoint.position.x, ((float)moneyList.Count / 30), moneyDropPoint.position.z); //paralarin droplanacagi pozisyon
                moneyList.Add(temp);

                RemoveLast();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void GetPaper()
    {
        GameObject temp = Instantiate(paperPrefab);
        temp.transform.position = new Vector3(paperPoint.position.x, 0.8f+((float)paperList.Count / 20), paperPoint.position.z); //kagitlarin birakilacagi pozisyon
        paperList.Add(temp);
    }

    public void RemoveLast()
    {
        if (paperList.Count > 0)
        {
            Destroy(paperList[paperList.Count - 1]);

            paperList.RemoveAt(paperList.Count - 1);
        }
    }
}