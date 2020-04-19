using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{
    /// <summary>
    /// define constants here so we don't have to remeber how they are spelled
    /// make all new scripts  in the LD namespace
    /// </summary>
    public static class Constants
    {
        public const string GROUND_TAG = "Ground";
        public const int OBSTACLE_LAYER = 8;
        public const string PLAYER_TAG = "Player";
    }

    public enum State
    {
        Normal,
        Attacking,
        Jumping,
        SpitSwingStart,
        SpitSwinging,
        SpitSwingTravel,
    }

}
