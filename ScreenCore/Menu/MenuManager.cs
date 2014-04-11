using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Andy.ScreenCore.Menu
{
    public class MenuManager
    {

        Menu menu;

        public MenuManager()
        {
            menu = new Menu();
            menu.OnMenuChange += menuOnMenuChange;
        }

        void menuOnMenuChange(object sender, EventArgs e)
        {
            XMLManager<Menu> xmlMenuManager = new XMLManager<Menu>();
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
