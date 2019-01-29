DROP TABLE USERCOMMENT;
DROP TABLE RECIPE;
DROP TABLE USERDATA;

CREATE TABLE USERDATA (
	userName		VARCHAR(50)			NOT NULL,
    email			VARCHAR(200)		NOT NULL,
    userPassword	VARCHAR(200)		NOT NULL,
    CONSTRAINT user_pk PRIMARY KEY (userName)
);


CREATE TABLE RECIPE (
	recipeID		INTEGER			AUTO_INCREMENT,
    re_name			VARCHAR(200)	NOT NULL,
    description		VARCHAR(500)	NOT NULL,
    re_time			INTEGER			NOT NULL,
    ingredients		VARCHAR(500)	NOT NULL,
    instructions	VARCHAR(1000)	NOT NULL,
    image			VARCHAR(200)	NOT NULL,
	userName		VARCHAR(200)	NOT NULL,
    CONSTRAINT recipe_pk PRIMARY KEY (recipeID),
	CONSTRAINT recipe_fk1 FOREIGN KEY (userName) REFERENCES USERDATA (userName)
);
/*
CREATE TABLE TECHNIQUE (
	tID				VARCHAR(20)		NOT NULL,
    te_name			VARCHAR(50)		NOT NULL,
    recipeID		VARCHAR(50)		NOT NULL,
    CONSTRAINT technique_pk PRIMARY KEY (tID),
    CONSTRAINT tedchnique_fk FOREIGN KEY (recipeID) REFERENCES RECIPE (recipeID)
);

CREATE TABLE RATING (
	rateID			VARCHAR(50)		NOT NULL,
    stars			INTEGER			NOT NULL,
    record_number	INTEGER			NOT NULL,
    average			INTEGER			NOT NULL,
    recipeID		VARCHAR(50)		NOT NULL,
    CONSTRAINT rate_pk PRIMARY KEY (rateID),
    CONSTRAINT rate_fk FOREIGN KEY (recipeID) REFERENCES RECIPE (recipeID)
);
*/

CREATE TABLE USERCOMMENT (
	recipeID		INTEGER			NOT NULL,
	userCmtName		VARCHAR(50)		NOT NULL,
    userCmt			VARCHAR(500)	NOT NULL,
    CONSTRAINT comment_fk1 FOREIGN KEY (recipeID) REFERENCES RECIPE (recipeID)
);