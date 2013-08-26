namespace Edward.Wilde.Note.For.Nurses.iOS.UI.iPad {
    using System;
    using System.Globalization;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI.Controls;

    using MonoTouch.Foundation;
    using MonoTouch.UIKit;
    using MonoTouch.Dialog.Utilities;

    using System.Drawing;

    /// <summary>
	/// Used in:
	///  iPad   * SessionSpeakersMasterDetail
	///         * SpeakerSessionsMasterDetail
	///  NOT used on iPhone ~ see Common.iPhone.PatientListViewController which dups some of this
	/// </summary>
	public class PatientDetailView : ViewBase, IImageUpdated 
    {
        private readonly UITextField nameField;

        private readonly UILabel dateOfBirthField;
        private readonly UILabel dateOfBirthLabel;

        private readonly UIImageView image;

        private Patient patient;

        private EmptyOverlay emptyOverlay;

        private NSDate editingDateOfBirth;

        public PatientDetailView()
		{
		    this.BackgroundColor = UIColor.White;
		    
            this.image = new UIImageView();
            this.nameField = this.CreateEditableTextField();
            
            var datePickerPopover = new DatePickerPopover(this);
            datePickerPopover.DatePicker.Mode = UIDatePickerMode.Date;
            datePickerPopover.DatePicker.ValueChanged += (s, e) =>
            {
                this.editingDateOfBirth = (s as UIDatePicker).Date;
                this.UpdateDateOfBirthLabel(this.editingDateOfBirth);
            };
            this.dateOfBirthLabel = CreateLabel(text: "date of birth:");
            this.dateOfBirthField = CreateLabel(tapGestureRecognizer: new UITapGestureRecognizer(
                recognizer =>
                {
                    if (!this.Editable)
                    {
                        return;
                    }

                    datePickerPopover.DatePicker.Date = this.patient.DateOfBirth;
                    datePickerPopover.Show(this.dateOfBirthField);
                }));

            this.AddSubview(this.nameField);
			this.AddSubview(this.dateOfBirthLabel);
			this.AddSubview(this.dateOfBirthField);
			this.AddSubview(this.image);	
		}

        public override void LayoutSubviews ()
		{
			if (EmptyOverlay.ShowIfRequired (ref this.emptyOverlay, this.patient, this, "No Patient info", EmptyOverlayType.Speaker)) return;

		    this.LayoutImage();
		    this.LayoutNameLabel();
		    this.LayoutDateOfBirth();            		    
		}

        private void LayoutImage()
        {
            this.image.Frame = new RectangleF(this.LeftMargin(), this.TopMargin(), 80, 80);
        }

        private void LayoutNameLabel()
        {
            this.nameField.Frame = 
                this.AlignBottom(this.image);            
        }

        private void LayoutDateOfBirth()
        {
            this.dateOfBirthLabel.Frame = this.AlignBottom(this.nameField, width: this.QuarterScreenWidth());
            this.dateOfBirthField.Frame = this.AlignBottom(this.dateOfBirthLabel, width: this.QuarterScreenWidth());
        }

        // for masterdetail

        public void Update(Patient patient)
		{
			this.patient = patient;
			this.Update();
			this.LayoutSubviews ();
		}

        public void Clear()
		{
			this.patient = null;
			this.nameField.Text = "";
			this.dateOfBirthField.Text = "";
			this.image.Image = null;
			this.LayoutSubviews (); // show the grey 'no Patient' message
		}

        void Update()
		{
			if (this.patient == null) {this.nameField.Text ="not found"; return;}

		    this.image.Image = ImageLoader.DefaultRequestImage(new Uri("https://en.gravatar.com/avatar/196d33ea9cdaf7817b98b981afe62c16?s=100"), this);
		    this.nameField.Text = this.patient.Name.ToString();
		    this.UpdateDateOfBirthLabel(this.patient.DateOfBirth);
		}

        private void UpdateDateOfBirthLabel(DateTime dateOfBirth)
        {
            this.dateOfBirthField.Text = dateOfBirth.ToString("d", CultureInfo.CurrentCulture);
        }

        public void UpdatedImage (Uri uri)
		{
			this.image.Image = ImageLoader.DefaultRequestImage(uri, this);
		}

        public void StartEditing()
        {
            this.Editable = true;
        }

        public void FinishedEditing()
        {
            this.Editable = false;

            // update patient object
            if (this.editingDateOfBirth != null)
            {
                this.patient.DateOfBirth = this.editingDateOfBirth;
            }

            this.patient.Name.DisplayName = this.nameField.Text;

            // reset ui to look like read-mode
            this.nameField.EndEditing(true);
        }
    }
}