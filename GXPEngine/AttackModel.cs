using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    //------------------------------------------------
    //         AttackModel : AnimationSprite   
    //------------------------------------------------
    class AttackModel :AnimationSprite
    {
        int _prevStartFrame;
        int _step;
        private State _curState = State.IDLE;

        //------------------------------------------------
        //                 enum State   
        //------------------------------------------------
        public enum State
        {
            IDLE,
            HIT,
        }

        //------------------------------------------------
        //               AttackModel()    
        //------------------------------------------------
        public AttackModel() : base("HitEffect.png", 4, 4)
        {
            SetOrigin(width / 2, height / 2);
        }

        //------------------------------------------------
        //                  Update()    
        //------------------------------------------------
        public void Update()
        {
            //-------------------------------------
            //          Switch STATE                                            //The place where the player's state is being changed.
            //-------------------------------------
            switch (_curState)
            {
                case State.IDLE:
                    SetFrame(1);
                    break;
                case State.HIT:
                    Animator(1, 15, 2);
                    break;
            }
        }

        //------------------------------------------------
        //                  Animator()    
        //------------------------------------------------
        public void Animator(int startFrame, int endFrame, int frameSpeed)
        {
            if (!(_prevStartFrame == startFrame))
            {
                _prevStartFrame = startFrame;
                currentFrame = startFrame;
            }
            _step++;
            if (_step > frameSpeed)
            {
                if (currentFrame == endFrame)
                {                    
                    SetFrame(startFrame);
                    SetState(State.IDLE);
                }
                else
                    SetFrame(currentFrame + 1);
                _step = 0;
            }
        }

        //------------------------------------------------
        //                  SetState()    
        //------------------------------------------------
        public void SetState(State state)
        {
            _curState = state;
        }
    }
}
