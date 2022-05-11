using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    public void OnClickLeft()
    {
        prefab.transform.Rotate(10.0f,0.0f,0.0f);
    }
}
