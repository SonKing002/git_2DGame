using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //���Ϳ��� ���� ��ü �˻�
    public class MonsterColl : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                case "Player":
                    break;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                case "Player":
                    break;
            }
        }
    }
}
