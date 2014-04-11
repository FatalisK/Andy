using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace Andy.ScreenCore
{
    public class ImageEffect
    {
        protected Image image;
        public bool isActivate;

        public ImageEffect()
        {
            this.isActivate = false;
        }

        public virtual void LoadContent(ref Image image)
        {
            this.image = image;
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gametime)
        {
        }
    }
}
