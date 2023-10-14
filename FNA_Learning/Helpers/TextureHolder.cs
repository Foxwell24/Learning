using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FNA_Learning.Helpers
{

    public static class TextureHolder
    {
        static Texture2D[] textures = new Texture2D[Enum.GetNames<TextureSelector>().Length];

        public static Texture2D GetTexture(TextureSelector texture)
        {
            return textures[(int)texture];
        }

        public static void LoadTextures()
        {
            string[] names = Enum.GetNames<TextureSelector>();
            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i];
                textures[i] = FNAGame.ContentManager_.Load<Texture2D>($"{name}.png");
            }
        }
    }
}
