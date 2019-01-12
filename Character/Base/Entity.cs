using System;
using GDPlatformer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Character.Base
{
  public abstract class Entity
  {
    protected ContentManager content;
    public Vector2 Position { get; set; }
    public Vector2 Dimensions { get; set; }

    public virtual void LoadContent()
    {
      content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
    }
    public virtual void UnloadContent() { content.Unload(); }
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
  }
}
