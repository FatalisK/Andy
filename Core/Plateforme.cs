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
        private float _PvitesseX;
        private float _PvitesseY;
        public Plateforme(Sprite s, Vector2 positionBase ):base(s) {
            _PvitesseX = 0.0f;
            _PvitesseY = 0.0f;
            collision = false;
            typeobjet = TypeObjet.PLAT;
            _positionBase=positionBase;
        }

        public Plateforme(Sprite s, Vector2 positionBase,float Vx,float Vy)
            : base(s)
        {
            _PvitesseX = Vx;
            _PvitesseY = Vy;
            collision = false;
            typeobjet = TypeObjet.PLAT;
            _positionBase = positionBase;
        }

        public void se_deplacerX(int x)
        {
            if (sprite.location.X > _positionBase.X + x) { _direction = Direction.LEFT; }
            if (sprite.location.X <= _positionBase.X) { _direction = Direction.RIGHT; }

            if (_direction == Direction.RIGHT) { sprite.location.X = sprite.location.X + _PvitesseX; }
            if (_direction == Direction.LEFT) { sprite.location.X = sprite.location.X - _PvitesseX; }
        }

        public void se_deplacerY(int x)
        {
            if (sprite.location.Y <= _positionBase.Y ) { _direction = Direction.BOT; }
            if (sprite.location.Y >= _positionBase.Y+x) { _direction = Direction.TOP; }

            if (_direction == Direction.BOT) { sprite.location.Y = sprite.location.Y + _PvitesseY; }
            if (_direction == Direction.TOP) { sprite.location.Y = sprite.location.Y - _PvitesseY; }
        }

        public override float getPVitesseX()
        {
            return _PvitesseX;

        }

        public override float getPVitesseY()
        {
            return _PvitesseY;

        }

  




            

        }

    }

