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
    private Texture2D texture;
    private Animation walkAnimation;
    private KeyboardState keyboardState;
    private float elapsedGameTimeSeconds;

    // Position, Speed & Velocity
    public new Vector2 Position;
    public new readonly Vector2 Dimensions = new Vector2(70, 86);
    private readonly float speed = 300f;
    private readonly float gravity = 2f;
    private Vector2 velocity = new Vector2(0f, 0f);

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
    private bool isInAir;
    private bool isGoingLeft;
    #endregion

    #region Constructor
    public Player (Vector2 startPosition) {
      Position = startPosition;
      LoadAnimations();
    }
    #endregion

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

      // Get Values & States
      List<ICollide> levelColliders = CollisionManager.Instance.GetLevelColliders();
      List<ICollide> enemyColliders = CollisionManager.Instance.GetEnemyColliders();
      elapsedGameTimeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
      keyboardState = Keyboard.GetState();

      // Apply gravity to the vertical velocity
      if (isInAir)
        velocity.Y += gravity * elapsedGameTimeSeconds;
      velocity.X = speed * elapsedGameTimeSeconds;

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
        allowLeftMovement &= !CheckCollision(moveLeftCollisionBox, collider.GetCollisionRectangle());
        allowRightMovement &= !CheckCollision(moveRightCollisionBox, collider.GetCollisionRectangle());
        allowDownMovement &= !CheckCollision(moveDownCollisionBox, collider.GetCollisionRectangle());
        allowUpMovement &= !CheckCollision(moveUpCollisionBox, collider.GetCollisionRectangle());
      }

      foreach (ICollide collider in enemyColliders)
      {
        if (CheckCollision(new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), collider.GetCollisionRectangle()))
          Console.WriteLine("Enemy Hit");
      }

      #region Horizontal Movement
      // Horizontal Movement
      if (keyboardState.IsKeyDown(Keys.A) && allowLeftMovement)
      {
        Position.X -= speed * elapsedGameTimeSeconds;
        walkAnimation.Update(gameTime);
        isGoingLeft = true;
      }
      else if (keyboardState.IsKeyDown(Keys.D) && allowRightMovement)
      {
        Position.X += speed * elapsedGameTimeSeconds;
        walkAnimation.Update(gameTime);
        isGoingLeft = false;
      }
      #endregion

      #region Vertical Movement
      // isInAir
      if (allowDownMovement)
        isInAir = true;
      else
        isInAir = false;

      // Jump
      if (keyboardState.IsKeyDown(Keys.Space) && !isInAir)
      {
        isInAir = true;
        velocity.Y = -1.2f;
      }

      // Ceiling Detection
      if (!allowUpMovement)
      {
        velocity.Y = 0f;
        Position.Y += 1;
      }

      // Apply Gravity
      if (isInAir)
        Position.Y += velocity.Y;
      else
        velocity.Y = 0f;
      #endregion
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      if (!isGoingLeft)
        spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), walkAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      else
        spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), walkAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
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
