using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyGara.Behaviors
{
    public class NumericInputBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += PreviewTextInput;
            DataObject.AddPastingHandler(AssociatedObject, Pasting);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= PreviewTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, Pasting);
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            
            TextBox textBox = sender as TextBox;
            if (e.Text == "0" && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "1";
                textBox.CaretIndex = textBox.Text.Length;
                e.Handled = true;
            }
        }

        private void Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private Boolean IsTextAllowed(String text)
        {
            var regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }
    }
}
