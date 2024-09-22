using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private float biteDuration = 3f;
        [SerializeField] private float eatDuration = 5f;
        private float time = 0;
        private bool isBiting = false;
        private Tackle tackle;

        public bool IsBiting => isBiting;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(tackle != null)
            {
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, tackle.transform.position, Time.deltaTime);
                transform.LookAt(tackle.transform, Vector3.up);

                if(time >= biteDuration && time <= eatDuration)
                {
                    isBiting = true;
                } else
                {
                    isBiting = false;
                    if(time > eatDuration)
                    {
                        tackle.Eat();
                        tackle = null;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Tackle")
            {
                time = 0;
                Tackle t = other.GetComponent<Tackle>();
                if(t != null && t.IsBaitAvailable && !t.IsBitten)
                {
                    tackle = t;
                    tackle.Bite(this);
                } else
                {
                    tackle = null;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Tackle")
            {
                if(tackle != null)
                {
                    tackle.Bite(null);
                    tackle = null;
                }
            }
        }

        public void Pull()
        {
            isBiting = false;
            tackle = null;
            gameObject.SetActive(false);
        }

        public void Escape()
        {
            isBiting = false;
            tackle = null;
        }
    }
}
