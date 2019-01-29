using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Managers.Base
{
  public abstract class Screen
  {
    public bool isGameOver;
    protected ContentManager content;

    public virtual void LoadContent()
    {
      content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
    }
    public virtual void UnloadContent() { content.Unload(); }
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
  }
}
