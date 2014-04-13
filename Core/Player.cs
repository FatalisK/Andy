using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Andy.ScreenCore;
namespace Andy.Core
{
    public class Player : Creature
    {
        private float _hauteurSaut;
        private bool _veutSauter;
        private float _hateurSautAbs = 100;
        ParticleEngine particleEngine;

        public Direction ancienneDirection;
        public Player(Sprite s, World world, ParticleEngine pe,Sprite vie)
            : base(s, world,3)
        {
           _mass = 0.5f;
            _world = world;
            _direction = Direction.RIGHT;
            ancienneDirection = Direction.RIGHT;
            _vitesse.X = 2;
            _vitesse.Y = 10;
            _saut = 5;
            typeobjet =TypeObjet.PERS;
            collisionEnAir=false;
            _veutSauter = false;
            particleEngine = pe;
            Poids = getMasse() * getWorld().getGravity();
            Accel = Poids + getSaut();
            _hateurSautAbs = 100;
            setSpriteVie(vie);
            _reculArme.X = 5;
            _reculArme.Y = 5;

            posArmR.W=35;
            posArmR.X=63;
            posArmR.Y=112;
            posArmR.Z=125;

            posArmL.W=60;
            posArmL.X=85;
            posArmL.Y=112;
            posArmL.Z=125;


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

            //Console.WriteLine("DD" + _direction);
            
            var keys = state.GetPressedKeys();

            if (particleEngine != null)
            {
                particleEngine.EmitterLocation = new Vector2(sprite.location.X + sprite.getFrameWidth()/2, sprite.location.Y + sprite.getFrameHeight());
                particleEngine.Update();
            }
    

            if (keys.Length > 0)
            {



                   if(state.IsKeyDown(Keys.V)){
                           _direction = Direction.TAPER;

                   }
                   else { 
 

                    if (state.IsKeyDown(Keys.Z))
                    {
                        //Console.WriteLine("inT" + inTheAir() + "cc" + collisionEnAir);

                        if (!inTheAir() || collisionEnAir)
                        {

                            _hauteurSaut = sprite.location.Y - _hateurSautAbs;
                            _veutSauter = true;


                            _direction = Direction.TOP;
                            sprite.location.Y += _vitesse.X;



                        }
                    }
                    if (state.IsKeyDown(Keys.S))
                    {
                        _direction = Direction.BOT;


                    }
                
                if (state.IsKeyDown(Keys.Q)/*&&_c_interdite!=Collision.Direction.LEFT*/)
                {
                    _direction = Direction.LEFT;
                    sprite.location.X -= _vitesse.X;

                    regard = Direction.LEFT;    
                    
                }

                if (state.IsKeyDown(Keys.D)/* && _c_interdite!=Collision.Direction.RIGHT*/)
                {
                    _direction = Direction.RIGHT;
                    regard = Direction.RIGHT;

                    sprite.location.X += _vitesse.X;
                       
                }}
                
            }
                   
            else
            {
                _direction = Direction.PASS;


            }
            
            switch (_direction)
            {

                case Direction.LEFT:
                    if (sprite.frameIndex.Y != 1) { sprite.frameIndex.X = 0; }
                    sprite.frameIndex.Y = 1;

                    break;

                case Direction.RIGHT:
                    if (sprite.frameIndex.Y != 0) { sprite.frameIndex.X = 0; }

                    sprite.frameIndex.Y = 0;
                    break;



                
            }


            ancienneDirection = _direction;
     
        }

        public  void DrawPlayer(SpriteBatch spriteBatch)
        {
            if (!_estMort)
            {
                DrawAnimation(spriteBatch);
                particleEngine.Draw(spriteBatch);

                for (int i = 0; i < _pvActuel; i++)
                {
                    getSpriteVie().location.X = 20 + i * 40;

                    spriteBatch.Draw(getSpriteVie().getTexture(), getSpriteVie().location, Color.White);
                }
        }

        }

        public void updatePlayer(){
            //Console.WriteLine("CC" + _invincible + "cpt"+_compteurTempsInv+"Ct"+_temps_invincible);
            if (_invincible == true)
            {
                _compteurTempsInv++;
                if (_compteurTempsInv == _temps_invincible)
                {
                    _invincible = false;
                    _compteurTempsInv = 0;
                }
            }
            if (_pvActuel == 0) { _estMort = true; }
            if (_estMort)
            {
                //Console.WriteLine("La mort par la tche tche");
                ScreenManager.Instance.ChangeScreens("TitleScreen");

            }

        }

       
    }


   
}

