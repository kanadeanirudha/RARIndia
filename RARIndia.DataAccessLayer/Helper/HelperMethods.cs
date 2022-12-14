﻿using RARIndia.DataAccessLayer.DataEntity;

using System;
using System.Web;

namespace RARIndia.DataAccessLayer.Helpers
{
    public static class HelperMethods
    {
        /// <summary>
        /// Get Login User Id from Request Headers
        /// </summary>
        /// <returns>Login User Id</returns>
        public static int GetLoginUserId()
        {
            int userId = 0;
            var headers = HttpContext.Current.Request.Headers;

            int.TryParse(headers["RARIndia-UserId"], out userId);

            return userId;
        }


        //Gets current context object
        public static RARIndiaEntities CurrentContext
        {
            get
            {
                return GetObjectContext();
            }
        }

        // Get current datetime.
        public static DateTime GetEntityDateTime() => DateTime.Now;

        //Create the Context object, return the context.
        private static RARIndiaEntities GetObjectContext()
        {
            if (HttpContext.Current != null)
            {
                string objectContextKey = "ocm_" + HttpContext.Current.GetHashCode().ToString("x");
                if (!HttpContext.Current.Items.Contains(objectContextKey))
                    HttpContext.Current.Items.Add(objectContextKey, new RARIndiaEntities());
                return HttpContext.Current.Items[objectContextKey] as RARIndiaEntities;
            }
            else
                return new RARIndiaEntities();
        }

        //Use this method very cautiously as it will create new DB context object each time which will be a costly operation.
        //This method should only be utilized when you need to access DB context from a thread/async call which is using a different thread than the APIs HTTP request thread.
        //It is recommended to use the new DB context when there is immense need of it otherwise it should be avoided.
        public static IDBContext GetNewDBContext() => new RARIndiaEntities();
    }
}
