using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //sprite 이미지들 불러오기

    public class ItemSprites : MonoBehaviour
    {
        //인종 3차원 배열
        public SpaciesGroups[] spacies;

        //눈 디테일 커스텀 되도록 나눔
        public EyesPart[] eyes;

        //머리
        public HairPart[] hairs;

        //수염
        public Sprite[] face_Hairs;

        //투구
        public Sprite[] helmets;

        //갑옷
        public ArmorPart[] armors;

        //윗옷 2차원배열
        public UpClothPart[] up_Cloths;

        //한벌옷 2차원배열
        public OnepiecePart[] onepiecePart;

        //바지 2차원배열 
        public PantLegPart[] Pants;

        //망또
        public Sprite[] backs;

        //벨트
        public Sprite[] belts;

        //크로스
        public Sprite[] cross;

        //목걸이
        public Sprite[] necklace;

        //반지
        public Sprite[] ring;

        //무기
        public WeaponPart[] weapon_LeftHands;

        //방패
        public Sprite[] shield_RightHands;

        //3차원 배열 그룹 인종별
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

        //3차원 배열 그룹 인종 파츠별
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

        //머리 길이별 2차원배열
        [System.Serializable]
        public struct HairPart
        {
            // 각 아이템 갯수가 다르기 때문에, 여기 구조체 내부에서 배열
            [SerializeField]
            public Sprite[] longHair;
            [SerializeField]
            public Sprite[] shortHair;
            [SerializeField]
            public Sprite[] sportHair;
        }

        //눈 관련
        [System.Serializable]
        public struct EyesPart
        {
            // 아이템이 (눈매 + 눈동자) 세트라서 갯수가 동일하기 때문에, 외부에서 배열
            [SerializeField]
            public Sprite backEyes;
            public Sprite frontEyes;
        }

        //갑옷 관련
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

        //윗옷 관련
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

        //바지 관련
        [System.Serializable]
        public struct PantLegPart
        {
            [SerializeField]
            public Sprite left;
            [SerializeField]
            public Sprite right;

        }

        //한벌옷 관련
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
        //무기 종류별 분류 2차원배열
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