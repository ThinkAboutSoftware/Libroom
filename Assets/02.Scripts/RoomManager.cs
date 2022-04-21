using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class RoomManager : MonoBehaviour
{
    public GameObject indicator;
    public GameObject myRoom;
    ARRaycastManager arManager;
    GameObject placedObject;

    void Start()
    {
        //시작하자마자 인디케이터 비활성화
        indicator.SetActive(false);
        //AR Raycast Manager 컴포넌트를 가져옴
        arManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        DetectGround();

        if(EventSystem.current.currentSelectedGameObject)
        {
            return;
        }

        // 인디케이터가 활성화 되어 있으면서 화면 터치가 있다면
        if (indicator.activeInHierarchy && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (placedObject == null)
                {
                    placedObject = Instantiate(myRoom, indicator.transform.position, indicator.transform.rotation);
                }
                else
                {
                    placedObject.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
                }
            }
            Destroy(myRoom);
            Destroy(arManager);
        }
    }

    // 바닥 감지 및 indicator 출력 함수
    void DetectGround()
    {
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

        // ray를 이용해 바닥 감지
        if (arManager.Raycast(screenSize, hitInfos, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            // 인디케이터 활성화
            indicator.SetActive(true);

            // 인디케이터의 위치와 회전 값을 레이가 닿은 지점에 일치
            indicator.transform.position = hitInfos[0].pose.position;
            indicator.transform.rotation = hitInfos[0].pose.rotation;
            // 인디케이터 위치를 위로 0.1미터 올림
            // indicator.transform.position += indicator.transform.up * 0.1f;
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
