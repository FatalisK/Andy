using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;


namespace Andy.Core
{
    public class Sprite 
    {
        private Texture2D _texture;
        public Vector2 location;

        /*Joueur*/
        public Vector2 frameIndex;
        private float _frameTime;
        private int _totalFrames;
        private int _frameWidth;
        private int _frameHeight;
        private Color[,] _couleurText;
        public Color[] pixelColor;

        public Sprite(Vector2 position, int frameWidth, int frameHeight)
        {
            location = position;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
    

        }

        public Sprite(Vector2 position,int totalFrames, int frameWidth, int frameHeight,float frameTime,Vector2 frameIndex)
        {
            location = position;
            _totalFrames = totalFrames;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _frameTime = frameTime;
            frameIndex = frameIndex;
        }

        public Rectangle Bbox
        {
            get
            {
                return new Rectangle( (int)location.X,(int)location.Y,_frameWidth,_frameHeight);
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

        public Color getCouleur(int i, int j){
           //Console.WriteLine(i + "i,j" + j);
            return _couleurText[i,j];
        }

       public void initCoulour(){

           pixelColor = new Color[_texture.Width * _texture.Height];
           _couleurText = new Color[_texture.Width, _texture.Height];
           pixelColor = new Color[_texture.Width * _texture.Height];

           getTexture().GetData<Color>(pixelColor);
           /*//Permet de creer une texture avec les zone à collisoin en rouge (je sais pas le quoi le mieux entre ça et la cmoparaison des couches alpha
           int i,j;
           for (i = 0; i < _texture.Width; i++)
           {
               for (j = 0; j < _texture.Height; j++)
               {

                   if (pixelColor[i + j * _texture.Width].A == 0)
                   {
                       pixelColor[i + j * _texture.Width] = Color.White;
                       _couleurText[i, j] = Color.White;

                   }
                   else
                   {
                       pixelColor[i + j * _texture.Width] = Color.Red;
                       _couleurText[i, j] = Color.Red;

                   }
               }
           }
            * */

           //TEST//_texture.SetData<Color>(pixelColor);
       }



    }
}
