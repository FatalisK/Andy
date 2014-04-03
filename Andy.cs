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
        Plateforme p1,p2;




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
            Sprite s_andy = new Sprite( new Vector2(300, 530),9,70,110,0.1f,new Vector2(0,0));
            Sprite s_plat = new Sprite(new Vector2(200, 500), 100, 25);
            Sprite s_plat1 = new Sprite(new Vector2(600, 575), 100, 25);


            s_world.setTexture(Content.Load<Texture2D>("world"));
            s_andy.setTexture(Content.Load<Texture2D>("SuperSprite"));
            s_plat.setTexture(Content.Load<Texture2D>("platforme"));
            s_plat1.setTexture(Content.Load<Texture2D>("platforme"));

            s_andy.initCoulour();
            s_plat.initCoulour();
            s_plat1.initCoulour();

            world = new World(s_world);
            p1 = new Plateforme(s_plat);
            p2 = new Plateforme(s_plat1);
            player = new Player(s_andy, world);


      

            world.ajouterElem(p1);
            world.ajouterElem(p2);

        

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
            Collision.Collided(player, player.getWorld());
            player.UpdateFrame(gameTime);
            player.Move(Keyboard.GetState());
            player.Physique(player);
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
            p2.Draw(spriteBatch);
     
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
