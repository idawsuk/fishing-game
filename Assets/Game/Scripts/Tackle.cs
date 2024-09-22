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
        [SerializeField] private Animator playerAnimator;
        private bool isInWater = false;
        private Fish fish;
        private bool isBaitAvailable = false;
        private bool isBitten = false;

        public bool IsBaitAvailable => isBaitAvailable;
        public bool IsBitten => isBitten;

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
                isInWater = false;
            }

            playerAnimator.SetBool("tackleTouchWater", isInWater);
            Debug.Log($"touch {other.tag}");
        }

        public void Cast()
        {
            isBaitAvailable = true;
            isBitten = false;
        }

        public void PullTackle()
        {
            if(fish != null && fish.IsBiting)
            {
                fish.Pull();
                OnFishCatch?.Invoke();
            } else
            {
                if(fish != null)
                {
                    fish.Escape();
                }
                OnFishEscape?.Invoke();
            }
        }

        public void SetVisible(bool visible)
        {
            visual.SetActive(visible);
        }

        public void Bite(Fish fish)
        {
            this.fish = fish;
            isBitten = true;
        }

        public void Eat()
        {
            this.fish = null;
            isBitten = false;
            isBaitAvailable = false;
        }
    }
}
