using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MakeItBtn : MonoBehaviour
{
    public UnityEvent unityEvent = new UnityEvent();
    public GameObject button;

    void Start()
    {
        button = this.gameObject;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                    {
                        unityEvent.Invoke();
                    }
                }
            }
        }
    }
}