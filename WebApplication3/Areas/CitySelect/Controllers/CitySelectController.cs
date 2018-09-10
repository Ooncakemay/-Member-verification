using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Areas.CitySelect.Models;

namespace WebApplication3.Areas.CitySelect.Controllers
{
    public class CitySelectController : Controller
    {
        CitySelectService citySelectService = new CitySelectService();

        public ActionResult CityList()
        {
            return PartialView("_CityList", citySelectService.GetCityList());
        }

        public ActionResult AreaList(int intCityCode)
        {
            return PartialView("_AreaList", citySelectService.GetAreaList(intCityCode));
        }
    }
}