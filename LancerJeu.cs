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
        Plateforme p1, p2, p3, p4, p5, p6, p7;
        Ennemies pretre;




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


            Sprite s_andy = new Sprite(new Vector2(0, 400), 9, 60, 50, 0.1f, new Vector2(0, 0));
            Sprite s_pretre = new Sprite(new Vector2(592, 477), 9, 60, 60, 0.1f, new Vector2(0, 0));


            Sprite s_plat1 = new Sprite(new Vector2(MenuPrincipal.WINDOW_WIDTH - 220, MenuPrincipal.WINDOW_HEIGHT - 62), 220, 62);
            Sprite s_plat2 = new Sprite(new Vector2(0, MenuPrincipal.WINDOW_HEIGHT - 168), 171, 168);
            Sprite s_plat3 = new Sprite(new Vector2(224, 490), 150, 39);
            Sprite s_plat4 = new Sprite(new Vector2(460, 350), 122, 106);
            Sprite s_plat5 = new Sprite(new Vector2(610, 230), 150, 39);
            Sprite s_plat6 = new Sprite(new Vector2(150, 200), 150, 39);
            Sprite s_plat7 = new Sprite(new Vector2(10, 160), 123, 100);


            Sprite s_vie = new Sprite(new Vector2(20, 20), 20, 20);


            //MONDE
            s_ciel.setTexture(content.Load<Texture2D>("Decor/Ciel"));
            s_arr_montagne.setTexture(content.Load<Texture2D>("Decor/arr_montagne"));
            s_petiteM.setTexture(content.Load<Texture2D>("Decor/petiteM"));
            s_nuage1.setTexture(content.Load<Texture2D>("Decor/nuage1"));
            s_nuage2.setTexture(content.Load<Texture2D>("Decor/nuage2"));
            s_nuage3.setTexture(content.Load<Texture2D>("Decor/nuage3"));
            s_gaucheM.setTexture(content.Load<Texture2D>("Decor/gaucheM"));
            s_droiteM.setTexture(content.Load<Texture2D>("Decor/droiteM"));
            s_vie.setTexture(content.Load<Texture2D>("vie"));



            s_world.setTexture(content.Load<Texture2D>("Decor/world"));


            s_andy.setTexture(content.Load<Texture2D>("Sprite/SuperSprite"));

            s_plat1.setTexture(content.Load<Texture2D>("Platforme/Platforme2"));
            s_plat2.setTexture(content.Load<Texture2D>("Platforme/Platforme1"));
            s_plat3.setTexture(content.Load<Texture2D>("Platforme/Platforme3"));
            s_plat4.setTexture(content.Load<Texture2D>("Platforme/Platforme4"));
            s_plat5.setTexture(content.Load<Texture2D>("Platforme/Platforme5"));
            s_plat6.setTexture(content.Load<Texture2D>("Platforme/Platforme6"));
            s_plat7.setTexture(content.Load<Texture2D>("Platforme/Platforme7"));

            s_pretre.setTexture(content.Load<Texture2D>("Sprite/pretreSprite"));

            s_andy.initCoulour();

            s_plat1.initCoulour();
            s_plat2.initCoulour();
            s_plat3.initCoulour();
            s_plat4.initCoulour();
            s_plat5.initCoulour();
            s_plat6.initCoulour();
            s_plat7.initCoulour();

            s_pretre.initCoulour();

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
            p3 = new Plateforme(s_plat3, new Vector2(224, 490), 0.5f,0);
            p4 = new Plateforme(s_plat4, new Vector2(460,350),0,0.5f);
            p5 = new Plateforme(s_plat5, new Vector2(610, 230));
            p6 = new Plateforme(s_plat6, new Vector2(150, 165), 1f,0);
            p7 = new Plateforme(s_plat7, new Vector2(10, 160), 0.5f,0);




            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("particle2"));
            particleEngine = new ParticleEngine(textures, new Vector2(0, 0));

            player = new Player(s_andy, world, particleEngine, s_vie);
            pretre = new Ennemies(s_pretre, world, new Vector2(592, 477));



            world.addListPlat(p1);
            world.addListPlat(p2);
            world.addListPlat(p3);
            world.addListPlat(p4);
            world.addListPlat(p5);
            world.addListPlat(p6);
            world.addListPlat(p7);

            world.addListCreat(pretre);
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
            pretre.UpdateFrame(gameTime);

            //Console.WriteLine("+++" + player.collisionEnAir);

            world.leMondeEve();
            //Collision.Collided(world.getPlayer(), -1, world.getPlayer().getWorld());

            nuage2.bouger();
            nuage1.bouger();
            nuage3.bouger();
            p3.se_deplacerX(70);
            p4.se_deplacerY(125);
            p6.se_deplacerX(230);
            pretre.action();

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

            player.DrawPlayer(spriteBatch);
            p1.Draw(spriteBatch);
            p2.Draw(spriteBatch);
            p3.Draw(spriteBatch);
            p4.Draw(spriteBatch);
            p5.Draw(spriteBatch);
            p6.Draw(spriteBatch);
            p7.Draw(spriteBatch);

            pretre.DrawEnnemie(spriteBatch);

            for (int i = 0; i < player.getPvActuel(); i++)
            {
                player.getSpriteVie().location.X = 20 + i * 40;

                spriteBatch.Draw(player.getSpriteVie().getTexture(), player.getSpriteVie().location, Color.White);
            }
        }
    }
}

