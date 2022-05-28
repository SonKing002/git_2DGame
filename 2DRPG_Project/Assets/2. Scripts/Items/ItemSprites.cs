using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //sprite �̹����� �ҷ�����

    public class ItemSprites : MonoBehaviour
    {
        //���� 3���� �迭
        public SpaciesGroups[] spacies;

        //�� ������ Ŀ���� �ǵ��� ����
        public EyesPart[] eyes;

        //�Ӹ�
        public HairPart[] hairs;

        //����
        public Sprite[] face_Hairs;

        //����
        public Sprite[] helmets;

        //����
        public ArmorPart[] armors;

        //���� 2�����迭
        public UpClothPart[] up_Cloths;

        //�ѹ��� 2�����迭
        public OnepiecePart[] onepiecePart;

        //���� 2�����迭 
        public PantLegPart[] Pants;

        //����
        public Sprite[] backs;

        //��Ʈ
        public Sprite[] belts;

        //ũ�ν�
        public Sprite[] cross;

        //�����
        public Sprite[] necklace;

        //����
        public Sprite[] ring;

        //����
        public WeaponPart[] weapon_LeftHands;

        //����
        public Sprite[] shield_RightHands;

        //3���� �迭 �׷� ������
        [System.Serializable]
        public struct SpaciesGroups
        {

            [SerializeField]
            public SpaciesParts[] humans;

            [SerializeField]
            public SpaciesParts[] elfs;

            [SerializeField]
            public SpaciesParts[] orcs;

            [SerializeField]
            public SpaciesParts[] theOrders;

        }

        //3���� �迭 �׷� ���� ������
        [System.Serializable]
        public struct SpaciesParts
        {
            [SerializeField]
            public Sprite head;
            [SerializeField]
            public Sprite body;
            [SerializeField]
            public Sprite leftArm;
            [SerializeField]
            public Sprite rightArm;
            [SerializeField]
            public Sprite leftLeg;
            [SerializeField]
            public Sprite rightLeg;
        }

        //�Ӹ� ���̺� 2�����迭
        [System.Serializable]
        public struct HairPart
        {
            // �� ������ ������ �ٸ��� ������, ���� ����ü ���ο��� �迭
            [SerializeField]
            public Sprite[] longHair;
            [SerializeField]
            public Sprite[] shortHair;
            [SerializeField]
            public Sprite[] sportHair;
        }

        //�� ����
        [System.Serializable]
        public struct EyesPart
        {
            // �������� (���� + ������) ��Ʈ�� ������ �����ϱ� ������, �ܺο��� �迭
            [SerializeField]
            public Sprite backEyes;
            public Sprite frontEyes;
        }

        //���� ����
        [System.Serializable]
        public struct ArmorPart
        {
            [SerializeField]
            public Sprite bodies;
            [SerializeField]
            public Sprite leftShoulders;
            [SerializeField]
            public Sprite rightShoulders;

        }

        //���� ����
        [System.Serializable]
        public struct UpClothPart
        {
            [SerializeField]
            public Sprite bodies;
            [SerializeField]
            public Sprite leftArms;
            [SerializeField]
            public Sprite rightArms;
        }

        //���� ����
        [System.Serializable]
        public struct PantLegPart
        {
            [SerializeField]
            public Sprite left;
            [SerializeField]
            public Sprite right;

        }

        //�ѹ��� ����
        [System.Serializable]
        public struct OnepiecePart
        {
            [SerializeField]
            public Sprite bodies;
            [SerializeField]
            public Sprite leftArms;
            [SerializeField]
            public Sprite rightArms;
        }
        //���� ������ �з� 2�����迭
        [System.Serializable]
        public struct WeaponPart
        {
            [SerializeField]
            public Sprite[] shortSword;
            [SerializeField]
            public Sprite[] middleSword;
            [SerializeField]
            public Sprite[] longSpear;
            [SerializeField]
            public Sprite[] wand;
            [SerializeField]
            public Sprite[] oneSideAxe;
            [SerializeField]
            public Sprite[] twoSideAxe;
            [SerializeField]
            public Sprite[] knife;
            [SerializeField]
            public Sprite[] Lancer;
        }

    }//class
}//namespace