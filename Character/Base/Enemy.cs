using System;
using GDPlatformer.Gameplay.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Character.Base
{
  public class Enemy : Entity, ICollide
  {
    public override void LoadContent()
    {
      base.LoadContent();
    }

    public override void UnloadContent()
    {
      base.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
    }

    public virtual void Hit()
    {

    }

    public virtual Rectangle GetCollisionRectangle()
    {
      return Rectangle.Empty;
    }

    public virtual bool IsDead() {
      return false;
    }
  }
}
