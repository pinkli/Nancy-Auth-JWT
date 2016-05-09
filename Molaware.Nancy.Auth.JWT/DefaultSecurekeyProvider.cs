using System;

namespace Molaware.Nancy.Auth.JWT
{
    class DefaultSecurekeyProvider : ISecurekeyProvider
    {
        #region ISecurekeyProvider implementation

        public string GetSecurekey()
        {
            return System.Configuration.ConfigurationManager.AppSettings["securekey"] ?? "Molaware0x10";
        }


        #endregion


    }
}

