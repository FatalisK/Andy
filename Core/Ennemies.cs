using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andy.Core
{
    using Microsoft.Xna.Framework;

    public class Ennemies : Creature
    {
        private Vector2 _positionBase; 
        private int _collisions;
        public Ennemies(Sprite s, World w,Vector2 p_b):
            base(s, w)
        {
            _collisions=0;
            _direction = GameObjects.Direction.RIGHT;
            _vitesse.X =5f;
            _vitesse.Y = 20;
            _saut = 50;
            _positionBase = p_b;
        
        }

        public void setCollisions(int c){
            _collisions=c;
        }

        public void se_deplacer(int x, int y)
        {
            if (sprite.location.X > _positionBase.X + x) { _direction = Direction.LEFT; }
            if (sprite.location.X <= _positionBase.X) { _direction = Direction.RIGHT; }
            //Console.WriteLine("c" +_direction);
            if (_collisions > 0)
            {
                if(_direction == Direction.RIGHT){
                    _direction = Direction.LEFT;
                    _collisions = 0;
                }
                else { 
                
                    _direction =Direction.RIGHT;
                    _collisions = 0;
                }

            }
            //Console.WriteLine(_direction);


                if (_direction == Direction.RIGHT) { sprite.location.X = sprite.location.X + _vitesse.X; }
                if (_direction == Direction.LEFT) { sprite.location.X = sprite.location.X - _vitesse.X; }

                

            
            
        }

        public void action()
        {
            //Console.WriteLine("c" + collision);
            se_deplacer(300, 10);
        }


    }
}
