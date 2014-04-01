using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Andy.Core
{
    public class GameObjects
    {
        public Sprite _sprite;
        public World _world;
        public Collision.Direction _direction;
        // Rectangle permettant de définir la zone de l'image à afficher
        public Rectangle _Source;
        // Durée depuis laquelle l'image est à l'écran
        public float _time;
        // Durée de visibilité d'une image
        
        // Indice de l'image en cours




        
        
    

        public GameObjects(Sprite s,World world) { 
            _sprite = s;
            this._world = world;
        
        }
        public GameObjects(Sprite s)
        {
            _sprite = s;

        }
        public GameObjects() {  }// a supprime plus tard

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite.getTexture(), _sprite._location, Color.White);

        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite.getTexture(), _sprite._location, _Source, Color.White);
        }

        public void UpdateFrame(GameTime gameTime)
        {
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            while (_time > _sprite.getFrameTime())
            {
                _sprite._frameIndex.X++;
                _time = 0f;
            }
            if (_sprite._frameIndex.X > _sprite.getTotalFrame()) 
                _sprite._frameIndex.X = 0;


            _Source = new Rectangle(
                (int)(_sprite._frameIndex.X * _sprite.getFrameWidth()),
               (int)(_sprite._frameIndex.Y * _sprite.getFrameHeight()),
                _sprite.getFrameWidth(),
                _sprite.getFrameHeight());

            Physique();

        }

       

        public void Physique(){

            if (_sprite._location.Y < 490)
            {
                _sprite._location.Y = _sprite._location.Y + 1;
            }

            if (_sprite._location.Y > 490)
            {
                _sprite._location.Y = 490;
            }

        }

  
        public bool inTheAir()
        {
            return (_sprite._location.Y < 490);
        }
    }
}
