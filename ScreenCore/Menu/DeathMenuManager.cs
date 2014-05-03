﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Andy.ScreenCore.Menu
{
    public class DeathMenuManager
    {

        DeathMenu menu;

        public DeathMenuManager()
        {
            menu = new DeathMenu();
            menu.OnMenuChange += menuOnMenuChange;
        }

        void menuOnMenuChange(object sender, EventArgs e)
        {
            XMLManager<DeathMenu> xmlMenuManager = new XMLManager<DeathMenu>();
            menu.UnloadContent();
            menu = xmlMenuManager.Load(menu.ID);
            menu.LoadContent();
        }

        public void LoadContent(string menuPath)
        {
            if (menuPath != String.Empty)
                menu.ID = menuPath;
        }

        public void UnloadContent()
        {
            menu.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
