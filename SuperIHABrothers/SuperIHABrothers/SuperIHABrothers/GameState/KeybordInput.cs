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
        private Keys _CurserDown = Keys.Down;
        private Keys _CurserUp = Keys.Up;
        private Keys _Select = Keys.Enter;

        public bool IsLeftPressed { get; private set; }
        public bool WasLeftPressed { get; private set; }
        public bool IsRightPressed { get; private set; }
        public bool WasRightPressed { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool WasJumpPressed { get; private set; }
        public bool IsCurserDownPressed { get; private set; }
        public bool IsCurserUpPressed { get; private set; }
        public bool IsSelectPressed { get; private set; }
        public bool WasCurserDownPressed { get; private set; }
        public bool WasCurserUpPressed { get; private set; }
        public bool WasSelectPressed { get; private set; }


        public KeybordInput()
        {
            this.Update();
        }

        public void Update()
        {
            this.UpdateLeft();
            this.UpdateRight();
            this.UpdateJump();
            this.UpdateCurserUp();
            this.UpdateCurserDown();
            this.UpdateSelect();
            
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
        private void UpdateCurserDown()
        {
            WasCurserDownPressed = IsCurserDownPressed;
            IsCurserDownPressed = Keyboard.GetState().IsKeyDown(_CurserDown);
        }
        private void UpdateCurserUp()
        {
            WasCurserUpPressed = IsCurserUpPressed;
            IsCurserUpPressed = Keyboard.GetState().IsKeyDown(_CurserUp);
        }
        private void UpdateSelect()
        {
            WasSelectPressed = IsSelectPressed;
            IsSelectPressed = Keyboard.GetState().IsKeyDown(_Select);
        }
    }
}
