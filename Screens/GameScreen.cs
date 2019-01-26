using GDPlatformer.Character;
using GDPlatformer.Gameplay;
using GDPlatformer.Gameplay.Base;
using GDPlatformer.Managers;
using GDPlatformer.Managers.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Screens
{
  public class GameScreen: Screen
  {
    #region Properties
    public Player Player;
    private Bee bee;
    private HUD hud;
    private Level level;
    #endregion

    #region Game Methods
    public override void LoadContent()
    {
      base.LoadContent();
      level = new Level();
      level.LoadContent(content);
      hud = new HUD();
      hud.LoadContent();
      Player = new Player(new Vector2(100, 100));
      Player.LoadContent();
      bee = new Bee(new Vector2(400, 430));
      bee.LoadContent();

      CollisionManager.Instance.AddEnemyCollider(bee);
    }

    public override void UnloadContent()
    {
      base.UnloadContent();
      Player.UnloadContent();
      hud.UnloadContent();
      bee.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      Player.Update(gameTime);
      hud.UpdateHealth(Player.GetHealth());
      hud.Update(gameTime);
      bee.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch, Camera camera)
    {
      base.Draw(spriteBatch);
      level.Draw(spriteBatch);
      Player.Draw(spriteBatch);
      hud.Draw(spriteBatch, camera);
      bee.Draw(spriteBatch);
    }
    #endregion
  }
}
