using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;


//First player controls: W,A,S,D / Block: R / Dodge: F / Attacks: Q % E

//Second player controls: I,J,K,L / Block: P / Dodge: ; / Attacks: U & O
namespace GXPEngine
{
    //------------------------------------------------
    //              Player : Sprite   
    //------------------------------------------------
    public class Player : Sprite
    {



        //-------------- Misc. --------------------
        private float _timerCountDown = 0;
        private string _filename;
        private CharModels _model;


        //-------------- SoundPlayer --------------------
        private SoundPlayer _soundPlayer;
        private int _lastSoundTime;
        private int _soundTimeIntervalMS = 500;


        //-------------- Movement -----------------
        private float _speed=10;
        private float _jumpHeight;
        private float _jumpMomentum;
        private State _curState = State.MOVING;//used to select the state of the player
        private float _JumpsLeft;
        private float _xKnockback, _yKnockback;


        //-------------- Actions ------------------
        private Boolean _isShielding = false;
        private AttackModel _hitEffect;

        //--------------Health stuff---------------
        private int _hitPointsLeft = 100;
        private int _amountOfLivesPlayer = 3;
        private int _damageTaken;
        private Boolean _hasDied = false;

        //--------------Key Inputs---------------
        private int _jumpKey;
        private int _leftKey;
        private int _rightKey;
        private int _blockKey;
        private int _quickKey;
        private int _uppercutKey;
        private int _dashKey;

        //------------- This value indicates the amount of jumps a player has in total (including the jump of the ground) -------------//
        private float _AmountOfJumps = 2;

        public CharModels Model { get => _model; set => _model = value; }

        //------------------------------------------------
        //                 enum State  
        //------------------------------------------------
        public enum State
        {
            MOVING,
            RESPAWNING,
            STUNNED,
            BLOCKING,
            ATTACKING,
        }

        //------------------------------------------------
        //              Player()   
        //------------------------------------------------
        public Player(float speed, float jumpHeight, string fileName,int jumpKey,int leftKey,int rightKey,int blockKey, int uppercutKey,int quickKey, int dashKey) : base("player.png")
        {
            //Console.WriteLine("help");
            _speed = speed;
            _jumpHeight = jumpHeight;
            _filename = fileName;
            Model = new CharModels(_filename);
            AddChild(Model);
            _soundPlayer = new SoundPlayer();
            AddChild(_soundPlayer);
            _hitEffect = new AttackModel();
            AddChild(_hitEffect);
            _jumpKey = jumpKey;
            _leftKey = leftKey;
            _rightKey = rightKey;
            _blockKey = blockKey;
            _uppercutKey = uppercutKey;
            _quickKey = quickKey;
            _dashKey = dashKey;
        }



        //-------------------------------------
        //          Update()
        //-------------------------------------
        public void Update()
        {

            _hasDied = false;
            _damageTaken = 100 - _hitPointsLeft;

            if (_jumpMomentum < -15)
            {
                _jumpMomentum = -15;
            }
            //-------------------------------------
            //          Switch STATE                                            //The place where the player's state is being changed.
            //-------------------------------------

            switch (_curState)
            {
                //Falling state, the state the player enters when it is falling.
                case State.MOVING:
                    Movement();
                    if (Model.AttackFinished())
                    {
                        Attack();
                        Block();
                    }
                    Dash();
                    _hasDied = false;

                    if (_model.GetState() == CharModels.State.STUNNED)
                    {
                        _model.SetFin();
                    }


                    break;


                case State.STUNNED:
                    Model.SetState(CharModels.State.STUNNED, _mirrorX);
                    x = x - _xKnockback;
                    y = y - _yKnockback;
                    if (_yKnockback > 0)
                    {
                        _yKnockback--;
                    }
                    else if (_yKnockback < 0)
                    {
                        _yKnockback++;
                    }
                    if (_xKnockback > 0)
                    {
                        _xKnockback--;
                    }
                    else if (_xKnockback < 0)
                    {
                        _xKnockback++;
                    }
                    if (_xKnockback < 1 && _yKnockback < 1)
                    {
                        _jumpMomentum = 0;
                        SetState(State.MOVING);
                    }
                    //Console.WriteLine("Stuck in Stunned");


                    break;
                case State.BLOCKING:
                    Block();
                    if (Input.GetKeyUp(_blockKey))
                    {
                        Model.SetState(CharModels.State.IDLE, _mirrorX);
                        SetState(State.MOVING);
                    }
                    Model.SetState(CharModels.State.BLOCKING, _mirrorX);
                    _jumpMomentum = _jumpMomentum - 1;
                    y -= _jumpMomentum;
                   // Console.WriteLine("Stuck in Blocking");

                    break;
                case State.ATTACKING:
                    Movement();
                    if (Model.AttackFinished())
                        Attack();
                    SetState(State.MOVING);
                    _hasDied = false;

                    //Console.WriteLine("Stuck in Attacking");

                    break;

            }

            if (_hitPointsLeft <= 0)
            {
                Respawn();
            }
        }



