using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemMovementDetails
    {
        public bool WillMoveAndDuplicate { get; set; }
        public GameObject GOToAnimatePosition { get; set; }

        public int NewRow { get; set; }
        public int NewColumn { get; set; }


        public ItemMovementDetails(int newRow, int newColumn, GameObject goToAnimatePosition, bool willMoveAndDuplicate)
        {
            NewRow = newRow;
            NewColumn = newColumn;
            GOToAnimatePosition = goToAnimatePosition;
            WillMoveAndDuplicate = willMoveAndDuplicate;
        }

    }


}
