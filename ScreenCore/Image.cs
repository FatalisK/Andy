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
    public class Image
    {

        public float alpha;
        public string text, fontName, path, effects;
        public Vector2 position, scale;
        public Rectangle rectancgle;
        [XmlIgnore] public Texture2D texture2D;
        public bool isActivate;


        Vector2 origin;
        ContentManager content;
        RenderTarget2D renderTarget2D;
        SpriteFont spriteFont;
        Dictionary<string, ImageEffect> effectList;
        public FadeEffect fadeEffect;
        

        public Image()
        {
            path = text = effects = String.Empty;
            rectancgle = Rectangle.Empty;
            fontName = "../Content/SpriteFont/Arial";
            position = Vector2.Zero;
            scale = Vector2.One;
            alpha = 1.0f;
            effectList = new Dictionary<string, ImageEffect>();
            fadeEffect = new FadeEffect();
            
        }

        void setEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).isActivate = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }

            effectList.Add(effect.GetType().ToString().Replace("Andy.ScreenCore.", ""), (effect as ImageEffect));
        }

        public void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].isActivate = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DesactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].isActivate = false;
                effectList[effect].UnloadContent(); ;
            }
        }

        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (path != String.Empty)
                texture2D = content.Load<Texture2D>(path);

            spriteFont = content.Load<SpriteFont>(fontName);

            Vector2 dimensions = Vector2.Zero;

            if (texture2D != null)
                dimensions.X += texture2D.Width;
            dimensions.X += spriteFont.MeasureString(text).X;

            if (texture2D != null)
                dimensions.Y = Math.Max(texture2D.Height, spriteFont.MeasureString(text).Y);
            else
               dimensions.Y = spriteFont.MeasureString(text).Y;

            if (rectancgle == Rectangle.Empty)
                rectancgle = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);


            renderTarget2D = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
                800, 600); 
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget2D);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if (texture2D != null)
                ScreenManager.Instance.SpriteBatch.Draw(texture2D, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.DrawString(spriteFont, text, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.End();

            texture2D = renderTarget2D;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            setEffect<FadeEffect>(ref fadeEffect);

            if (effects != String.Empty)
            {
                string[] split = effects.Split(':');
                foreach (string item in split)
                    ActivateEffect(item);
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
                DesactivateEffect(effect.Key);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                if(effect.Value.isActivate)
                    effect.Value.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(rectancgle.Width / 2, rectancgle.Height / 2);
            spriteBatch.Draw(
                texture2D, position + origin, rectancgle,
                Color.White * alpha, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
        }
    }
}
