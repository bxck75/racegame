﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace racegame
{
    class Car : MovableObject
    {
        private int health;
        private int fuel;
        private int nitro;

        private float maxSpeed;
        private float accelerationSpeed;

        private bool isOnGrass;
        private bool isOnRoad;

        Track track;

        public Car(Vector2 position, Texture2D texture, int health, int fuel, int nitro, float maxSpeed, float accelerationSpeed, Track track)
            : base(position, texture, new Vector2(0.0f, 0.0f))
        {
            this.health = health;
            this.fuel = fuel;
            this.nitro = nitro;

            this.maxSpeed = maxSpeed;
            this.accelerationSpeed = accelerationSpeed;

            this.track = track;
        }

        public override void Update()
        {
            base.Update();

            GetInput();

            HandleCollisions();
        }

        private float GetAcceleration(float speed)
        {
            //return acceleratie gebasseerd op huidige speed, later... eerst altijd 1, oftewel lineare speed
            // return (Acceleraion < MaxAcceleration) ? 1 : 0;
            return 1;
        }

        public void GetInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Speed += 5 * GetAcceleration(Speed); // rekening houden met gametime?

                //speed naar coordinaten, gejat van elo..
                Velocity = new Vector2((float)(Speed * Math.Cos(Rotation)),
                (float)(Speed * Math.Sin(Rotation)));
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Velocity = new Vector2(0.0f, 2.0f);
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Velocity = new Vector2(-2.0f, 0.0f);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Velocity = new Vector2(2.0f, 0.0f);
            }
            
        }

        protected override void CalculateMovement()
        {
            // Use a smooth-curve for acceleration & deceleration of the car

            base.CalculateMovement();
        }

        public void HandleCollisions()
        {
            // 1. Haal de car-Rectangle op. Deze is op te halen via het veld: BoundingRectangle
            // 2. Zoek uit met welke tiles de Car in contact komt.
            // 3. Loop door deze tiles heen en controleer vervolgens welke collision-type deze tile heeft
            //      3b. Is deze tile passable?
            //      3c. Is deze tile NIET passable? dan is er een collision.
            //

            // this.BoundingRectangle wordt gebruikt om de tiles rondom de auto te vinden. 

            int xLeftTile = (int)Math.Floor((float)BoundingRectangle.Left / Tile.Width);
            int xRightTile = (int)Math.Ceiling((float)BoundingRectangle.Right / Tile.Width) - 1;
            int yTopTile = (int)Math.Floor((float)BoundingRectangle.Top / Tile.Height);
            int yBottomTile = (int)Math.Ceiling((float)BoundingRectangle.Bottom / Tile.Height) - 1;

            // Loop door de verticale tiles
            for (int y = yTopTile; y <= yBottomTile; ++y)
            {
                // En door de horizontale tiles
                for (int x = xLeftTile; x <= xRightTile; ++x)
                {
                    TileCollision collision = track.GetCollisionOfTile(x, y); // Haal collision-type op van de current Tile

                    switch (collision)
                    {
                        case TileCollision.Road:
                            break;

                        case TileCollision.Grass:
                            break;

                        case TileCollision.Solid:

                            break;

                        case TileCollision.Pitstop:
                            break;
                    }
                }

            }
        }
    }
}