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
        public event EventHandler OnMenuChange;

        public string axis;
        public string effects;
        [XmlElement("item")]
        public List<MenuItem> items;
        int itemNumber;
        string id;
        public static bool statRunGame = false;

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnMenuChange(this, null);
            }
        }

        public DeathScreen()
        {
            id = effects = String.Empty;
            itemNumber = 0;
            axis = "Y";
            items = new List<MenuItem>();
            Console.WriteLine("Je suis làà");

        }

        void AlignMenuItems()
        {
            Vector2 dimensions = Vector2.Zero;
            foreach (MenuItem item in items)
                dimensions += new Vector2(item.image.rectancgle.Width, 
                    item.image.rectancgle.Height);

            dimensions = new Vector2((ScreenManager.Instance.Dimensions.X - dimensions.X) / 2, 
                (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2);

            foreach (MenuItem item in items)
            {
                if (axis == "X")
                    item.image.position = new Vector2(dimensions.X,
                        (ScreenManager.Instance.Dimensions.Y - item.image.rectancgle.Height) / 2);
                else if (axis == "Y")
                    item.image.position = new Vector2(
                        (ScreenManager.Instance.Dimensions.X - item.image.rectancgle.Width) / 2, dimensions.Y)  ;

                dimensions += new Vector2(item.image.rectancgle.Width, item.image.rectancgle.Height);
            }
        }

        public override void LoadContent()
        {
            string[] split = effects.Split(':');
            foreach(MenuItem item in items)
            {
                item.image.LoadContent();
                foreach (string s in split)
                {
                    item.image.ActivateEffect(s);
                }
            }
            AlignMenuItems();
        }

        public override void UnloadContent()
        {
            foreach (MenuItem item in items)
            {
                item.image.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("uppp");
            if (axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Right))
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Left))
                    itemNumber--;
            }
            else if (axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Up))
                    itemNumber--;
            }

            if (itemNumber < 0)
                itemNumber = 0;
            else if (itemNumber > items.Count - 1)
                itemNumber = items.Count - 1;

            for (int i = 0; i < items.Count; i++)
            {
                if (i == itemNumber)
                    items[i].image.isActivate = true;
                else
                    items[i].image.isActivate = false;

                items[i].image.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuItem item in items)
                item.image.Draw(spriteBatch);
        }

        public void conditionMenu()
        {
            if (itemNumber == 0 && InputManager.Instance.KeyPressed(Keys.Enter))
            {
                if (InputManager.Instance.KeyPressed(Keys.Space, Keys.Enter))
                    ScreenManager.Instance.ChangeScreens("LancerJeu");
            }
            else if (itemNumber == 1 && InputManager.Instance.KeyPressed(Keys.Enter))
            {
                if (InputManager.Instance.KeyPressed(Keys.Space, Keys.Enter))
                    ScreenManager.Instance.ChangeScreens("MenuPrincipal");
            }
        }
    }
}
