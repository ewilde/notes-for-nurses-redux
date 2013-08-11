// -----------------------------------------------------------------------
// <copyright file="IApplicationSettingsService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    public interface IApplicationSettingsService
    {
        TValue GetValue<TValue>(string key); 
        string GetValue(string key); 
    }
}