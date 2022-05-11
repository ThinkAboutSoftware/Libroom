using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    public float rotateSpeed = 180.0f;

    public void OnClickLeft()
    {
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
    }
}
