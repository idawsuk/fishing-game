using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField] private CastFishingRodAction castingAction;
        [SerializeField] private FishingAction fishingAction;
        [SerializeField] private Tackle tackle;
        [SerializeField] private State currentState;
        private BaseAction currentAction;

        private enum State
        {
            Casting, Fishing
        }

        // Start is called before the first frame update
        void Start()
        {
            ChangeAction(State.Casting);
            tackle.OnTouchWater += OnTackleTouchWater;
            tackle.OnTouchGround += OnTackleTouchGround;
            tackle.OnFishCatch += OnFishCatch;
            tackle.OnFishEscape += OnFishEscape;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void ChangeAction(State state)
        {
            //if(currentState != state)
            //{
                currentState = state;
            //}

            Debug.Log($"change state to {state}");

            currentAction?.End();

            switch (currentState)
            {
                case State.Casting:
                    currentAction = castingAction;
                    break;
                case State.Fishing:
                    currentAction = fishingAction;
                    break;
            }

            currentAction.Begin();
        }

        private void OnTackleTouchWater()
        {
            ChangeAction(State.Fishing);
        }

        private void OnTackleTouchGround()
        {
            ChangeAction(State.Casting);
        }

        private void OnFishCatch()
        {
            Debug.Log("get fish");
            ChangeAction(State.Casting);
        }

        private void OnFishEscape()
        {
            Debug.Log("fish escape");
            ChangeAction(State.Casting);
        }
    }
}
