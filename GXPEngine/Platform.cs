using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{

    //------------------------------------------------
    //            Platform : AnimationSprite
    //------------------------------------------------
    class Platform : AnimationSprite
    {
        float _speedX = 2f;
        float _speedY = 2f;
        float _floatingNewX;
        float _floatingStartX;
        float _floatingNewY;
        float _floatingStartY;
        bool _reachedLocationX = false;
        bool _reachedLocationY = false;

        //------------------------------------------------
        //                 Platform()   
        //------------------------------------------------
        public Platform(string filename, float pHeight, float pWidth, float newX, float startX, float newY, float startY, float speedX, float speedY) : base(filename + ".png", 3, 4)
        {
            width = (int)pWidth;
            height = (int)pHeight;

            x = startX;
            y = startY;
            _floatingStartY = startY;
            _floatingNewY = newY;
            _floatingNewX = newX;
            _floatingStartX = startX;
            _speedX = speedX;
            _speedY = speedY;

        }




        //------------------------------------------------
        //                 Update()   
        //------------------------------------------------
        void Update()
        {
            if (!(_floatingNewY == 0))
            {
                PlatformMovementY();
            }

            if (!(_floatingNewX == 0))
            {
                PlatformMovementX();
            }
        }

        //------------------------------------------------
        //             PlatformMovementX()   
        //------------------------------------------------
        void PlatformMovementX()//(bool hasX, bool hasY)
        {
            if (_reachedLocationX)
            {
                if (x > _floatingStartX)
                {
                    x -= _speedX;
                }
                if (x < _floatingStartX)
                {
                    x += _speedX;
                }
                if (_floatingStartX == x)
                {
                    _reachedLocationX = false;
                }
            }
            else
            {
                if (x > _floatingNewX)
                {
                    x -= _speedX;
                }
                if (x < _floatingNewX)
                {
                    x += _speedX;
                }
                if (_floatingNewX == x)
                {
                    _reachedLocationX = true;
                }

            }
        }

        //------------------------------------------------
        //            PlatformMovementY()   
        //------------------------------------------------
        void PlatformMovementY()//(bool hasX, bool hasY)
        {
            if (_reachedLocationY)
            {
                if (y > _floatingStartY)
                {
                    y -= _speedY;
                }
                if (y < _floatingStartY)
                {
                    y += _speedY;
                }
                if (_floatingStartY == y)
                {
                    _reachedLocationY = false;
                }
            }
            else
            {
                if (y > _floatingNewY)
                {
                    y -= _speedY;
                }
                if (y < _floatingNewY)
                {
                    y += _speedY;
                }
                if (_floatingNewY == y)
                {
                    _reachedLocationY = true;
                }

            }
        }

        //------------------------------------------------
        //               OnCollision()   
        //------------------------------------------------
        void OnCollision(GameObject other)
        {
            if (other is Player && (!(_floatingNewX == 0)))
            {
                if (_reachedLocationX)
                {
                    if (x > _floatingNewX)
                    {
                        other.x = other.x + _speedX;
                        other.y = other.y + _speedY;
                    }
                    if (x < _floatingNewX)
                    {
                        other.x = other.x - _speedX;
                        other.y = other.y - _speedY;
                    }
                }
                else
                {
                    if (x > _floatingNewX)
                    {
                        other.x = other.x - _speedX;
                        other.y = other.y - _speedY;
                    }                               
                    if (x < _floatingNewX)          
                    {                               
                        other.x = other.x + _speedX;
                        other.y = other.y + _speedY;
                    }
                }
            }            
        }
    }
}