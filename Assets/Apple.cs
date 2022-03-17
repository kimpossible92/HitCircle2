using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] Sprite spriteApple2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject RotateGO;
    bool ingredientFly = false;
    IEnumerator StartAnimateRotate(GameObject item)
    {
        //if (ingrCountTarget[i] > 0)
        //ingrCountTarget[i]--;

        ingredientFly = true;
        GameObject ingr = RotateGO.gameObject;
        AnimationCurve curveX = new AnimationCurve(new Keyframe(0, item.transform.localPosition.x), new Keyframe(0.4f, ingr.transform.position.x));
        AnimationCurve curveY = new AnimationCurve(new Keyframe(0, item.transform.localPosition.y), new Keyframe(0.5f, ingr.transform.position.y));
        curveY.AddKey(0.2f, item.transform.localPosition.y + UnityEngine.Random.Range(-2, 0.5f));
        float startTime = Time.time;
        Vector3 startPos = item.transform.localPosition;
        float speed = UnityEngine.Random.Range(0.4f, 0.6f);
        float distCovered = 0;
        while (distCovered < 0.5f && item != null)
        {
            distCovered = (Time.time - startTime) * speed;
            item.transform.localPosition = new Vector3(curveX.Evaluate(distCovered), curveY.Evaluate(distCovered), 0);
            //item.transform.Rotate(Vector3.back, Time.deltaTime * 30);
            yield return new WaitForFixedUpdate();
        }
        Destroy(item);
        ingredientFly = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "knife")
        {
            //transform.SetParent(Handle.transform);
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);
            GetComponent<SpriteRenderer>().sprite = spriteApple2;
            StartCoroutine(StartAnimateRotate(gameObject));
        }
    }
}
