﻿using LachlanBarclayNet.Areas.Admin.Controllers;
using LachlanBarclayNet.DAO.Standard;

using OtpNet;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

using System.Web;

namespace LachlanBarclayNet.DAO
{

    public class UserDAO
    {
        private lachlanbarclaynet2Context GetContext()
        {
            return new lachlanbarclaynet2Context(
                ConfigurationManager.ConnectionStrings["LbNet"].ConnectionString);
        }

        public void SetPassword(string username, string password)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                var u = context.Users.First(x => x.Username == username);
                u.Password = password;
                context.SaveChanges();
            }
        }

        public bool PasswordOk(string actualPassword, string suppliedPassword)
        {
            Hasher hasher = new Hasher();
            return hasher.VerifyHashedPassword(actualPassword, suppliedPassword);
        }

        public bool QrCodeOk(string actualQrCode, string suppliedQrCode)
        {
            if (actualQrCode == null)
                return true;

            long timeStepMatched = 0;
            byte[] decodedKey = Base32Encoding.ToBytes(actualQrCode);
            var otp = new Totp(decodedKey);

            return otp.VerifyTotp(suppliedQrCode, out timeStepMatched, new VerificationWindow(2, 2));
        }

        public bool Verify(string username, string password, string qrcode)
        {
            string[] names = { "john", "ben", "bob", "rod", "harry" };

            using (lachlanbarclaynet2Context context = GetContext())
            {
                var u = context.Users.FirstOrDefault(x => x.Username == username);

#if DEBUG
                u = new Users { };
#endif 
                if (u == null)
                    return false;

                if (u.LockedOutUntil.HasValue && u.LockedOutUntil.Value < DateTime.Now)
                    return false;

                bool authPassed = true;

#if RELEASE
                authPassed = (PasswordOk(u.Password, password) && QrCodeOk(u.QrCode, qrcode));
#endif

                if (authPassed)
                {
                    u.Attempts = 0;
                    u.LockedOutUntil = null;
                }
                else
                {
                    u.Attempts += 1;

                    if (u.Attempts > 3)
                        u.LockedOutUntil = DateTime.Now.AddMinutes(5);
                }

#if RELEASE
                context.SaveChanges();
#endif 

                return authPassed;
            }
        }

        public void SetQrCode(string username, string secretKey)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                var u = context.Users.First(x => x.Username == username);

                if (u != null)
                {
                    u.QrCode = secretKey;
                    context.SaveChanges();
                }
            }
        }
    }
}