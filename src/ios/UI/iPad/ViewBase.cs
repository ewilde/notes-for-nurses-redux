namespace Edward.Wilde.Note.For.Nurses.iOS.UI.iPad
{
    using System;
    using System.Diagnostics;
    using System.Drawing;

    using MonoTouch.UIKit;

    public class ViewBase : UIView
    {
        protected virtual float GetLineHeight(UIFont font)
        {
            if (Math.Abs(font.PointSize - AppDelegate.Font16pt) < 0.5)
            {
                return 26;
            }

            return 15;
        }

        protected virtual float LeftMargin()
        {
            return this.LeftPadding();
        }

        protected virtual float LeftMargin(UIView view)
        {
            return view.Frame.Right + this.LeftControlPadding();
        }

        protected virtual float TopMargin()
        {
            return this.TopPadding();
        }

        protected virtual int LeftControlPadding()
        {
            return 10;
        }

        protected virtual int TopPadding()
        {
            return 15;
        }

        protected virtual float HalfScreenWidth()
        {
            return (this.Bounds.Width / 2) - this.LeftControlPadding();
        }

        protected virtual float QuarterScreenWidth()
        {
            return (this.Bounds.Width / 4) - (this.LeftControlPadding() * 2);
        }

        protected virtual int LeftPadding()
        {
            return 13;
        }

        protected virtual float TopMargin(UILabel label)
        {
            return label.Frame.Bottom + this.TopControlPadding();
        }

        protected virtual float TopControlPadding()
        {
            return 10;
        }

        protected float SingleLineControlHeight(UITextField textField)
        {
            return this.GetLineHeight(textField.Font);
        }

        protected virtual float SingleLineControlHeight(UILabel view)
        {
            return this.GetLineHeight(view.Font);
        }

        protected RectangleF AlignBottom(UIView label, float width = 0)
        {
            return new RectangleF(this.LeftMargin(label), label.Frame.Top,
                width > 0 ? width : this.HalfScreenWidth(), label.Frame.Height);
        }

        protected bool Editable { get; set; }

        protected UITextField CreateEditableTextField()
        {
            var textField = new UITextField () {
                                                           ShouldBeginEditing = field => this.Editable,
                                                           TextAlignment = UITextAlignment.Left,
                                                           Font = UIFont.FromName ("Helvetica-Light", AppDelegate.Font16pt),
                                                           BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
                                                       };
            this.ApplyDebugUIAttributes(textField);
            return textField;
        }

        protected UILabel CreateLabel(string text = null, UITapGestureRecognizer tapGestureRecognizer = null)
        {
            var label = new UILabel {
                                        Text = text,                                      
                                        TextAlignment = UITextAlignment.Left,
                                        Font = UIFont.FromName ("Helvetica-Light", AppDelegate.Font10_5Pt),
                                        TextColor = UIColor.DarkGray,
                                        BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
                                    };

            if (tapGestureRecognizer != null)
            {
                label.UserInteractionEnabled = true;
                label.AddGestureRecognizer(tapGestureRecognizer);
            }

            this.ApplyDebugUIAttributes(label);
            return label;
        }

        [Conditional("UI_DEBUG")]
        protected void ApplyDebugUIAttributes(UIView view)
        {
            view.Layer.BorderColor = UIColor.Red.CGColor;
            view.Layer.BorderWidth = 1;
        }
    }
}