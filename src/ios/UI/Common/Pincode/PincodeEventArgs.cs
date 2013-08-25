using System;

namespace Edward.Wilde.Note.For.Nurses.iOS
{
    public class PincodeEventArgs : EventArgs
    {
        public PincodeEventArgs(string pincode, bool cancelled)
        {
            this.Pincode = pincode;
            this.Cancelled = cancelled;
        }       

        public string Pincode
        {
            get;
            set;
        }

        public bool Cancelled
        {
            get;
            set;
        }
    }
}

