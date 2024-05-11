using System.Windows;

namespace QuanLyGara.Helpers
{
    public static class TextBoxHelper
    {
        public static readonly DependencyProperty HintTextProperty = DependencyProperty.RegisterAttached(
            "HintText",
            typeof(string),
            typeof(TextBoxHelper),
            new FrameworkPropertyMetadata(string.Empty));

        public static string GetHintText(DependencyObject d)
        {
            return (string)d.GetValue(HintTextProperty);
        }

        public static void SetHintText(DependencyObject d, string value)
        {
            d.SetValue(HintTextProperty, value);
        }
    }

}
