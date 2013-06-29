// -----------------------------------------------------------------------
// <copyright file="CompareResult.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System;

    /// <summary>
    /// A list of possible outcomes whilst comparing two entities of the same type <see cref="IComparable{T}"/>.
    /// </summary>
    public enum CompareResult
    {
        LessThan = -1,
        Equal = 0,
        MoreThan = 1
    }
}