using UnityEngine;
using UnityEngine.UI;
public class BubbleShooter : MonoBehaviour
{
    Vector3 startPos, endPos, mousePos, mouseDir;
    Camera cam;
    LineRenderer lr;

    public GameObject bubbleObj;

    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Text text;
    GameManager gameManager;
    int bubLvL;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        cam = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
        bubLvL = Random.Range(1, 6);
    }
    void Update()
    {
        renderer.color = gameManager.bubbleColor[bubLvL - 1];
        text.text = gameManager.bubbleNum[bubLvL - 1].ToString();


        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseDir = mousePos - gameObject.transform.position;
        mouseDir.z = 0;
        mouseDir = mouseDir.normalized;

        if (Input.GetMouseButtonDown(0))
        {
            lr.enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            startPos = gameObject.transform.position;
            startPos.z = 0;
            lr.SetPosition(0, startPos);
            endPos = mousePos;
            endPos.z = 0;
            lr.SetPosition(1, endPos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
            GameObject bubbShot = Instantiate(bubbleObj, transform.position, Quaternion.identity);
            bubbShot.GetComponent<BubbleManager>().bubbleType = 2;
            bubbShot.GetComponent<BubbleManager>().bubbleLvl = bubLvL;
            bubbShot.GetComponent<Rigidbody2D>().AddForce(mouseDir * 600f);
            bubLvL = Random.Range(1, 6);
            gameManager.aaa++;
        }
    }
}