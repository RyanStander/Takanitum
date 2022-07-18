using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{

    //------------------------------------------------
    //         LivesCounter : GameObject 
    //------------------------------------------------
    class LivesCounter : GameObject
    {
        private int _amountOfLivesPlayer1;
        private int _amountOfLivesPlayer2;

        private Player _dummyPlayer1;
        private Player _dummyPlayer2;

        public int _player1Health;
        public int _player2Health;

        private LiveBar _player1Bar;
        private LiveBar _player2Bar;


        //------------------------------------------------
        //              LivesCounter()   
        //------------------------------------------------
        public LivesCounter(Player player1, Player player2,string leftChar, string rightChar)
        {
            _dummyPlayer1 = player1;
            _dummyPlayer2 = player2;
            if (leftChar == "Pob")
            {
                _player1Bar = new LiveBar("PobBar");
            }
            if (leftChar == "Bob")
            {
                _player1Bar = new LiveBar("BobBar");
            }
            if (rightChar == "Bob")
            {
                _player2Bar = new LiveBar("BobBar");
            }
            if (rightChar == "Pob")
            {
                _player2Bar = new LiveBar("PobBar");
            }
            AddChild(_player1Bar);
            _player1Bar.SetXY(0, 25);
            AddChild(_player2Bar);
            _player2Bar.SetXY(1065, 25);
            _player2Bar.Mirror(true, false); 
        }


        //------------------------------------------------
        //                   Update()   
        //------------------------------------------------
        void Update()
        {
            LiveManager();
        }

        //------------------------------------------------
        //                LiveManager()   
        //------------------------------------------------
        public void LiveManager() {
            _amountOfLivesPlayer1 = _dummyPlayer1.GetLives();
            _amountOfLivesPlayer2 = _dummyPlayer2.GetLives();

            _player1Health = _dummyPlayer1.GetHitPointsLeft();
            _player2Health = _dummyPlayer2.GetHitPointsLeft();



            if (_amountOfLivesPlayer1 == 0)
            {
                _dummyPlayer1.LateDestroy();
            }

            if (_amountOfLivesPlayer2 == 0)
            {
                _dummyPlayer2.LateDestroy();
            }
            _player1Bar.SetHP(_amountOfLivesPlayer1);
            _player2Bar.SetHP(_amountOfLivesPlayer2);
        }
    }
}
 















