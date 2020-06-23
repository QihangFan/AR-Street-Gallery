using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GraffitiControllor : MonoBehaviour
{
    ARSessionOrigin sessionOrigin;
    public GameObject Graffiti1;
    public GameObject Graffiti2;
    public GameObject Graffiti3;
    public GameObject Graffiti4;
    public GameObject Botton;
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits;
    bool graffiti1IsAlreadyPlaced;
    bool graffiti2IsAlreadyPlaced;
    bool graffiti3IsAlreadyPlaced;
    bool graffiti4IsAlreadyPlaced;

    void Start()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>(); //Using FindObjectOfType find ARsessionOrigin
        //sessionOrigin = GetComponent<ARRaycastManager>();
        //generate the list everytime running this file
        hits = new List<ARRaycastHit>();
        raycastManager = FindObjectOfType<ARRaycastManager>();//not recommend if use for several time

        graffiti1IsAlreadyPlaced = false;
        graffiti2IsAlreadyPlaced = false;
        graffiti3IsAlreadyPlaced = false;
        graffiti4IsAlreadyPlaced = false;
    }

    // Start is called before the first frame update

    public void enableGraffiti1()
    {
        Touch touch = Input.GetTouch(0);
        if (graffiti1IsAlreadyPlaced == false)
        {

            Vector2 middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            if (raycastManager.Raycast(middleScreen, hits))
            {
                Graffiti1.SetActive(true);//Activity of the object is true
                Graffiti1.transform.position = hits[0].pose.position;
                Graffiti1.transform.rotation = hits[0].pose.rotation;
                graffiti1IsAlreadyPlaced = true;


            }
        }
    }

    public void enableGraffiti2()
    {
        Touch touch = Input.GetTouch(0);
        if (graffiti2IsAlreadyPlaced == false)
        {

            Vector2 middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            if (raycastManager.Raycast(middleScreen, hits))
            {
                Graffiti2.SetActive(true);//Activity of the object is true
                Graffiti2.transform.position = hits[0].pose.position;
                Graffiti2.transform.rotation = hits[0].pose.rotation;
                graffiti2IsAlreadyPlaced = true;


            }
        }
    }

    public void enableGraffiti3()
    {
        Touch touch = Input.GetTouch(0);
        if (graffiti1IsAlreadyPlaced == false)
        {

            Vector2 middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            if (raycastManager.Raycast(middleScreen, hits))
            {
                Graffiti3.SetActive(true);//Activity of the object is true
                Graffiti3.transform.position = hits[0].pose.position;
                Graffiti3.transform.rotation = hits[0].pose.rotation;
                graffiti3IsAlreadyPlaced = true;


            }
        }
    }


    public void enableGraffiti4()
    {
        Touch touch = Input.GetTouch(0);
        if (graffiti1IsAlreadyPlaced == false)
        {

            Vector2 middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            if (raycastManager.Raycast(middleScreen, hits))
            {
                Graffiti4.SetActive(true);//Activity of the object is true
                Graffiti4.transform.position = hits[0].pose.position;
                Graffiti4.transform.rotation = hits[0].pose.rotation;
                graffiti4IsAlreadyPlaced = true;
                Botton.SetActive(false);


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
