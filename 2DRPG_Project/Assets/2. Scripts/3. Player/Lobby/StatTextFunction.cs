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
            textAnim.SetBool("isFadeIn", true);//�ִϸ��̼� ���κ� �ϸ�ũ (On�ִϸ��̼� �۵�)
        }

        public void Out()
        {
            textAnim.SetBool("isFadeIn", false);//�ִϸ��̼� ���κ� �ϸ�ũ (Out�ִϸ��̼� �۵�)
        }

    }
}