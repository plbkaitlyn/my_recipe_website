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
    if (recipes.length === 0) {
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

    var count = 0;
    for (var i = recipes.length - 1; i >= 0; i--) {
        count++;
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
        recipeImage.setAttribute("src", recipes[i].Image);
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

        if (count === 6) break;
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
    recipeImage.setAttribute("src", recipe.Image);
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
        if (comment.value !== "") {
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
    if (result === "") {
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
    if (result === "") {
        window.location.reload(true);
    }
    else {
        var loginForm = document.getElementById("loginForm");
        var errorMessage = document.getElementById("loginError");
        if (errorMessage === null) {            
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

function register(username, email, password, repassword) {
    RecipeService.Register(username, email, password, repassword, onRegister);
}

function onRegister(result) {
    if (result === "") {
        displayLoginForm();
    }
    else {
        var registerForm = document.getElementById("registerForm");
        var errorMessage = document.getElementById("registerError");
        if (errorMessage === null) {
            errorMessage = document.createElement("span");
            errorMessage.setAttribute("id", "registerError");
            errorMessage.setAttribute("class", "errorMessage");
            registerForm.appendChild(document.createElement("br"));
            registerForm.appendChild(document.createElement("br"));
            registerForm.appendChild(errorMessage);
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

function displayRegisterForm() {
    var contents = document.getElementById("pageContents");
    while (contents.hasChildNodes()) {
        contents.removeChild(contents.lastChild);
    }
    
    var registerForm = document.createElement("div");
    registerForm.setAttribute("id", "registerForm");
    contents.appendChild(registerForm);

    var userText = document.createElement("span");
    userText.innerText = "Username: ";
    registerForm.appendChild(userText);
    registerForm.appendChild(document.createElement("br"));
    var userInput = document.createElement("input");
    userInput.setAttribute("type", "text");
    userInput.setAttribute("id", "usernameInput");
    userInput.setAttribute("class", "inputText");
    registerForm.appendChild(userInput);
    registerForm.appendChild(document.createElement("br"));
    registerForm.appendChild(document.createElement("br"));

    var emailAddress = document.createElement("span");
    emailAddress.innerText = "Email address: ";
    registerForm.appendChild(emailAddress);
    registerForm.appendChild(document.createElement("br"));
    var emailInput = document.createElement("input");
    emailInput.setAttribute("type", "email");
    emailInput.setAttribute("id", "emailInput");
    emailInput.setAttribute("class", "inputText");
    registerForm.appendChild(emailInput);
    registerForm.appendChild(document.createElement("br"));
    registerForm.appendChild(document.createElement("br"));
    
    var passwordText = document.createElement("span");
    passwordText.innerText = "Password: ";
    registerForm.appendChild(passwordText);
    registerForm.appendChild(document.createElement("br"));
    var passwordInput = document.createElement("input");
    passwordInput.setAttribute("type", "password");
    passwordInput.setAttribute("id", "userpasswordInput");
    passwordInput.setAttribute("class", "inputText");
    registerForm.appendChild(passwordInput);
    registerForm.appendChild(document.createElement("br"));
    registerForm.appendChild(document.createElement("br"));

    var passwordConfirmText = document.createElement("span");
    passwordConfirmText.innerText = "Confirm password: ";
    registerForm.appendChild(passwordConfirmText);
    registerForm.appendChild(document.createElement("br"));
    var passwordConfirmInput = document.createElement("input");
    passwordConfirmInput.setAttribute("type", "password");
    passwordConfirmInput.setAttribute("id", "passwordConfirmInput");
    passwordConfirmInput.setAttribute("class", "inputText");
    registerForm.appendChild(passwordConfirmInput);
    registerForm.appendChild(document.createElement("br"));
    registerForm.appendChild(document.createElement("br"));

    var registerButton = document.createElement("input");
    registerButton.setAttribute("type", "button");
    registerButton.setAttribute("value", "   Register   ");
    registerButton.setAttribute("class", "inputButton");
    registerButton.addEventListener("click", function () {
        var username = document.getElementById("usernameInput");
        var email = document.getElementById("emailInput");
        var password = document.getElementById("userpasswordInput");
        var repassword = document.getElementById("passwordConfirmInput");

        register(username.value, email.value, password.value, repassword.value);
    });
    registerForm.appendChild(registerButton);
    
}

function displayAddedRecipe() {
    var contents = document.getElementById("pageContents");
    while (contents.hasChildNodes()) {
        contents.removeChild(contents.lastChild);
    }

    // Add recipe form
    var recipeForm = document.createElement("div");
    recipeForm.setAttribute("id", "recipeForm");
    contents.appendChild(recipeForm);

    // Image
    var imageText = document.createElement("span");
    imageText.innerText = "Image link: ";
    recipeForm.appendChild(imageText);
    recipeForm.appendChild(document.createElement("br"));
    var imageInput = document.createElement("input");
    imageInput.setAttribute("type", "text");
    imageInput.setAttribute("id", "imageInput");
    imageInput.setAttribute("class", "inputText");
    recipeForm.appendChild(imageInput);
    recipeForm.appendChild(document.createElement("br"));
    recipeForm.appendChild(document.createElement("br"));

    // Name
    var recipeNameText = document.createElement("span");
    recipeNameText.innerText = "Recipe name: ";
    recipeForm.appendChild(recipeNameText);
    recipeForm.appendChild(document.createElement("br"));
    var recipeNameInput = document.createElement("input");
    recipeNameInput.setAttribute("type", "text");
    recipeNameInput.setAttribute("id", "recipeNameInput");
    recipeNameInput.setAttribute("class", "inputText");
    recipeForm.appendChild(recipeNameInput);
    recipeForm.appendChild(document.createElement("br"));
    recipeForm.appendChild(document.createElement("br"));

    // Description
    var descriptionText = document.createElement("span");
    descriptionText.innerText = "Brief description about the dish: ";
    recipeForm.appendChild(descriptionText);
    recipeForm.appendChild(document.createElement("br"));
    var descriptionInput = document.createElement("textarea");
    descriptionInput.setAttribute("id", "descriptionInput");
    descriptionInput.setAttribute("class", "inputTextArea");
    descriptionInput.setAttribute("rows", "3");
    descriptionInput.setAttribute("cols", "60");
    recipeForm.appendChild(descriptionInput);
    recipeForm.appendChild(document.createElement("br"));
    recipeForm.appendChild(document.createElement("br"));

    // Time
    var timeText = document.createElement("span");
    timeText.innerText = "Time to cook: ";
    recipeForm.appendChild(timeText);
    recipeForm.appendChild(document.createElement("br"));
    var timeInput = document.createElement("input");
    timeInput.setAttribute("type", "text");
    timeInput.setAttribute("id", "timeInput");
    timeInput.setAttribute("class", "inputText");
    recipeForm.appendChild(timeInput);
    recipeForm.appendChild(document.createElement("br"));
    recipeForm.appendChild(document.createElement("br"));

    // Ingredients
    var ingredientsText = document.createElement("span");
    ingredientsText.innerText = "List of ingredients: ";
    recipeForm.appendChild(ingredientsText);
    recipeForm.appendChild(document.createElement("br"));
    var ingredientsInput = document.createElement("textarea");
    ingredientsInput.setAttribute("id", "ingredientsInput");
    ingredientsInput.setAttribute("class", "inputTextArea");
    ingredientsInput.setAttribute("rows", "10");
    ingredientsInput.setAttribute("cols", "60");
    recipeForm.appendChild(ingredientsInput);
    recipeForm.appendChild(document.createElement("br"));
    recipeForm.appendChild(document.createElement("br"));

    // Instructions
    var instructionText = document.createElement("span");
    instructionText.innerText = "Instructions: ";
    recipeForm.appendChild(instructionText);
    recipeForm.appendChild(document.createElement("br"));
    var instructionInput = document.createElement("textarea");
    instructionInput.setAttribute("id", "instructionInput");
    instructionInput.setAttribute("class", "inputTextArea");
    instructionInput.setAttribute("rows", "10");
    instructionInput.setAttribute("cols", "60");
    recipeForm.appendChild(instructionInput);
    recipeForm.appendChild(document.createElement("br"));
    recipeForm.appendChild(document.createElement("br"));
    
    var addButton = document.createElement("input");
    addButton.setAttribute("type", "button");
    addButton.setAttribute("value", "   ADD RECIPE   ");
    addButton.setAttribute("class", "inputButton");
    addButton.addEventListener("click", function () {
        var name = document.getElementById("recipeNameInput");
        var description = document.getElementById("descriptionInput");
        var time = document.getElementById("timeInput");
        var ingredients = document.getElementById("ingredientsInput");
        var instruction = document.getElementById("instructionInput");
        var image = document.getElementById("imageInput");
        addRecipe(name.value, description.value, time.value, ingredients.value, instruction.value, image.value);    //create a new Recipe object and add to the array list
    });
    recipeForm.appendChild(addButton);
    
}

function addRecipe(name, description, time, ingredients, instruction, image) {
    RecipeService.AddRecipe(name, description, time, ingredients, instruction, image, onAddRecipe);
}

function onAddRecipe(result) {
    var recipe = JSON.parse(result);
    if (recipe === null) {
        var recipeForm = document.getElementById("recipeForm");
        var errorMessage = document.getElementById("recipeError");
        if (errorMessage === null) {
            errorMessage = document.createElement("span");
            errorMessage.setAttribute("id", "recipeError");
            errorMessage.setAttribute("class", "errorMessage");
            recipeForm.appendChild(document.createElement("br"));
            recipeForm.appendChild(document.createElement("br"));
            recipeForm.appendChild(errorMessage);
        }
        errorMessage.innerText = result;
    }
    else {
        displayRecipe(recipe);
    }
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
};
