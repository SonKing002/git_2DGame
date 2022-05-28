using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{ 
    public class StatTextFunction : MonoBehaviour
    {
        Animator textAnim;

        private void Start()
        {
            textAnim = GetComponent<Animator>();    
        }

        public void In()
        {
            textAnim.SetBool("isFadeIn", true);//애니메이션 끝부분 북마크 (On애니메이션 작동)
        }

        public void Out()
        {
            textAnim.SetBool("isFadeIn", false);//애니메이션 끝부분 북마크 (Out애니메이션 작동)
        }

    }
}