using System;
using System.Collections.Generic;
using GDPlatformer.Character.Animate;
using GDPlatformer.Character.Base;
using GDPlatformer.Gameplay.Collision;
using GDPlatformer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Character
{
  public class Player : Entity
  {
    #region Properties
    public new Vector2 Position;
    public new readonly Vector2 Dimensions = new Vector2(70, 86);
    private Texture2D texture;
    private Animation walkAnimation;
    private KeyboardState keyboardState;
    private float elapsedGameTimeSeconds;

    // Speed & Velocity
    private readonly float speed = 300f;
    private readonly float gravity = 200f;
    private Vector2 velocity = new Vector2(300f, 0f);

    // Collision Boxes
    private Rectangle moveLeftCollisionBox;
    private Rectangle moveRightCollisionBox;
    private Rectangle moveUpCollisionBox;
    private Rectangle moveDownCollisionBox;

    // Movement
    private bool allowLeftMovement;
    private bool allowRightMovement;
    private bool allowUpMovement;
    private bool allowDownMovement;
    private bool isJumping;
    #endregion

    public Player (Vector2 startPosition) {
      Position = startPosition;
      LoadAnimations();
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

      // FPS Counter
      //Console.WriteLine(1 / gameTime.ElapsedGameTime.TotalSeconds);

      // Apply gravity to the vertical velocity
      if (velocity.Y != 0)
        velocity.Y += gravity * elapsedGameTimeSeconds;

      velocity.X = speed * elapsedGameTimeSeconds;

      // Get Values & States
      List<ICollide> levelColliders = CollisionManager.Instance.GetLevelColliders();
      elapsedGameTimeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
      keyboardState = Keyboard.GetState();

      // Generated Collision Boxes
      moveLeftCollisionBox = new Rectangle((int)(Position.X - velocity.X), (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
      moveRightCollisionBox = new Rectangle((int)(Position.X + velocity.X), (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
      moveUpCollisionBox = new Rectangle((int)Position.X, (int)(Position.Y - velocity.Y), (int)Dimensions.X, (int)Dimensions.Y);
      moveDownCollisionBox = new Rectangle((int)Position.X, (int)(Position.Y + velocity.Y), (int)Dimensions.X, (int)Dimensions.Y);

      // Set default values
      allowLeftMovement = true;
      allowRightMovement = true;
      allowDownMovement = true;
      allowUpMovement = true;

      // Check Collision Boxes against Player
      foreach (ICollide collider in levelColliders)
      {
        allowLeftMovement |= CheckCollision(moveLeftCollisionBox, collider.GetCollisionRectangle());
        allowRightMovement |= CheckCollision(moveRightCollisionBox, collider.GetCollisionRectangle());
        allowDownMovement |= CheckCollision(moveDownCollisionBox, collider.GetCollisionRectangle());
        allowUpMovement |= CheckCollision(moveUpCollisionBox, collider.GetCollisionRectangle());
      }

      #region Horizontal Movement
      // Horizontal Movement
      if (keyboardState.IsKeyDown(Keys.A) && allowLeftMovement)
      {
        Position.X -= speed * elapsedGameTimeSeconds;
        walkAnimation.Update(gameTime);
      }
      else if (keyboardState.IsKeyDown(Keys.D) && allowRightMovement)
      {
        Position.X += speed * elapsedGameTimeSeconds;
        walkAnimation.Update(gameTime);
      }
      #endregion

      #region Vertical Movement
      // Jump
      if (allowDownMovement)
        isJumping = true;

      if (isJumping)
      Position.Y += velocity.Y;
      #endregion

      #region Previous Vertical Movement
      ///// Vertical Movement

      //// Jump
      //if (keyboardState.IsKeyDown(Keys.Space) && !isJumping)
      //{
      //  isJumping = true;
      //  verticalVelocity = -800f;
      //}

      //// Falling Gravity
      //if (allowDownMovement && !isJumping)
      //{
      //  isJumping = true;
      //  verticalVelocity = 0f;
      //}

      //// Top Detection
      //if (!allowUpMovement)
      //{
      //  verticalVelocity = 0f;
      //}

      //// Bottom Detection
      //if (verticalVelocity > 0 && !allowDownMovement)
      //{
      //  isJumping = false;
      //  verticalVelocity = 0f;
      //}
      #endregion
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      spriteBatch.Draw(texture, Position, walkAnimation.CurrentFrame.SourceRectangle, Color.White);
    }
    #endregion

    #region Other Methods
    private bool CheckCollision(Rectangle boundBox, Rectangle colliderBox)
    {
      if (boundBox.Intersects(colliderBox))
      {
        return true;
      }
      return false;
      //if (boundBox.Right < colliderBox.Left ||
      //    boundBox.Left > colliderBox.Right ||
      //    boundBox.Bottom < colliderBox.Top ||
      //    boundBox.Top > colliderBox.Bottom)
      //{
      //  return false;
      //}
      //return true;
    }

    private void LoadAnimations()
    {
      walkAnimation = new Animation();
      walkAnimation.AddFrame(new Rectangle(0, 339, 68, 83));
      walkAnimation.AddFrame(new Rectangle(0, 0, 70, 86));
    }
    #endregion
  }
}
