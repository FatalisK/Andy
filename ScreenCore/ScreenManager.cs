using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Andy.ScreenCore
{
    public class ScreenManager
    {

        private static ScreenManager instance;
        [XmlIgnore]
        public Vector2 Dimensions { private set; get; }
        [XmlIgnore]
        public ContentManager Content { private set; get; }
        XMLManager<GameScreen> xmlGameScreenManager;

        GameScreen currentScreen, newScreen;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;

        public Image image;
        [XmlIgnore]
        public bool isTransition { get; private set; }


        public ScreenManager()
        {
            Dimensions = new Vector2(800, 600);
            currentScreen = new SplashScreen();
            xmlGameScreenManager = new XMLManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.Type;
            currentScreen = xmlGameScreenManager.Load("Content/XMLFiles/SplashScreen.xml");
            image = new Image();
        }

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    XMLManager<ScreenManager> xml = new XMLManager<ScreenManager>();
                    instance = xml.Load("Content/XMLFiles/ScreenManager.xml");
                }
                return instance;
            }
        }

        public void ChangeScreens(string screenName)
        {
            if (screenName == "LancerJeu")
                newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("Andy." + screenName));
            else
                newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("Andy.ScreenCore." + screenName));
            image.isActivate = true;
            image.fadeEffect.increase = true;
            image.alpha = 0.0f;
            isTransition = true;
        }

        void Transition(GameTime gameTime)
        {
            if (isTransition)
            {
                image.Update(gameTime);
                if (image.alpha == 1.0f)
                {
                    currentScreen.UnloadContent();
                    currentScreen = newScreen;
                    xmlGameScreenManager.Type = currentScreen.Type;
                    if (File.Exists(currentScreen.xmlPath))
                        currentScreen = xmlGameScreenManager.Load(currentScreen.xmlPath);
                    currentScreen.LoadContent();
                }
                else if (image.alpha == 0.0f)
                {
                    image.isActivate = false;
                    isTransition = false;
                }

            }
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
            image.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
            image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (isTransition)
            {
                image.Draw(spriteBatch);
            }
        }
    }
}
