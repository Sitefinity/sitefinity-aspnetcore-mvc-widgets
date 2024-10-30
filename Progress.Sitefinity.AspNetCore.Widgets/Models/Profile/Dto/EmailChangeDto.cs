using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Profile.Dto
{
    internal class EmailChangeDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string ProviderName { get; set; }
    }
}
