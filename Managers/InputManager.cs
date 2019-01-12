using System;
using Microsoft.Xna.Framework.Input;

namespace GDPlatformer.Managers
{
  public class InputManager
  {
    #region Singleton Properties
    private static InputManager instance;
    public static InputManager Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new InputManager();
        }
        return instance;
      }
    }
    #endregion

    #region Properties
    public bool Left { get; set; }
    public bool Right { get; set; }
    public bool Up { get; set; }
    #endregion

    #region Methods
    public void Update()
    {
      KeyboardState keyState = Keyboard.GetState();

      // Left
      if (keyState.IsKeyDown(Keys.A))
      {
        Left = true;
      }
      if (keyState.IsKeyUp(Keys.A))
      {
        Left = false;
      }

      // Right
      if (keyState.IsKeyDown(Keys.D))
      {
        Right = true;
      }
      if (keyState.IsKeyUp(Keys.D))
      {
        Right = false;
      }

      // Up
      if (keyState.IsKeyDown(Keys.Space))
      {
        Up = true;
      }
      if (keyState.IsKeyUp(Keys.Space))
      {
        Up = false;
      }
    }
    #endregion
  }
}
