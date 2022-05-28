using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{

    public class FieldItem : MonoBehaviour
    {
        //날라가는 힘
        float power_x;
        float power_y;

        //힘 대입할 vector2
        Vector2 power;

        //리지드바디2d
        Rigidbody2D rgb2d;

        //임시_만약에 제어한다면 
        bool isKilled;

        //플래이어에 관한 것
        GameObject player;

        void Start()
        {
            
        }

        //몬스터를 잡으면 아이템 드랍되도록 하는 함수
        public void DropItem()
        {
            //리지드바디 컴포넌트 사용
            rgb2d = GetComponent<Rigidbody2D>();

            //랜덤 범위 -2~2.0999..f까지
            power_x = Random.Range(-4f, 4.1f);
            power_y = Random.Range(-4f, 4.1f);

            //벡터
            power = new Vector2(power_x, power_y);
            //물리 적용
            rgb2d.AddForce(power, ForceMode2D.Impulse);
        }

        //소지하고 있는 아이템을 버리게 하도록 하는 함수


    }//class
}//namespace