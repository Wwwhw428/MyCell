using System;
using UnityEngine;
using Edgar.Unity;
using MyCell.DungeonGenerator.levels;

namespace MyCell.DungeonGenerator
{
    [Serializable]
    public class RoomTemplatesConfig
    {
        public GameObject[] DefaultRoomTemplates;

        public GameObject[] ShopRoomTemplates;

        #region hide

        public GameObject[] TeleportRoomTemplates;

        public GameObject[] TreasureRoomTemplates;

        public GameObject[] EntranceRoomTemplates;

        public GameObject[] ExitRoomTemplates;

        public GameObject[] CorridorRoomTemplates;

        #endregion


        public GameObject[] GetRoomTemplates(MyRoom room)
        {
            switch (room.Type)
            {
                case MyRoomType.Shop:
                    return ShopRoomTemplates;

                #region hide

                case MyRoomType.Teleport:
                    return TeleportRoomTemplates;

                case MyRoomType.Treasure:
                    return TreasureRoomTemplates;

                case MyRoomType.Entrance:
                    return EntranceRoomTemplates;

                case MyRoomType.Exit:
                    return ExitRoomTemplates;

                #endregion

                default:
                    return DefaultRoomTemplates;
            }
        }
    }
}