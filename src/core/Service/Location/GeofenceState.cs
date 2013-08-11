// -----------------------------------------------------------------------
// <copyright file="GeofenceState.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    public enum GeofenceState
    {
        Initializing,
        InsideFence,
        OutsideFence,
        TimeoutWaitingForLocation
    }
}