using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{
    //SetActive (true false) �� ������ �帧�� ��������� �� ���Ƽ� �÷��� ���İ����� ����
    //���̹� ��Ģ ���� : �����Ϳ��� SetActive�� false�� �ξ�� �Ѵ�. ( _ + name )
    //���̹� ��Ģ ���� : �����Ϳ��� color ���İ��� 0�� �ξ�� �Ѵ�.  ( . + name )

    public class FadeINFadeOUT : MonoBehaviour
    {
        //���ϴ�
        bool isClick; //true false 

        int num;
        public float transitionSpeed;//ȭ����ȯ ���� �� ���ϰ�
        public Image[] uiImages;// ��Ʈ���� ���  . + name
        public Text[] uiTexts; // ��Ʈ���� �ؽ�Ʈ . + name
        public GameObject[] canvases; //��Ʈ���� ĵ����

        private void Start()
        {
            if (canvases.Length > 0)
            {
                //ĵ���� ����
                canvases[0].SetActive(false);
                canvases[1].SetActive(false);
            }
        }

        private void Update()
        {
            if (!isClick)//Ŭ�� ���̶��
            {
                if (canvases.Length > 0)//������ġ
                {
                    //ĵ���� ����
                    canvases[0].SetActive(false);
                    canvases[1].SetActive(false);
                }

                if (uiImages.Length > 0)//������ġ
                {
                    for (int i = 0; i < uiImages.Length; i++)
                    {
                        //�̹����� ���İ��� 0���� lerp
                        uiImages[i].color = Color.Lerp(uiImages[i].color, new Color(uiImages[i].color.r, uiImages[i].color.g, uiImages[i].color.b, 0f), Time.deltaTime * transitionSpeed);
                    }
                }

                if (uiTexts.Length > 0)//������ġ
                {
                    for (int i = 0; i < uiTexts.Length; i++)
                    {
                        //�ؽ�Ʈ �÷��� ���İ��� 0���� lerp
                        uiTexts[i].color = Color.Lerp(uiTexts[i].color, new Color(uiTexts[i].color.r, uiTexts[i].color.g, uiTexts[i].color.b, 0f), Time.deltaTime * transitionSpeed);
                    }
                }
            }
            else//Ŭ�� ���Ķ��
            {
                if (uiImages.Length > 0)//������ġ
                {
                    for (int i = 0; i < uiImages.Length; i++)
                    {
                        //�̹����� ���İ��� 1�� lerp
                        uiImages[i].color = Color.Lerp(uiImages[i].color, new Color(uiImages[i].color.r, uiImages[i].color.g, uiImages[i].color.b, 1f), Time.deltaTime * transitionSpeed);
                    }
                }

                if (uiTexts.Length > 0)//������ġ
                {
                    for (int i = 0; i < uiTexts.Length; i++)
                    {
                        //�ؽ�Ʈ �÷��� ���İ��� 1�� lerp
                        uiTexts[i].color = Color.Lerp(uiTexts[i].color, new Color(uiTexts[i].color.r, uiTexts[i].color.g, uiTexts[i].color.b, 1f), Time.deltaTime * transitionSpeed);
                    }
                }
                //uiImage[num].color = Color.Lerp(uiImage[num].color, new Color(uiImage[num].color.r, uiImage[num].color.g, uiImage[num].color.b, 1f), Time.deltaTime * 3f);
            }
        }
        public void OnClick_AllFadeOut_btn()
        {
            isClick = false;
        }

        public void OnClick_AllFadeIn_btn()
        {
            isClick = true;

            if (canvases.Length > 0)//������ġ
            {
                //ĵ���� �ѱ�
                canvases[0].SetActive(true);
            }
        }
        
        public void ChooseOne_FadeContrl(int i)
        {
            num = i;
        }
    }
}