﻿namespace RGUH
{
    public partial class ResolutionProblem : System.Web.UI.Page
    {
        public DbHandler dbHandler
        {
            get { return (DbHandler)Session[SessionMap.DbHandlerStr]; }
            set { Session[SessionMap.DbHandlerStr] = value; }
        }
    }
}