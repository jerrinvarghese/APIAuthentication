using APIAuthentication.Interfaces;
using APIAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIAuthentication
{
    public class BusinessServices : IAuthInterface
    {
        public APIAuth GenerateToken(string username,string password)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddMinutes(5);
            var tokendomain = new APIAuth
            {
                UserName = username,
                Token = token,
                TknGenerateTime = issuedOn,
                TknExpiryTime = expiredOn
            };

            Step2017Entities ent = new Step2017Entities();
            APIAuth auth = ent.APIAuths.Where(c => c.UserName == username && c.Password == password).FirstOrDefault();
            if (auth != null)
            {
                auth.Token = token;
                auth.TknGenerateTime = issuedOn;
                auth.TknExpiryTime = expiredOn;
                ent.SaveChanges();
            }
            
            return tokendomain;
        }

        public bool ValidateToken(string username)
        {
            string token = "abcd";
            DateTime tokenexpirydate = DateTime.Now.AddHours(1);
            if(token!=null&&!(DateTime.Now>tokenexpirydate))
            {
                //update token expiry time in db
            }
            return true;
        }
    }
}