using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
namespace Andy.Core
{
    public class Player : GameObjects
    {

        private int _vitesse;
        private int _saut;
        private Collision.Direction _collidedDirection;
        public Collision.Direction collidedDirection
        {
            get { return _collidedDirection; }
            set { _collidedDirection = value; }
        }
        public Collision.Direction ancienneDirection;

        public Player(Sprite s,World world)
            : base(s,world)
        {
            _direction = Collision.Direction.RIGHT;
            ancienneDirection = Collision.Direction.RIGHT;
            _vitesse = 2;
            _saut = 300;
            _collidedDirection = Collision.Direction.NONE;

        }

        


        public int getVitesse()
        {
            return _vitesse;
        }

        public void Move(KeyboardState state)
        {
            var keys = state.GetPressedKeys();

            if (keys.Length > 0)
            {
                if (state.IsKeyDown(Keys.Z))
                {
                    _direction = Collision.Direction.TOP;

                    if (!Collision.Collided(this, _world))
                    {
                        if (collidedDirection != Collision.Direction.TOP)
                        {
                            collidedDirection = Collision.Direction.NONE;
                            if (!inTheAir()) { _sprite._location.Y -= _saut; }
                        }
                    }

                }
                if (state.IsKeyDown(Keys.Q))
                {
                    _direction = Collision.Direction.LEFT;

                    if (!Collision.Collided(this, _world))
                    {
                        if (collidedDirection != Collision.Direction.LEFT)
                        {
                            collidedDirection = Collision.Direction.NONE;
                            _sprite._location.X -= _vitesse;
                        }
                    }
                }
                if (state.IsKeyDown(Keys.S))
                {
                    _direction = Collision.Direction.BOT;
                }
                if (state.IsKeyDown(Keys.D))
                {
                    _direction = Collision.Direction.RIGHT;

                    if ((!Collision.Collided(this, _world)))
                    {
                        if (collidedDirection != Collision.Direction.RIGHT)
                        {
                            collidedDirection = Collision.Direction.NONE;
                            _sprite._location.X += _vitesse;
                        }
                    }
                }
            }
            else
            {
                _direction = Collision.Direction.PASS;

            }

            switch (_direction)
            {
                case Collision.Direction.TOP:
                    if (_sprite._frameIndex.Y != 2) { _sprite._frameIndex.X = 0; }
                    _sprite._frameIndex.Y = 2;


                    break;
                case Collision.Direction.LEFT:
                    if (_sprite._frameIndex.Y != 1) { _sprite._frameIndex.X = 0; }
                    _sprite._frameIndex.Y = 1;

                    break;
                case Collision.Direction.BOT:
                    if (_sprite._frameIndex.Y != 3) { _sprite._frameIndex.X = 0; }
                    _sprite._frameIndex.Y = 3;

                    break;
                case Collision.Direction.RIGHT:
                    if (_sprite._frameIndex.Y != 0) { _sprite._frameIndex.X = 0; }

                    _sprite._frameIndex.Y = 0;
                    break;
                case Collision.Direction.PASS:
                    if (_sprite._frameIndex.Y != 4) { _sprite._frameIndex.X = 0; }

                    _sprite._frameIndex.Y = 4;
                    break;
            }

            ancienneDirection = _direction;
     
        }
    }
}

