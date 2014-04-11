using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace Andy.ScreenCore
{
    public class FadeEffect : ImageEffect
    {
        public float fadeSpeed;
        public bool increase;

        public FadeEffect()
        {
            fadeSpeed = 1;
            increase = false;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (image.isActivate)
            {
                if (!increase)
                    image.alpha -= fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    image.alpha += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (image.alpha < 0.0f)
                {
                    increase = true;
                    image.alpha = 0.0f;
                }
                else if (image.alpha > 1.0f)
                {
                    increase = false;
                    image.alpha = 1.0f;
                }
            }
            else
                image.alpha = 1.0f;
        }
    }
}
