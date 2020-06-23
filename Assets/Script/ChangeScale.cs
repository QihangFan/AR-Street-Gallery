using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    Vector3 oriScale;
    float t;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        oriScale = transform.localScale;
        transform.localScale = Vector3.zero;
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (t < 1f)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, oriScale, t);
            t += speed * Time.deltaTime;
        }
    }
}
