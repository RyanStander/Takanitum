using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{

    //------------------------------------------------
    //          HealthPool : Sprite   
    //------------------------------------------------
    class HealthPool : Sprite
    {

        //------------------------------------------------
        //              HealthPool()   
        //------------------------------------------------
        public HealthPool(string Filename) : base(Filename + ".png")
        {

        }

        //------------------------------------------------
        //              Crash()   
        //------------------------------------------------
        public void Crash()
        {
            y++;
            alpha = -0.1f;
        }

        //------------------------------------------------
        //              setHP()   
        //------------------------------------------------
        public void setHp(int HpLeft)
        {
            width = HpLeft*2;                
        }

        //------------------------------------------------
        //              setDmg()   
        //------------------------------------------------
        public void setDmg(int DmgDone)
        {
                Crash();
            
        }
    }
    
}
