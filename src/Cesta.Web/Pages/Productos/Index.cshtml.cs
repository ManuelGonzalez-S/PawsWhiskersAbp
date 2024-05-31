using Cesta.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace Cesta.Web.Pages.Productos
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {

        #region Constructor

        public IndexModel()
        {
        }
        #endregion

        #region Get
        public void OnGetAsync()
        {
        }

        #endregion
    }
}
