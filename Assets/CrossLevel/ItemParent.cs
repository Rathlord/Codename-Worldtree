using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParent : MonoBehaviour {

    [SerializeField] Vector3 bounceAmount = new Vector3(0.0f,10f, 0.0f);
    [SerializeField] bool goingUp = true;

    Vector3 thisTransform;
    Vector3 highPoint;
    Vector3 lowPoint;

    [SerializeField] float buffer = 0.01f;

    public float smoothing = 1f;

    private void Start()
    {
        thisTransform = transform.position;
        highPoint = thisTransform + bounceAmount;
        lowPoint = thisTransform - bounceAmount;
        StartCoroutine(Floating());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopAllCoroutines();
        //Object.Destroy(gameObject);

    }

    IEnumerator Floating()
    {
        while (goingUp == true)
        {
            transform.position = Vector3.Lerp(transform.position, highPoint, smoothing * Time.deltaTime);
            print("Lerped down");
            yield return null;
        }

        while (goingUp == false)
        {
            transform.position = Vector3.Lerp(transform.position, lowPoint, smoothing * Time.deltaTime);
            print("Lerped up");
            yield return null;
        }

        print("Didn't lerp");
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
