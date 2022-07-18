using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Environmental : Sprite
{
    public Environmental(string filename,int tWidth,int tHeight) : base(filename + ".png")
    {
        //alpha = 0;
        width = tWidth;
        height = tHeight;
    }
}