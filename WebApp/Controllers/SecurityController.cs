
using System;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Data.Access;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.Controllers
{
    public class SecurityController : Controller
    {
        private EFKullaniciDal _efKullaniciDal = new EFKullaniciDal();
        IHttpContextAccessor accessor;

        private EFUrunDal _efUrunDal = new EFUrunDal();

        public SecurityController(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        // GET: Security
        public bool MailFormat(string mail)
        {
            string mailformat = "\\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\\Z";
            if (!Regex.IsMatch(mail, mailformat, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        public bool MailVarMi(string mail)
        {
            if (MailFormat(mail) && _efKullaniciDal.MailKontrol(mail))
            {
                return false;
            }

            return true;
        }


        public bool OturumKontrol()
        {
            return string.IsNullOrEmpty(HttpContext.User.Identity.Name);
        }

        public int KullaniciId()
        {
           
            if (string.IsNullOrEmpty(accessor.HttpContext.User.Identity.Name)) return 0;
            return int.Parse(accessor.HttpContext.User.Identity.Name);
        }


        public void CookieCreate(string cookiename, string value)
        {
           
            HttpContext.Response.Cookies.Append(cookiename,value);
        
        }

        public string CookieGet(string cookiename)
        {
                var value = HttpContext.Request.Cookies[cookiename];
                return value;
        }

     /*  public void CookieDelete(string cookiename)
        {
            System.Web.HttpContext.Current.Response.Cookies[cookiename].Expires = DateTime.Now.AddYears(-1);
            System.Web.HttpContext.Current.Request.Cookies.Remove(cookiename);
        }*/


    }
}