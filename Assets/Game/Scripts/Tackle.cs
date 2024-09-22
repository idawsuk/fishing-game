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

        public bool IsBaitAvailable => isBaitAvailable;
        public bool IsBitten => isBitten;
        public bool IsInWater => isInWater;

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
            if(!isInWater)
            {
                if(other.tag == waterTag)
                {
                    OnTouchWater?.Invoke();
                    isInWater = true;
                    splashParticle.Play();
                } else if(other.tag == groundTag)
                {
                    OnTouchGround?.Invoke();
                    isInWater = false;
                }
                playerAnimator.SetBool("tackleTouchWater", isInWater);
            }

            Debug.Log($"touch {other.tag}");
        }

        public void Cast()
        {
            isBaitAvailable = true;
            isBitten = false;
            isInWater = false;
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
