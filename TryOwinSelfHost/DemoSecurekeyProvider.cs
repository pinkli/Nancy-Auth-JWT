﻿using System;
using Molaware.Nancy.Auth.JWT;

namespace TryOwinSelfHost
{
    public class DemoSecurekeyProvider : ISecurekeyProvider
    {
        #region ISecurekeyProvider implementation

        public string GetSecurekey()
        {
            return "my secure key";
        }



        #endregion


    }
}

