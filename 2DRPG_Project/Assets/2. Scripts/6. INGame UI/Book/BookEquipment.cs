using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{
    public class BookEquipment : MonoBehaviour
    {
        public GameObject Helmet_Slot_img;
        public GameObject Armor_Slot_img;
        public GameObject Back_Slot_img;
        public GameObject UpCloth_Slot_img;
        public GameObject Leg_Slot_img;
        public GameObject LeftWeapon_Slot_img;

        public GameObject Right_Weapon_Slot_img;
        public GameObject CrossAccessory_Slot_img;
        public GameObject BeltAccessory_Slot_img;
        public GameObject Neckless_Slot_img;
        public GameObject Ring_Slot_img;

        GameObject[] myRoutine = new GameObject[11];

        private void Start()
        {
            myRoutine[0] = Helmet_Slot_img;
            myRoutine[1] = Armor_Slot_img;
            myRoutine[2] = Back_Slot_img;
            myRoutine[3] = UpCloth_Slot_img;
            myRoutine[4] = Leg_Slot_img;
            myRoutine[5] = LeftWeapon_Slot_img;
            myRoutine[6] = Right_Weapon_Slot_img;
            myRoutine[7] = CrossAccessory_Slot_img;
            myRoutine[8] = BeltAccessory_Slot_img;
            myRoutine[9] = Neckless_Slot_img;
            myRoutine[10] = Ring_Slot_img;
        }

        public void PlusImageInit()
        {
            //전체 돌리기
            for (int i = 0; i < myRoutine.Length; i++)
            {
                myRoutine[i].SetActive(false);
            }
        }//init
    }//class
}//namespace