using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class Tackle : MonoBehaviour
    {
        [SerializeField] private string waterTag;
        [SerializeField] private string groundTag;
        [SerializeField] private GameObject visual;
        private bool isInWater = false;
        private Fish fish;

        public delegate void TackleEvent();
        public TackleEvent OnTouchWater;
        public TackleEvent OnTouchGround;
        public TackleEvent OnFishCatch;
        public TackleEvent OnFishEscape;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == waterTag)
            {
                OnTouchWater?.Invoke();
                isInWater = true;
            }

            if(other.tag == groundTag)
            {
                OnTouchGround?.Invoke();
            }

            Debug.Log($"touch {other.tag}");
        }

        public void PullTackle()
        {
            if(fish != null && fish.IsBiting)
            {
                OnFishCatch?.Invoke();
            } else
            {
                OnFishEscape?.Invoke();
            }
        }

        public void SetVisible(bool visible)
        {
            visual.SetActive(visible);
        }

        public void SetBite(Fish fish)
        {
            this.fish = fish;
        }
    }
}
