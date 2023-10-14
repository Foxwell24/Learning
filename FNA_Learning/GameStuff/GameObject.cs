using FNA_Learning.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FNA_Learning.GameStuff
{
    public struct GameObject
    {
        public TextureSelector texture;
        public Vector2 position;
        public Rectangle rectangle;
        public Color color;

        public Vector2 offset;

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(TextureHolder.GetTexture(texture), position + offset, rectangle, color);
        }
    }
}
