using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STP.Info
{
    [CreateAssetMenu(menuName = "Info/Character Info", fileName = "Char")]
    public class CharInfo : ScriptableObject
    {
        public string Name;

        public Sprite Portrait;
    }
}