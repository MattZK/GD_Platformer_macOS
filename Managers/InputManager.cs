using System;
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
  }
}
