using System;
using Xamarin.Forms;

namespace InputMvvm.Behaviors
{
    public class MaxLengthValidator : Behavior<Entry>
    {
        public static BindableProperty maxLength =
        BindableProperty.Create("MaxLength", typeof(int), typeof(MaxLengthValidator), 0);

        public int MaxLength
        {
            get { return (int)GetValue(maxLength); }
            set { SetValue(maxLength, value); }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnTextChanged;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > MaxLength)
                ((Entry)sender).Text = e.NewTextValue.Substring(0, MaxLength);
        }

    }
}
