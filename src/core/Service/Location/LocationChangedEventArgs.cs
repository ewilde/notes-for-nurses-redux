// -----------------------------------------------------------------------
// <copyright file="LocationChangedEventArgs.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using System;
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public class LocationChangedEventArgs : EventArgs
    {
        public IList<Location> Locations { get; set; }

        public LocationChangedEventArgs() : this(new List<Location>())
        {
        }

        public LocationChangedEventArgs(IEnumerable<Location> locations)
        {
            this.Locations = new List<Location>(locations);
        }
    }
}