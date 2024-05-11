using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace QuanLyGara.Behaviors
{
    public class TextChangedBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(TextChangedBehavior), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += OnTextChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Command != null && Command.CanExecute(AssociatedObject.DataContext))
            {
                Command.Execute(AssociatedObject.DataContext);
            }
        }
    }
}
