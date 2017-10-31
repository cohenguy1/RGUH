using System.Diagnostics;

namespace RGUH
{
    public partial class InstructionsPage : System.Web.UI.Page
    {
        public Stopwatch InstructionsStopwatch
        {
            get { return (Stopwatch)Session[SessionMap.InstructionsStopwatchStr]; }
            set { Session[SessionMap.InstructionsStopwatchStr] = value; }
        }
        
        public DbHandler dbHandler
        {
            get { return (DbHandler)Session[SessionMap.DbHandlerStr]; }
            set { Session[SessionMap.DbHandlerStr] = value; }
        }
    }
}