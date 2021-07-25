using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public Color[] bubbleColor;
    public int[] bubbleNum;
    public float[] posX1;
    public float[] posX2;
    public float[] posY;

    public GameObject bubbleObj;

    public bool position;

    public GameObject[] bubbles;

    public int maxY = 3;

    public int aaa;

    public List<GameObject> bubbleMultiple;

    public bool mult;
    void Start()
    {
        CreatStartBubble();   
    }
    void Update()
    {
        bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        if (maxY > 10) GameOver();

        if (aaa > 3)
        {
            aaa = 0;
            if (maxY < 11)
                CreatLineBubble();
        }

        bubbleMultiple.Clear();

        foreach (GameObject bub in bubbles)
        {
            if (maxY < bub.GetComponent<BubbleManager>().lvlY)
                maxY = bub.GetComponent<BubbleManager>().lvlY;

            if (bub.GetComponent<BubbleManager>().bubbleExplose)
            {
                bubbleMultiple.Add(bub);
                if (mult)
                {
                    bub.GetComponent<BubbleManager>().bubbleLvl += bubbleMultiple.Count;
                    GameObject eee = Instantiate(bubbleObj, bub.transform.position, Quaternion.identity);
                    eee.GetComponent<BubbleManager>().bubbleLvl = bub.GetComponent<BubbleManager>().bubbleLvl + bubbleMultiple.Count;
                    eee.GetComponent<BubbleManager>().lvlX = bub.GetComponent<BubbleManager>().lvlX;
                    eee.GetComponent<BubbleManager>().lvlY = bub.GetComponent<BubbleManager>().lvlY;
                    eee.GetComponent<BubbleManager>().bubblePos = bub.GetComponent<BubbleManager>().bubblePos;
                    eee.GetComponent<BubbleManager>().bubbleType = 1;
                    mult = false;
                }
            }
        }
    }
    void CreatStartBubble()
    {
        for (int j = 0; j < maxY + 1; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject bubbObj = Instantiate(bubbleObj, new Vector2(0, 4.5f), Quaternion.identity);
                bubbObj.GetComponent<BubbleManager>().lvlX = i;
                bubbObj.GetComponent<BubbleManager>().lvlY = j;
                bubbObj.GetComponent<BubbleManager>().bubbleType = 1;
                bubbObj.GetComponent<BubbleManager>().bubbleLvl = Random.Range(1, 6);
                if (position) bubbObj.GetComponent<BubbleManager>().bubblePos = true;
            }
            CreatBubble();
        }
        position = true;
    }
    void CreatLineBubble()
    {
        foreach (GameObject bub in bubbles)
        {
                bub.GetComponent<BubbleManager>().lvlY++;
            if (maxY < bub.GetComponent<BubbleManager>().lvlY)
                maxY = bub.GetComponent<BubbleManager>().lvlY;
        }
        for (int i = 0; i < 6; i++)
        {
            GameObject bubbObj = Instantiate(bubbleObj, new Vector2(0, 4.5f), Quaternion.identity);
            bubbObj.GetComponent<BubbleManager>().lvlX = i;
            bubbObj.GetComponent<BubbleManager>().lvlY = 0;
            bubbObj.GetComponent<BubbleManager>().bubbleType = 1;
            bubbObj.GetComponent<BubbleManager>().bubbleLvl = Random.Range(1, 6);
            if (position) bubbObj.GetComponent<BubbleManager>().bubblePos = true;
        }
        CreatBubble();
    }
    void CreatBubble()
    {
        if (position) position = false;
        else position = true;
    }
    void GameOver()
    {
        Debug.Log("Game Over");
    }
}