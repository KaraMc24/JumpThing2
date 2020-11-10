using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace JumpThing
{
    class PlayerSprite : Sprite
    {
        public PlayerSprite(Texture2D newSpriteSheet, Texture2D newCollisionTxr, Vector2 newLocation)
            :  base(newSpriteSheet, newCollisionTxr, newLocation)
        {
            frameTime = 0.1f;
            animations = new List<List<Rectangle>>();

            animations.Add(new List<Rectangle>());
            animations[0].Add(new Rectangle(0, 0, 48, 48));
            animations[0].Add(new Rectangle(48, 0, 48, 48));

            animations.Add(new List<Rectangle>());
            animations[1].Add(new Rectangle(48, 0, 48, 48));
            animations[1].Add(new Rectangle(96, 0, 48, 48));
            animations[1].Add(new Rectangle(48, 0, 48, 48));
            animations[1].Add(new Rectangle(144, 0, 48, 48));

            animations.Add(new List<Rectangle>());
            animations[2].Add(new Rectangle(96, 0, 48, 48));

            animations.Add(new List<Rectangle>());
            animations[3].Add(new Rectangle(0, 48, 48, 48));
        }
    }
    
}
