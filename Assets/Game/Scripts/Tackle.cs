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
        [SerializeField] private Animator tackleAnimator;
        [SerializeField] private ParticleSystem splashParticle;
        private bool isInWater = false;
        private Fish fish;
        private bool isBaitAvailable = false;
        private bool isBitten = false;
        private bool isTouchSomething = false;
        private bool isInteractedWithFish = false;

        public bool IsBaitAvailable => isBaitAvailable;
        public bool IsBitten => isBitten;
        public bool IsInWater => isInWater;
        public bool IsInteractedWithFish => isInteractedWithFish;

        public delegate void TackleEvent();
        public TackleEvent OnTouchWater;
        public TackleEvent OnTouchGround;

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
            if(!isInWater && !isTouchSomething)
            {
                if(other.tag == waterTag)
                {
                    OnTouchWater?.Invoke();
                    isInWater = true;
                    splashParticle.Play();
                    isTouchSomething = true;
                } else if(other.tag == groundTag)
                {
                    OnTouchGround?.Invoke();
                    isTouchSomething = true;
                    isInWater = false;
                }
                playerAnimator.SetBool("tackleTouchWater", isInWater);
            }
        }

        public void Cast()
        {
            isBaitAvailable = true;
            isBitten = false;
            isTouchSomething = false;
            isInWater = false;
            isInteractedWithFish = false;
            tackleAnimator.Play("idle");
        }

        public Fish PullTackle()
        {
            isInWater = false;
            if(fish != null && fish.IsBiting)
            {
                fish.Pull();
                return fish;
            } else
            {
                if(fish != null)
                {
                    fish.Escape();
                }
                return null;
            }
        }

        public void SetVisible(bool visible)
        {
            splashParticle.Stop();
            visual.SetActive(visible);
        }

        public void Bite(Fish fish)
        {
            this.fish = fish;
            isBitten = true;
            isInteractedWithFish = true;
        }

        public void EatFinish()
        {
            this.fish = null;
            isBitten = false;
            isBaitAvailable = false;
            tackleAnimator.Play("idle");
        }

        public void Eating()
        {
            tackleAnimator.Play("bit");
        }
    }
}
