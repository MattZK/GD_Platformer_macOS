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

    // Speed & Velocity
    private readonly float speed = 300f;
    private readonly float gravity = 2f;
    private float verticalVelocity = 0f;

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

      // Generated Collision Boxes
      moveLeftCollisionBox = new Rectangle((int)(Position.X - speed * (float)gameTime.ElapsedGameTime.TotalSeconds), (int)Position.Y + 1, (int)Dimensions.X, (int)Dimensions.Y - 2);
      moveRightCollisionBox = new Rectangle((int)(Position.X + speed * (float)gameTime.ElapsedGameTime.TotalSeconds), (int)Position.Y + 1, (int)Dimensions.X, (int)Dimensions.Y - 2);
      moveUpCollisionBox = new Rectangle((int)Position.X, (int)(Position.Y - (verticalVelocity + gravity * (float)gameTime.ElapsedGameTime.TotalSeconds) + 2), (int)Dimensions.X, (int)Dimensions.Y);
      moveDownCollisionBox = new Rectangle((int)Position.X, (int)(Position.Y + (verticalVelocity + gravity * (float)gameTime.ElapsedGameTime.TotalSeconds)), (int)Dimensions.X, (int)Dimensions.Y);

      // Set defaults
      keyboardState = Keyboard.GetState();
      allowLeftMovement = true;
      allowRightMovement = true;
      allowDownMovement = true;
      allowUpMovement = true;

      // Get all levelColliders
      List<ICollide> levelColliders = CollisionManager.Instance.GetLevelColliders();

      // Check Collision Boxes against Player
      foreach (ICollide collider in levelColliders)
      {
        allowLeftMovement &= !CheckCollision(moveLeftCollisionBox, collider.GetCollisionRectangle());
        allowRightMovement &= !CheckCollision(moveRightCollisionBox, collider.GetCollisionRectangle());
        allowDownMovement &= !CheckCollision(moveDownCollisionBox, collider.GetCollisionRectangle());
        // TODO: Something is wrong here
        allowUpMovement &= !CheckCollision(moveUpCollisionBox, collider.GetCollisionRectangle());
      }

      // Apply gravity to the vertical velocity
      if (isJumping && allowDownMovement)
        verticalVelocity += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

      // Horizontal Movement
      if (keyboardState.IsKeyDown(Keys.A) && allowLeftMovement)
      {
        Position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        walkAnimation.Update(gameTime);
      }
      else if (keyboardState.IsKeyDown(Keys.D) && allowRightMovement)
      {
        Position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        walkAnimation.Update(gameTime);
      }

      /// Vertical Movement
      // Jump
      if (keyboardState.IsKeyDown(Keys.Space) && !isJumping)
      {
        isJumping = true;
        verticalVelocity = -1.2f;
      }

      // Falling Gravity
      if (allowDownMovement && !isJumping)
      {
        isJumping = true;
        verticalVelocity = 0f;
      }

      // Top Detection
      if (!allowUpMovement)
      {
        //verticalVelocity = 0f;
      }

      // Bottom Detection
      if (verticalVelocity > 0 && !allowDownMovement)
      {
        isJumping = false;
        verticalVelocity = 0f;
      }

      Position.Y += verticalVelocity;
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
      if (boundBox.Right < colliderBox.Left ||
          boundBox.Left > colliderBox.Right ||
          boundBox.Bottom < colliderBox.Top ||
          boundBox.Top > colliderBox.Bottom)
      {
        return false;
      }
      return true;
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
