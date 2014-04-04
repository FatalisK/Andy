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
        Ennemies bf;
        



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
            Sprite s_andy = new Sprite( new Vector2(0, 530),9,70,110,0.1f,new Vector2(0,0));
            Sprite s_plat = new Sprite(new Vector2(100, 580), 100, 25);
            Sprite s_plat1 = new Sprite(new Vector2(500, 580), 100, 25);
            Sprite s_bf = new Sprite(new Vector2 (300,100),119,121);

            s_world.setTexture(Content.Load<Texture2D>("world"));
            s_andy.setTexture(Content.Load<Texture2D>("SuperSprite"));
            s_plat.setTexture(Content.Load<Texture2D>("platforme"));
            s_plat1.setTexture(Content.Load<Texture2D>("platforme"));
            s_bf.setTexture(Content.Load<Texture2D>("bf"));

            s_andy.initCoulour();
            s_plat.initCoulour();
            s_plat1.initCoulour();
            s_bf.initCoulour();

            world = new World(s_world);
            p1 = new Plateforme(s_plat,new Vector2(100, 580));
            p2 = new Plateforme(s_plat1,new Vector2(500, 580));
            player = new Player(s_andy, world);
            bf=new Ennemies(s_bf, world,new Vector2(300,100));

            Console.WriteLine("PW" + player.getWorld().getGravity());
            Console.WriteLine("PE" + bf.getWorld().getGravity());
            Console.WriteLine("W" + world.getGravity());

            world.addListPlat(p1);
            world.addListPlat(p2);
            world.addListCreat(bf);
            world.setPlayer(player);
            Console.WriteLine("PW" + player.getWorld().getGravity());
            Console.WriteLine("PE" + bf.getWorld().getGravity());
            Console.WriteLine("W" + world.getGravity());


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
            bf.setCollisions(Collision.Collided(bf, bf.getWorld()));

            player.UpdateFrame(gameTime);
            player.Move(Keyboard.GetState());

            world.Physique();
            bf.action();
            p1.se_deplacer(250, 10);
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
            bf.Draw(spriteBatch);
     
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
