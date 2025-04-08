using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public ARRaycastManager raycastManager;
    public float maxDistanceFromUser = 2.0f; // Maximum distance allowed

    public bool useCursor = true;

    void Start()
    {
        cursorChildObject.SetActive(useCursor);
    }

    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }
    }

    void UpdateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            Vector3 hitPosition = hits[0].pose.position;

            // Check if the cursor is too far from the user
            float distanceFromUser = Vector3.Distance(Camera.main.transform.position, hitPosition);
            if (distanceFromUser > maxDistanceFromUser)
            {
                // Limit the cursor position to the maximum distance
                Vector3 directionFromUser = (hitPosition - Camera.main.transform.position).normalized;
                hitPosition = Camera.main.transform.position + directionFromUser * maxDistanceFromUser;
            }

            // Smoothly interpolate cursor position
            transform.position = Vector3.Lerp(transform.position, hitPosition, Time.deltaTime * 10);
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
