using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Areas.Login.Models;

namespace WebApplication3.Areas.CitySelect.Models
{
    public class CitySelectService
    {
        private FAQEntities db = new FAQEntities();
        public List<BS_Zip> GetCityList() {
            List<BS_Zip> cityList = db.BS_Zip.ToList();
            cityList = cityList.GroupBy(c => c.intCityCode)
                   .Select(c => c.First())
                   .ToList();
            return cityList;
        }
        public List<BS_Zip> GetAreaList(int intCityCode)
        {
            List<BS_Zip> cityList = db.BS_Zip.Where(
                b => b.intCityCode.Equals(intCityCode))
                .ToList();
            return cityList;
        }

    }
}