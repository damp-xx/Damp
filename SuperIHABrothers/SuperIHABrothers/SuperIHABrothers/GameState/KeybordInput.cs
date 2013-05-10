using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace GameState
{
    public class KeybordInput: IKeybordInput, IKeyboardUpdate
    {
        private Keys _Left = Keys.Left;
        private Keys _Right = Keys.Right;
        private Keys _Jump = Keys.Space;

        public bool IsLeftPressed { get; private set; }
        public bool WasLeftPressed { get; private set; }
        public bool IsRightPressed { get; private set; }
        public bool WasRightPressed { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool WasJumpPressed { get; private set; }

        public KeybordInput()
        {
            this.Update();
        }

        public void Update()
        {
            this.UpdateLeft();
            this.UpdateRight();
            this.UpdateJump();
        }

        private void UpdateLeft()
        {
            if (IsLeftPressed == true)
            {
                WasLeftPressed = true;
            }
            else
            {
                WasLeftPressed = false;
            }
            if (Keyboard.GetState().IsKeyDown(_Left))
            {
                IsLeftPressed = true;
            }
            else
            {
                IsLeftPressed = false;
            }
        }
        private void UpdateRight()
        {
            if (IsRightPressed == true)
            {
                WasRightPressed = true;
            }
            else
            {
                WasRightPressed = false;
            }
            if (Keyboard.GetState().IsKeyDown(_Right))
            {
                IsRightPressed = true;
            }
            else
            {
                IsRightPressed = false;
            }
        }
        private void UpdateJump()
        {
            if (IsJumpPressed  == true)
            {
                WasJumpPressed = true;
            }
            else
            {
                WasJumpPressed = false;
            }
            if (Keyboard.GetState().IsKeyDown(_Jump))
            {
                IsJumpPressed = true;
            }
            else
            {
                IsJumpPressed = false;
            }
        }
    }
}
