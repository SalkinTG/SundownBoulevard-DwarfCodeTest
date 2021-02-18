using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server;

namespace Client.Pages
{
    public class makeNewReservation : PageModel
    {
        private readonly ILogger<makeNewReservation> _logger;

        public makeNewReservation(ILogger<makeNewReservation> logger)
        {
            _logger = logger;
        }
    }
}
