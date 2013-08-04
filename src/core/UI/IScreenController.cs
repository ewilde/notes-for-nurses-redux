// -----------------------------------------------------------------------
// <copyright file="IScreenController.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.UI
{
    public interface IScreenController
    {
        void ShowConfigurationScreen();

        void ShowHomeScreen();

        void ShowExitScreen(string message);
    }
}