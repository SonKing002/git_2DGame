using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //몬스터에게 닿은 물체 검사
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
