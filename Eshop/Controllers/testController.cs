﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Controllers
{
    
    public class testController : Controller
    {
        // GET: test
        [Authorize]
        public ActionResult Test1()
        {
            return View();
        }
        [Authorize(Roles ="admin")]
        public ActionResult Test2()
        {
            return View();
        }
    }
}