using System;
using System.Collections.Generic;
using GDPlatformer.Character.Animate;
using GDPlatformer.Character.Base;
using GDPlatformer.Gameplay.Collision;
using GDPlatformer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Character.Enemies
{
  public class Bee : Enemy, ICollide
  {
    #region Properties
    private Texture2D texture;
    private Animation moveAnimation, deadAnimation, currentAnimation;
    private float elapsedGameTimeSeconds;

    // Position, Speed & Velocity
    public new Vector2 Position;
    public readonly new Vector2 Dimensions = new Vector2(56, 48);
    private readonly float speed = 100f;
    private Vector2 velocity = new Vector2(0f, 0f);

    // States
    private bool goingLeft;
    private bool isInAir;
    private bool isDead;
    private bool isGone;

    // Collision Boxes
    private Rectangle moveLeftCollisionBox;
    private Rectangle moveRightCollisionBox;
    private Rectangle moveDownCollisionBox;

    // Movement
    private bool allowLeftMovement;
    private bool allowRightMovement;
    private bool allowDownMovement;
    #endregion

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

      elapsedGameTimeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

      ApplyVelocity();

      CheckMoveCollision();

      if (!isDead)
        ApplyHorizontalMovement();
      else
      {
        currentAnimation = deadAnimation;
        ApplyVerticalMovement(gameTime);
      }

      isGone |= !allowDownMovement || Position.Y > 1500;

      currentAnimation.Update(gameTime);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      if (!isGone)
      {
        if (goingLeft)
          spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        else
          spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
      }
    }
    #endregion

    #region Collision Methods
    private bool CheckCollision(Rectangle boundBox, Rectangle colliderBox)
    {
      if (boundBox.Intersects(colliderBox))
      {
        return true;
      }
      return false;
    }
    public void CheckMoveCollision()
    {
      // Get Values & States
      List<ICollide> levelColliders = CollisionManager.Instance.GetLevelColliders();

      // Generated Collision Boxes
      moveLeftCollisionBox = new Rectangle((int)(Position.X - velocity.X), (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
      moveRightCollisionBox = new Rectangle((int)(Position.X + velocity.X), (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
      moveDownCollisionBox = new Rectangle((int)Position.X, (int)(Position.Y + velocity.Y), (int)Dimensions.X, (int)Dimensions.Y);

      // Set default values
      allowLeftMovement = true;
      allowRightMovement = true;
      allowDownMovement = true;

      // Check Collision Boxes against Player
      foreach (ICollide collider in levelColliders)
      {
        allowLeftMovement &= !CheckCollision(moveLeftCollisionBox, collider.GetCollisionRectangle());
        allowRightMovement &= !CheckCollision(moveRightCollisionBox, collider.GetCollisionRectangle());
        allowDownMovement &= !CheckCollision(moveDownCollisionBox, collider.GetCollisionRectangle());
      }
    }
    public override Rectangle GetCollisionRectangle()
    {
      return new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
    }
    public override void Hit()
    {
      isDead = true;
    }
    #endregion

    #region Movement Methods
    private void ApplyVelocity()
    {
      if (isInAir)
        velocity.Y += 2f * elapsedGameTimeSeconds;
      velocity.X = speed * elapsedGameTimeSeconds;
    }
    private void ApplyHorizontalMovement()
    {
      if (allowLeftMovement && goingLeft)
        Position.X -= velocity.X;
      else
        goingLeft = false;

      if (allowRightMovement && !goingLeft)
        Position.X += velocity.X;
      else
        goingLeft = true;
    }
    private void ApplyVerticalMovement(GameTime gameTime)
    {
      // isInAir
      if (allowDownMovement)
        isInAir = true;
      else
        isInAir = false;

      if (isInAir && isDead)
        Position.Y += velocity.Y;
      else
        velocity.Y = 0f;
    }
    #endregion

    #region Load Methods
    private void LoadAnimations()
    {
      moveAnimation = new Animation();
      moveAnimation.AddFrame(new Rectangle(315, 353, 56, 48));
      moveAnimation.AddFrame(new Rectangle(140, 23, 61, 42));
      deadAnimation = new Animation();
      deadAnimation.AddFrame(new Rectangle(315, 305, 56, 48));
      currentAnimation = moveAnimation;
    }
    #endregion

    #region Various Methods
    public override bool IsDead()
    {
      return isGone;
    }
    #endregion
  }
}
