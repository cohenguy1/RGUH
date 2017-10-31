<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProceedToGame.aspx.cs" Inherits="RGUH.ProceedToGame" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Game Instructions</h2>
    <div style="text-align: center; width: 750px; margin: 0 auto;">
        <table style="text-align: left; width: 750px;" border="1">
            <tr>
                <td>
                    <br />
                    &nbsp;We will now proceed to the real game.
                            <br />
                    <br />
                    &nbsp;Remember, you won't see each interview, only the hired candidate's rank.
                    <br />
                    <br />
                    &nbsp;After a candidate is hired, you will choose the uniform for the position.
                    <br />
                    <br />
                    <br />

                    &nbsp;Press 'Next' to continue.
                            <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Button ID="btnNextToGame" runat="server" Text="Next" OnClick="btnNextToGame_Click" />

</asp:Content>