        //-------------------------------------
        //          OnCollision()
        //-------------------------------------
        public void OnCollision(GameObject other)
        {
            if (other is Platform)
            {
                y = other.y - height + 1;
                _JumpsLeft = _AmountOfJumps;
            }



            if (other is Attacks)
            {
                _hitEffect.SetState(AttackModel.State.HIT); ;

                Attacks atk = (Attacks)other;
                if (atk.GetAttackType() == "Light")
                {
                    if (atk.GetIsDirectionLeft())
                    {
                        if (!_isShielding)
                        {
                            SetState(State.STUNNED);
                            _yKnockback = 5 * _damageTaken / 20;
                            _xKnockback = 20 * _damageTaken / 40;
                            _hitPointsLeft = _hitPointsLeft - 5;
                        }
                        else
                        {
                            if (Time.time > _lastSoundTime + _soundTimeIntervalMS)
                            {
                                _soundPlayer.Block();
                                _lastSoundTime = Time.time;
                            }
                            _hitPointsLeft = (int)(_hitPointsLeft - 2.5f);
                        }
                    }
                    else
                    {
                        if (!_isShielding)
                        {
                            SetState(State.STUNNED);
                            _yKnockback = 5 * _damageTaken / 20;
                            _xKnockback = -20 * _damageTaken / 40;
                            _hitPointsLeft = _hitPointsLeft - 5;
                        }
                        else
                        {
                            if (Time.time > _lastSoundTime + _soundTimeIntervalMS)
                            {
                                _soundPlayer.Block();
                                _lastSoundTime = Time.time;
                            }
                            _hitPointsLeft = (int)(_hitPointsLeft - 2.5f);
                        }
                    }
                }
                if (atk.GetAttackType() == "Heavy")
                {
                    if (atk.GetIsDirectionLeft())
                    {
                        if (!_isShielding)
                        {
                            SetState(State.STUNNED);
                            _yKnockback = 20 * _damageTaken / 35;
                            _xKnockback = 5 * _damageTaken / 40;
                            _hitPointsLeft = (int)(_hitPointsLeft - 7.5f);
                        }
                        else
                        {
                            if (Time.time > _lastSoundTime + _soundTimeIntervalMS)
                            {
                                _soundPlayer.Block();
                                _lastSoundTime = Time.time;
                            }
                            _hitPointsLeft = (int)(_hitPointsLeft - 3.75f);
                        }

                    }
                    else
                    {
                        if (!_isShielding)
                        {
                            SetState(State.STUNNED);
                            _yKnockback = 20 * _damageTaken / 35;
                            _xKnockback = -5 * _damageTaken / 40;
                            _hitPointsLeft = (int)(_hitPointsLeft - 7.5f);
                        }
                        else
                        {
                            if (Time.time > _lastSoundTime + _soundTimeIntervalMS)
                            {
                                _soundPlayer.Block();
                                _lastSoundTime = Time.time;
                            }
                            _hitPointsLeft = (int)(_hitPointsLeft - 3.75f);
                        }

                    }
                }

            }
            if (other is Environmental)
            {
                Respawn();
            }
        }
        //-------------------------------------
        //          Movement()
        //-------------------------------------
        public void Movement()
        {
            _jumpMomentum = _jumpMomentum - 1;
            y = y - _jumpMomentum;
            if (Input.GetKey(_leftKey))
            {
                x = x - _speed;

                this.Mirror(true, false);


            }
            if (Input.GetKey(_rightKey))
            {
                x = x + _speed;

                this.Mirror(false, false);

            }
            if (Input.GetKeyDown(_jumpKey) && _JumpsLeft >= 1)
            {
                _jumpMomentum = _jumpHeight;
                _JumpsLeft = _JumpsLeft - 1;
            }
            if (!Input.GetKey(_leftKey) && !Input.GetKey(_rightKey) && !Input.GetKeyDown(_jumpKey))
            {
                if (Model.AttackFinished())
                    Model.SetState(CharModels.State.IDLE, _mirrorX);
            }
            else
            {
                if (Model.AttackFinished())
                    Model.SetState(CharModels.State.MOVING, _mirrorX);
            }
        }
        //-------------------------------------
        //          SetState()
        //-------------------------------------
        public void SetState(State state)
        {
            _curState = state;

        }
        //-------------------------------------
        //          Respawn()
        //-------------------------------------
        void Respawn()
        {
            if (Time.time > _lastSoundTime + _soundTimeIntervalMS)
            {
                _soundPlayer.Death();
                _lastSoundTime = Time.time;
            }
            _hasDied = true;
            x = 780;
            y = 0;
            _yKnockback = 0;
            _xKnockback = 0;
            _amountOfLivesPlayer = _amountOfLivesPlayer - 1;
            _hitPointsLeft = 100;
            _jumpMomentum = 0;


        }
        //-------------------------------------
        //          Attack()                        //This is where the attacks are being created (and on what place) as well as what for type it is
        //-------------------------------------
        public void Attack()
        {

            //You can change the attack type when the attack is created/casted.
            //Attacks hitbox = new Attacks("Light"); for a Light attack
            //Attacks hitbox = new Attacks("Heavy"); for a Heavy attack

            if (Input.GetKeyDown(_quickKey) && !Input.GetKeyDown(_uppercutKey))

            {
                Model.SetFinish(false);
                SetState(State.ATTACKING);

                if (_mirrorX)
                {
                    Model.SetState(CharModels.State.QUICK, _mirrorX);
                }
                else
                {
                    Model.SetState(CharModels.State.QUICK, _mirrorX);
                }
            }

            if (Input.GetKeyDown(_uppercutKey) && !Input.GetKeyDown(_quickKey))
            {
                Model.SetFinish(false);

                if (_mirrorX)
                {                    
                    Model.SetState(CharModels.State.UPPERCUT, _mirrorX);
                }
                else
                {
                    Model.SetState(CharModels.State.UPPERCUT, _mirrorX);
                }
            }

        }
        //-------------------------------------
        //          Block()                        //This is where the player spawns a shield around them to protect themselves.
        //-------------------------------------

        public void Block()
        {
            if (Input.GetKey(_blockKey))
            {

                _isShielding = true;
                SetState(State.BLOCKING);


            }
            if (Input.GetKeyUp(_blockKey))
            {
                _isShielding = false;
            }
        }


        public void Dash()
        {



            if (Input.GetKeyDown(_dashKey))
            {
                if (Time.time > _lastSoundTime + _soundTimeIntervalMS)
                {
                    _soundPlayer.Dash();
                    _lastSoundTime = Time.time;
                }
                if (_mirrorX)
                {
                    x = x - _speed - 107;
                }
                else
                {
                    x = x + _speed + 107;
                }
            }
        }


        public int GetHitPointsLeft()
        {

            return _hitPointsLeft;

        }



        public int GetLives()
        {

            return _amountOfLivesPlayer;

        }


        //-------------------------------------
        //          GetY()                       
        //-------------------------------------
        public float GetY()
        {
            return y;
        }


        //-------------------------------------
        //          GetX()                       
        //-------------------------------------
        public float GetX()
        {
            return x;
        }

        //-------------------------------------
        //          GetHasDied()                       
        //-------------------------------------
        public Boolean GetHasDied()
        {
            return (_hasDied);
        }




    }
}
