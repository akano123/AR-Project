using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Bomb_Has_Been_Planted.Classes
{
    public class ItemUnity
    {
        public int ID;
        public string ItemName { get; set; }
        public Button Button { get; set; }
        public ModelEntity Model { get; set; }
        //List<GameObject> ListGO { get; set; }

        public ItemUnity() { }

        public ItemUnity(int ID)
        {
            this.ID = ID;
        }

        public ItemUnity(int ID, string ItemName, Button Button, ModelEntity Model/*, List<GameObject> ListGO*/)
        {
            this.ID = ID;
            this.ItemName = ItemName;
            this.Button = Button;
            this.Model = Model;
            //this.ListGO = ListGO;
        }


    }
}
