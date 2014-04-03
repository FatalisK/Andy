using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
namespace Andy.Core
{
    public class Player : World
    {
        private float _mass;
        private float _saut;
        private float _hauteurSaut;
        private bool _veutSauter = false;
        private World _world;
        public Collision.Direction ancienneDirection;
        public Player(Sprite s,World world)
            : base(s)
        {
           _masse = 10;
            _world = world;
            _direction = Collision.Direction.RIGHT;
            ancienneDirection = Collision.Direction.RIGHT;
            _vitesse.X = 2;
            _vitesse.Y = 20;
            _saut = 50;
            typeobjet =TypeObjet.PERS;

        }


        public World getWorld()
        {
            return _world;
        }
        public override float getMasse(){
            return _mass;
        }
        public float getVitesse()
        {
            return _vitesse.X;
        }

        public bool getVeutSauter()
        {
            return _veutSauter;
        }

        public void setVeutSauter(bool b)
        {
            _veutSauter = b;
        }

        public float getHauteurSaut()
        {
            return _hauteurSaut;
        }

        public override float getSaut()
        {
            return _saut;
        }
        public void Move(KeyboardState state)
        {
            var keys = state.GetPressedKeys();

    

            if (keys.Length > 0)
            {


               
                if (state.IsKeyDown(Keys.Z))
                {
                    if (!inTheAir()||collisionEnAir)
                    {
                        _saut = 2;
                        _hauteurSaut = sprite.location.Y - 200;
                        _veutSauter = true;


                    _direction = Collision.Direction.TOP;
                    sprite.location.Y += _vitesse.X;

                        

                    }

                }
                if (state.IsKeyDown(Keys.Q)/*&&_c_interdite!=Collision.Direction.LEFT*/)
                {
                    _direction = Collision.Direction.LEFT;
                    sprite.location.X -= _vitesse.X;

                        
                    
                }
                if (state.IsKeyDown(Keys.S))
                {
                    _direction = Collision.Direction.BOT;
                    //sprite.location.Y += _vitesse.X;


                }
                if (state.IsKeyDown(Keys.D)/* && _c_interdite!=Collision.Direction.RIGHT*/)
                {
                    _direction = Collision.Direction.RIGHT;


                    sprite.location.X += _vitesse.X;
                       
                  }
                
            }
            
            else
            {
                _direction = Collision.Direction.PASS;

            }

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

        

       
    }


   
}

