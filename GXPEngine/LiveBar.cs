using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{

    //------------------------------------------------
    //         LiveBar : AnimationSprite()   
    //------------------------------------------------
    public class LiveBar : AnimationSprite
    {
        int hp = 3;


        //------------------------------------------------
        //                 LiveBar()   
        //------------------------------------------------
        public LiveBar(string fileName) : base(fileName+ ".png",4,1)
        {

        }

        //------------------------------------------------
        //                 Update()   
        //------------------------------------------------
        public void Update()
        {
            currentFrame = hp;
        }

        //------------------------------------------------
        //                  SetHp()   
        //------------------------------------------------
        public void SetHP(int tempHp)
        {
            hp = tempHp;
        }
    }
}
