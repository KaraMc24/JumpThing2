﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace JumpThing

{   // PlayerSprite is a child of the Sprite class
    class PlayerSprite : Sprite
    {

        bool jumping, walking, falling, jumpIsPressed;
        const float jumpSpeed = 4f;
        const float walkSpeed = 100f;
        public int lives = 3;
        SoundEffect jumpSound, bumpSound;

        // Constructor
        public PlayerSprite(Texture2D newSpriteSheet, Texture2D newCollisionTxr, Vector2 newLocation, SoundEffect newJumpSound, SoundEffect newBumpSound)
            :  base(newSpriteSheet, newCollisionTxr, newLocation)
        {
            // assign the parameters to the member variables:
            jumpSound = newJumpSound;
            bumpSound = newBumpSound;


            // assign values, they override the Sprite class
            spriteOrigin = new Vector2(0.5f, 1f);
            isColliding = true; // is collision active
            //drawCollision = true;
            collisionInsetMin = new Vector2(0.25f, 0.3f); // where the collision will be drawn
            collisionInsetMax = new Vector2(0.25f, 0.03f);

            // initialise animation lists and add frames of animation
            frameTime = 0.1f;
            animations = new List<List<Rectangle>>();

            animations.Add(new List<Rectangle>());
            animations[0].Add(new Rectangle(0, 0, 48, 48));
            animations[0].Add(new Rectangle(0, 0, 48, 48));
            animations[0].Add(new Rectangle(0, 0, 48, 48));
            animations[0].Add(new Rectangle(48, 0, 48, 48));
            animations[0].Add(new Rectangle(48, 0, 48, 48));
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

            jumping = false;
            walking = false;
            falling = true;
            jumpIsPressed = false;
        }

        public void Update(GameTime gameTime, List<PlatformSprite> platforms)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (!jumpIsPressed && !jumping && !falling &&
                (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Space)
                || gamePadState.IsButtonDown(Buttons.A)))
            {
                jumpIsPressed = true;
                jumping = true;
                walking = false;
                falling = false;
                spriteVelocity.Y -= jumpSpeed;
                jumpSound.Play();

            }
            else if (jumpIsPressed && !jumping && !falling &&
                (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Space)
                || gamePadState.IsButtonDown(Buttons.A)))
            {
                jumpIsPressed = false;
            }

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left)
                || gamePadState.IsButtonDown(Buttons.DPadLeft))
            {
                walking = true;
                spriteVelocity.X = -walkSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                flipped = true;
            }
            else if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right)
                || gamePadState.IsButtonDown(Buttons.DPadRight))
            {
                walking = true;
                spriteVelocity.X = walkSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                flipped = false;
            }
            else
            {
                walking = false;
                spriteVelocity.X = 0;
            }

            if ((falling || jumping) && spriteVelocity.Y < 500f)
                spriteVelocity.Y += 5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            spritePos += spriteVelocity;

            bool hasCollided = false;

            foreach (PlatformSprite platform in platforms)
            {
                if (checkCollisionBelow(platform))
                {
                    bumpSound.Play();
                    hasCollided = true;
                    while (checkCollision(platform)) spritePos.Y--;
                    spriteVelocity.Y = 0;
                    jumping = false;
                    falling = false;
                }
               else if (checkCollisionAbove(platform))
                {
                    hasCollided = true;
                    while (checkCollision(platform)) spritePos.Y++;
                    spriteVelocity.Y = 0;
                    jumping = false;
                    falling = true;
                }

                if (checkCollisionLeft(platform))
                {
                    hasCollided = true;
                    while (checkCollision(platform)) spritePos.X--;
                    spriteVelocity.X = 0;
                    
                }
                else if (checkCollisionRight(platform))
                {
                    hasCollided = true;
                    while (checkCollision(platform)) spritePos.X++;
                    spriteVelocity.X = 0;
                    
                }

                if (!hasCollided && walking) falling = true;
                if (jumping && spriteVelocity.Y > 0)
                {
                    jumping = false;
                    falling = true;

                }

                if (walking) setAnim(1);
                else if (falling) setAnim(3);
                else if (jumping) setAnim(2);
                else setAnim(0);
;
            }
        }

        public void ResetPlayer(Vector2 newPos)
        {
            spritePos = newPos;
            spriteVelocity = new Vector2();
            jumping = false;
            walking = false;
            falling = true;
            
        }
    }


    
}
