/*
 * ITSE 1430
 */
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieLib
{
    /// <summary>Provides the different types of movie ratings.</summary>
    public enum Rating : byte
    {
        /// <summary>Unknown or unspecified rating.</summary>
        [Display(Name = "Unspecified")]
        Unspecified,

        /// <summary>General Audience.</summary>
        [Display(Name = "G")]
        G,

        /// <summary>Parental Guidance suggested.</summary>
        [Display(Name = "PG")]
        PG,

        /// <summary>Parental Guidance suggested for those under 13.</summary>
        [Display(Name = "PG13")]
        PG13,

        /// <summary>Restricted</summary>
        [Display(Name = "R")]
        R
    }
}
