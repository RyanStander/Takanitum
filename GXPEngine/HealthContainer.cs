using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{

    //------------------------------------------------
    //        HealthContainer : GameObject
    //------------------------------------------------
    class HealthContainer :GameObject
    {
        private Player _player1;
        private Player _player2;        

        private HealthPool _healthPool1;
        private HealthPool _healthPool2;
        private HealthPool _DmgPool1;
        private HealthPool _DmgPool2;
        private int _P1HP=100;
        private int _P2HP=100;
        public HealthContainer(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
            _healthPool1 = new HealthPool("HpBar");
            _healthPool2 = new HealthPool("HpBar");
            _DmgPool1 = new HealthPool("DmgBar");
            _DmgPool2 = new HealthPool("DmgBar");
            AddChild(_healthPool1);
            _healthPool1.SetXY(70, 33);
            AddChild(_healthPool2);
            _healthPool2.SetOrigin(_healthPool2.width,0);
            _DmgPool2.SetOrigin(_DmgPool2.width, 0);
            _DmgPool2.SetXY(1270, 33);
            _healthPool2.SetXY(1270, 33);
            
        }

        //------------------------------------------------
        //                Update()   
        //------------------------------------------------
        public void Update()
        {
            if (!(_P1HP == _player1.GetHitPointsLeft()))
            {
                AddChild(_DmgPool1);
                _healthPool1.setHp(_player1.GetHitPointsLeft());
                _DmgPool1.SetXY(_healthPool1.x + _healthPool1.width, 33);
                _DmgPool1.width = (200 - _player1.GetHitPointsLeft() * 2);
                _DmgPool1.setDmg(100 - _player1.GetHitPointsLeft());

            }
            if (!(_P2HP == _player2.GetHitPointsLeft()))
            {
                AddChild(_DmgPool2);
                _healthPool2.setHp(_player2.GetHitPointsLeft());
                _DmgPool2.SetXY(_healthPool2.x - _healthPool2.width, 33);
                _DmgPool2.width = (200 - _player2.GetHitPointsLeft() * 2);
                _DmgPool2.setDmg(100 - _player2.GetHitPointsLeft());
            }
        }
        
        
    }
}
