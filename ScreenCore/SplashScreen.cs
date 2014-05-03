using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Andy.ScreenCore
{
    public class SplashScreen : GameScreen
    {

        public Image Image;

        public SplashScreen()
        {
            this.Image = new Image();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
            Image.fadeEffect.fadeSpeed = 0.5f;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Enter)
                || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                ScreenManager.Instance.ChangeScreens("TitleScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
