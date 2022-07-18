using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace GXPEngine
{   
    //------------------------------------------------
    //           SoundPlayer : GameObject 
    //------------------------------------------------
    class SoundPlayer : GameObject
    {
        private Sound _quick;
        private SoundChannel _quickChannel;
        private Sound _uppercut;
        private SoundChannel _uppercutChannel;
        private Sound _jump;
        private SoundChannel _jumpChannel;
        private Sound _block;
        private SoundChannel _blockChannel;
        private Sound _death;
        private SoundChannel _deathChannel;
        private Sound _dash;
        private SoundChannel _dashChannel;

        //------------------------------------------------
        //                    SoundPlayer()   
        //------------------------------------------------
        public SoundPlayer()
        {
            _quick = new Sound("Quick.wav",false,false);
            _uppercut = new Sound("Uppercut.wav", false, false);
            _jump = new Sound("Jump.wav", false, false);
            _block = new Sound("Block.wav", false, false);
            _death = new Sound("DeathExplosion.wav", false, false);
            _dash = new Sound("Dash.wav", false, false);
        }

        //------------------------------------------------
        //                    Quick()   
        //------------------------------------------------
        public void Quick()
        {
            _quickChannel = _quick.Play();
            _quickChannel.Volume = 1f;
        }

        //------------------------------------------------
        //                  Uppercut()   
        //------------------------------------------------
        public void Uppercut()
        {
            _uppercutChannel = _uppercut.Play();
            _uppercutChannel.Volume = 1f;
        }

        //------------------------------------------------
        //                  Block()   
        //------------------------------------------------
        public void Block()
        {
            _blockChannel = _block.Play();
            _blockChannel.Volume = 1f;
        }

        //------------------------------------------------
        //                  Death()   
        //------------------------------------------------
        public void Death()
        {
            _deathChannel = _death.Play();
            _deathChannel.Volume = 1f;
        }

        //------------------------------------------------
        //                  Dash()   
        //------------------------------------------------
        public void Dash()
        {
            _dashChannel = _dash.Play();
            _dashChannel.Volume = 1f;
        }
    }
}
