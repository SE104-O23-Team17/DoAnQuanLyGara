using Microsoft.Xaml.Behaviors;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Globalization;

namespace QuanLyGara.Behaviors
{
    public class DoubleInputBehavior : Behavior<TextBox>
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
            var proposedText = AssociatedObject.Text.Insert(AssociatedObject.SelectionStart, e.Text);
            decimal m;
            e.Handled = !decimal.TryParse(proposedText, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { NumberDecimalSeparator = "," }, out m) || proposedText.Contains(' ') || proposedText.Contains('.');
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
            return Array.TrueForAll<Char>(text.ToCharArray(), delegate (Char c) {
                return Char.IsDigit(c) || Char.IsControl(c) || (c == ',' && !text.Contains(' ') && !text.Contains('.'));
            });
        }
    }
}
