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
    private Texture2D playerTexture;
    private Texture2D hudTexture;
    private Animation walkAnimation;
    private KeyboardState keyboardState;
    private float elapsedGameTimeSeconds;

    // Position, Speed & Velocity
    public new Vector2 Position;
    public new readonly Vector2 Dimensions = new Vector2(70, 86);
    private readonly float speed = 300f;
    private readonly float gravity = 2f;
    private Vector2 velocity = new Vector2(0f, 0f);
    private int health = 3;

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

    // Debug
    private readonly bool DEBUG = false;
    #endregion

    #region Constructor
    public Player (Vector2 startPosition) {
      Position = startPosition;
    }
    #endregion

    #region Game Methods
    public override void LoadContent()
    {
      base.LoadContent();
      LoadTextures();
      LoadAnimations();
    }

    public override void UnloadContent() => base.UnloadContent();

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      ShowFPSCounter(gameTime);

      // Get Values & States
      elapsedGameTimeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
      keyboardState = Keyboard.GetState();

      CalculateVelocity();

      CheckMoveCollisions();
      CheckEnemyCollisions();

      ApplyHorizontalMovement(gameTime);
      ApplyVerticalMovement(gameTime);
      ApplyGravity();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      if (!isGoingLeft)
        spriteBatch.Draw(playerTexture, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), walkAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      else
        spriteBatch.Draw(playerTexture, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), walkAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
    }
    #endregion

    #region Load Methods
    private void LoadTextures()
    {
      playerTexture = content.Load<Texture2D>("Character/alien_yellow");
    }
    private void LoadAnimations()
    {
      walkAnimation = new Animation();
      walkAnimation.AddFrame(new Rectangle(0, 339, 68, 83));
      walkAnimation.AddFrame(new Rectangle(0, 0, 70, 86));
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
    private void CheckMoveCollisions()
    {
      // Generated Collision Boxes
      moveLeftCollisionBox = new Rectangle((int)(Position.X - velocity.X), (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
      moveRightCollisionBox = new Rectangle((int)(Position.X + velocity.X), (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
      moveUpCollisionBox = new Rectangle((int)Position.X, (int)(Position.Y - velocity.Y), (int)Dimensions.X, (int)Dimensions.Y);
      moveDownCollisionBox = new Rectangle((int)Position.X, (int)(Position.Y + velocity.Y), (int)Dimensions.X, (int)Dimensions.Y);

      // Get Level Colliders
      List<ICollide> levelColliders = CollisionManager.Instance.GetLevelColliders();

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
    }
    private void CheckEnemyCollisions()
    {
      // Get Enemy Colliders
      List<Enemy> enemyColliders = CollisionManager.Instance.GetEnemyColliders();

      // Check Collision Boxes against Player
      foreach (Enemy collider in enemyColliders)
      {
        if (CheckCollision(new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), collider.GetCollisionRectangle()))
        {
          health = 2;
          collider.Hit();
        }
      }
    }
    #endregion

    #region Movement Methods
    private void CalculateVelocity()
    {
      if (isInAir)
        velocity.Y += gravity * elapsedGameTimeSeconds;
      velocity.X = speed * elapsedGameTimeSeconds;
    }
    private void ApplyHorizontalMovement(GameTime gameTime)
    {
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
    }
    private void ApplyVerticalMovement(GameTime gameTime)
    {
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
    }
    private void ApplyGravity()
    {
      if (isInAir)
        Position.Y += velocity.Y;
      else
        velocity.Y = 0f;
    }
    #endregion

    #region Various Methods
    private void ShowFPSCounter(GameTime gameTime, bool force = false)
    {
      if(DEBUG || force)
        Console.WriteLine(1 / gameTime.ElapsedGameTime.TotalSeconds);
    }
    public int GetHealth() {
      return health;
    }
    #endregion
  }
}
