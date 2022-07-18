using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class PlayerHealth : Sprite
    {


        private int _hitPointsPlayer1 = 100;

        public PlayerHealth() : base("Player1Health.png")
        {



        }


        public void Update()
        {



        }



        public int GetHealth()
        {
            return _hitPointsPlayer1;
        }

        public void SetHealth(int _totalPlayerHitPoints)
        {
            this._hitPointsPlayer1 = _totalPlayerHitPoints;
        }




    }
}
