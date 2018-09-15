using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParent : MonoBehaviour {

    [SerializeField] Vector3 bounceAmount = new Vector3(0.0f,0.5f, 0.0f);
    [SerializeField] bool goingUp = true;

    Vector3 thisTransform;
    Vector3 highPoint;
    Vector3 lowPoint;

    [SerializeField] float buffer = 0.25f;

    public float smoothing = 1f;

    int iDidThis = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (iDidThis == 0 && collision.gameObject.tag == "Player")
        {
            iDidThis++;
            ItemActions();
            StopAllCoroutines();
            Object.Destroy(gameObject);
        }
        else
        {
            print("I tried to proc twice");
            return;
        }
    }

    public virtual void ItemActions()
    {
        print("I'm doing the wrong thing");
    }

    private void Start()
    {
        SetBounceValues();
        StartCoroutine(Floating());
        Invoke("AddCollider", 1f);
    }

    private void AddCollider()
    {
        gameObject.AddComponent<BoxCollider2D>();
        BoxCollider2D thisCollider = GetComponent<BoxCollider2D>();
        thisCollider.isTrigger = true;
    }

    private void SetBounceValues()
    {
        thisTransform = transform.position;
        highPoint = thisTransform + bounceAmount;
        lowPoint = thisTransform - bounceAmount;
    }

    IEnumerator Floating()
    {
        while (goingUp == true)
        {
            transform.position = Vector3.Lerp(transform.position, highPoint, smoothing * Time.deltaTime);
            //print("Lerped down");
            yield return null;
        }

        while (goingUp == false)
        {
            transform.position = Vector3.Lerp(transform.position, lowPoint, smoothing * Time.deltaTime);
            //print("Lerped up");
            yield return null;
        }

        // print("Didn't lerp");
        StartCoroutine(Floating());
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, highPoint) <= buffer)
        {
            goingUp = false;

        }
        if (Vector3.Distance(transform.position, lowPoint) <= buffer)
        {
            goingUp = true;
        }
    }
}
