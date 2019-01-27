using System;
using System.Collections.Generic;
using GDPlatformer.Character.Animate;
using GDPlatformer.Character.Base;
using GDPlatformer.Gameplay.Collision;
using GDPlatformer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using GDPlatformer.Gameplay.Items;

namespace GDPlatformer.Character
{
  public class Player : Entity
  {
    #region Properties
    private Texture2D playerTexture;
    private Animation walkAnimation, hurtAnimation, currentAnimation;
    private KeyboardState keyboardState;
    private float elapsedGameTimeSeconds;

    // Position, Speed & Velocity
    public new Vector2 Position;
    public new readonly Vector2 Dimensions = new Vector2(70, 86);
    private readonly float speed = 300f;
    private readonly float gravity = 1.9f;
    private Vector2 velocity = new Vector2(0f, 0f);
    private int health = 3;
    private int score = 0;

    // Movement
    private bool allowLeftMovement;
    private bool allowRightMovement;
    private bool allowUpMovement;
    private bool allowDownMovement;

    // Booleans
    private bool isInAir;
    private bool isGoingLeft;
    private bool isHit;
    private bool isMoving;
    private double hitAnimationTimer;
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
      if (!isHit)
        CheckEnemyCollisions();

      CheckCoinCollisions();

      ApplyHorizontalMovement(gameTime);
      ApplyVerticalMovement(gameTime);

      UpdateAnimation(gameTime);

      ApplyGravity();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      SpriteEffects spriteEffect;
      if (isGoingLeft) spriteEffect = SpriteEffects.FlipHorizontally; else spriteEffect = SpriteEffects.None;
      spriteBatch.Draw(playerTexture, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffect, 0f);
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
      hurtAnimation = new Animation();
      hurtAnimation.AddFrame(new Rectangle(0, 258, 69, 82));
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
      Rectangle moveLeftCollisionBox = new Rectangle((int)(Position.X - velocity.X), (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
      Rectangle moveRightCollisionBox = new Rectangle((int)(Position.X + velocity.X), (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
      Rectangle moveUpCollisionBox = new Rectangle((int)Position.X, (int)(Position.Y - velocity.Y), (int)Dimensions.X, (int)Dimensions.Y);
      Rectangle moveDownCollisionBox = new Rectangle((int)Position.X, (int)(Position.Y + velocity.Y), (int)Dimensions.X, (int)Dimensions.Y);

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
      if (!isHit)
      {      // Get Enemy Colliders
        List<Enemy> enemyColliders = CollisionManager.Instance.GetEnemyColliders();

        // Check Collision Boxes against Player
        foreach (Enemy enemy in enemyColliders)
        {
          if (CheckCollision(new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), enemy.GetCollisionRectangle()))
          {
            if (health != 1)
              health--;
            else
              Die();
            enemy.Hit();
            isHit = true;
          }
        }
      }
    }
    private void CheckCoinCollisions()
    {
      List<Coin> coinColliders = CollisionManager.Instance.GetCoinColliders();

      foreach (Coin coin in coinColliders)
      {
        if(CheckCollision(new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y), coin.GetCollisionRectangle()))
        {
          score += coin.GetValue();
          CollisionManager.Instance.RemoveCoinCollider(coin);
          coin.Collect();
          return;
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
      isMoving = false;
      if (keyboardState.IsKeyDown(Keys.A) && allowLeftMovement)
      {
        Position.X -= speed * elapsedGameTimeSeconds;
        isMoving = true;
        isGoingLeft = true;
      }
      else if (keyboardState.IsKeyDown(Keys.D) && allowRightMovement)
      {
        Position.X += speed * elapsedGameTimeSeconds;
        isMoving = true;
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

    #region LifeCycle Methods
    public int GetHealth()
    {
      return health;
    }
    public int GetScore()
    {
      return score;
    }
    private void Die()
    {
      health = 0;
    }
    private void UpdateAnimation(GameTime gameTime)
    {
      if (isHit)
      {
        currentAnimation = hurtAnimation;
        hitAnimationTimer += gameTime.ElapsedGameTime.TotalSeconds;
      }
      else
      {
        hitAnimationTimer = 0f;
        currentAnimation = walkAnimation;
      }

      if (isHit || (!isHit && isMoving))
        currentAnimation.Update(gameTime);

      isHit &= hitAnimationTimer <= .6f;
    }
    #endregion

    #region Debug Methods
    private void ShowFPSCounter(GameTime gameTime, bool force = false)
    {
      if(DEBUG || force)
        Console.WriteLine(1 / gameTime.ElapsedGameTime.TotalSeconds);
    }
    #endregion
  }
}
