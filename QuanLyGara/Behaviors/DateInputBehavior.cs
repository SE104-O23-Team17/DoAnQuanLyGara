using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGara.Behaviors
{
    public class DateInputBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += TextBox_PreviewTextInput;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewTextInput -= TextBox_PreviewTextInput;
            base.OnDetaching();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^(0?[1-9]|[12][0-9]|3[01])/(0?[1-9]|1[012])/((19|20)\d\d)?$");
            if (!regex.IsMatch((sender as TextBox).Text + e.Text))
                e.Handled = true;
        }
    }
}
