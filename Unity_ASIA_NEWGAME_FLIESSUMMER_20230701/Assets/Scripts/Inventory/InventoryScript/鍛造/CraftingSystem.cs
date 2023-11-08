using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLORY;

namespace GLORY
{
    public class CraftingSystem : MonoBehaviour
    {
        public List<Items> items = new List<Items>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    [System.Serializable]
    public class Items
    {
        public string name;
        public int ID;
        public Sprite sprite;
        public bool craftable;
        public string cratif;
    }
}

