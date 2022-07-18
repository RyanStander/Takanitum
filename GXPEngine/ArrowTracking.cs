using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{

    //----------------------------------------------------
    //               ArrowTracking : Sprite
    //----------------------------------------------------
    class ArrowTracking : Sprite
    {

        private Player _dummyPlayer1;
        private Player _dummyPlayer2;
        private float _player1Y;
        private float _player2Y;
        private float _player1X;
        private float _player2X;
        private int _focusedPlayer;


        //NOTE: capatilize the Color name plez.


        //----------------------------------------------------
        //                 ArrowTracking()
        //----------------------------------------------------
        public ArrowTracking(Player player1, Player player2, int ForWhatPlayer, String ColorOfArrow) : base(ColorOfArrow + "Arrow.png")
        {
            SetOrigin(width / 2, height / 2);
            _focusedPlayer = ForWhatPlayer;
            _dummyPlayer1 = player1;
            _dummyPlayer2 = player2;
        }

        //----------------------------------------------------
        //                   Update()
        //----------------------------------------------------
        public void Update()
        {
            RecieveCoordinates();
            SettingRotation();
            SettingPosition();

            if(_focusedPlayer == 1)
            {
                SettingAlphaPlayer1();
            }
            if (_focusedPlayer == 2)
            {
                SettingAlphaPlayer2();
            }
        }

        //----------------------------------------------------
        //               RecieveCoordinates
        //----------------------------------------------------
        public void RecieveCoordinates()
        {
            _player1Y = _dummyPlayer1.GetY();
            _player2Y = _dummyPlayer2.GetY();

            _player1X = _dummyPlayer1.GetX();
            _player2X = _dummyPlayer2.GetX();
        }



        //----------------------------------------------------
        //               SettingPosition()
        //----------------------------------------------------
        public void SettingPosition()
        {

            //----------------- Player 1 is being focused -----------------\\

            if (_focusedPlayer == 1 && _player1Y < 0)
            {
                this.SetXY(_player1X, 0 + this.height);
            }

            if (_focusedPlayer == 1 && _player1X < 0)
            {
                this.SetXY(0 + this.width, _player1Y);
            }
            if (_focusedPlayer == 1 && _player1X > game.width)
            {
                this.SetXY(game.width - this.width , _player1Y);
            }
            if (_focusedPlayer == 1 && _player1Y > game.height)
            {
                this.SetXY(_player1X, game.height - this.height);
            }


            //----------------- Player 2 is being focused -----------------\\
            if (_focusedPlayer == 2 && _player2Y < 0)
            {
                this.SetXY(_player2X, 0 + this.height);
            }

            if (_focusedPlayer == 2 && _player2X < 0)
            {
                this.SetXY(0 + this.width, _player2Y);
            }
            if (_focusedPlayer == 2 && _player2X > game.width)
            {
                this.SetXY(game.width - this.width, _player2Y);
            }
            if (_focusedPlayer == 2 && _player2Y > game.height)
            {
                this.SetXY(_player2X, game.height - this.height);
            }

        }

        //----------------------------------------------------
        //               SettingRotation()
        //----------------------------------------------------
        public void SettingRotation()
        {
            if(_player1Y < 0 && _focusedPlayer == 1 || _player2Y < 0 && _focusedPlayer == 2)
            {
                rotation = 0;
            }

            if (_player1X < 0 && _focusedPlayer == 1 || _player2X < 0 && _focusedPlayer == 2)
            {
                rotation = 270;
            }

            if (_player1X > game.width && _focusedPlayer == 1 || _player2X > game.width && _focusedPlayer == 2)
            {
                rotation = 90;
            }
            if (_player1Y > game.height && _focusedPlayer == 1 || _player2Y > game.height && _focusedPlayer == 2)
            {
                rotation = 180;
            }
        }

        //----------------------------------------------------
        //             SettingAlphaPlayer1()
        //----------------------------------------------------
        public void SettingAlphaPlayer1()
        {
            if (_focusedPlayer == 1 && _player1Y < 0 - _dummyPlayer1.height || _player1X > game.width && _focusedPlayer == 1 || _player1X < 0 && _focusedPlayer == 1 || _player1Y > game.height)
            {
                visible = true;
            }
            else
            {
                visible = false;
            }
        }

        //----------------------------------------------------
        //             SettingAlphaPlayer2()
        //----------------------------------------------------
        public void SettingAlphaPlayer2()
        {
            if (_focusedPlayer == 2 && _player2Y < 0 - _dummyPlayer2.height || _player2X > game.width && _focusedPlayer == 2 || _player2X < 0 && _focusedPlayer == 2 || _player1Y > game.height)
            {
                visible = true;
            }
            else
            {
                visible = false;
            }
        }










    }
}
