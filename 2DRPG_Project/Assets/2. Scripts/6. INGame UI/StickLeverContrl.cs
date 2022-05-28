using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Main;
using UnityEngine.UI;

//드래그 가능한 오브젝트가 있다면 스크립트마다 작성하지말고 여기서 작성 후 참조한다.
namespace Main
{
    public class StickLeverContrl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    { 
        public bool isMoblie;

        RectTransform move_back; // 제한
        public RectTransform move_Stick; //레버
        

        //위치초기화 임시
        Vector2 back_ResetPosition;
        //반지름 레버 움직임 제한
        float radius;

        //방향
        [HideInInspector]
        public float dir_x;
        [HideInInspector]
        public float dir_y;
        
        public Color color_NotClickBack;
        public Color color_NotClickStick;
        public Color color_OnClickBack;
        public Color color_OnClickStick;

        private void Start()
        {
            //할당
            move_back = GetComponent<RectTransform>();

            back_ResetPosition = move_back.position;

            radius = move_back.rect.width * 0.5f;

            //기본 임시세팅
            isMoblie = true;
        }

        //스틱 방향확인 함수
        public void Direction()
        {
            //방향 확인
            dir_x = move_Stick.localPosition.x;

            //설정
            if (dir_x > 0)
            {
                dir_x = 1f;
            }
            else if (dir_x == 0)
            {
                dir_x = 0;
            }
            else
            {
                dir_x = -1f;
            }

            //방향 확인
            dir_y = move_Stick.localPosition.y;

            //설정
            if (dir_y > 30f)
            {
                dir_y = 1f;
            }
            else if (dir_y < 30f)
            {
                dir_y = -1f;
            }
            else
            {
                dir_y = 0;
            }

        }

        private void Update()
        {
            Direction();
        }

        public void OnDrag(PointerEventData eventData)
        {

            Vector2 value = eventData.position - (Vector2)move_back.position;
            //구역 제한
            value = Vector2.ClampMagnitude(value, radius);

            //대입 (로컬포지션)
            move_Stick.localPosition= value;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //사용할 때
            move_back.GetComponent<Image>().color = color_OnClickBack;
            move_Stick.GetComponent<Image>().color = color_OnClickStick; 
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //사용하지 않을 때
            move_back.GetComponent<Image>().color = color_NotClickBack;
            move_Stick.GetComponent<Image>().color = color_NotClickStick;

            //위치 초기화
            move_back.position = back_ResetPosition;
            move_Stick.localPosition = Vector2.zero;
        }
    }

}
