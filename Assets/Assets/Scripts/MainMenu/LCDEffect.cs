using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCDEffect : MonoBehaviour
{
    public float speed;

    public float maxPos;

    void Update()
    {
        RectTransform rectTransGo = GetComponent<RectTransform>();

        if (rectTransGo.transform.localPosition.y > maxPos)
        {
            rectTransGo.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - speed * Time.deltaTime, transform.localPosition.z);
        }
        else if (rectTransGo.transform.localPosition.y <= maxPos)
        {
            rectTransGo.transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z); ;
        }
    }
}
