using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
public class BubbleManager : MonoBehaviour
{
    [Range(1, 11)] public int bubbleLvl = 1;
    [SerializeField] SpriteRenderer bubbleRenderer;
    [SerializeField] Text textNum;
    [SerializeField] GameObject[] bubbMask;
    GameManager gameManager;
    public int lvlX, lvlY;
    float xPos, yPos;
    Rigidbody2D rigid;

    Vector2 startPos, endPos;

    public int bubbleType;

    public bool bubblePos;
    float nearestX;
    int indexX;

    public bool bubbleExplose;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!bubbleExplose)
        {
            bubbleRenderer.color = gameManager.bubbleColor[bubbleLvl - 1];
            textNum.text = gameManager.bubbleNum[bubbleLvl - 1].ToString();
        }
        if (bubblePos) xPos = gameManager.posX1[lvlX];
        else xPos = gameManager.posX2[lvlX];
        yPos = gameManager.posY[lvlY];


        if (bubbleType == 1)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            startPos = transform.position;
            endPos = new Vector2(xPos, yPos);
            transform.position = Vector2.Lerp(startPos, endPos, 20f * Time.deltaTime);
        }
        else if (bubbleType == 2)
        {
            rigid.constraints = RigidbodyConstraints2D.None;
        }
        if (bubbleExplose) BubbleExplosion();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (bubbleType == 2 && collision.gameObject.GetComponent<BubbleManager>())
            if (collision.gameObject.GetComponent<BubbleManager>().bubbleType == 1)
            {
                if (bubblePos)
                {
                    nearestX = gameManager.posX1.OrderBy(x => Mathf.Abs((float)x - transform.position.x)).First();
                    indexX = Array.FindIndex(gameManager.posX1, x => x == nearestX);
                }
                else
                {
                    nearestX = gameManager.posX2.OrderBy(x => Mathf.Abs((float)x - transform.position.x)).First();
                    indexX = Array.FindIndex(gameManager.posX2, x => x == nearestX);
                }
                float nearestY = gameManager.posY.OrderBy(x => Mathf.Abs((float)x - transform.position.y)).First();
                int indexY = Array.FindIndex(gameManager.posY, x => x == nearestY);
                lvlX = indexX;
                lvlY = indexY;

                bubbleType = 1;

                if (gameManager.position == true && lvlY % 2 == 1)
                    bubblePos = true;
                if (gameManager.position == false && lvlY % 2 == 0)
                    bubblePos = true;
            }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (bubbleType == 2 && other.gameObject.GetComponent<BubbleManager>())
            if (other.gameObject.GetComponent<BubbleManager>().bubbleType == 1)
            {
                if (bubbleLvl == other.gameObject.GetComponent<BubbleManager>().bubbleLvl)
                {
                    bubbleExplose = true;
                    other.gameObject.GetComponent<BubbleManager>().bubbleExplose = true;
                    gameManager.mult = true;
                }
            }
    }
   void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.GetComponent<BubbleManager>())
        {
            if (bubbleLvl == other.gameObject.GetComponent<BubbleManager>().bubbleLvl)
                if (bubbleExplose)
                    other.gameObject.GetComponent<BubbleManager>().bubbleExplose = true;
        }
    }
    void BubbleExplosion()
    {
            Destroy(bubbMask[0]);
            Destroy(bubbMask[1]);
            Destroy(gameObject, 1f);
    }
}