//
// Fingers Gestures
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DigitalRubyShared
{
    public class DemoScript : MonoBehaviour
    {
        [Tooltip("Drag your AR object here")]
        public GameObject ARObjectToPlace;
        [Tooltip("Enable it to use scale gesture")]
        public bool useScaleGesture = true;
        [Tooltip("Enable it to use rotate gesture")]
        public bool useRotateGesture = true;
        private TapGestureRecognizer tapGesture;
        private TapGestureRecognizer doubleTapGesture;
        private TapGestureRecognizer tripleTapGesture;
        private SwipeGestureRecognizer swipeGesture;
        private PanGestureRecognizer panGesture;
        private ScaleGestureRecognizer scaleGesture;
        private RotateGestureRecognizer rotateGesture;
        private LongPressGestureRecognizer longPressGesture;

        private float nextAsteroid = float.MinValue;
        private GameObject draggingAsteroid;

        private readonly List<Vector3> swipeLines = new List<Vector3>();

        private void DebugText(string text, params object[] format)
        {
            //bottomLabel.text = string.Format(text, format);
            Debug.Log(string.Format(text, format));
        }


        private void DragTo(float screenX, float screenY)
        {
            if (draggingAsteroid == null)
            {
                return;
            }

            Vector3 pos = new Vector3(screenX, screenY, 0.0f);
            pos = Camera.main.ScreenToWorldPoint(pos);
            draggingAsteroid.GetComponent<Rigidbody2D>().MovePosition(pos);
        }

        private void EndDrag(float velocityXScreen, float velocityYScreen)
        {
            if (draggingAsteroid == null)
            {
                return;
            }

            Vector3 origin = Camera.main.ScreenToWorldPoint(Vector3.zero);
            Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(velocityXScreen, velocityYScreen, 0.0f));
            Vector3 velocity = (end - origin);
            draggingAsteroid.GetComponent<Rigidbody2D>().velocity = velocity;
            draggingAsteroid.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(5.0f, 45.0f);
            draggingAsteroid = null;

            DebugText("Long tap flick velocity: {0}", velocity);
        }

        private void HandleSwipe(float endX, float endY)
        {
            Vector2 start = new Vector2(swipeGesture.StartFocusX, swipeGesture.StartFocusY);
            Vector3 startWorld = Camera.main.ScreenToWorldPoint(start);
            Vector3 endWorld = Camera.main.ScreenToWorldPoint(new Vector2(endX, endY));
            float distance = Vector3.Distance(startWorld, endWorld);
            startWorld.z = endWorld.z = 0.0f;

            swipeLines.Add(startWorld);
            swipeLines.Add(endWorld);

            if (swipeLines.Count > 4)
            {
                swipeLines.RemoveRange(0, swipeLines.Count - 4);
            }

            RaycastHit2D[] collisions = Physics2D.CircleCastAll(startWorld, 10.0f, (endWorld - startWorld).normalized, distance);

            if (collisions.Length != 0)
            {
                Debug.Log("Raycast hits: " + collisions.Length + ", start: " + startWorld + ", end: " + endWorld + ", distance: " + distance);

                Vector3 origin = Camera.main.ScreenToWorldPoint(Vector3.zero);
                Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(swipeGesture.VelocityX, swipeGesture.VelocityY, Camera.main.nearClipPlane));
                Vector3 velocity = (end - origin);
                Vector2 force = velocity * 500.0f;

                foreach (RaycastHit2D h in collisions)
                {
                    h.rigidbody.AddForceAtPosition(force, h.point);
                }
            }
        }


        private void ScaleGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Executing)
            {
                DebugText("Scaled: {0}, Focus: {1}, {2}", scaleGesture.ScaleMultiplier, scaleGesture.FocusX, scaleGesture.FocusY);
                ARObjectToPlace.transform.localScale *= scaleGesture.ScaleMultiplier;
            }
        }

        private void CreateScaleGesture()
        {
            scaleGesture = new ScaleGestureRecognizer();
            scaleGesture.StateUpdated += ScaleGestureCallback;
            FingersScript.Instance.AddGesture(scaleGesture);
        }

        private void RotateGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Executing)
            {
                ARObjectToPlace.transform.Rotate(0.0f,  -1f * rotateGesture.RotationRadiansDelta * Mathf.Rad2Deg, 0.0f);
            }
        }

        private void CreateRotateGesture()
        {
            rotateGesture = new RotateGestureRecognizer();
            rotateGesture.StateUpdated += RotateGestureCallback;
            FingersScript.Instance.AddGesture(rotateGesture);
        }

      
        private void PlatformSpecificViewTapUpdated(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Ended)
            {
                Debug.Log("You triple tapped the platform specific label!");
            }
        }

  

        private void Start()
        {

            if(useScaleGesture){
                CreateScaleGesture();
            }
            if(useRotateGesture){
                CreateRotateGesture();
            }

            scaleGesture.AllowSimultaneousExecution(rotateGesture);
        }


        private void LateUpdate()
        {
            if (Time.timeSinceLevelLoad > nextAsteroid)
            {
                nextAsteroid = Time.timeSinceLevelLoad + UnityEngine.Random.Range(1.0f, 4.0f);
               // CreateAsteroid(float.MinValue, float.MinValue);
            }

            int touchCount = Input.touchCount;
            if (FingersScript.Instance.TreatMousePointerAsFinger && Input.mousePresent)
            {
                touchCount += (Input.GetMouseButton(0) ? 1 : 0);
                touchCount += (Input.GetMouseButton(1) ? 1 : 0);
                touchCount += (Input.GetMouseButton(2) ? 1 : 0);
            }
            string touchIds = string.Empty;
            int gestureTouchCount = 0;
            foreach (GestureRecognizer g in FingersScript.Instance.Gestures)
            {
                gestureTouchCount += g.CurrentTrackedTouches.Count;
            }
            foreach (GestureTouch t in FingersScript.Instance.CurrentTouches)
            {
                touchIds += ":" + t.Id + ":";
            }
        }

       
    }
}
