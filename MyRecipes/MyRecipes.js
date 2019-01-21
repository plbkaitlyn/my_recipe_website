function getRecipeHeadings() {
    RecipeService.GetRecipeHeadings(onGetRecipeHeadings);
}

function onGetRecipeHeadings(result) {
    var recipes = JSON.parse(result);
    displayRecipeHeadings(recipes);
}

function searchRecipes() {
    var keyword = document.getElementById("searchKeyword");
    RecipeService.SearchRecipes(keyword.value, onSearchRecipes);
}

function onSearchRecipes(result) {
    var recipes = JSON.parse(result);
    if (recipes.length == 0) {
        var contents = document.getElementById("pageContents");
        while (contents.hasChildNodes()) {
            contents.removeChild(contents.lastChild);
        }
        contents.innerHTML = "<br><br>Your search did not return any results. Please try again with other keyworks<br><br>";
    }
    else {
        displayRecipeHeadings(recipes);
    }
}

function displayRecipeHeadings(recipes) {
    var contents = document.getElementById("pageContents");
    while (contents.hasChildNodes()) {
        contents.removeChild(contents.lastChild);
    }

    for (var i = 0; i < recipes.length; i++) {
        // Heading panel
        var recipeHeadingPanel = document.createElement("div");
        recipeHeadingPanel.setAttribute("class", "recipeHeadingPanel");
        contents.appendChild(recipeHeadingPanel); //add cai div vua tao vao panel/ page content, 1 recipe vao 1 div con

        // Image
        var recipeLink = document.createElement("a");
        recipeLink.setAttribute("href", "javascript:void(0);");
        recipeLink.setAttribute("recipeId", recipes[i].Id); //property id cua recipe
        recipeLink.addEventListener("click", function () {
            getRecipe(this.getAttribute("recipeId"));
        });
        var recipeImage = document.createElement("img");
        recipeImage.setAttribute("class", "recipeImage");
        recipeImage.setAttribute("src", "images/" + recipes[i].Image);
        recipeLink.appendChild(recipeImage);
        recipeHeadingPanel.appendChild(recipeLink);
        recipeHeadingPanel.appendChild(document.createElement("br"));

        // Name
        var recipeName = document.createElement("span");
        recipeName.setAttribute("class", "recipeName");
        recipeName.innerText = recipes[i].Name;
        recipeHeadingPanel.appendChild(recipeName);
        recipeHeadingPanel.appendChild(document.createElement("br"));

        // Description
        var recipeDescription = document.createElement("span");
        recipeDescription.innerText = strimText(recipes[i].Description);
        recipeHeadingPanel.appendChild(recipeDescription);
        recipeHeadingPanel.appendChild(document.createElement("br"));
    }
}

function strimText(text) {
    if (text.length > 100) {
        var position = text.indexOf(" ", 100);
        return text.substr(0, position) + "...";
    }
    return text;
}

function getRecipe(recipeId) {
    RecipeService.GetRecipe(recipeId, onGetRecipe);
}

function onGetRecipe(result) {
    var recipe = JSON.parse(result);
    displayRecipe(recipe);
}

