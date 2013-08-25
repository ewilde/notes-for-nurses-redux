namespace Edward.Wilde.Note.For.Nurses.iOS.UI.iPad {
    using System;
    using System.Globalization;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI.Controls;

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

        private int patientId;

        private Patient patient;

        private EmptyOverlay emptyOverlay;

        public PatientDetailView(IPatientManager patientManager, int patientId)
		{
		    this.PatientManager = patientManager;
		    this.patientId = patientId;

			this.BackgroundColor = UIColor.White;
		    
            this.image = new UIImageView();
            this.nameField = this.CreateEditableTextField();
            /*var actionSheetDatePicker = new ActionSheetDatePicker(this);
            actionSheetDatePicker.Title = "Choose Date:";
            actionSheetDatePicker.DatePicker.Mode = UIDatePickerMode.Date;
            actionSheetDatePicker.DatePicker.ValueChanged += (s, e) =>
            {
                this.patient.DateOfBirth = (s as UIDatePicker).Date;
                this.Update();
            };
             * */
            var datePicker = new DatePickerPopover(this);
            datePicker.DatePicker.Mode = UIDatePickerMode.Date;
            datePicker.DatePicker.ValueChanged += (s, e) =>
            {
                this.patient.DateOfBirth = (s as UIDatePicker).Date;
                this.Update();
            };
            this.dateOfBirthLabel = CreateLabel(text: "date of birth:");
            this.dateOfBirthField = CreateLabel(tapGestureRecognizer: new UITapGestureRecognizer(
                recognizer =>
                {
                    if (!this.Editable)
                    {
                        return;
                    }

                    datePicker.Show(this.dateOfBirthField);
                }));

            this.AddSubview(this.nameField);
			this.AddSubview(this.dateOfBirthField);
			this.AddSubview(this.image);	
		}

        public IPatientManager PatientManager { get; set; }

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

        public void Update(int speakerID)
		{
			this.patientId = speakerID;
			this.patient = this.PatientManager.GetById(this.patientId);
			this.Update ();
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
		    this.dateOfBirthField.Text = this.patient.DateOfBirth.ToString("d", CultureInfo.CurrentCulture);
		}

        public void UpdatedImage (Uri uri)
		{
			this.image.Image = ImageLoader.DefaultRequestImage(uri, this);
		}

        public void ShowEditMode()
        {
            this.Editable = true;
        }
    }
}