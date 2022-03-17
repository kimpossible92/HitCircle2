using System.Collections;
using UnityEngine;
using Gameplay.Spaceships;
using Gameplay.ShipSystems;
using System.Collections.Generic;

public class CollShip : MonoBehaviour
{
    public void setLevel(int level)
    {
        Level = level;
    }
    public int viewLevel() { return Level; }
    [SerializeField]int Level = 0;
    public enum slojnost
    {
        easy = 1, normal = 3, hard = 5, nevosmojno = 10
    }
    public slojnost OnSlojnost;
    public static bool isInvincible = false;
    public static float timeSpentInvincible;
    public Texture2D lifeIconTexture;
    public static bool dead = false;
    public static int life = 100;
    [SerializeField] LayerMask layer; public float speed2 = 0.04f;
    public float speed = 0.1f;[SerializeField]
    bool showGUI = true;
    [SerializeField] GameObject[] Cubs= new GameObject[3];
    public void NewStart()
    {
        dead = false;
        life = 100;
        //startpos1 = transform.position;
    }
    // Use this for initialization
    private void Start()
    {
        NewStart();
    }
    private void Update()
    {
        if (dead)
        {
            //Application.LoadLevel("SampleScene"); 
            //foreach (var pi in FindObjectsOfType<Gameplay.Spawners.Spawner>())
            //{
            //    pi.StopSpawn();
            //}
            //GameObject.Find("Canvas").transform.Find("New Game").gameObject.SetActive(true);
        }
        if (isInvincible)
        {
            timeSpentInvincible += Time.deltaTime;

            if (timeSpentInvincible < 3f)
            {
                float remainder = timeSpentInvincible % .3f;
                GetComponent<Renderer>().enabled = remainder > .15f;
            }

            else
            {
                GetComponent<Renderer>().enabled = true;
                isInvincible = false;
            }
        }
    }
    void DisplayLifeCount()
    {
        Rect lifeIconRect = new Rect(10, 150, 32, 32);
        GUI.DrawTexture(lifeIconRect, lifeIconTexture);

        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.yellow;

        Rect labelRect = new Rect(lifeIconRect.xMax + 10, lifeIconRect.y, 60, 32);
        GUI.Label(labelRect, PlayerPrefs.GetInt("score").ToString(), style);
    }
    int apples = 0;
    void OnGUI()
    {
        if (!showGUI)
            return;
        DisplayLifeCount();
    }
    private List<int> types = new List<int>();
    public void AddList(List<int> list)
    {
        foreach(var t in types)
        {
            list.Add(t);
        }
    }
    public bool isnull()
    {
        return types.Count >= 1;
    }
    public void clearList()
    {
        types.Clear();
        //types = new List<int>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "w")
        {
            if (types.Count < 75)
            {
                Destroy(collision.gameObject);
            }//life -= (int)OnSlojnost;
            //if (life <= 0)
            //{
            //    dead = true;
            //}
            //isInvincible = true;
            //timeSpentInvincible = 0;
        }
        if (collision.gameObject.tag == "bonus")
        {
        }
    }
}