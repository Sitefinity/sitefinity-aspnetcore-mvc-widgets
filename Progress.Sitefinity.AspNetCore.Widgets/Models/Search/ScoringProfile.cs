using System;
using System.Text;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Search
{
    /// <summary>
    /// Represents the Azure searc scroing profile settings.
    /// </summary>
    public class ScoringProfile
    {
        /// <summary>
        /// Gets or sets the scoring setting fot the Azure search.
        /// </summary>
        public string ScoringSetting { get; set; }

        /// <summary>
        /// Gets or sets the scoring parameters for the Azure search.
        /// </summary>
        public string ScoringParameters { get; set; }

        /// <summary>
        /// Encodes the object to base64 string.
        /// </summary>
        /// <returns>The encoded value.</returns>
        public override string ToString()
        {
            string res = this.ScoringSetting;

            if (!string.IsNullOrEmpty(this.ScoringParameters))
            {
                res = $"{res};{this.ScoringParameters}";
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(res));
        }
    }
}
