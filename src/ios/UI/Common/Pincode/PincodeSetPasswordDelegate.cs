using System;
using Edward.Wilde.Note.For.Nurses.Core.UI;

namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common
{
	public class PincodeSetPasswordDelegate : PincodeBinding.CPLockControllerDelegate
	{
		IScreenController screenController;
        public event EventHandler<PincodeEventArgs> Finished;
        public event EventHandler<PincodeEventArgs> Cancelled;

		public PincodeSetPasswordDelegate(IScreenController screenController)
		{
			this.screenController = screenController;
		}

		public override void DidFinish (PincodeBinding.CPLockController lockController, string passcode)
		{
            if (this.Finished != null)
            {
                this.Finished(this, new PincodeEventArgs(passcode, false));
            }
		}

		public override void DidCancel (PincodeBinding.CPLockController lockController)
		{
            if (this.Cancelled != null)
            {
                this.Cancelled(this, new PincodeEventArgs(string.Empty, true));
            }
		}

		public override bool ShouldAcceptPasscode (PincodeBinding.CPLockController lockController, string passcode)
		{
			this.screenController.ShowMessage ("Configuration", "Should accept password: " + passcode);
			return true;
		}
	}
}

