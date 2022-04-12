using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorControllerDesk : MonoBehaviour
{
    public GameObject[] bodyObject;
    public Color32[] colors;
    Material[] deskMats;

    void Start()
    {
        deskMats = new Material[bodyObject.Length];
        for (int i = 0; i < deskMats.Length; i++)
        {
            deskMats[i] = bodyObject[i].GetComponent<MeshRenderer>().material;
        }
        colors[0] = deskMats[0].color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeColor(int num)
    {
        for (int i = 0; i < deskMats.Length; i++)
        {
            deskMats[i].color = colors[num];
        }
    }
}
