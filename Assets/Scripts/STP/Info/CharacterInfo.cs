using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STP.Info
{
    [CreateAssetMenu(menuName = "Info/Character Info", fileName = "Char")]
    public class CharInfo : ScriptableObject
    {
        public static CharInfo Empty
        {
            get
            {
                return new CharInfo();
            }
        }

        public string Name;

        public Sprite Portrait;

        protected CharInfo()
        {
            Name = "";
            Portrait = default;
        }
    }
}