INSERT INTO USERDATA VALUES ('linh', 'linh@gmail.com', 'linh1');
INSERT INTO USERDATA VALUES ('julie', 'julie@yahoo.com', 'julie1');
INSERT INTO USERDATA VALUES ('josh', 'josh@gmail.com', 'josh1');
INSERT INTO USERDATA VALUES ('evelyn', 'evelyn@gmail.com', 'evelyn1');
INSERT INTO USERDATA VALUES ('dxt', 'dxt@gmail.com', 'dxt1');
INSERT INTO USERDATA VALUES ('helen', 'helen@hotmail.com', 'helen1');
INSERT INTO USERDATA VALUES ('jess', 'jess@gmail.com', 'jess1');
INSERT INTO USERDATA VALUES ('bella', 'bella@yahoo.com', 'bella1');
INSERT INTO USERDATA VALUES ('alex', 'alex@yahoo.com', 'alex1');
INSERT INTO USERDATA VALUES ('daniel', 'daniel@gmail.com', 'daniel1');
INSERT INTO USERDATA VALUES ('admin', 'admin@gmail.com', 'admin1');



SET @recipeID = 01,
	@re_name = "Chicken Pot Pie",
    @description = "This thick, hearty chicken and veggie slow cooker pot pie eats like a meal, filled with onion, carrots, celery, and peas",
	@re_time = 60,
	@ingredients = "2 (10.75 ounce) cans condensed cream of chicken soup \n 1 1/2 cups chopped carrots \n 1 1/2 cups chopped celery \n 1 yellow onion, chopped",
	@instructions = "Combine cream of chicken soup, carrots, celery, onion, stock, parsley, paprika, oregano, salt, and black pepper in a slow cooker and stir to combine \n Cook on Low for 7 1/2 hours \n Continue cooking on Low until heated through",
	@image = "images\\6119674.jpg",
	@userName = "admin";
INSERT INTO RECIPE VALUES (@recipeID, @re_name, @description, @re_time, @ingredients, @instructions, @image, @userName);


SET @recipeID = 02,
	@re_name = "Polish Borscht",
    @description = "This delicious vegetarian borscht is made with beets and dried mushrooms and is a traditional dish in Poland on Christmas Eve",
	@re_time = 30,
	@ingredients = "6 dried wild mushrooms \n 8 medium beets, trimmed \n 4 quarts water, or more as needed \n 2 cloves garlic, halved",
	@instructions = "Place dried mushrooms in a bowl, cover with cold water, and soak for 30 minutes \n While mushrooms are soaking, place beets in a pot, cover with water, and bring to a boil. Reduce heat and simmer until tender, about 30 minutes \n Place sliced beets in a large pot and cover with 4 quarts water. Add drained mushrooms, onions, garlic, allspice, bay leaves, salt, and pepper",
	@image = "images\\5136042.jpg",
	@userName = "admin";
INSERT INTO RECIPE VALUES (@recipeID, @re_name, @description, @re_time, @ingredients, @instructions, @image, @userName);

SET @recipeID = 03,
	@re_name = "Butternut Bisque",
    @description = "For a winter special occasion or just a weekday meal, this butternut bisque makes a perfect, warming starter course",
	@re_time = 45,
	@ingredients = "3 tablespoons butter \n 1 large onion, diced \n 1 teaspoon kosher salt, plus more to taste, divided \n 1 (2 pound) butternut squash",
	@instructions = "Melt butter in a pot over medium-low heat. Add onions and a large pinch of salt \n Cut off ends of squash. Carefully cut squash in half lengthwise and remove the seeds. Peel the squash with a vegetable peeler \n Raise heat under pot to medium-high. Stir in tomato paste; cook and stir until mixture begins to caramelize and turn brown, about 2 minutes",
	@image = "images\\4684123.jpg",
	@userName = "admin";
INSERT INTO RECIPE VALUES (@recipeID, @re_name, @description, @re_time, @ingredients, @instructions, @image, @userName);

