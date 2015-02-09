using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enemy.Snail
{
    public class WanderState : State<BasicSnailCore>
    {
        private static Dictionary<StateMachine<BasicSnailCore>, WanderState> cache =
            new Dictionary<StateMachine<BasicSnailCore>,WanderState>();
        private float PAUSE_DURATION;
        private float MOVE_DURATION;
        private float SPEED;

        private float nextAction = 0.0f;
        private float speed = 0.0f;

        public WanderState(BasicSnailCore owner, StateMachine<BasicSnailCore> fsm)
            : base(owner, fsm)
        {
            PAUSE_DURATION = owner.PAUSE_DURATION;
            MOVE_DURATION = owner.MOVE_DURATION;
            SPEED = owner.SPEED;
        }

        public static WanderState Instance(BasicSnailCore owner, StateMachine<BasicSnailCore> owningFsm)
        {
            if(!cache.ContainsKey(owningFsm))
            {
                cache[owningFsm] = new WanderState(owner, owningFsm);
            }
            return cache[owningFsm];
        }

        public override void Enter()
        {
            Owner.LeaveAwareness(null);
            nextAction = 0.0f;
            Owner.Direction = (Util.Dir)(int)(Random.value * 8);
            speed = 0.0f;
        }

        public override void Execute()
        {
            if (Owner.Aggro)
            {
                FSM.ChangeState(AggroState.Instance(Owner, FSM));
                return;
            }

            nextAction -= Time.deltaTime;

            if (nextAction <= 0.0)
            {
                if (speed > 0.0f)
                {
                    speed = 0.0f;
                    nextAction = PAUSE_DURATION;
                }
                else
                {
                    Owner.Direction = (Util.Dir)(int)(Random.value * 8);
                    speed = SPEED;
                    nextAction = MOVE_DURATION;
                }
            }
        }

        public override void FixedExecute()
        {
            Owner.rigidbody2D.velocity = 
                Util.GetVectorFromDirection(Owner.Direction) * speed;
        }

        public override void Exit()
        {
            // Do nothing
        }
    }

}