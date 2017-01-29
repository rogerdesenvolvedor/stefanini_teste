using ApplicationTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationTest.Helpers
{
    public class ComboHelper
    {
        private static StefaniniContext db = new StefaniniContext();

        private ComboHelper()
        {

        }


        public static List<SelectListItem> ComboCity(string value)
        {
            bool selectedItem = false;
            var select = from c in db.City select c;

            List<SelectListItem> dropdownResult = new List<SelectListItem>();
            foreach (City item in select)
            {
                selectedItem = item.Id.Equals(value);
                dropdownResult.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = selectedItem });
            }

            return dropdownResult;

        }

        public static List<SelectListItem> ComboRegion(string value)
        {
            bool selectedItem = false;
            var select = from c in db.Region select c;

            List<SelectListItem> dropdownResult = new List<SelectListItem>();
            foreach (Region item in select)
            {
                selectedItem = item.Id.Equals(value);
                dropdownResult.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = selectedItem });
            }

            return dropdownResult;

        }

        public static List<SelectListItem> ComboClassification(string value)
        {
            bool selectedItem = false;
            var select = from c in db.Classification select c;

            List<SelectListItem> dropdownResult = new List<SelectListItem>();
            foreach (Classification item in select)
            {
                selectedItem = item.Id.Equals(value);
                dropdownResult.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = selectedItem });
            }

            return dropdownResult;

        }

        public static List<SelectListItem> ComboSeller(string value)
        {
            bool selectedItem = false;
            var select = from c in db.UserSys select c;

            List<SelectListItem> dropdownResult = new List<SelectListItem>();
            foreach (UserSys item in select)
            {
                selectedItem = item.Id.Equals(value);
                dropdownResult.Add(new SelectListItem { Text = item.Login, Value = item.Id.ToString(), Selected = selectedItem });
            }

            return dropdownResult;

        }

        public static SelectList ComboGender(string gender)
        {
            SelectList selectedList;
            IList<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem() { Value = "1", Text = "Masculine" });
            lista.Add(new SelectListItem() { Value = "2", Text = "Feminine" });

            if (!string.IsNullOrEmpty(gender) && gender.Trim() != "")
                return selectedList = new SelectList(lista, "Value", "Text", gender);
            else
                return selectedList = new SelectList(lista, "Value", "Text");
        }

    }
}