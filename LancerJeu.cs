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
using Andy.ScreenCore;

namespace Andy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class LancerJeu : GameScreen
    {
        ParticleEngine particleEngine;
        GraphicsDevice graphics;
        SpriteBatch spriteBatch;

        Decor ciel;
        Decor arr_montagne;
        Decor petiteM;
        Decor gaucheM;
        Decor droiteM;
        Decor nuage1;
        Decor nuage2;
        Decor nuage3;

        World world;
        Player player;
        Plateforme p1, p2, p3;
        Ennemies bf;




        public LancerJeu()
        {
            this.spriteBatch = ScreenManager.Instance.SpriteBatch;
            this.graphics = ScreenManager.Instance.GraphicsDevice;
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            Initialize();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization logic here
            Sprite s_ciel = new Sprite(new Vector2(0, 0), 800, 600);
            Sprite s_arr_montagne = new Sprite(new Vector2(0, MenuPrincipal.WINDOW_HEIGHT - 397), 800, 397);
            Sprite s_petiteM = new Sprite(new Vector2(139, 295), 391, 294);
            Sprite s_gaucheM = new Sprite(new Vector2(0, ScreenManager.Instance.Dimensions.Y - 359), 662, 359);
            Sprite s_droiteM = new Sprite(new Vector2(ScreenManager.Instance.Dimensions.X - 472, ScreenManager.Instance.Dimensions.Y - 385), 472, 385);
            Sprite s_nuage1 = new Sprite(new Vector2(0, 245), 94, 69);
            Sprite s_nuage2 = new Sprite(new Vector2(160, 17), 241, 89);
            Sprite s_nuage3 = new Sprite(new Vector2(300, 200), 111, 78);



            Sprite s_world = new Sprite(new Vector2(0, 0), 800, 600);
            Sprite s_andy = new Sprite(new Vector2(0, 0), 9, 60, 50, 0.1f, new Vector2(0, 0));
            Sprite s_plat1 = new Sprite(new Vector2(MenuPrincipal.WINDOW_WIDTH - 220, MenuPrincipal.WINDOW_HEIGHT - 62), 220, 62);
            Sprite s_plat2 = new Sprite(new Vector2(0, MenuPrincipal.WINDOW_HEIGHT - 168), 171, 168);
            Sprite s_plat3 = new Sprite(new Vector2(224, 490), 150, 39);
            Sprite s_bf = new Sprite(new Vector2(592, 490), 30, 30);
            Sprite s_vie = new Sprite(new Vector2(20, 20), 20, 20);


            //MONDE
            s_ciel.setTexture(content.Load<Texture2D>("Ciel"));
            s_arr_montagne.setTexture(content.Load<Texture2D>("arr_montagne"));
            s_petiteM.setTexture(content.Load<Texture2D>("petiteM"));
            s_nuage1.setTexture(content.Load<Texture2D>("nuage1"));
            s_nuage2.setTexture(content.Load<Texture2D>("nuage2"));
            s_nuage3.setTexture(content.Load<Texture2D>("nuage3"));
            s_gaucheM.setTexture(content.Load<Texture2D>("gaucheM"));
            s_droiteM.setTexture(content.Load<Texture2D>("droiteM"));
            s_vie.setTexture(content.Load<Texture2D>("vie"));



            s_world.setTexture(content.Load<Texture2D>("world"));


            s_andy.setTexture(content.Load<Texture2D>("SuperSprite"));
            s_plat1.setTexture(content.Load<Texture2D>("Platforme2"));
            s_plat2.setTexture(content.Load<Texture2D>("Platforme1"));
            s_plat3.setTexture(content.Load<Texture2D>("Platforme3"));

            s_bf.setTexture(content.Load<Texture2D>("bf"));

            s_andy.initCoulour();
            s_plat1.initCoulour();
            s_plat2.initCoulour();
            s_plat3.initCoulour();
            s_bf.initCoulour();

            ciel = new Decor(s_ciel);
            arr_montagne = new Decor(s_arr_montagne);
            petiteM = new Decor(s_petiteM);
            gaucheM = new Decor(s_gaucheM);
            nuage1 = new Decor(s_nuage1, 0.1f, 0);
            nuage2 = new Decor(s_nuage2, 0.05f, 0);
            nuage3 = new Decor(s_nuage3, 0.15f, 0);

            droiteM = new Decor(s_droiteM);

            world = new World(s_world);

            p1 = new Plateforme(s_plat1, new Vector2(MenuPrincipal.WINDOW_WIDTH - 220, MenuPrincipal.WINDOW_HEIGHT - 62));
            p2 = new Plateforme(s_plat2, new Vector2(0, MenuPrincipal.WINDOW_HEIGHT - 148));
            p3 = new Plateforme(s_plat3, new Vector2(224, 490), 0.5f);

            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("particle2"));
            particleEngine = new ParticleEngine(textures, new Vector2(0, 0));

            player = new Player(s_andy, world, particleEngine, s_vie);
            bf = new Ennemies(s_bf, world, new Vector2(592, 490));



            world.addListPlat(p1);
            world.addListPlat(p2);
            world.addListPlat(p3);
            world.addListCreat(bf);
            world.setPlayer(player);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Console.WriteLine("Py" + (world.getPlayer().sprite.location.Y +"Height"+ world.getPlayer().sprite.getFrameHeight()));

            //Collision.Collided(player,-1, player.getWorld());

            player.UpdateFrame(gameTime);

            //Console.WriteLine("+++" + player.collisionEnAir);

            world.Physique();
            //Collision.Collided(world.getPlayer(), -1, world.getPlayer().getWorld());

            nuage2.bouger();
            nuage1.bouger();
            nuage3.bouger();
            p3.se_deplacer(150);
            bf.action();

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            ciel.Draw(spriteBatch);
            arr_montagne.Draw(spriteBatch);
            petiteM.Draw(spriteBatch);
            nuage2.Draw(spriteBatch);
            nuage3.Draw(spriteBatch);
            gaucheM.Draw(spriteBatch);
            droiteM.Draw(spriteBatch);
            nuage1.Draw(spriteBatch);
            particleEngine.Draw(spriteBatch);

            player.DrawPlayer(spriteBatch);
            p1.Draw(spriteBatch);
            p2.Draw(spriteBatch);
            p3.Draw(spriteBatch);
            bf.Draw(spriteBatch);

            for (int i = 0; i < player.getPvActuel(); i++)
            {
                player.getSpriteVie().location.X = 20 + i * 40;

                spriteBatch.Draw(player.getSpriteVie().getTexture(), player.getSpriteVie().location, Color.White);
            }
        }
    }
}

