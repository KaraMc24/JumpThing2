using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace JumpThing
{   // class name, CoinSprite is a child of the Sprite class
    class CoinSprite : Sprite
    {
        public CoinSprite(Texture2D newSpriteSheet, Texture2D newCollisionTxr, Vector2 newLocation) // visible texture sheet used to draw coin and collision box
       : base(newSpriteSheet, newCollisionTxr, newLocation)
        {
            spriteOrigin = new Vector2(0.5f, 0f); // where the coin will be drawn
            isColliding = true; // whether the collision is active

            // initialize animation lists and add frames so appears to move
            animations = new List<List<Rectangle>>();
            animations.Add(new List<Rectangle>());
            animations[0].Add(new Rectangle(48, 48, 48, 48));
            animations[0].Add(new Rectangle(96, 48, 48, 48));
            animations[0].Add(new Rectangle(144, 48, 48, 48));
            animations[0].Add(new Rectangle(96, 48, 48, 48));
        }
    }
}
