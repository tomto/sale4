using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sale4.Controllers.API.Models;

namespace Sale4.Models
{
    public class PreActivityViewModel
    {
        public Fct_StaticHtml FctStaticHtml { get; set; }
        public List<Fct_StaticDetail> FctStaticDetails { get; set; }
    }


}