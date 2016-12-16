<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoggedIn.aspx.cs" Inherits="DealQuestionAnswer.UI.LoggedIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/MainStyleSheet.css" rel="stylesheet" />
    <link href="../Content/loggedInPage.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.1.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <header>
                <hgroup id="headerText">
                    <h1>AjkerDeal.com</h1>
                </hgroup>
            </header>
            <nav>

            </nav>
            <div id="content">
                <div id="contentHead"></div>
                <div id="contentNav"></div>
                <div id="contentBody">
                    <table>
                        <tr>
                            <td><label for="email">Email</label></td>
                            <td><input type="email" id="email" name="email" placeholder="Enter your email" /></td>
                        </tr>
                        <tr>
                            <td><label for="email">Password</label></td>
                            <td><input type="password" id="password" name="password" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><button id="login" name="login" onclick="LoggedIn()">Log In</button></td>
                        </tr>
                    </table>                    
                </div>
                <div id="contentAside"></div>
            </div>
            <aside>

            </aside>
            <footer>

            </footer>
        </div>
    </form>
</body>
</html>
<script>
    function LoggedIn() {
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: 'LoggedIn.aspx'
        })
    }
</script>
