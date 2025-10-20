using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace LR10_RPM
{
    public partial class MainWindow : Window
    {
        private bool _isAnimating = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Znak_Click(object sender, MouseButtonEventArgs e)
        {
            if (_isAnimating) return;
            _isAnimating = true;

            var znak = sender as Viewbox;
            if (znak == null) return;

            double left = Canvas.GetLeft(znak);
            if (double.IsNaN(left)) left = 0;
            double top = Canvas.GetTop(znak);
            if (double.IsNaN(top)) top = 0;

            var scale = new ScaleTransform(1, 1);
            znak.RenderTransform = scale;
            znak.RenderTransformOrigin = new Point(0.5, 0.5);

            var scaleAnim = new DoubleAnimation(1, 1.5, TimeSpan.FromSeconds(0.8))
            {
                AccelerationRatio = 0.3,
                DecelerationRatio = 0.3
            };

            var scaleBack = new DoubleAnimation(1.5, 1, TimeSpan.FromSeconds(0.8))
            {
                BeginTime = TimeSpan.FromSeconds(2)
            };

            var moveXForward = new DoubleAnimation(left, 500, TimeSpan.FromSeconds(0.8))
            {
                AccelerationRatio = 0.3,
                DecelerationRatio = 0.3
            };

            var moveYForward = new DoubleAnimation(top, 175, TimeSpan.FromSeconds(0.8))
            {
                AccelerationRatio = 0.3,
                DecelerationRatio = 0.3
            };

            var moveXBack = new DoubleAnimation(500, left, TimeSpan.FromSeconds(0.8))
            {
                BeginTime = TimeSpan.FromSeconds(2),
                AccelerationRatio = 0.3,
                DecelerationRatio = 0.3
            };

            var moveYBack = new DoubleAnimation(175, top, TimeSpan.FromSeconds(0.8))
            {
                BeginTime = TimeSpan.FromSeconds(2),
                AccelerationRatio = 0.3,
                DecelerationRatio = 0.3
            };

            var sb = new Storyboard();

            Storyboard.SetTarget(moveXForward, znak);
            Storyboard.SetTargetProperty(moveXForward, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTarget(moveYForward, znak);
            Storyboard.SetTargetProperty(moveYForward, new PropertyPath("(Canvas.Top)"));

            Storyboard.SetTarget(moveXBack, znak);
            Storyboard.SetTargetProperty(moveXBack, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTarget(moveYBack, znak);
            Storyboard.SetTargetProperty(moveYBack, new PropertyPath("(Canvas.Top)"));

            Storyboard.SetTarget(scaleAnim, znak);
            Storyboard.SetTargetProperty(scaleAnim, new PropertyPath("(RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(scaleBack, znak);
            Storyboard.SetTargetProperty(scaleBack, new PropertyPath("(RenderTransform).(ScaleTransform.ScaleX)"));

            var scaleAnimY = scaleAnim.Clone();
            Storyboard.SetTarget(scaleAnimY, znak);
            Storyboard.SetTargetProperty(scaleAnimY, new PropertyPath("(RenderTransform).(ScaleTransform.ScaleY)"));

            var scaleBackY = scaleBack.Clone();
            Storyboard.SetTarget(scaleBackY, znak);
            Storyboard.SetTargetProperty(scaleBackY, new PropertyPath("(RenderTransform).(ScaleTransform.ScaleY)"));

            sb.Children.Add(moveXForward);
            sb.Children.Add(moveYForward);
            sb.Children.Add(moveXBack);
            sb.Children.Add(moveYBack);
            sb.Children.Add(scaleAnim);
            sb.Children.Add(scaleAnimY);
            sb.Children.Add(scaleBack);
            sb.Children.Add(scaleBackY);

            sb.Completed += (s, _) => _isAnimating = false;

            sb.Begin();
        }




        private void Znak_MouseEnter(object sender, MouseEventArgs e)
        {
            Path redPart = null;
            if (sender == Nerovno_xaml) redPart = RedPart1;
            if (sender == Fire) redPart = RedPart2;
            if (sender == Layer_1) redPart = RedPart3;
            if (redPart == null) return;

            var brush = redPart.Fill as SolidColorBrush;
            if (brush == null)
            {
                brush = new SolidColorBrush(Colors.Red);
                redPart.Fill = brush;
            }

            var colorAnim = new ColorAnimation
            {
                To = Colors.Orange,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnim);
        }

        private void Znak_MouseLeave(object sender, MouseEventArgs e)
        {
            Path redPart = null;
            if (sender == Nerovno_xaml) redPart = RedPart1;
            if (sender == Fire) redPart = RedPart2;
            if (sender == Layer_1) redPart = RedPart3;
            if (redPart == null) return;

            var brush = redPart.Fill as SolidColorBrush;
            if (brush == null)
            {
                brush = new SolidColorBrush(Colors.Red);
                redPart.Fill = brush;
            }

            var colorAnim = new ColorAnimation
            {
                To = Colors.Red,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnim);
        }
    }
}
