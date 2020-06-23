using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GraffitiControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ARsession;
    public GameObject Graffiti1;
    public GameObject Graffiti2;
    public GameObject Graffiti3;
    public GameObject Graffiti4;
    ARSessionOrigin sessionOrigin;
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits;

    int layermask1 = 1 << 9;
    int layermask2 = 1 << 10;
    int layermask3 = 1 << 11;
    int layermask4 = 1 << 12;

    bool isAlreadyPlaced;

    void Start()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>(); 
        raycastManager = FindObjectOfType<ARRaycastManager>();
        isAlreadyPlaced = false;

    }

    void Update()
    {


        if (Input.touchCount > 0 && isAlreadyPlaced == false)
        {
            Vector2 middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);

            if (raycastManager.Raycast(middleScreen, hits))
            {
                Graffiti1.SetActive(true);
                Graffiti1.transform.position = hits[0].pose.position;
                Graffiti1.transform.rotation = hits[0].pose.rotation;
                isAlreadyPlaced = true;
            }
        }



        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);


                if (Physics.Raycast(ray, 100, layermask1))
                {
                    ARsession.GetComponent<ARObjectPlacement>().enabled = true;
                    Graffiti1.transform.Find("GestureControl").gameObject.SetActive(true);
                    Graffiti1.transform.Find("ring").gameObject.SetActive(true);
                }
            }
        }
    }
}

