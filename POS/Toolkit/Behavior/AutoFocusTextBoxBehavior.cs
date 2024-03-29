﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace CPC.Toolkit.Behavior
{
    public class AutoFocusTextBoxBehavior : Behavior<TextBox>
    {
        private bool _hasPlaceholder;
        private Brush _textBoxForeground;

        public String Text { get; set; }
        public Brush Foreground { get; set; }

        protected override void OnAttached()
        {
            _textBoxForeground = AssociatedObject.Foreground;
            base.OnAttached();
            if (Text != null)
                SetPlaceholderText();
            AssociatedObject.GotKeyboardFocus += HandleKeyboardFocus;
            AssociatedObject.GotMouseCapture += HandleMouseCapture;
            AssociatedObject.LostFocus += LostFocus;
            AssociatedObject.TextChanged += TextChanged;
            AssociatedObject.GotFocus += GotFocus;

          
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= HandleKeyboardFocus;
            AssociatedObject.GotMouseCapture -= HandleMouseCapture;
            AssociatedObject.LostFocus -= LostFocus;
            AssociatedObject.TextChanged -= TextChanged;
        }

        private void HandleKeyboardFocus(object sender,
            System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            var txt = e.NewFocus as TextBox;
            if (txt != null && !_hasPlaceholder)
                txt.SelectAll();
        }

        private void HandleMouseCapture(object sender,
            System.Windows.Input.MouseEventArgs e)
        {
            var txt = e.OriginalSource as TextBox;
            if (txt != null && !_hasPlaceholder)
                txt.SelectAll();
            
        }

        private void TextChanged(object sender,
             TextChangedEventArgs textChangedEventArgs)
        {
            if (string.IsNullOrWhiteSpace(AssociatedObject.Text) 
                && !AssociatedObject.IsFocused
                && FocusManager.GetFocusedElement(this) != AssociatedObject)
            {
                if (Text != null)
                    SetPlaceholderText();
            }
           
        }
        
        private void LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AssociatedObject.Text))
            {
                if (Text != null)
                    SetPlaceholderText();
            }
        }
        private void GotFocus(object sender, RoutedEventArgs e)
        {
            if (_hasPlaceholder)
                RemovePlaceholderText();
        }


        private void RemovePlaceholderText()
        {
            AssociatedObject.Foreground = _textBoxForeground;
            AssociatedObject.Text = "";
            _hasPlaceholder = false;
        }

        private void SetPlaceholderText()
        {
            AssociatedObject.Foreground = Foreground;
            AssociatedObject.Text = Text;
            _hasPlaceholder = true;
        }
    }
}
