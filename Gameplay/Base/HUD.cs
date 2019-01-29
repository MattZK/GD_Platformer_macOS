using GDPlatformer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Gameplay.Base
{
  public class HUD
  {
    protected ContentManager content;
    private Texture2D hudTexture, deadOverlayTexture;
    private SpriteFont font;
    private int health;
    private int score;

    public void LoadContent()
    {
      content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
      hudTexture = content.Load<Texture2D>("Items/hud");
      deadOverlayTexture = content.Load<Texture2D>("Background/deadColorOverlay");
      font = content.Load<SpriteFont>("Fonts/Arial");

    }
    public void UnloadContent() { content.Unload(); }
    public void Update(GameTime gameTime) { }
    public void Draw(SpriteBatch spriteBatch, Camera camera) {
      Vector2 centerOrigin = new Vector2(camera.GetOrigin().X + camera.GetViewPort().Width / 2, camera.GetOrigin().Y);
      DrawHearts(spriteBatch, health, centerOrigin);
      DrawScore(spriteBatch, score, centerOrigin);
    }

    // Update the health
    public void UpdateHealth(int amount)
    {
      health = amount;
    }

    // Update the score
    public void UpdateScore(int amount)
    {
      score = amount;
    }

    // Draw the Hearts
    private void DrawHearts(SpriteBatch spriteBatch, int amount, Vector2 origin) {
      if (amount >= 1)
        spriteBatch.Draw(hudTexture, new Vector2(origin.X - 48, origin.Y + 24), new Rectangle(0, 256, 128, 128), Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
      if (amount >= 2)
        spriteBatch.Draw(hudTexture, new Vector2(origin.X - 16, origin.Y + 24), new Rectangle(0, 256, 128, 128), Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
      if (amount >= 3)
        spriteBatch.Draw(hudTexture, new Vector2(origin.X + 16, origin.Y + 24), new Rectangle(0, 256, 128, 128), Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
    }

    // Draw the Score
    private void DrawScore(SpriteBatch spriteBatch, int amount, Vector2 origin)
    {

      Vector2 dimensions = font.MeasureString(score.ToString());
      spriteBatch.DrawString(font, amount.ToString(), new Vector2(origin.X - dimensions.X / 4, origin.Y + 8), Color.Black, 0f, Vector2.Zero, .5f, SpriteEffects.None, 0f);
    }

    // Show a Game Over Overlay
    public void ShowGameOver(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(deadOverlayTexture, new Vector2(0, 0), Color.White);
      Vector2 position = new Vector2((ScreenManager.Instance.Dimensions.X / 2), (ScreenManager.Instance.Dimensions.Y / 2));
      Vector2 origin = new Vector2(font.MeasureString("You Died!").X / 2, font.MeasureString("You Died!").Y / 2);
      spriteBatch.DrawString(font, "You Died!", position, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
    }

  }
}
