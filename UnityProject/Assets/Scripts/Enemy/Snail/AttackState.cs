using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enemy.Snail
{

    public class AttackState : State<BasicSnailCore>
    {

        private static Dictionary<StateMachine<BasicSnailCore>, AttackState> cache =
            new Dictionary<StateMachine<BasicSnailCore>, AttackState>();
        private float ATTACK_SPEED;
        private float ATTACK_PREP_DURATION;
        private float ATTACK_JUMP_DURATION;
        private float ATTACK_RECOVER_DURATION;

        private float time;
        private enum Stage { PREP, ATTACK, RECOVER };
        private Stage stage;


        public AttackState(BasicSnailCore owner, StateMachine<BasicSnailCore> fsm)
            : base(owner, fsm)
        {
            ATTACK_SPEED = owner.ATTACK_SPEED;
            ATTACK_PREP_DURATION = owner.ATTACK_PREP_DURATION;
            ATTACK_JUMP_DURATION = owner.ATTACK_JUMP_DURATION;
            ATTACK_RECOVER_DURATION = owner.ATTACK_RECOVER_DURATION;
        }

        public static AttackState Instance(BasicSnailCore owner, StateMachine<BasicSnailCore> owningFsm)
        {
            if (!cache.ContainsKey(owningFsm))
            {
                cache[owningFsm] = new AttackState(owner, owningFsm);
            }
            return cache[owningFsm];
        }

        public override void Enter()
        {
            Vector3 dirVector =
                (GameManager.Player.transform.position
                 - Owner.transform.position).normalized;
            Owner.Direction = Util.GetDirection8FromVector(dirVector);
            stage = Stage.PREP;
            time = ATTACK_PREP_DURATION;
        }

        public override void Execute()
        {
            time -= Time.deltaTime;

            if (time <= 0.0f)
            {
                switch (stage)
                {
                    case Stage.PREP:
                        stage = Stage.ATTACK;
                        time = ATTACK_JUMP_DURATION;
                        Owner.DamageHitboxEnable = true;
                        break;
                    case Stage.ATTACK:
                        stage = Stage.RECOVER;
                        time = ATTACK_RECOVER_DURATION;
                        Owner.DamageHitboxEnable = false;
                        break;
                    case Stage.RECOVER:
                        FSM.ChangeState(AggroState.Instance(Owner, FSM));
                        break;
                }
            }
        }

        public override void FixedExecute()
        {
            switch (stage)
            {
                case Stage.PREP:
                    Owner.rigidbody2D.velocity = Vector2.zero;
                    break;
                case Stage.ATTACK:
                    Owner.rigidbody2D.velocity =
                        ATTACK_SPEED * Util.GetVectorFromDirection(Owner.Direction);
                    break;
                case Stage.RECOVER:
                    Owner.rigidbody2D.velocity = Vector2.zero;
                    break;
            }
        }

        public override void Exit()
        {
            // Do nothing
        }
    }

}