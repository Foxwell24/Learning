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
        public float scale = 1;

        public GameObject(TextureSelector texture, Color color, Vector2 offset = default) : this()
        {
            this.texture = texture;
            this.color = color;
            this.offset = offset;
            rectangle = TextureHolder.GetTexture(texture).Bounds;
        }

        public void Draw(SpriteBatch batch)
        {
            //batch.Draw(TextureHolder.GetTexture(texture), position + offset, rectangle, color);

            batch.Draw(TextureHolder.GetTexture(texture), position, rectangle, color, 0f, -offset, scale, SpriteEffects.None, 0f);
        }
    }
}
