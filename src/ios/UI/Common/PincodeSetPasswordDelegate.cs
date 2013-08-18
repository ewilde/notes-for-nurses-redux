using System;
using Edward.Wilde.Note.For.Nurses.Core.UI;

namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common
{
	public class PincodeSetPasswordDelegate : PincodeBinding.CPLockControllerDelegate
	{
		IScreenController screenController;

		public PincodeSetPasswordDelegate(IScreenController screenController)
		{
			this.screenController = screenController;
		}

		public override void DidFinish (PincodeBinding.CPLockController lockController, string passcode)
		{
			this.screenController.ShowMessage ("Configuration", "Set password: " + passcode);
		}

		public override void DidCancel (PincodeBinding.CPLockController lockController)
		{
			this.screenController.ShowMessage ("Configuration", "canceled set password");			
		}

		public override bool ShouldAcceptPasscode (PincodeBinding.CPLockController lockController, string passcode)
		{
			this.screenController.ShowMessage ("Configuration", "Should accept password: " + passcode);
			return true;
		}
	}
}

