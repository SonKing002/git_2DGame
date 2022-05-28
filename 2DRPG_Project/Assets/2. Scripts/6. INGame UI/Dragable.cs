using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Main;

namespace Main
{

    public class Dragable : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        //���콺�� �巹�� �ϴ� ����
        bool isDrag;

        //RectTransform gameUI_Position;

        //������ ������ ��ġ 
        public Vector2 myPosition;


        void Start()
        {
            //gameUI_Position = GetComponent<RectTransform>();
        }
 
        void Update()
        {


        }
        public void OnDrag(PointerEventData eventData)
        {
            //gameUI_Position = eventData.position;
            transform.position = eventData.position + myPosition;
            //throw new System.NotImplementedException();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //throw new System.NotImplementedException();
        }

    }

}