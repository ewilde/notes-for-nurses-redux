// -----------------------------------------------------------------------
// <copyright file="ActionSheetDatePicker.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI.Controls
{
    using System;

    using MonoTouch.UIKit;

    using System.Drawing;

    /// <summary>
    /// A class to show a date picker on an action sheet. To use, create a new ActionSheetDatePicker,
    /// set the Title, modify any settings on the DatePicker property, and call Show(). It will 
    /// automatically dismiss when the user clicks "Done," or you can call Hide() to dismiss it 
    /// manually.
    /// </summary>
    [MonoTouch.Foundation.Register("SlideOnDatePicker")]
    public class ActionSheetDatePicker
    {
        #region -= declarations =-
        const int CHROMEWIDTHLEFT = 9;
        const int CHROMEWIDTHRIGHT = 8;
        const int MARGIN = 50;

        private UIActionSheet actionSheet;

        private UIButton doneButton = UIButton.FromType(UIButtonType.RoundedRect);

        private UIView owner;

        private UILabel titleLabel = new UILabel();

        #endregion

        #region -= properties =-

        /// <summary>
        /// Set any datepicker properties here
        /// </summary>
        public UIDatePicker DatePicker
        {
            get
            {
                return datePicker;
            }
            set
            {
                datePicker = value;
            }
        }

        private UIDatePicker datePicker = new UIDatePicker(RectangleF.Empty);

        /// <summary>
        /// The title that shows up for the date picker
        /// </summary>
        public string Title
        {
            get
            {
                return titleLabel.Text;
            }
            set
            {
                titleLabel.Text = value;
            }
        }

        #endregion

        #region -= constructor =-

        /// <summary>
        /// 
        /// </summary>
        public ActionSheetDatePicker(UIView owner)
        {
            // save our uiview owner
            this.owner = owner;

            // configure the title label
            titleLabel.BackgroundColor = UIColor.Clear;
            titleLabel.TextColor = UIColor.LightTextColor;
            titleLabel.Font = UIFont.BoldSystemFontOfSize(18);

            // configure the done button
            doneButton.SetTitle("done", UIControlState.Normal);
            doneButton.TouchUpInside += (s, e) => { actionSheet.DismissWithClickedButtonIndex(0, true); };

            // create + configure the action sheet
            actionSheet = new UIActionSheet() { Style = UIActionSheetStyle.BlackTranslucent };
            actionSheet.Clicked += (s, e) => { Console.WriteLine("Clicked on item {0}", e.ButtonIndex); };

            // add our controls to the action sheet
            actionSheet.AddSubview(datePicker);
            actionSheet.AddSubview(titleLabel);
            actionSheet.AddSubview(doneButton);
        }

        #endregion

        #region -= public methods =-

        /// <summary>
        /// Shows the action sheet picker from the view that was set as the owner.
        /// </summary>
        public void Show()
        {
            // show the action sheet and add the controls to it
            actionSheet.ShowInView(owner);
            // resize the action sheet to fit our other stuff

            this.ShowActionSheet();
        }
        /// <summary>
        /// Shows the action sheet picker from the view that was set as the owner.
        /// </summary>
        public void Show(UIView view)
        {
            // show the action sheet and add the controls to it
            actionSheet.ShowFrom(new RectangleF(view.Frame.Width/2, view.Frame.Height, 0, 0), view, true);
            // resize the action sheet to fit our other stuff

            this.ShowActionSheet();
        }

        private void ShowActionSheet()
        {
            // declare vars
            float titleBarHeight = 40;
            SizeF doneButtonSize = new SizeF(71, 30);
            SizeF actionSheetSize = new SizeF(this.owner.Frame.Width, this.datePicker.Frame.Height + titleBarHeight);
            RectangleF actionSheetFrame = new RectangleF(
                0,
                this.owner.Frame.Height - actionSheetSize.Height,
                actionSheetSize.Width,
                actionSheetSize.Height);

            this.actionSheet.Frame = actionSheetFrame;

            if (UIDevice.CurrentDevice.UserInterfaceIdiom != UIUserInterfaceIdiom.Pad)
            {
                var popover = this.actionSheet.Superview.Superview;
                if (popover != null)
                {
                    var x = this.actionSheet.Frame.X + MARGIN;
                    var y = (UIScreen.MainScreen.ApplicationFrame.Height - this.actionSheet.Frame.Height) / 2;
                    var width = this.actionSheet.Frame.Width - (MARGIN * 2);
                    var height = this.actionSheet.Frame.Height;

                    popover.Frame = new RectangleF(x, y, width, height);
                    this.actionSheet.Frame = new RectangleF(
                        x,
                        y,
                        width - (CHROMEWIDTHLEFT + CHROMEWIDTHRIGHT),
                        height - (CHROMEWIDTHLEFT + CHROMEWIDTHRIGHT));
                }
            }

            // move our picker to be at the bottom of the actionsheet (view coords are relative to the action sheet)
            this.datePicker.Frame = new RectangleF(
                this.datePicker.Frame.X,
                titleBarHeight,
                this.datePicker.Frame.Width,
                this.datePicker.Frame.Height);

            // move our label to the top of the action sheet
            this.titleLabel.Frame = new RectangleF(10, 4, this.owner.Frame.Width - 100, 35);

            // move our button
            this.doneButton.Frame = new RectangleF(
                actionSheetSize.Width - doneButtonSize.Width - 10,
                7,
                doneButtonSize.Width,
                doneButtonSize.Height);
        }

        /// <summary>
        /// Dismisses the action sheet date picker
        /// </summary>
        public void Hide(bool animated)
        {
            actionSheet.DismissWithClickedButtonIndex(0, animated);
        }

        #endregion
    }
}