SET @recipeID = 04,
	@re_name = "Homemade Chicken Parmigiana",
    @description = "I received this recipe several years ago from a family member who swore it was a great romantic meal. She was right. My husband asks me to make this meal every time we have a date night. The quantity of ingredients look intimidating, but don't let them fool you. It's easy and delicious!",
	@re_time = 40,
	@ingredients = "1 tablespoon butter \n 1 egg, beaten \n 3 (5 ounce) skinless, boneless chicken breast halves \n 3/4 cup shredded Mozzarella cheese",
	@instructions = "Melt butter in a saucepan over medium heat. Stir in garlic and onion, and cook until the onion has softened and turned translucent, about 2 minutes. Pour in diced tomatoes and sugar. \n Stir together bread crumbs, 2 tablespoons Parmesan cheese, and dried oregano; set aside. In a small bowl, whisk together egg and 2 tablespoons milk until blended \n Heat olive oil in a large skillet over medium heat. Add chicken breasts and cook on both sides until they reach an internal temperature of 160 degrees F (70 degrees C)",
	@image = "images\\4473147.jpg",
	@userName = "admin";
INSERT INTO RECIPE VALUES (@recipeID, @re_name, @description, @re_time, @ingredients, @instructions, @image, @userName);

SET @recipeID = 05,
	@re_name = "Tuscan Pork Tenderloin",
	@description = "This is a very easy weeknight pork tenderloin recipe that is also keto-friendly. Serve with rice for a complete meal, or fried rice for a treat.",
    @re_time = 120,
    @ingredients = "4 teaspoons garlic, minced \n 1 teaspoon ground black pepper \n 4 pounds pork tenderloin \n 2 teaspoons dried oregano",
    @instructions = "Combine garlic, rosemary, oregano, salt, and pepper in a small bowl. Rub spice mixture all over the pork tenderloin \n Bake in the preheated oven until pork is slightly pink in the center, 20 to 25 minutes. An instant-read thermometer inserted into the center should read at least 145 degrees F (63 degrees C)",
    @image = "images\\6155203.jpg",
	@userName = "admin";
INSERT INTO RECIPE VALUES (@recipeID, @re_name, @description, @re_time, @ingredients, @instructions, @image, @userName);
    
SET @recipeID = 06,
    @re_name = "Easy Pineapple Chicken",
    @description = "An easy weeknight dinner, this quick chicken stir-fry with pineapple is as delicious as it is colorful! Serve with rice for a complete meal, or fried rice for a treat. Add red chile flakes to the mix if you want something sweet and spicy!",
    @re_time = 20,
    @ingredients = "3 tablespoons soy sauce \n 1 pound boneless, skinless chicken breast, cut into strips \n 1 red bell pepper, cubed \n 3 tablespoons olive oil, divided",
    @instructions = "Combine soy sauce, 2 tablespoons olive oil, paprika, and salt in a bowl \n Heat the remaining 1 tablespoon of olive oil in a wok. Add bell pepper and stir-fry for 3 minutes. Add scallions and cook for 2 more minutes",
    @image = "images\\5151529.jpg",
	@userName = "admin";
INSERT INTO RECIPE VALUES (@recipeID, @re_name, @description, @re_time, @ingredients, @instructions, @image, @userName);

INSERT INTO USERCOMMENT VALUES (01, "josh", "Good!");
INSERT INTO USERCOMMENT VALUES (01, "evelyn", "It takes too much time.");
INSERT INTO USERCOMMENT VALUES (02, "evelyn", "I love it!");
INSERT INTO USERCOMMENT VALUES (03, "evelyn", "The instruction is not really clear to me :-(");
INSERT INTO USERCOMMENT VALUES (03, "josh", "Thanks!");
INSERT INTO USERCOMMENT VALUES (04, "josh", "Do not really like the taste");
INSERT INTO USERCOMMENT VALUES (05, "evelyn", "Great!");
/*SET @id = 01;
INSERT INTO USERCOMMENT VALUES (@id, 01, "josh", "Good!");
SET @id = @id + 1;
INSERT INTO USERCOMMENT VALUES (@id, 01, "evelyn", "It takes too much time.");
SET @id = @id + 1;
INSERT INTO USERCOMMENT VALUES (@id, 02, "evelyn", "I love it!");
SET @id = @id + 1;
INSERT INTO USERCOMMENT VALUES (@id, 03, "evelyn", "The instruction is not really clear to me :-(");
SET @id = @id + 1;
INSERT INTO USERCOMMENT VALUES (@id, 03, "josh", "Thanks!");
SET @id = @id + 1;
INSERT INTO USERCOMMENT VALUES (@id, 04, "josh", "Do not really like the taste");
SET @id = @id + 1;
INSERT INTO USERCOMMENT VALUES (@id, 05, "evelyn", "Great!");
/**/