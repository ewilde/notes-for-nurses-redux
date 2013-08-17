// -----------------------------------------------------------------------
// <copyright file="IScreenController.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.UI
{
    public interface IScreenController
    {
        void StartConfiguration();

        void ShowHomeScreen();

        void ShowExitScreen(string message);

        void ShowMessage(string title, string message);

        void MapConfigurationCompleted();
    }
}