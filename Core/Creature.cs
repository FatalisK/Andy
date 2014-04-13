using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Andy.Core
{
    public class Creature :GameObjects
    {
        protected World _world;
        protected float _mass;
        protected float _saut;
        public bool collisionEnAir;
        protected int _collisions;
        public float Poids;
        public float Accel;
        protected bool taper;
        protected int _pvTotal;
        protected int _pvActuel;
        protected Sprite _s_vie;
        protected bool _estMort;



        protected Vector2 _reculArme;//X=Le temps du recul Y=La valeur dont on recul à chaque update
        protected Vector2 _degatRecul;//X= nb de fois ou l'on recule, Y=valeur dont on recule
        protected Vector4 posArmR;
        protected Vector4 posArmL;

        protected bool _invincible = false;
        protected int _temps_invincible;
        protected int _compteurTempsInv;


        protected Vector2 _vitesse;
        public Creature(Sprite s,  World  world, int pvTotal)
            :base(s)
        {
            _world = world;
            regard = Direction.RIGHT;
            taper = false;
            _pvTotal = pvTotal;
            _pvActuel = _pvTotal;
            _degatRecul.X = 0;
            _invincible = false;
            _temps_invincible = 25;//ms
            _compteurTempsInv = 0;
            _mass = 0.1f;


        }

        public override Vector4 getPosArmeR()
        {
            return posArmR;
        }
        public override Vector4 getPosArmeL()
        {
            return posArmL;
        }
        public int getPvActuel(){
            return _pvActuel;
        }
        public int getPvTotal()
        {
            return _pvTotal;
        }

       public override void setDegatRecul(Vector2 s){
           _degatRecul = s;
        }

        public override Vector2 getReculArme(){
            return _reculArme;
        }
        public void setPvActuel(int v)
        {
            _pvActuel = v;
        }
        public Sprite getSpriteVie()
        {
            return _s_vie;
        }

        public void setSpriteVie(Sprite s)
        {
            _s_vie = s;
        }
        public virtual float getVitesseX()
        {
            return _vitesse.X;
        }

        public virtual float getVitesseY()
        {
            return _vitesse.Y;
        }

        public World getWorld()
        {
            return _world;
        }

        public void setCollisions(int c)
        {
            _collisions = c;
        }
        public float getSaut() { return _saut; }//Sale peut mieux faire
        public float getMasse() { return _mass; }//idem

        public virtual void setVeutSauter(bool b) { }//
        public virtual bool getVeutSauter()
        {
            return false;
        }


        public virtual float getHauteurSaut()
        {
            return 1;
        }

        public override void colllision(GameObjects p, List<Vector2> inter)
        {
            int maxX = 0;
            int minX = 1000;
            int maxY = 1000;//Y inversés
            int minY = 0;
            int margeErX = 4;
            int margeErY = 4;


            int[] tabCollision = new int[4];//0 gauche 1 droite 2 haut 3 bas

            //if (a.typeobjet == GameObjects.TypeObjet.PERS)//Collision avec une personne
            //{

            //Objectif repousser le personnage de la zone
            int i;
            p.sprite.initCoulour();
/*
           for (i = 0; i < p.sprite.pixelColor.Count(); i++)
            {
                if (p.sprite.pixelColor[i].A != 0) { p.sprite.pixelColor[i] = Color.White; }

            }
  */          
           for (i = 0; i < inter.Count; i++)
            {

                
                if (inter[i].X > maxX) { maxX = i; }
                if (inter[i].X < minX) { minX = i; }
                if (inter[i].Y > minY) { minY = i; } // y orienté vers le bas
                if (inter[i].Y < maxY) { maxY = i; }
                

                if (inter[i].Y >= p.sprite.Bbox.Top - margeErY && inter[i].Y <= p.sprite.Bbox.Top + margeErY)
                {
                    tabCollision[2]++;
                }
                //BOT
                if (inter[i].Y >= p.sprite.Bbox.Bottom - margeErY && inter[i].Y <= p.sprite.Bbox.Bottom + margeErY && getVeutSauter())
                {
                    tabCollision[3]++;
                }

                if (inter[i].X >= p.sprite.Bbox.Left - margeErX && inter[i].X <= p.sprite.Bbox.Left + margeErX)
                {//Coté gauche
                    tabCollision[0]++;
                }
                //Console.WriteLine("X" + inter[i].X + "BR" + sprite.Bbox.Right);
                if (inter[i].X >= p.sprite.Bbox.Right - margeErX && inter[i].X <= p.sprite.Bbox.Right + margeErX)
                {//Coté droit
                    tabCollision[1]++;
                }




                //Console.WriteLine(inter[i]);
         /*       p.sprite.pixelColor[(int)((inter[i].X - p.sprite.Bbox.Left) +
                                     (inter[i].Y - p.sprite.Bbox.Top) * p.sprite.Bbox.Width)] = Color.Black;
           */    
            }
            int max = 0;
            int val = 0;
            for (i = 0; i < 4; i++)
            {
                if (tabCollision[i] > val)
                {
                    val = tabCollision[i];
                    max = i;

                }
                //Console.WriteLine(max); 
            }
            //Console.WriteLine("G" + tabCollision[0] + "D" + tabCollision[1] + "T" + tabCollision[2] + "B" + tabCollision[3] + "Max" + max);
            p.sprite.getTexture().SetData(p.sprite.pixelColor);

            if (max == 1)
            {
                //Console.WriteLine("minX" + inter[maxX].X + "R" + sprite.Bbox.Left + "T" + getTypeObjet());

                sprite.location.X = sprite.location.X + getVitesseX();

                
                    if (inter[minX].X < p.sprite.Bbox.Right)//Il est dedans la boite il faut l'ejecter !
                    {
                        sprite.location.X = sprite.location.X + (p.sprite.Bbox.Right - inter[minX].X);
                    }
                
            }

            //Se cogne à gauche

            if (max == 0)
            {
                //Console.WriteLine("assss");
                //Console.WriteLine("minX" + inter[maxX].X + "R" + sprite.Bbox.Left + "T" + getTypeObjet());
                sprite.location.X = sprite.location.X - getVitesseX();
                if (inter[maxX].X > p.sprite.Bbox.Left && inter[maxX].X < p.sprite.Bbox.Left+p.sprite.Bbox.Width/2)//Il est dedans la boite il faut l'ejecter !
                {
                  sprite.location.X = sprite.location.X - (inter[maxX].X - p.sprite.Bbox.Left);

                }
            }

            //se cogne en haut
            if (max == 2)
            {

                //Console.WriteLine("minX" + inter[maxX].X + "R" + sprite.Bbox.Left + "T" + getTypeObjet());
                sprite.location.Y = sprite.location.Y - Poids; 
                this.collisionEnAir= true;
                //Console.WriteLine("minX" + inter[maxX].X + "R" + sprite.Bbox.Left + "T" + getTypeObjet()+"Collisin en Air"+collisionEnAir);

                if (inter[minY].Y > p.sprite.Bbox.Top)//Il est dedans la boite il faut l'ejecter !
                {
                    sprite.location.Y = sprite.location.Y - (inter[minY].Y - p.sprite.Bbox.Top);
                }

                //TODO QUAND LA PLATOFORME BOUGE RESTER DESSUS
                if (p.getTypeObjet() == GameObjects.TypeObjet.PLAT)
                {

                    if (_direction == GameObjects.Direction.PASS || _direction == GameObjects.Direction.BOT)
                    {
                        if (p.getDirection() == Direction.RIGHT) { sprite.location.X = sprite.location.X + p.getPVitesseX();}
                        if (p.getDirection() == Direction.LEFT) { sprite.location.X = sprite.location.X - p.getPVitesseX(); }

                    }
                    if (p.getDirection() == Direction.TOP) { sprite.location.Y = sprite.location.Y - p.getPVitesseY(); }
                    if (p.getDirection() == Direction.BOT) { sprite.location.Y = sprite.location.Y + p.getPVitesseY(); }

                }
            }

            //se cogne en bas
            if (max == 3)
            {
                //Console.WriteLine("Mass"+a.getMasse()+"P"+Poids+"M"+(0.5f * Accel + a.getVitesseY()));
                setVeutSauter(false);

            }


            //}
        }

        public override void prendreDegat(float a, float b) {
            _degatRecul= new Vector2 (a,b);
            if (_invincible == false)
            {
                _pvActuel--;
                if (_pvActuel == 0)
                {
                    Console.WriteLine("Morrrt");

                }
                _invincible = true;
            }
            //Console.WriteLine("Pv" + _pvActuel);
        
        }

        public void UpdateFrame(GameTime gameTime)
        {
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (typeobjet == GameObjects.TypeObjet.PERS)
            {
                if (_direction == Direction.TAPER)
                {
                    if (regard == Direction.RIGHT)
                    {
                        sprite.frameIndex.X = 0;
                        sprite.frameIndex.Y = 2;
                    }
                    else
                    {
                        sprite.frameIndex.X = 1;
                        sprite.frameIndex.Y = 2;
                    }
                    _time = 0f;
                }

                if (_direction == Direction.PASS)
                {
                    if (regard == Direction.RIGHT)
                    {
                        sprite.frameIndex.X = 0;
                        sprite.frameIndex.Y = 0;
                    }
                    else
                    {
                        sprite.frameIndex.X = 0;
                        sprite.frameIndex.Y = 1;
                    }
                    _time = 0f;
                }
                else
                {
                    if (_direction == Direction.BOT)
                    {
                        if (regard == Direction.RIGHT)
                        {
                            sprite.frameIndex.X = 0;
                            sprite.frameIndex.Y = 3;
                        }
                        else
                        {
                            sprite.frameIndex.X = 1;
                            sprite.frameIndex.Y = 3;
                        }
                        _time = 0f;
                    }
                    else
                    {
                        if (_direction == Direction.TOP)
                        {
                            if (regard == Direction.RIGHT)
                            {
                                sprite.frameIndex.X = 2;
                                sprite.frameIndex.Y = 0;
                            }
                            else
                            {
                                sprite.frameIndex.X = 2;
                                sprite.frameIndex.Y = 1;
                            }
                            _time = 0f;
                        }


                    }



                }
            }

            if (typeobjet == TypeObjet.ENN)
            {
                //Console.WriteLine("babouin"+sprite.frameIndex.X+"//"+sprite.frameIndex.Y+"//"+_direction);
                switch (_direction)
                {

                    case Direction.LEFT:
                        if (sprite.frameIndex.Y != 1) { sprite.frameIndex.X = 0; }
                        sprite.frameIndex.Y = 1;

                        break;

                    case Direction.RIGHT:
                        if (sprite.frameIndex.Y != 0) { sprite.frameIndex.X = 0; }

                        sprite.frameIndex.Y = 0;
                        break;




                }


            }


            while (_time > sprite.getFrameTime())
            {
                sprite.frameIndex.X++;
                _time = 0f;
            }
            if (sprite.frameIndex.X > sprite.getTotalFrame())
                sprite.frameIndex.X = 0;

            _Source = new Rectangle(
                (int)(sprite.frameIndex.X * sprite.getFrameWidth()),
               (int)(sprite.frameIndex.Y * sprite.getFrameHeight()),
                sprite.getFrameWidth(),
                sprite.getFrameHeight());



        }

        public void setMort(bool b)
        {
            _estMort = b;
        }
        public bool inTheAir()
        {

            if (sprite.location.Y + sprite.getFrameHeight() > MenuPrincipal.WINDOW_HEIGHT)//Desactive le saut si on tombe en dessous de l'cran
            {
                return true;
            } 

            return (sprite.location.Y + sprite.getFrameHeight() < MenuPrincipal.WINDOW_HEIGHT);
        }

  

        public void updateCrea()
        {
            //Console.WriteLine(_compteurTempsInv);
            if (_invincible == true)
            {
                _compteurTempsInv++;
                if (_compteurTempsInv == _temps_invincible)
                {
                    _invincible = false;
                    _compteurTempsInv = 0;
                }
            }
        }

    }
}
