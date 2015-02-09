using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enemy.Snail
{
    public class AggroState : State<BasicSnailCore>
    {
        private static Dictionary<StateMachine<BasicSnailCore>, AggroState> cache =
            new Dictionary<StateMachine<BasicSnailCore>, AggroState>();
        private float SPEED;
        private float AGGRO_CHASE_DURATION;
        private float AGGRO_PAUSE_DURATION;
        private float AGGRO_ATTACK_RADIUS;

        private float chaseTime;
        private float speed;


        public AggroState(BasicSnailCore owner, StateMachine<BasicSnailCore> fsm)
            : base(owner, fsm)
        {
            SPEED = owner.SPEED;
            AGGRO_CHASE_DURATION = owner.AGGRO_CHASE_DURATION;
            AGGRO_PAUSE_DURATION = owner.AGGRO_PAUSE_DURATION;
            AGGRO_ATTACK_RADIUS = owner.AGGRO_ATTACK_RADIUS;
        }

        public static AggroState Instance(BasicSnailCore owner, StateMachine<BasicSnailCore> owningFsm)
        {
            if (!cache.ContainsKey(owningFsm))
            {
                cache[owningFsm] = new AggroState(owner, owningFsm);
            }
            return cache[owningFsm];
        }

        public override void Enter()
        {
            chaseTime = AGGRO_CHASE_DURATION;
            speed = SPEED;
        }

        public override void Execute()
        {
            chaseTime -= Time.deltaTime;

            if (chaseTime <= 0.0f)
            {
                if (speed > 0.0f)
                {
                    chaseTime = AGGRO_PAUSE_DURATION;
                    speed = 0.0f;
                }
                else
                {
                    chaseTime = AGGRO_CHASE_DURATION;
                    speed = SPEED;
                }
            }
            else
            {
                // Chasing
                if (speed > 0.0f)
                {
                    Vector3 displacement =
                       (GameManager.Player.transform.position
                        - Owner.transform.position);
                    Owner.Direction = Util.GetDirection8FromVector(displacement);
                    if(displacement.magnitude < AGGRO_ATTACK_RADIUS)
                    {
                        FSM.ChangeState(AttackState.Instance(Owner, FSM));
                        return;
                    }
                }
            }
        }

        public override void FixedExecute()
        {
            Owner.rigidbody2D.velocity =
                speed * Util.GetVectorFromDirection(Owner.Direction);
        }

        public override void Exit()
        {
            // Do nothing
        }
    }

}