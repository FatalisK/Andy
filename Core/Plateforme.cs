using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Andy.Core
{
    public class Plateforme : World
    {
        private Vector2 _positionBase; 
        public bool collision;
        public Plateforme(Sprite s, Vector2 positionBase ):base(s) {

            collision = false;
            typeobjet = TypeObjet.PLAT;
            _positionBase=positionBase;
        }

        public void se_deplacer(int x, int y)
        {
            if (sprite.location.X > _positionBase.X + x) { _direction = Direction.LEFT; }
            if (sprite.location.X <= _positionBase.X) { _direction = Direction.RIGHT; }

            if (_direction == Direction.RIGHT) { sprite.location.X = sprite.location.X + 2; }
            if (_direction == Direction.LEFT) { sprite.location.X = sprite.location.X - 2; }





        }

  




            

        }

    }

