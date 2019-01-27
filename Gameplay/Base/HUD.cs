using GDPlatformer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Gameplay.Base
{
  public class HUD
  {
    protected ContentManager content;
    private Texture2D hudTexture;
    private SpriteFont font;
    private int health;
    private int score;

    public void LoadContent()
    {
      content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
      hudTexture = content.Load<Texture2D>("Items/hud");
      font = content.Load<SpriteFont>("Fonts/Arial");

    }
    public void UnloadContent() { content.Unload(); }
    public void Update(GameTime gameTime) { }
    public void Draw(SpriteBatch spriteBatch, Camera camera) {
      Vector2 centerOrigin = new Vector2(camera.GetOrigin().X + camera.GetViewPort().Width / 2, camera.GetOrigin().Y);
      DrawHearts(spriteBatch, health, centerOrigin);
      DrawScore(spriteBatch, score, centerOrigin);
    }

    public void UpdateHealth(int amount)
    {
      health = amount;
    }

    public void UpdateScore(int amount)
    {
      score = amount;
    }

    private void DrawHearts(SpriteBatch spriteBatch, int amount, Vector2 origin) {
      if (amount >= 1)
        spriteBatch.Draw(hudTexture, new Vector2(origin.X - 48, origin.Y + 16), new Rectangle(0, 256, 128, 128), Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
      if (amount >= 2)
        spriteBatch.Draw(hudTexture, new Vector2(origin.X - 16, origin.Y + 16), new Rectangle(0, 256, 128, 128), Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
      if (amount >= 3)
        spriteBatch.Draw(hudTexture, new Vector2(origin.X + 16, origin.Y + 16), new Rectangle(0, 256, 128, 128), Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
    }

    private void DrawScore(SpriteBatch spriteBatch, int amount, Vector2 origin)
    {
      spriteBatch.DrawString(font, amount.ToString(), new Vector2(origin.X - 48, origin.Y + 4), Color.Black);
    }

    private void drawDeadScreen()
    {

    }

  }
}
