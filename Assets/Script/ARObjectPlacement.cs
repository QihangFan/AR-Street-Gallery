using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class ARObjectPlacement : MonoBehaviour
{
    ARSessionOrigin sessionOrigin;
    ARRaycastManager raycastManager;
    public GameObject ARObjectPrefab1;
    public GameObject ARObjectPrefab2;
    public GameObject ARObjectPrefab3;
    public GameObject ARObjectPrefab4;

    List<ARRaycastHit> hits;
    bool isAlreadyPlaced;
    bool hitRobot;
    Vector3 ARObjectOriPosition;
    Vector3 RaycastHitOriPosition;

    public GameObject ARBase1;
    public GameObject ARBase2;
    public GameObject ARBase3;
    public GameObject ARBase4;

    public GameObject gesturecon1;
    public GameObject gesturecon2;
    public GameObject gesturecon3;
    public GameObject gesturecon4;

    void Start()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();
        hits = new List<ARRaycastHit>();
        raycastManager = FindObjectOfType<ARRaycastManager>();

        isAlreadyPlaced = false;
        ARBase1.SetActive(false);
        ARBase2.SetActive(false);
        ARBase3.SetActive(false);
        ARBase4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount > 0 && isAlreadyPlaced == false)
        {
            Vector2 middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            if (raycastManager.Raycast(middleScreen, hits))
            {
                ARObjectPrefab1.SetActive(true);
                ARObjectPrefab1.transform.position = hits[0].pose.position;
                ARObjectPrefab1.transform.rotation = hits[0].pose.rotation;

                ARObjectPrefab2.SetActive(true);
                ARObjectPrefab2.transform.position = hits[0].pose.position + new Vector3(2, 0, 1.41f);
                ARObjectPrefab2.transform.eulerAngles = new Vector3(ARObjectPrefab2.transform.eulerAngles.x, ARObjectPrefab2.transform.eulerAngles.y + 210, ARObjectPrefab2.transform.eulerAngles.z);

                ARObjectPrefab3.SetActive(true);
                ARObjectPrefab3.transform.position = hits[0].pose.position + new Vector3(2, 0, -1.41f);
                ARObjectPrefab2.transform.eulerAngles = new Vector3(ARObjectPrefab2.transform.eulerAngles.x, ARObjectPrefab2.transform.eulerAngles.y + 240, ARObjectPrefab2.transform.eulerAngles.z);

                ARObjectPrefab4.SetActive(true);
                ARObjectPrefab4.transform.position = hits[0].pose.position + new Vector3(-2, 0, -1.41f);
                ARObjectPrefab2.transform.eulerAngles = new Vector3(ARObjectPrefab2.transform.eulerAngles.x, ARObjectPrefab2.transform.eulerAngles.y - 150, ARObjectPrefab2.transform.eulerAngles.z);

                isAlreadyPlaced = true;

            }

        }

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        int layermask1 = 1 << 9;
        int layermask2 = 1 << 10;
        int layermask3 = 1 << 11;
        int layermask4 = 1 << 12;


        if (Physics.Raycast(ray, 100, layermask1))
        {
            if (touch.phase == TouchPhase.Began)
            {
                hitRobot = true;
                ARObjectOriPosition = ARObjectPrefab1.transform.position;
                if (raycastManager.Raycast(touch.position, hits))
                {
                    RaycastHitOriPosition = hits[0].pose.position;
                }
                ARBase1.SetActive(true);

                gesturecon1.SetActive(true);
                gesturecon2.SetActive(false);
                gesturecon3.SetActive(false);
                gesturecon4.SetActive(false);

            }

            else if (touch.phase == TouchPhase.Moved)
            {
                if (hitRobot)
                {
                    if (raycastManager.Raycast(touch.position, hits))
                    {
                        ARObjectPrefab1.transform.position = ARObjectOriPosition + hits[0].pose.position - RaycastHitOriPosition;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                hitRobot = false;
                ARBase1.SetActive(false);


            }
        }

        else if (Physics.Raycast(ray, 100, layermask2))
        {
            if (touch.phase == TouchPhase.Began)
            {
                hitRobot = true;
                ARObjectOriPosition = ARObjectPrefab2.transform.position;
                if (raycastManager.Raycast(touch.position, hits))
                {
                    RaycastHitOriPosition = hits[0].pose.position;
                }
                ARBase2.SetActive(true);

                gesturecon1.SetActive(false);
                gesturecon2.SetActive(true);
                gesturecon3.SetActive(false);
                gesturecon4.SetActive(false);
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                if (hitRobot)
                {
                    if (raycastManager.Raycast(touch.position, hits))
                    {
                        ARObjectPrefab2.transform.position = ARObjectOriPosition + hits[0].pose.position - RaycastHitOriPosition;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                hitRobot = false;
                ARBase2.SetActive(false);


            }
        }

        else if (Physics.Raycast(ray, 100, layermask3))
        {
            if (touch.phase == TouchPhase.Began)
            {
                hitRobot = true;
                ARObjectOriPosition = ARObjectPrefab3.transform.position;
                if (raycastManager.Raycast(touch.position, hits))
                {
                    RaycastHitOriPosition = hits[0].pose.position;
                }
                ARBase3.SetActive(true);

                gesturecon1.SetActive(false);
                gesturecon2.SetActive(false);
                gesturecon3.SetActive(true);
                gesturecon4.SetActive(false);

            }

            else if (touch.phase == TouchPhase.Moved)
            {
                if (hitRobot)
                {
                    if (raycastManager.Raycast(touch.position, hits))
                    {
                        ARObjectPrefab3.transform.position = ARObjectOriPosition + hits[0].pose.position - RaycastHitOriPosition;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                hitRobot = false;
                ARBase3.SetActive(false);
            }
        }

        else if (Physics.Raycast(ray, 100, layermask4))
        {
            if (touch.phase == TouchPhase.Began)
            {
                hitRobot = true;
                ARObjectOriPosition = ARObjectPrefab4.transform.position;
                if (raycastManager.Raycast(touch.position, hits))
                {
                    RaycastHitOriPosition = hits[0].pose.position;
                }
                ARBase4.SetActive(true);

                gesturecon1.SetActive(false);
                gesturecon2.SetActive(false);
                gesturecon3.SetActive(false);
                gesturecon4.SetActive(true);
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                if (hitRobot)
                {
                    if (raycastManager.Raycast(touch.position, hits))
                    {
                        ARObjectPrefab4.transform.position = ARObjectOriPosition + hits[0].pose.position - RaycastHitOriPosition;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                hitRobot = false;
                ARBase4.SetActive(false);
            }
        }
    }
}
