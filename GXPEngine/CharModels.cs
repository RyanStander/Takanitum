using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{

    //------------------------------------------------
    //              CharModels : AnimationSprite   
    //------------------------------------------------
    public partial class CharModels : AnimationSprite
    {
        private int _step = 0;
        private int _prevStartFrame;
        private State _curState = State.IDLE;
        private bool _isLeft;
        private bool _didFinish = true;
        private SoundPlayer _attacks;


        //------------------------------------------------
        //                Charmodels()   
        //------------------------------------------------
        public CharModels(string filename) : base(filename + ".png", 6, 7)
        {
            SetFrame(1);
            _attacks = new SoundPlayer();
            AddChild(_attacks);
        }


        //------------------------------------------------
        //                  Update()   
        //------------------------------------------------
        void Update()
        {
            x = -30;
            y = -55;
            if (_isLeft)
            {
                this.Mirror(true, false);
            }
            else
            {
                this.Mirror(false, false);
            }
            //-------------------------------------
            //          Switch STATE                                            //The place where the player's state is being changed.
            //-------------------------------------
            switch (_curState)
            {
                case State.IDLE:
                    Animator(0, 1, 16);
                    break;
                case State.MOVING:
                    Animator(36, 41, 4);
                    break;
                case State.BLOCKING:
                    Animator(30, 30, 0);
                    break;
                case State.QUICK:
                    Animator(12, 15, 8);
                    if (currentFrame == 15)
                    {
                        if (_isLeft)
                        {
                            _didFinish = true;
                            _attacks.Quick();
                            Attacks hitbox = new Attacks("Light", _mirrorX);
                            (parent as Player).AddChild(hitbox);
                            hitbox.SetXY(hitbox.x - width + 40, hitbox.y + 10);
                        }
                        else
                        {
                            _didFinish = true;
                            _attacks.Quick();
                            Attacks hitbox = new Attacks("Light", _mirrorX);
                            (parent as Player).AddChild(hitbox);
                            hitbox.SetXY(hitbox.x + width - 50, hitbox.y + 10);
                        }
                    }
                    break;
                case State.UPPERCUT:
                    Animator(5, 11, 5);
                    if (currentFrame == 11)
                    {
                        if (_isLeft)
                        {
                            _didFinish = true;
                            _attacks.Uppercut();
                            Attacks hitbox = new Attacks("Heavy", _mirrorX);
                            (parent as Player).AddChild(hitbox);
                            hitbox.SetXY(hitbox.x - width + 30, hitbox.y - 70);
                        }
                        else
                        {
                            _didFinish = true;
                            _attacks.Uppercut();
                            Attacks hitbox = new Attacks("Heavy", _mirrorX);
                            (parent as Player).AddChild(hitbox);
                            hitbox.SetXY(hitbox.x + width - 50, hitbox.y - 70);
                        }


                    }
                    break;
                case State.FALLING:
                    Animator(56, 62, 10);
                    break;
                case State.STUNNED:
                    Animator(24, 24, 10);
                    break;
            }
        }

        //------------------------------------------------
        //                SetState()   
        //------------------------------------------------
        public void SetState(State state, bool isLeft)
        {
            _curState = state;
            _isLeft = isLeft;
        }

        //------------------------------------------------
        //               Animator()   
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
                }
                else
                    SetFrame(currentFrame + 1);
                _step = 0;
            }
        }

        //------------------------------------------------
        //              AttackFinished()   
        //------------------------------------------------
        public bool AttackFinished()
        {
            return _didFinish;
        }

        //------------------------------------------------
        //              SetFinish()   
        //------------------------------------------------
        public void SetFinish(bool fin)
        {
            _didFinish = fin;
        }

        //------------------------------------------------
        //              GetState()   
        //------------------------------------------------
        public State GetState()
        {
            return _curState;
        }

        //------------------------------------------------
        //              SetFin()   
        //------------------------------------------------
        public void SetFin()
        {
            _didFinish = true;
        }

    }
}

