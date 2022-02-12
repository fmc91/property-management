using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PropertyManagementUi
{
    public class CircularButton : Button
    {
        static CircularButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircularButton), new FrameworkPropertyMetadata(typeof(CircularButton)));
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

        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        public static readonly DependencyProperty HighlightStrokeProperty =
            DependencyProperty.Register("HighlightStroke", typeof(Brush), typeof(CircularButton), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty HighlightStrokeThicknessProperty =
            DependencyProperty.Register("HighlightStrokeThickness", typeof(double), typeof(CircularButton), new PropertyMetadata(0d));

        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(CircularButton), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(CircularButton), new PropertyMetadata(null));
    }
}
