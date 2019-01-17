using System;
using System.Collections.Generic;
using GDPlatformer.Character.Animate;
using GDPlatformer.Character.Base;
using GDPlatformer.Gameplay.Collision;
using GDPlatformer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Character
{
  public class Player : Entity
  {
    #region Properties
    public new Vector2 Position;
    public new Vector2 Dimensions = new Vector2(70, 86);
    public Vector2 Velocity = new Vector2(0, 0);
    private Texture2D texture;
    private Animation walkAnimation;
    private float speed = 250f;
    #endregion

    public Player (Vector2 startPosition) {
      Position = startPosition;
      walkAnimation = new Animation();
      walkAnimation.AddFrame(new Rectangle(0, 339, 68, 83));
      walkAnimation.AddFrame(new Rectangle(0, 0, 70, 86));
    }

    #region Game Methods
    public override void LoadContent()
    {
      base.LoadContent();
      texture = content.Load<Texture2D>("Character/alien_yellow");
    }

    public override void UnloadContent() => base.UnloadContent();

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);

      List<ICollide> levelColliders = CollisionManager.Instance.GetLevelColliders();

      if (InputManager.Instance.Left)
      {
        Velocity.X = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      else if (InputManager.Instance.Right)
      {
        Velocity.X = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      else
      {
        Velocity.X = 0;
      }


      Position += Velocity;

      if ((int)Velocity.X != 0)
        walkAnimation.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      spriteBatch.Draw(texture, Position, walkAnimation.CurrentFrame.SourceRectangle, Color.White);
    }

    public Rectangle GetCollisionRectangle(int xOffset, int yOffset)
    {
      return new Rectangle((int)Position.X + 20 + xOffset, (int)Position.Y + yOffset, (int)Dimensions.X - 40 + xOffset, (int)Dimensions.Y + yOffset);
    }

    private bool checkCollision(ICollide collision)
    {
      Rectangle collider = collision.GetCollisionRectangle();
      if (Position.X + Dimensions.X < collider.X ||
                            Position.X > collider.X + collider.X ||
                            Position.Y + Dimensions.Y < collider.Y ||
                            Position.Y > collider.Y + collider.Y)
      {
        return true;
      }
      return false;
    }
    #endregion
  }
}
