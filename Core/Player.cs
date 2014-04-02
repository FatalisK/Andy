using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
namespace Andy.Core
{
    public class Player : GameObjects
    {

        private float _saut;
        private float _hauteurSaut;
        private Collision.Direction _collidedDirection;
        private Collision.Direction _c_interdite;
        private bool _veutSauter = false;
        public Collision.Direction collidedDirection
        {
            get { return _collidedDirection; }
            set { _collidedDirection = value; }
        }
        public Collision.Direction ancienneDirection;

        public Player(Sprite s,World world)
            : base(s,0.1f,world)
        {
            _direction = Collision.Direction.RIGHT;
            ancienneDirection = Collision.Direction.RIGHT;
            _c_interdite = Collision.Direction.NONE;
            _vitesse.X = 2;
            _saut = 0;
            _collidedDirection = Collision.Direction.NONE;

        }

        


        public float getVitesse()
        {
            return _vitesse.X;
        }

        public float getSaut()
        {
            return _saut;
        }
        public void Move(KeyboardState state)
        {
            var keys = state.GetPressedKeys();

            //if (keys.Length > 0)
            //{

            if (Collision.Collided(this, _world))
            {
                _c_interdite = ancienneDirection;
            }
               
                if (state.IsKeyDown(Keys.Z))
                {
                    if (!inTheAir())
                    {
                        _saut = 2;
                        _hauteurSaut = sprite.location.Y - 200;
                        _veutSauter = true;


                    _direction = Collision.Direction.TOP;
                    
                    _c_interdite = Collision.Direction.NONE;
                        

                    }

                }
                if (state.IsKeyDown(Keys.Q)&&_c_interdite!=Collision.Direction.LEFT)
                {
                    _direction = Collision.Direction.LEFT;
                    sprite.location.X -= _vitesse.X;
                    _c_interdite = Collision.Direction.NONE;

                        
                    
                }
                if (state.IsKeyDown(Keys.S))
                {
                    _direction = Collision.Direction.BOT;
                    _c_interdite = Collision.Direction.NONE;

                }
                if (state.IsKeyDown(Keys.D) && _c_interdite!=Collision.Direction.RIGHT)
                {
                    _direction = Collision.Direction.RIGHT;
                    _c_interdite = Collision.Direction.NONE;


                    sprite.location.X += _vitesse.X;
                       
                  }
                
            //}
            
            //else
            //{
             //   _direction = Collision.Direction.PASS;

//            }

            switch (_direction)
            {
                case Collision.Direction.TOP:
                    if (sprite.frameIndex.Y != 2) { sprite.frameIndex.X = 0; }
                    sprite.frameIndex.Y = 2;


                    break;
                case Collision.Direction.LEFT:
                    if (sprite.frameIndex.Y != 1) { sprite.frameIndex.X = 0; }
                    sprite.frameIndex.Y = 1;

                    break;
                case Collision.Direction.BOT:
                    if (sprite.frameIndex.Y != 3) { sprite.frameIndex.X = 0; }
                    sprite.frameIndex.Y = 3;

                    break;
                case Collision.Direction.RIGHT:
                    if (sprite.frameIndex.Y != 0) { sprite.frameIndex.X = 0; }

                    sprite.frameIndex.Y = 0;
                    break;
                case Collision.Direction.PASS:
                    if (sprite.frameIndex.Y != 4) { sprite.frameIndex.X = 0; }

                    sprite.frameIndex.Y = 4;
                    break;
            }


            ancienneDirection = _direction;
     
        }

        public void Physique()
        {
            float Poids = _masse * _gravity;
            float Accel = Poids + _saut;

            if (_veutSauter) { 

    
    

            if (sprite.location.Y > _hauteurSaut)
            {
                if (_direction == Collision.Direction.RIGHT) {
                    sprite.location.X = sprite.location.X + 0.5f * Accel + _vitesse.X;

                }
                if (_direction == Collision.Direction.LEFT)
                {
                    sprite.location.X = sprite.location.X -( _vitesse.X  + 0.5f * Accel) ;

                }
                sprite.location.Y = sprite.location.Y - (0.5f * Accel + _vitesse.Y);
            }
            else
            {
                _veutSauter = false;
            }

                //_vitesse.X = 0.5f * Accel + _vitesse.X + sprite.location.X;
            //_vitesse.Y = 0.5f * Accel + _vitesse.Y + sprite.location.Y;
        }

            if (!_veutSauter)
            {
                if (sprite.location.Y < 490) { sprite.location.Y = sprite.location.Y + (0.5f * Accel + _vitesse.Y); }

                if (sprite.location.Y > 490) { sprite.location.Y = 490; }
            }

            if (Collision.Collided(this, _world) && inTheAir())
            {
                sprite.location.Y = sprite.location.Y - (0.5f * Accel + _vitesse.Y);

            }
        }
    }


   
}

