<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RGUH.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input type="hidden" value="" name="clientScreenHeight" id="clientScreenHeight" />
    <input type="hidden" value="" name="clientScreenWidth" id="clientScreenWidth" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h2>You will recieve 20 cents for this HIT and it will take about 10 minutes of your time. </h2>
    <h2>Game Background</h2>

    <div style="text-align: center; width: 800px; margin: 0 auto;">
        <table style="text-align: center; max-width: 800px; font-size: large">
            <tr>
                <td>You have decided to open up a restaurant.</td>
            </tr>
            <tr>
                <td>In this game you and your Human Resource (HR) executive are in charge of hiring waiters for the restaurant. The HR executive will be doing the interviewing and hiring and you will choose the uniform for each position.</td>
            </tr>
            <tr>
                <td>The number of waiters you need to hire is 10 on average and 20 in worst case.</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>For each position you have 10 candidates, ranked from 1 to 10 (where "1" is the best and "10" is the worst). Your (and the HR executive's) interest is to pick highly 
                            qualified (as possible) workers for the different positions.<br />
                    <br />
                    <asp:Label ID="backgroundText" runat="server"
                        Text="When a candidate is interviewed, the HR executive must decide whether to hire him or not."></asp:Label>
                    <br />
                    <asp:Label ID="backgroundText2" runat="server"
                        Text="If the candidate is rejected, he leaves forever and cannot be recalled."></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>

            <tr>
                <td>
                    <b><span style="font-size: 20px;">You will receive an additional bonus after the game. </span></b>
                    <br />
                    <asp:Label ID="backgroundText3" runat="server" Text="Each worker hired will get you prize points according to his ranking. #1 ranked will get you 100 points, #2 will get you 90 points, and so on (#10 will get you 10 points). At the end, the average prize points achieved for the waiters is calculated, and you get a cent for each 4 prize points."></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Press 'Next' to continue. </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnNextToInfo" runat="server" Text="Next" OnClick="btnNextToInfo_Click" Enabled="true" /></td>
            </tr>
        </table>
    </div>
</asp:Content>