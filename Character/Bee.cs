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
  public class Bee: Entity, ICollide
  {
    private Texture2D texture;
    private Animation moveAnimation, dieAnimation, deadAnimation;
    private float elapsedGameTimeSeconds;

    // Position, Speed & Velocity
    public new Vector2 Position;
    public new readonly Vector2 Dimensions = new Vector2(56, 48);
    private readonly float speed = 100f;
    private Vector2 velocity = new Vector2(0f, 0f);
    private bool goingLeft;

    // Collision Boxes
    private Rectangle moveLeftCollisionBox;
    private Rectangle moveRightCollisionBox;

    // Movement
    private bool allowLeftMovement;
    private bool allowRightMovement;

    public Bee(Vector2 startPosition)
    {
      Position = startPosition;
      LoadAnimations();
    }

    #region Game Methods
    public override void LoadContent()
    {
      base.LoadContent();
      texture = content.Load<Texture2D>("Character/enemies");
    }

    public override void UnloadContent() => base.UnloadContent();

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);

      // Get Values & States
      List<ICollide> levelColliders = CollisionManager.Instance.GetLevelColliders();
      elapsedGameTimeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

      velocity.X = speed * elapsedGameTimeSeconds;

      // Generated Collision Boxes
      moveLeftCollisionBox = new Rectangle((int)(Position.X - velocity.X), (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
      moveRightCollisionBox = new Rectangle((int)(Position.X + velocity.X), (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);

      // Set default values
      allowLeftMovement = true;
      allowRightMovement = true;

      // Check Collision Boxes against Player
      foreach (ICollide collider in levelColliders)
      {
        allowLeftMovement &= !CheckCollision(moveLeftCollisionBox, collider.GetCollisionRectangle());
        allowRightMovement &= !CheckCollision(moveRightCollisionBox, collider.GetCollisionRectangle());
      }

      if (allowLeftMovement && goingLeft)
        Position.X -= velocity.X;
      else
        goingLeft = false;

      if (allowRightMovement && !goingLeft)
        Position.X += velocity.X;
      else
        goingLeft = true;

      moveAnimation.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      if(goingLeft)
        spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      else
        spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
    }
    #endregion

    private void LoadAnimations()
    {
      moveAnimation = new Animation();
      moveAnimation.AddFrame(new Rectangle(315, 353, 56, 48));
      moveAnimation.AddFrame(new Rectangle(140, 23, 61, 42));
      dieAnimation = new Animation();
      dieAnimation.AddFrame(new Rectangle(315, 353, 56, 48));
      dieAnimation.AddFrame(new Rectangle(315, 44, 56, 48));
      deadAnimation = new Animation();
      deadAnimation.AddFrame(new Rectangle(315, 305, 56, 48));
    }

    private bool CheckCollision(Rectangle boundBox, Rectangle colliderBox)
    {
      if (boundBox.Intersects(colliderBox))
      {
        return true;
      }
      return false;
    }

    public Rectangle GetCollisionRectangle()
    {
      return new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
    }
  }
}
