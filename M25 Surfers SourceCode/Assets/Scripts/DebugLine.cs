using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved) // Left mouse button held down
            {
                Vector3 mousePos = Input.GetTouch(0).position;
                mousePos.z = 10f; // Set a fixed distance from the camera
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], worldPos) > 0.1f)
                {
                    AddPoint(worldPos);
                }
            }

            else if (Input.GetTouch(0).phase == TouchPhase.Ended) // Clear points on release
            {
                points.Clear();
                lineRenderer.positionCount = 0;
                lineRenderer.material.color = Color.white;
            }
        }
       
    }

    private void AddPoint(Vector3 point)
    {
        points.Add(point);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
    }
}
