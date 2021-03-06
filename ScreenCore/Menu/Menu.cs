﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Andy.ScreenCore.Menu
{
    public class Menu
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

        public Menu()
        {
            id = effects = String.Empty;
            itemNumber = 0;
            axis = "Y";
            items = new List<MenuItem>();
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
                        (ScreenManager.Instance.Dimensions.X - item.image.rectancgle.Width) / 2, dimensions.Y);

                dimensions += new Vector2(item.image.rectancgle.Width, item.image.rectancgle.Height);
            }
        }

        public void LoadContent()
        {
            string[] split = effects.Split(':');
            foreach (MenuItem item in items)
            {
                item.image.LoadContent();
                foreach (string s in split)
                {
                    item.image.ActivateEffect(s);
                }
            }
            AlignMenuItems();
        }

        public void UnloadContent()
        {
            foreach (MenuItem item in items)
            {
                item.image.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {

            if (itemNumber == 0 && InputManager.Instance.KeyPressed(Keys.Enter)
                || itemNumber == 0 && GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                || itemNumber == 0 && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                if (InputManager.Instance.KeyPressed(Keys.Enter)
                || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                    ScreenManager.Instance.ChangeScreens("LancerJeu");
            }
            if (itemNumber == 1 && InputManager.Instance.KeyPressed(Keys.Enter)
                || itemNumber == 1 && GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                //A finir
            }
            if (itemNumber == 2 && InputManager.Instance.KeyPressed(Keys.Enter)
                || itemNumber == 2 && GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                //A finir
            }
            if (itemNumber == 3 && InputManager.Instance.KeyPressed(Keys.Enter)
                || itemNumber == 3 && GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                || itemNumber == 3 && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                MenuPrincipal.stateQuit = true;
            }
            if (axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Right)
                || GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Left)
                || GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                    itemNumber--;
            }
            else if (axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down)
                || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Up)
                || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuItem item in items)
                item.image.Draw(spriteBatch);
        }
    }
}
