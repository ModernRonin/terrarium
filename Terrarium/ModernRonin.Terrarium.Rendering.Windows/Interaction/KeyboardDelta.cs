using Microsoft.Xna.Framework.Input;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public class KeyboardDelta : IUpdateable
    {
        KeyboardState mCurrent = Keyboard.GetState();
        KeyboardState mLast = Keyboard.GetState();
        public void Update()
        {
            mLast = mCurrent;
            mCurrent = Keyboard.GetState();
        }
        public bool WasPressed(Keys key) => mLast.IsKeyDown(key) && mCurrent.IsKeyUp(key);
    }
}