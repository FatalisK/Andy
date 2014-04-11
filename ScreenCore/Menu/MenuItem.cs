using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andy.ScreenCore.Menu
{
    public class MenuItem
    {
        public string LinkType;
        public string LinkID;
        public Image image;

        public MenuItem()
        {
            this.image = new Image();
        }
    }
}
