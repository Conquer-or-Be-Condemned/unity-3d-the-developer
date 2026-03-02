using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICUHpHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public GameObject uiInfoWrapper;
    public TMP_Text uiInfo;
    
    private ControlUnitStatus cuInfo;
    private Canvas canvas;
    private Vector2 canvasSize;
    
    private bool isHover;

    private void Start()
    {
        // UI 초기 상태 설정
        uiInfoWrapper.SetActive(false);

        // ControlUnit과 Canvas 참조
        cuInfo = GameObject.Find("ControlUnit").GetComponent<ControlUnitStatus>();
        canvas = GameObject.Find("HUD").GetComponent<Canvas>();
        canvasSize = canvas.GetComponentInParent<RectTransform>().sizeDelta;

        isHover = false;
    }

    private void Update()
    {
        if (isHover)
        {
            // UI 정보를 업데이트
            UpdateUIInfo();
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHover)
        {
            isHover = true;
            uiInfoWrapper.SetActive(true);

            UpdateUIInfo();
            UpdateUIPosition(eventData.position);
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHover)
        {
            isHover = false;
            uiInfoWrapper.SetActive(false);
        }
    }
    
    public void OnPointerMove(PointerEventData eventData)
    {
        if (isHover)
        {
            UpdateUIInfo();
            UpdateUIPosition(eventData.position);
        }
    }

    private void UpdateUIInfo()
    {
        if (cuInfo.GetCurHp() < 0)
        {
            return;
        }
        // ControlUnit의 HP 정보 업데이트
        uiInfo.SetText("HP : " + cuInfo.GetCurHp() + " / " + cuInfo.GetMaxHp());
    }

    private void UpdateUIPosition(Vector2 pointerPosition)
    {
        // RectTransform의 World Space 크기 계산
        RectTransform rectTransform = uiInfoWrapper.GetComponent<RectTransform>();
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);

        float width = worldCorners[2].x - worldCorners[0].x; // 우측 상단 - 좌측 하단 (World Space 기준 너비)
        float height = worldCorners[2].y - worldCorners[0].y; // 우측 상단 - 좌측 하단 (World Space 기준 높이)

        // UI의 위치를 설정
        uiInfoWrapper.GetComponent<RectTransform>().position = pointerPosition + 
                                                               new Vector2(-width / 2 - 1, height / 2 + 1);
    }
}
