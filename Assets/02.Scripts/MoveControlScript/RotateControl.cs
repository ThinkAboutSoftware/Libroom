using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateControl : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;

    private GameObject cube1;

    void Awake()
    {
        cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube1.transform.position = new Vector3(0.75f, 0.0f, 0.0f);
        cube1.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
        cube1.name = "Self";
    }

    void Update()
    {
        cube1.transform.Rotate(xAngle,yAngle,zAngle,Space.Self);
    }
}
