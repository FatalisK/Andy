using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Andy.Core
{
    public class ParticleEngine
    {
        private int cpt;
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;
      











        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            cpt = 0;
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        public List<Texture2D> getListTexture()
        {
            return textures;
        }
        public void Update()
        {
     
                int total = 1;
                //if (cpt == 10)
                //{
                    for (int i = 0; i < total; i++)
                    {
                        particles.Add(GenerateNewParticlePers());
                    }
                    cpt = 0;
                //}

                for (int particle = 0; particle < particles.Count; particle++)
                {
                    particles[particle].Update();
                    if (particles[particle].TTL <= 0)
                    {
                        particles.RemoveAt(particle);
                        particle--;
                    }
                }
                
            
            cpt++;
        }

        private Particle GenerateNewParticlePers()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    1f * (float)(random.NextDouble() * 2 - 1),
                                    1f * (float)(random.NextDouble() * 2 - 1));
            float gravity = -2;
            float angle = 0;
            Vector2 ralentissement=new Vector2(1,1);

            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);

            Color color = Color.Black;
            if (random.Next(50)%2 == 0)
            {
              color = Color.Red;
            }

               

            

            float size = (float)random.NextDouble()/5;
            int ttl = 10 + random.Next(10);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl,gravity,ralentissement);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }
    }
}
