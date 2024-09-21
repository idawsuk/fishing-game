using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class BaseAction : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public virtual void Begin()
        {
            this.enabled = true;
        }

        public virtual void End()
        {
            this.enabled = false;
        }
    }
}
