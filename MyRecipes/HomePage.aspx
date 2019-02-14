<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="MyRecipes.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Recipes</title>
    <link rel="shortcut icon" href="#" />
    <link rel="stylesheet" href="MyRecipes.css" />
    <script src="MyRecipes.js"></script>
</head>
<body>
    <div id="pageHeader">
        <div id="headerForm">
            <input type="text" id="searchKeyword" placeholder="Ingredients" onkeydown="selectSuggestedValue(event);" onblur="hideSuggestions();"/> &nbsp; <!--- onkeydown="selectSuggestedValue(event);" --->
            <input type="button" class="inputButton" value="SEARCH" onclick="searchRecipes();" />
            <div id="suggestionList"></div>
        </div>
    </div>
    <div id="pageNavigation">
        <div id="guestNavigation" runat="server">
            <a href="/HomePage.aspx">H O M E</a> | <a href="javascript:void(0);" onclick="displayRegisterForm();">R E G I S T E R</a> | <a href="javascript:void(0);" onclick="displayLoginForm();">L O G I N</a>
        </div>
        <div id="userNavigation" runat="server">
            <a href="/HomePage.aspx">H O M E</a> | <a href="javascript:void(0);" onclick="displayAddedRecipe();">A D D &nbsp; R E C I P E</a> | <a href="javascript:void(0);" onclick="logout();">L O G O U T</a>
        </div>
    </div>
    <div id="pageContents"></div>
    <div id="pageFooter">
        Phuong Linh Bui<br/>
        Email: plb070@uowmail.edu.au<br/>
        LinkedIn: <a href="https://www.linkedin.com/in/phuonglinh-bui">phuonglinh-bui</a><br/>
        GitHub: <a href="https://github.com/plbkaitlyn">plbkaitlyn</a><br/>
    </div>

    <form id="HomePageForm" runat="server">
        <asp:scriptmanager runat="server">
            <Services>
               <asp:ServiceReference Path="~/RecipeService.svc" />
            </Services>
        </asp:scriptmanager>
    </form>
</body>
</html>
