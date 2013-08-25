using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edward.Wilde.Note.For.Nurses.iOS.UI.iPad
{
    using System.Drawing;

    using MonoTouch.UIKit;

    class DatePickerPopover : UIPopoverControllerDelegate
    {
        private UIDatePicker datePicker;

        private UIPopoverController popoverController;

        public UIView Owner { get; set; }

        public UIDatePicker DatePicker
        {
            get
            {
                return this.datePicker;
            }
        }

        public DatePickerPopover(UIView owner)
        {
            this.Owner = owner;
            this.datePicker = new UIDatePicker();
            this.datePicker.Frame = new RectangleF(0, 44, 320, 216);

            var popoverContent = new UIViewController(); //ViewController
            var popoverView = new UIView
            {
                BackgroundColor = UIColor.Black
            };   //view

            popoverView.AddSubview(this.DatePicker);

            popoverContent.View = popoverView;
            popoverController = new UIPopoverController(popoverContent);
            popoverController.Delegate=this;

            popoverController.SetPopoverContentSize(new SizeF(320, 264),  false);
        }

        public void Show(UIView anchor)
        {
            popoverController.PresentFromRect(anchor.Frame, this.Owner, UIPopoverArrowDirection.Any, true);
        }
    }
}
