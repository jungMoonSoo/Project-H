using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yang
{
    public class UnitManager : MonoBehaviour
    {
        public List<Unit> units = new();

        private void Start()
        {
            for (int i = 0; i < units.Count; i++) units[i].Init(this);
        }
    }
}
