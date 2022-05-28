using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{

    public class FieldItem : MonoBehaviour
    {
        //���󰡴� ��
        float power_x;
        float power_y;

        //�� ������ vector2
        Vector2 power;

        //������ٵ�2d
        Rigidbody2D rgb2d;

        //�ӽ�_���࿡ �����Ѵٸ� 
        bool isKilled;

        //�÷��̾ ���� ��
        GameObject player;

        void Start()
        {
            
        }

        //���͸� ������ ������ ����ǵ��� �ϴ� �Լ�
        public void DropItem()
        {
            //������ٵ� ������Ʈ ���
            rgb2d = GetComponent<Rigidbody2D>();

            //���� ���� -2~2.0999..f����
            power_x = Random.Range(-4f, 4.1f);
            power_y = Random.Range(-4f, 4.1f);

            //����
            power = new Vector2(power_x, power_y);
            //���� ����
            rgb2d.AddForce(power, ForceMode2D.Impulse);
        }

        //�����ϰ� �ִ� �������� ������ �ϵ��� �ϴ� �Լ�


    }//class
}//namespace