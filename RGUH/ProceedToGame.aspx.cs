using RGUH.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RGUH
{
    public partial class ProceedToGame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dbHandler.UpdateTimesTable(GameState.GameStart);
            }
        }

        protected void btnNextToGame_Click(object sender, EventArgs e)
        {
            GameMode = GameMode.Advisor;
            Response.Redirect("Game.aspx");
        }

    }
}