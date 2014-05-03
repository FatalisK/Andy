using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Andy.ScreenCore.Menu;

namespace Andy.ScreenCore
{
    public class TitleScreen : GameScreen
    {
        MenuManager menuManager;
        public Image image;

        public TitleScreen()
        {
            menuManager = new MenuManager();
            this.image = new Image();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            image.LoadContent();
            menuManager.LoadContent("Content/XMLFiles/TitleMenu.xml");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            image.UnloadContent();
            menuManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            image.Update(gameTime);
            menuManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            image.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
        }
    }
}
