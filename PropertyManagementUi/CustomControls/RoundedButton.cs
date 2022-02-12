using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PropertyManagementUi
{
    public class RoundedButton : Button
    {
        static RoundedButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundedButton), new FrameworkPropertyMetadata(typeof(RoundedButton)));
        }

        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public Brush HighlightStroke
        {
            get { return (Brush)GetValue(HighlightStrokeProperty); }
            set { SetValue(HighlightStrokeProperty, value); }
        }

        public double HighlightStrokeThickness
        {
            get { return (double)GetValue(HighlightStrokeThicknessProperty); }
            set { SetValue(HighlightStrokeThicknessProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(RoundedButton), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty HighlightStrokeProperty =
            DependencyProperty.Register("HighlightStroke", typeof(Brush), typeof(RoundedButton), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty HighlightStrokeThicknessProperty =
            DependencyProperty.Register("HighlightStrokeThickness", typeof(double), typeof(RoundedButton), new PropertyMetadata(0d));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(RoundedButton), new PropertyMetadata(new CornerRadius()));
    }
}
