namespace Scenes.Data
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public struct GameStateSave
    {
        public Vector3 position;
        public Quaternion rotation;

        /// <summary>
        /// 인벤토리 아이템
        /// </summary>
        public List<string> items;

        /// <summary>
        /// 돈....
        /// </summary>
        public int money;
    }
}