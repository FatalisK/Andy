using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Andy.Core
{
    public class Sprite 
    {
        private Texture2D _texture;
        public Vector2 _location;

        /*Joueur*/
        public Vector2 _frameIndex;
        private float _frameTime;
        private int _totalFrames;
        private int _frameWidth;
        private int _frameHeight;

        public Sprite(Vector2 position, int frameWidth, int frameHeight)
        {
            _location = position;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;

        }

        public Sprite(Vector2 position,int totalFrames, int frameWidth, int frameHeight,float frameTime,Vector2 frameIndex)
        {
            _location = position;
            _totalFrames = totalFrames;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _frameTime = frameTime;
            _frameIndex = frameIndex;
        }

        public Rectangle Bbox
        {
            get
            {
                return new Rectangle( (int)_location.X,(int)_location.Y,_frameWidth,_frameHeight);
            }

        }
        public Texture2D getTexture()
        {
            return _texture;
        }

        public void setTexture(Texture2D t)
        {
            _texture = t;
        }

        public int getTotalFrame(){
            return _totalFrames;
        }

        public int getFrameWidth(){
            return _frameWidth;
        }

        public int getFrameHeight(){
            return _frameHeight;
        }

        public float getFrameTime(){
            return _frameTime;
        }



    }
}
