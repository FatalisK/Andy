using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Andy.ScreenCore;
using Microsoft.Xna.Framework.Input;
using Andy.ScreenCore.Menu;
using System.Xml.Serialization;

namespace Andy.ScreenCore
{
    public class DeathScreen : GameScreen
    {
        DeathMenuManager menuManager;

        public DeathScreen()
        {
            menuManager = new DeathMenuManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            menuManager.LoadContent("Content/XMLFiles/DeathScreen.xml");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menuManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            menuManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
        }
    }
}
