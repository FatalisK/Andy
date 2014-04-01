#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

#endregion
using Andy.Core;
using System.IO;
namespace Andy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Andy : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public const int WINDOW_WIDTH = 800;
        public const int WINDOW_HEIGHT = 600;
        World world;
        Player player;
        Plateforme p1;




        public Andy()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth=WINDOW_WIDTH;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Sprite s_world = new Sprite(new Vector2(0, 0),800,600);
            Sprite s_andy = new Sprite( new Vector2(0, 400),9,70,110,0.1f,new Vector2(0,0));
            Sprite s_plat = new Sprite(new Vector2(100, 500), 100, 25);

            s_world.setTexture(Content.Load<Texture2D>("world"));
            s_andy.setTexture(Content.Load<Texture2D>("SuperSprite"));
            s_plat.setTexture(Content.Load<Texture2D>("platforme"));


            string text;

            StreamWriter sw = new StreamWriter("./texte.txt");//création du fichier 

            text = "test";
            sw.WriteLine("{0}", text);//enregistrement du message dans le fichier 
            sw.Close();



            world = new World(s_world,Color.Red);
            player = new Player(s_andy,world);
            p1 = new Plateforme(s_plat);

            int i, j;
      

            world.ajouterElem(p1);

        

            base.Initialize();
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            player.UpdateFrame(gameTime);
            player.Move(Keyboard.GetState());
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here





            spriteBatch.Begin();
            world.Draw(spriteBatch);
            player.DrawAnimation(spriteBatch);
            p1.Draw(spriteBatch);
     
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
