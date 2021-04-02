using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SwitchAvoidance.Joins
{
    /// <summary>
    /// Contains the join numbers for digitals on the Touch Panel.
    /// Easy to change without having to search through the code
    /// and replace a bunch of numbers.
    /// </summary>
    public enum DigitalJoins : uint
    {
        VolumeUp = 10,
        VolumeDown = 11,
        Mute = 12,
        SourceAppleTV = 1,
        SourceRoku = 2,
        SourceDirecTV = 3,
        SourceOff = 4
    }
}