using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    //------------------------------------------------
    //              Attacks : Sprite    
    //------------------------------------------------
    public class Attacks : Sprite
    {
        string _attackType;
        bool _isLeft;

        //------------------------------------------------
        //                  Attacks()    
        //------------------------------------------------
        public Attacks(string typeOfAttack,bool isLeft) : base(typeOfAttack + ".png")
        {
            alpha = 0;
            _isLeft = isLeft;
            _attackType = typeOfAttack;
        }

        //------------------------------------------------
        //                  Update()    
        //------------------------------------------------
        void Update()
        {
            this.LateDestroy(); //Makes it only appear for one frame.
        }

        //------------------------------------------------
        //              GetAttackType()    
        //------------------------------------------------
        public string GetAttackType()
        {
            return _attackType;
        }

        //------------------------------------------------
        //           GetIsDirectionLeft()    
        //------------------------------------------------
        public bool GetIsDirectionLeft()
        {
            return _isLeft;
        }
    }
}
