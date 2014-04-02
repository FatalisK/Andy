using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Andy.Core
{
    public class World : GameObjects
    {
        public Color[,] colorTab;
        private Color _collisionColor;
        private List<GameObjects> _listeMonde;



        public Color collisionColor
        {
            get { return _collisionColor; }
        }

        public World(Sprite s,Color collisionColor):base(s)
        {
            _collisionColor = collisionColor;
            _listeMonde = new List<GameObjects>();
            colorTab = new Color[800, 600];
            int i,j;
            for (i=0;i<800;i++){
                for (j = 0; j < 600; j++)
                {
                    colorTab[i,j] = Color.White;
                }
            }

        }

        public void ajouterElem(GameObjects e)
        {
            _listeMonde.Add(e);
            int i,j;

            for (i = (int)e.sprite.location.X; i < (int)e.sprite.location.X + e.sprite.getFrameWidth(); i++)
            {

                for (j = (int)e.sprite.location.Y; j < (int)e.sprite.location.Y + e.sprite.getFrameHeight(); j++)
                {
                    colorTab[i, j] = collisionColor;
                }

            }
        }

        public int tailleListe()
        {
            return _listeMonde.Count();
        }

        public GameObjects getElem(int i)
        {
            return _listeMonde[i];
        }
    }
}
