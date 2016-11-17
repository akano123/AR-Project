using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Bomb_Has_Been_Planted.Classes
{
    public class ModelEntity
    {
        public GameObject take_away { get; set; }
        public GameObject in_door { get; set; }
        public ItemDTO itemDTO { get; set; }


        public ModelEntity (ItemDTO itemDTO,GameObject take_away, GameObject in_door)
        {
            this.itemDTO = itemDTO;
            this.take_away = take_away;
            this.in_door = in_door;
        }

        public GameObject getModel(string type_of_GO)
        {
            return "Take Away".Equals(type_of_GO) ? this.take_away : this.in_door;
        }


    }
}