function displayRecipe(recipe) {
    var contents = document.getElementById("pageContents");
    while (contents.hasChildNodes()) {
        contents.removeChild(contents.lastChild);
    }
    
    // Image panel
    var recipeImagePanel = document.createElement("div");
    recipeImagePanel.setAttribute("id", "recipeImagePanel");
    contents.appendChild(recipeImagePanel);

    // Image
    var recipeImage = document.createElement("img");
    recipeImage.setAttribute("class", "recipeImage");
    recipeImage.setAttribute("src", "images/" + recipe.Image);
    recipeImagePanel.appendChild(recipeImage);

    // Recipe contents panel
    var recipeContentsPanel = document.createElement("div");
    recipeContentsPanel.setAttribute("id", "recipeContentsPanel");
    contents.appendChild(recipeContentsPanel);

    // Name
    var recipeName = document.createElement("span");
    recipeName.setAttribute("class", "recipeName");
    recipeName.innerHTML = recipe.Name + "<br><br>";
    recipeContentsPanel.appendChild(recipeName);

    // Description
    var recipeDescription = document.createElement("span");
    recipeDescription.innerHTML = recipe.Description + "<br><br>";
    recipeContentsPanel.appendChild(recipeDescription);

    // Time
    var recipeTime = document.createElement("span");
    recipeTime.innerHTML = "<b>Time:</b> " + recipe.Time + " minutes<br><br>";
    recipeContentsPanel.appendChild(recipeTime);

    // Ingredients    
    var recipeIngredients = document.createElement("span");
    recipeIngredients.innerHTML = "<b>Ingredients:</b><br>" + convertToList(recipe.Ingredients);
    recipeContentsPanel.appendChild(recipeIngredients);

    // Instructions
    var recipeInstructions = document.createElement("span");
    recipeInstructions.innerHTML = "<b>Instructions:</b><br>" + convertToList(recipe.Instructions);
    recipeContentsPanel.appendChild(recipeInstructions);

    // Comments
    var recipeComments = document.createElement("span");
    recipeComments.innerHTML = "<b>Comments: </b><br><br>";
    recipeContentsPanel.appendChild(recipeComments);
    var userComments = recipe.UserComments;
    for (var i = 0; i < userComments.length; i++) {
        // Comment
        var comment = document.createElement("span");
        comment.setAttribute("class", "userComment");
        comment.innerHTML = userComments[i].Comment + "<br>";
        recipeContentsPanel.appendChild(comment);
        // Username
        var username = document.createElement("span");
        username.setAttribute("class", "userName");
        username.innerHTML = userComments[i].UserName + "<br><br>";
        recipeContentsPanel.appendChild(username);
    }

    var commentText = document.createElement("span");
    commentText.innerHTML = "Anything to say about this recipe?<br><br>";
    recipeContentsPanel.appendChild(commentText);

    var commentInput = document.createElement("textarea");
    commentInput.setAttribute("id", "commentInput");
    commentInput.setAttribute("class", "inputTextArea");
    commentInput.setAttribute("rows", "5");
    commentInput.setAttribute("cols", "60");
    recipeContentsPanel.appendChild(commentInput);
    recipeContentsPanel.appendChild(document.createElement("br"));
    recipeContentsPanel.appendChild(document.createElement("br"));

    var commentButton = document.createElement("input");
    commentButton.setAttribute("id", "commentButton");
    commentButton.setAttribute("type", "button");
    commentButton.setAttribute("value", "ADD COMMENT");
    commentButton.setAttribute("class", "inputButton");
    commentButton.setAttribute("recipeId", recipe.Id);
    commentButton.addEventListener("click", function () {        
        var comment = document.getElementById("commentInput");
        if (comment.value != "") {
            var recipeId = this.getAttribute("recipeId");
            addComment(recipeId, comment.value);
        }
    });
    recipeContentsPanel.appendChild(commentButton);
}

function convertToList(string) {
    return "<ul><li>" + string.replace(/\n/g, "</li><li>") + "</li></ul>";
}

function addComment(recipeId, comment) {
    RecipeService.AddComment(recipeId, comment, onAddComment);
}

function onAddComment(result) {
    if (result == "") {
        var commentButton = document.getElementById("commentButton");
        var recipeId = commentButton.getAttribute("recipeId");        
        getRecipe(recipeId);
    }
    else {
        displayLoginForm();
    }
}

function login(username, password) {
    RecipeService.Login(username, password, onLogin);
}

function onLogin(result) {
    if (result == "") {
        window.location.reload(true);
    }
    else {
        var loginForm = document.getElementById("loginForm");
        var errorMessage = document.getElementById("loginError");
        if (errorMessage == null) {            
            errorMessage = document.createElement("span");
            errorMessage.setAttribute("id", "loginError");
            errorMessage.setAttribute("class", "errorMessage");
            loginForm.appendChild(document.createElement("br"));
            loginForm.appendChild(document.createElement("br"));
            loginForm.appendChild(errorMessage);
        }
        errorMessage.innerText = result;
    }
}

function displayLoginForm() {
    var contents = document.getElementById("pageContents");
    while (contents.hasChildNodes()) {
        contents.removeChild(contents.lastChild);
    }

    var loginForm = document.createElement("div");
    loginForm.setAttribute("id", "loginForm");
    contents.appendChild(loginForm);

    var userText = document.createElement("span");
    userText.innerText = "Username: ";
    loginForm.appendChild(userText);
    loginForm.appendChild(document.createElement("br"));
    var userInput = document.createElement("input");
    userInput.setAttribute("type", "text");
    userInput.setAttribute("id", "userInput");
    userInput.setAttribute("class", "inputText");
    loginForm.appendChild(userInput);
    loginForm.appendChild(document.createElement("br"));
    loginForm.appendChild(document.createElement("br"));

    var passwordText = document.createElement("span");    
    passwordText.innerText = "Password: ";
    loginForm.appendChild(passwordText);
    loginForm.appendChild(document.createElement("br"));
    var passwordInput = document.createElement("input");
    passwordInput.setAttribute("type", "password");
    passwordInput.setAttribute("id", "passwordInput");
    passwordInput.setAttribute("class", "inputText");
    loginForm.appendChild(passwordInput);
    loginForm.appendChild(document.createElement("br"));
    loginForm.appendChild(document.createElement("br"));

    var loginButton = document.createElement("input");
    loginButton.setAttribute("type", "button");
    loginButton.setAttribute("value", "   LOGIN   ");
    loginButton.setAttribute("class", "inputButton");
    loginButton.addEventListener("click", function () {
        var username = document.getElementById("userInput");
        var password = document.getElementById("passwordInput");
        login(username.value, password.value);
    });
    loginForm.appendChild(loginButton);    
}

function logout() {
    RecipeService.Logout(onLogout);
}

function onLogout(result)
{
    window.location.reload(true);
}

window.onload = function () {
    getRecipeHeadings();
}
