using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Main;
using UnityEngine.UI;

//�巡�� ������ ������Ʈ�� �ִٸ� ��ũ��Ʈ���� �ۼ��������� ���⼭ �ۼ� �� �����Ѵ�.
namespace Main
{
    public class StickLeverContrl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    { 
        public bool isMoblie;

        RectTransform move_back; // ����
        public RectTransform move_Stick; //����
        

        //��ġ�ʱ�ȭ �ӽ�
        Vector2 back_ResetPosition;
        //������ ���� ������ ����
        float radius;

        //����
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
            //�Ҵ�
            move_back = GetComponent<RectTransform>();

            back_ResetPosition = move_back.position;

            radius = move_back.rect.width * 0.5f;

            //�⺻ �ӽü���
            isMoblie = true;
        }

        //��ƽ ����Ȯ�� �Լ�
        public void Direction()
        {
            //���� Ȯ��
            dir_x = move_Stick.localPosition.x;

            //����
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

            //���� Ȯ��
            dir_y = move_Stick.localPosition.y;

            //����
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
            //���� ����
            value = Vector2.ClampMagnitude(value, radius);

            //���� (����������)
            move_Stick.localPosition= value;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //����� ��
            move_back.GetComponent<Image>().color = color_OnClickBack;
            move_Stick.GetComponent<Image>().color = color_OnClickStick; 
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //������� ���� ��
            move_back.GetComponent<Image>().color = color_NotClickBack;
            move_Stick.GetComponent<Image>().color = color_NotClickStick;

            //��ġ �ʱ�ȭ
            move_back.position = back_ResetPosition;
            move_Stick.localPosition = Vector2.zero;
        }
    }

}
