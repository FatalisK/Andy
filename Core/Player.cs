using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;
namespace Andy.Core
{
    public class Player : Creature
    {
        private float _hauteurSaut;
        private bool _veutSauter;

        public Direction ancienneDirection;
        public Player(Sprite s, World world)
            : base(s, world)
        {
           _mass = 0.5f;
            _world = world;
            _direction = Direction.RIGHT;
            ancienneDirection = Direction.RIGHT;
            _vitesse.X = 2;
            _vitesse.Y = 5;
            _saut = 50;
            typeobjet =TypeObjet.PERS;
            collisionEnAir=false;
            _veutSauter = false;

            Poids = getMasse() * getWorld().getGravity();
            Accel = Poids + getSaut();



        }





        public override bool getVeutSauter()
        {
            return _veutSauter;
        }

  

        public override float getHauteurSaut()
        {
            return _hauteurSaut;
        }

        public override void setVeutSauter(bool b){
            _veutSauter=b;

        }

        public void Move(KeyboardState state)
        {
            
            var keys = state.GetPressedKeys();

    

            if (keys.Length > 0)
            {


            
                    if (state.IsKeyDown(Keys.Z))
                    {
                        //Console.WriteLine("inT" + inTheAir() + "cc" + collisionEnAir);
                        if (!inTheAir() || collisionEnAir)
                        {
                            _saut = 2;
                            _hauteurSaut = sprite.location.Y - 100;
                            _veutSauter = true;


                            _direction = Direction.TOP;
                            sprite.location.Y += _vitesse.X;



                        }
                    }

                
                if (state.IsKeyDown(Keys.Q)/*&&_c_interdite!=Collision.Direction.LEFT*/)
                {
                    _direction = Direction.LEFT;
                    sprite.location.X -= _vitesse.X;

                        
                    
                }
                if (state.IsKeyDown(Keys.S))
                {
                    _direction = Direction.BOT;
                    //sprite.location.Y += _vitesse.X;


                }
                if (state.IsKeyDown(Keys.D)/* && _c_interdite!=Collision.Direction.RIGHT*/)
                {
                    _direction = Direction.RIGHT;


                    sprite.location.X += _vitesse.X;
                       
                  }
                
            }
            
            else
            {
                _direction = Direction.PASS;

            }

            switch (_direction)
            {
                case Direction.TOP:
                    if (sprite.frameIndex.Y != 2) { sprite.frameIndex.X = 0; }
                    sprite.frameIndex.Y = 2;


                    break;
                case Direction.LEFT:
                    if (sprite.frameIndex.Y != 1) { sprite.frameIndex.X = 0; }
                    sprite.frameIndex.Y = 1;

                    break;
                case Direction.BOT:
                    if (sprite.frameIndex.Y != 3) { sprite.frameIndex.X = 0; }
                    sprite.frameIndex.Y = 3;

                    break;
                case Direction.RIGHT:
                    if (sprite.frameIndex.Y != 0) { sprite.frameIndex.X = 0; }

                    sprite.frameIndex.Y = 0;
                    break;
                case Direction.PASS:
                    if (sprite.frameIndex.Y != 4) { sprite.frameIndex.X = 0; }

                    sprite.frameIndex.Y = 4;
                    break;
            }


            ancienneDirection = _direction;
     
        }

        

       
    }


   
}

