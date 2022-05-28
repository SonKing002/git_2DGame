using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //ī�޶� �̵� ���� ĳ���� ���󰡱� : �п� ���� ��������
    public class CameraCtrl : MonoBehaviour
    {
        //Ÿ ������Ʈ�� ��ũ��Ʈ
        Transform player; // �÷��̾� ��������
        Transform cam;    //ī�޶� ��������

        //����
        Vector3 targetPosition; //��ǥ ��ġ
        public float camSpeed; //ī�޶� �ӵ� ����

        //�����Ϳ� �����
        public Vector2 areaCenter, areaSize; //ī�޶� ���� ������ �߽���, ũ��
        float camera_HalfWeight, camera_HalfHeight; //ī�޶� ���� ����, ���� ����
        float limitX, limitY; //���� ���� ������ �� �ִ� �Ѱ�Ÿ�
        float clampX, clampY; // ���� ���� �Ÿ�

        void Start()
        {
            //find �Ҵ�
            player = FindObjectOfType<PlayerMove>().transform;

            //ī�޶� �Ҵ�
            cam = Camera.main.transform;

            camera_HalfHeight = Camera.main.orthographicSize; //ī�޶� ������ ����
            camera_HalfWeight = (float)( Screen.width / Screen.height ) * camera_HalfHeight; //ī�޶� ������ ����
        }

        //����� �׸��� �Լ�
        private void OnDrawGizmos()
        {
            //���� �� �����
            Gizmos.color = Color.red;

            //ī�޶� ���� ���� �׸���
            Gizmos.DrawWireCube(areaCenter, areaSize);
        }

        //update�Լ����� ���� ������ ȣ��
        void LateUpdate()
        {
         
            //ī�޶��� ��ġ y +3f ����ġ
            targetPosition = new Vector3(player.position.x, player.position.y + 2f, cam.position.z); //�÷��̾��� x,y ���� ����

            //fallowing cam �ӵ��� �������� ����
            cam.position = Vector3.Lerp(cam.position, targetPosition, Time.deltaTime * camSpeed);

            //center�� ī�޶� �� ������ ����
            limitX = areaSize.x * 0.5f - camera_HalfHeight; //(0 ~ areaSize.x = ����) / 2 = ����  - ī�޶��� ���ݰŸ� = �Ÿ� ����
            limitY = areaSize.y * 0.5f - camera_HalfWeight;

            //���� ���� areaCenter�κ��� limit���� ������ �� �ִ�.
            clampX = Mathf.Clamp(cam.position.x, areaCenter.x - limitX, areaCenter.x + limitX);
            clampY = Mathf.Clamp(cam.position.y, areaCenter.y - limitY, areaCenter.y + limitY);

            cam.position = new Vector3(clampX, clampY, cam.position.z);
            
        }
    }

}