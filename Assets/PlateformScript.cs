using System.Collections.Generic;
using UnityEngine;

public class PlateformScript : MonoBehaviour
{
    public List<Transform> points;
    public float speed = 2.0f;
    private int currentPointIndex = 0;

    void Start()
    {
        if (points.Count > 0)
        {
            transform.position = points[0].position;
        }
    }

    void Update()
    {
        if (points.Count == 0)
            return;

        transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex].position, speed * Time.deltaTime);

        if (transform.position == points[currentPointIndex].position)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Count;
        }
    }
}