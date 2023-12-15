using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Ukrainian_greenhouse
{
    public class PasswordBoxBindingBehavior : Behavior<PasswordBox>
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBoxBindingBehavior),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        private static void OnPasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = ((PasswordBoxBindingBehavior)sender).AssociatedObject;
            if (passwordBox != null)
            {
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
                passwordBox.Password = e.NewValue?.ToString() ?? string.Empty;
            }
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            var behavior = Interaction.GetBehaviors(passwordBox).OfType<PasswordBoxBindingBehavior>().FirstOrDefault();
            if (behavior != null)
            {
                behavior.Password = passwordBox.Password;
            }
        }
    }
}
