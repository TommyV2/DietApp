DROP DATABASE IF EXISTS projectdiet;

CREATE DATABASE projectdiet;

-- Make sure we're using our `ProjectDiet` database
\c projectdiet;

--Tables
CREATE TABLE IF NOT EXISTS Accounts
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Email			VARCHAR(256) NOT NULL,
  Password		VARCHAR(128) NOT NULL,
  Specialist_id BIGINT NULL,
  Customer_id   BIGINT NULL
) ;

CREATE TABLE IF NOT EXISTS Product
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  
  Owner_Id       BIGINT NULL,
  Source        VARCHAR(64) NULL,
  Name          VARCHAR(256) NULL,
  Kcal          INT NULL,
  Carbohydrates FLOAT NULL,
  Sugar         FLOAT NULL,
  Fat           FLOAT NULL,
  Saturated_Fat  FLOAT NULL,
  Protein       FLOAT NULL,
  Fiber         FLOAT NULL
) ;

CREATE TABLE IF NOT EXISTS Nutrient
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Name          VARCHAR(256) NULL,
  Pol_Name       VARCHAR(256) NULL,
  Unit          VARCHAR(64) NULL,
  Short_Name     VARCHAR(32) NULL
) ;

CREATE TABLE IF NOT EXISTS Product_Nutrient
(
  Id             BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Nutrient_Id    BIGINT NULL,
  Product_Id     BIGINT NULL,
  Amount        FLOAT NULL
) ;

CREATE TABLE IF NOT EXISTS Home_Measure
(
  Id             BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Product_Id     BIGINT NULL,
  Name          VARCHAR(256) NULL,
  Mass          INT NULL
) ;

CREATE TABLE IF NOT EXISTS Recipe
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Name          VARCHAR(256) NULL,
  Instruction   TEXT NULL,
  Time          INT NULL,
  Portions      INT NULL,
  Amount        FLOAT NULL,
  Meal_Id        BIGINT NULL
) ;

CREATE TABLE IF NOT EXISTS Recipe_Product
(
  Id             BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Recipe_Id      BIGINT NULL,
  Product_Id     BIGINT NULL,
  Amount        INT NULL
) ;

CREATE TABLE IF NOT EXISTS Meal
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Diet_Plan_Id    BIGINT NULL,
  Day           INT NULL,
  Position      INT NULL
) ;


CREATE TABLE IF NOT EXISTS Meal_Recipe
(
  Meal_Id        BIGINT NULL,
  Recipe_Id      BIGINT NULL
) ;

CREATE TABLE IF NOT EXISTS Diet_Plan
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Collaboration_Id    BIGINT NULL,
  Demands_Id     BIGINT NULL,
  Send_Date      DATE NULL,
  Start_Date     DATE NULL
) ;

CREATE TABLE IF NOT EXISTS Customer
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Name          VARCHAR(256) NULL,
  Surname       VARCHAR(256) NULL,
  Username      VARCHAR(128) NULL UNIQUE,
  Email         VARCHAR(256) NULL UNIQUE,
  Phone_Number   VARCHAR(64) NULL,
  Birth_Date     DATE NULL,
  Gender        varchar(16) NULL CHECK (Gender IN ('man', 'woman')),
  City          VARCHAR(128) NULL,
  Photo_Url      VARCHAR(512) NULL
) ;

CREATE TABLE IF NOT EXISTS Specialist
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Name          VARCHAR(256) NULL,
  Surname       VARCHAR(256) NULL,
  Username      VARCHAR(128) NULL UNIQUE,
  Email         VARCHAR(256) NULL UNIQUE,
  Phone_Number   VARCHAR(64) NULL,
  Birth_Date     DATE NULL,
  Gender        VARCHAR(16) NULL CHECK (Gender IN ('man', 'woman')),
  City          VARCHAR(128) NULL,
  Photo_Url      VARCHAR(512) NULL,
  Role          VARCHAR(16) NULL CHECK (Role IN ('dietician', 'trainer', 'both'))
) ;

CREATE TABLE IF NOT EXISTS Collaboration
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Specialist_Id  BIGINT NULL,
  Customer_Id    BIGINT NULL,
  Type          varchar(16) NULL CHECK (Type IN ('dietician', 'trainer', 'both')),
  Status        varchar(16) NULL CHECK (Status IN ('began','active','ended','reactivated'))
) ;

CREATE TABLE IF NOT EXISTS Body_Measurement
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Customer_Id    BIGINT NULL,
  Height        FLOAT NULL,
  Weight        FLOAT NULL,
  Neck          FLOAT NULL,
  Chest         FLOAT NULL,
  Waist         FLOAT NULL,
  Abdomen       FLOAT NULL,
  Wrist         FLOAT NULL,
  Hips          FLOAT NULL,
  Thigh         FLOAT NULL,
  Calf          FLOAT NULL,
  Ankle         FLOAT NULL,
  Date          DATE NULL
) ;


CREATE TABLE IF NOT EXISTS Food_Preference
(
  Customer_Id    BIGINT NULL,
  Product       VARCHAR(256) NULL,
  Relation_Type  VARCHAR(32) NULL CHECK (Relation_Type IN ('like', 'dislike', 'allergic'))
) ;

CREATE TABLE IF NOT EXISTS Diet_Recommendation
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Collaboration_Id    BIGINT NULL,
  Demands_Id     BIGINT NULL,
  Send_Date      DATE NULL,
  Text          TEXT NULL
) ;

CREATE TABLE IF NOT EXISTS Demands
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Kcal          INT NULL,
  Carbohydrates INT NULL,
  Fat           INT NULL,
  Protein       INT NULL,
  Fibre         FLOAT NULL,
  Magnesium     FLOAT NULL,
  Calcium       FLOAT NULL,
  Iron          FLOAT NULL,
  Biotin        FLOAT NULL,
  Phosphorus    FLOAT NULL,
  Folate        FLOAT NULL,
  Cobalamin     FLOAT NULL,
  Zinc          FLOAT NULL,
  Copper        FLOAT NULL,
  Iodine        FLOAT NULL,
  Selenium      FLOAT NULL,
  Fluorine      FLOAT NULL,
  Sodium        FLOAT NULL,
  Potassium     FLOAT NULL,
  Chlorine      FLOAT NULL,
  Choline       FLOAT NULL,
  A             FLOAT NULL,
  D             FLOAT NULL,
  E             FLOAT NULL,
  K             FLOAT NULL,
  C             FLOAT NULL,
  Timine        FLOAT NULL,
  Riboflavin    FLOAT NULL,
  Niacin        FLOAT NULL,
  Pantothenic_Acid FLOAT NULL,
  B6            FLOAT NULL
) ;

CREATE TABLE IF NOT EXISTS Base_Recipe
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Owner_Id       BIGINT NULL,
  Owner_Name    VARCHAR(256) NULL,
  Name          VARCHAR(256) NULL,
  Instruction   TEXT NULL,
  Time          INT NULL,
  Portions      INT NULL
) ;


CREATE TABLE IF NOT EXISTS Base_Recipe_Product
(
  Id             BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Recipe_Id      BIGINT NULL,
  Product_Id     BIGINT NULL,
  Amount        INT NULL
) ;


CREATE TABLE IF NOT EXISTS Meal_For_Template
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Diet_Plan_Id    BIGINT NULL,
  Day           INT NULL,
  Position      INT NULL
) ;

CREATE TABLE IF NOT EXISTS Meal_Recipe_For_Template
(
  Meal_Id        BIGINT NULL,
  Recipe_Id      BIGINT NULL
) ;

CREATE TABLE IF NOT EXISTS Diet_Plan_For_Template
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Specialist_Id BIGINT NULL,
  Demands_Id     BIGINT NULL,
  Name          VARCHAR(256) NULL,
  Description   TEXT NULL
) ;

CREATE TABLE IF NOT EXISTS Demands_Template
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Specialist_Id BIGINT NULL,
  Kcal          INT NULL,
  Carbohydrates INT NULL,
  Fat           INT NULL,
  Protein       INT NULL,
  Fibre         FLOAT NULL,
  Magnesium     FLOAT NULL,
  Calcium       FLOAT NULL,
  Iron          FLOAT NULL,
  Biotin        FLOAT NULL,
  Phosphorus    FLOAT NULL,
  Folate        FLOAT NULL,
  Cobalamin     FLOAT NULL,
  Zinc          FLOAT NULL,
  Copper        FLOAT NULL,
  Iodine        FLOAT NULL,
  Selenium      FLOAT NULL,
  Fluorine      FLOAT NULL,
  Sodium        FLOAT NULL,
  Potassium     FLOAT NULL,
  Chlorine      FLOAT NULL,
  Choline       FLOAT NULL,
  A             FLOAT NULL,
  D             FLOAT NULL,
  E             FLOAT NULL,
  K             FLOAT NULL,
  C             FLOAT NULL,
  Timine        FLOAT NULL,
  Riboflavin    FLOAT NULL,
  Niacin        FLOAT NULL,
  Pantothenic_Acid FLOAT NULL,
  B6            FLOAT NULL,
  Name          VARCHAR(256) NULL
) ;

CREATE TABLE IF NOT EXISTS Invitation
(
  Id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Specialist_Id BIGINT NULL,
  Customer_Id   BIGINT NULL,
  Send_Date     DATE NULL,
  Type          varchar(16) NULL CHECK (Type IN ('dietician', 'trainer', 'both')),
  Invited_By	varchar(16) NULL CHECK (Invited_By IN ('specialist', 'customer'))
) ;
ALTER TABLE Invitation ADD CONSTRAINT fk_specialist_to_Invitation FOREIGN KEY (Specialist_Id) REFERENCES Specialist (Id) ON DELETE CASCADE;
ALTER TABLE Invitation ADD CONSTRAINT fk_customer_to_Invitation FOREIGN KEY (Customer_Id) REFERENCES Customer (Id) ON DELETE CASCADE;

ALTER TABLE Meal_For_Template ADD CONSTRAINT fk_dietplan_to_meal_for_templates FOREIGN KEY (Diet_Plan_Id) REFERENCES Diet_Plan_For_Template (Id) ON DELETE CASCADE;--
ALTER TABLE Meal_Recipe_For_Template ADD CONSTRAINT fk_meal_to_mealrecipe_for_template FOREIGN KEY (Meal_Id) REFERENCES Meal_For_Template (Id) ON DELETE CASCADE;--
ALTER TABLE Meal_Recipe_For_Template ADD CONSTRAINT fk_recipe_to_mealrecipe_for_template FOREIGN KEY (Recipe_Id) REFERENCES Recipe (Id) ON DELETE CASCADE;
ALTER TABLE Diet_Plan_For_Template ADD CONSTRAINT fk_specialist_to_diet_plan_templates FOREIGN KEY (Specialist_Id) REFERENCES Specialist (Id) ON DELETE CASCADE;

ALTER TABLE Product_Nutrient ADD CONSTRAINT fk_product_to_product_nutrient FOREIGN KEY (Product_Id) REFERENCES Product (Id) ON DELETE CASCADE;
ALTER TABLE Product_Nutrient ADD CONSTRAINT fk_nutrient_to_product_nutrient FOREIGN KEY (Nutrient_Id) REFERENCES Nutrient (Id);
ALTER TABLE Home_Measure ADD CONSTRAINT fk_product_to_home_measure FOREIGN KEY (Product_Id) REFERENCES Product (Id) ON DELETE CASCADE;
ALTER TABLE Recipe_Product ADD CONSTRAINT fk_recipe_to_recipe_product FOREIGN KEY (Recipe_Id) REFERENCES Recipe (Id) ON DELETE CASCADE;
ALTER TABLE Recipe_Product ADD CONSTRAINT fk_product_to_recipe_product FOREIGN KEY (Product_Id) REFERENCES Product (Id);
ALTER TABLE Meal_Recipe ADD CONSTRAINT fk_recipe_to_meal_recipe FOREIGN KEY (Recipe_Id) REFERENCES Recipe (Id);
ALTER TABLE Meal_Recipe ADD CONSTRAINT fk_meal_to_meal_recipe FOREIGN KEY (Meal_Id) REFERENCES Meal (Id) ON DELETE CASCADE;--
ALTER TABLE Recipe ADD CONSTRAINT fk_meal_to_recipe FOREIGN KEY (Meal_Id) REFERENCES Meal (Id) ON DELETE CASCADE;--
ALTER TABLE Meal ADD CONSTRAINT fk_diet_plan_to_meal FOREIGN KEY (Diet_Plan_Id) REFERENCES Diet_Plan (Id) ON DELETE CASCADE;--
ALTER TABLE Collaboration ADD CONSTRAINT fk_specialist_to_Collaboration FOREIGN KEY (Specialist_Id) REFERENCES Specialist (Id) ON DELETE SET NULL;
ALTER TABLE Collaboration ADD CONSTRAINT fk_customer_to_Collaboration FOREIGN KEY (Customer_Id) REFERENCES Customer (Id) ON DELETE SET NULL;
ALTER TABLE Diet_Plan ADD CONSTRAINT fk_Collaboration_to_diet_plan FOREIGN KEY (Collaboration_Id) REFERENCES Collaboration (Id) ON DELETE CASCADE;
ALTER TABLE Body_Measurement ADD CONSTRAINT fk_customer_to_body_measurement FOREIGN KEY (Customer_Id) REFERENCES Customer (Id) ON DELETE CASCADE;
ALTER TABLE Food_Preference ADD CONSTRAINT fk_customer_to_food_preference FOREIGN KEY (Customer_Id) REFERENCES Customer (Id) ON DELETE CASCADE;
ALTER TABLE Diet_Recommendation ADD CONSTRAINT fk_Collaboration_to_diet_recommendation FOREIGN KEY (Collaboration_Id) REFERENCES Collaboration (Id) ON DELETE CASCADE;
ALTER TABLE Diet_Plan ADD CONSTRAINT fk_demands_template_to_diet_plan FOREIGN KEY (Demands_Id) REFERENCES Demands (Id);
ALTER TABLE Diet_Recommendation ADD CONSTRAINT fk_demands_template_to_diet_recommendation FOREIGN KEY (Demands_Id) REFERENCES Demands (Id) ON DELETE CASCADE;
ALTER TABLE Base_Recipe_Product ADD CONSTRAINT fk_base_recipe_to_product FOREIGN KEY (Recipe_Id) REFERENCES Base_Recipe (Id) ON DELETE CASCADE;
ALTER TABLE Base_Recipe_Product ADD CONSTRAINT fk_product_to_base_recipe FOREIGN KEY (Product_Id) REFERENCES Product (Id);
ALTER TABLE Demands_Template ADD CONSTRAINT fk_specialist_to_demands_template FOREIGN KEY (Specialist_Id) REFERENCES Specialist (Id);

--User with id =1
INSERT INTO customer(Name, Surname,Username, Email, Phone_Number, Birth_Date, Gender, City, Photo_Url)
VALUES('Test', 'Test','Test', 'Test@Test.pl', '123456789', '1999-01-01', 'man', 'Test', 'Test/url');
--User with id =2
INSERT INTO customer(Name, Surname,Username, Email, Phone_Number, Birth_Date, Gender, City, Photo_Url)
VALUES('Aleksander', 'Kowalski','Aakow', 'alkow@test.pl', '123456789', '1999-01-01', 'man', 'Kraków', 'Test/url');

--Specialist with id =1
INSERT INTO Specialist(Name, Surname,Username, Email, Phone_Number, Birth_Date, Gender, City, Photo_Url, Role)
VALUES('Marcin', 'Stodoła','Marstod', 'marstod@test.pl', '123456789', '1999-01-01', 'man', 'Warszawa', 'Test/url', 'both');

--Collaboration with id =1
INSERT INTO Collaboration(Specialist_Id, Customer_Id,Type, Status)
VALUES(1, 2,'dietician', 'active');

--Invitation with id =1
INSERT INTO Invitation(Specialist_Id, Customer_Id,Send_Date,Type, Invited_By)
VALUES(1, 1, current_date,'dietician', 'specialist');

--Product with id =1
INSERT INTO Product(
  Owner_Id,
  Source,
  Name,
  Kcal,
  Carbohydrates,
  Sugar,
  Fat,
  Saturated_Fat,
  Protein,
  Fiber)
VALUES(
  0,
  'USDA',
  'Jablko',
  73,
  7.6,
  6.4,
  0.3,
  0.02,
  1.2,
  3.2
);
--Product with id =2
INSERT INTO Product(
  Owner_Id,
  Source,
  Name,
  Kcal,
  Carbohydrates,
  Sugar,
  Fat,
  Saturated_Fat,
  Protein,
  Fiber)
VALUES(
  0,
  'IZZ',
  'Gruszka',
  81,
  7.9,
  6.8,
  0.2,
  0.01,
  1.2,
  2.3
);

--Product with id =3
INSERT INTO Product(
  Owner_Id,
  Source,
  Name,
  Kcal,
  Carbohydrates,
  Sugar,
  Fat,
  Saturated_Fat,
  Protein,
  Fiber
  )
VALUES(
  0,
  'IZZ',
  'Czekolada mleczna',
  481,
  47.9,
  36.8,
  23.2,
  12.1,
  9.2,
  0.3
);

INSERT INTO Demands(
  Kcal,
  Carbohydrates,
  Fat,
  Protein,
  Fibre,
  Magnesium,
  Calcium,
  Iron,
  Biotin,
  Phosphorus,
  Folate,
  Cobalamin,
  Zinc,
  Copper,
  Iodine,
  Selenium,
  Fluorine,
  Sodium,
  Potassium ,
  Chlorine,
  Choline,
  A,
  D,
  E,
  K,
  C,
  Timine,
  Riboflavin,
  Niacin,
  Pantothenic_Acid,
  B6
)
VALUES
(2000,
  400,
  100,
  120,
  25,
  400,
  1000,
  10,
  1,
  2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23
);


--RETURNING id;

INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Magnesium','Magnez', 'mg', 'Mg');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Calcium','Wapń', 'mg', 'Ca');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Iron','Żelazo', 'mg', 'Fe');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Biotin','Biotyna', 'µg', 'C10H16N2O3S');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Phosphorus','Fosfor', 'mg', 'P');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Folate','Foliany', 'mg', 'C19H19N7O6');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Cobalamin ','Kobalamina ', 'µg', 'B12');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Zinc','Cynk', 'mg', 'Zn');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Copper','Miedź', 'mg', 'Cu');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Iodine','Jod', 'µg', 'I');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Selenium','Selen', 'µg', 'Se');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Fluorine ','Fluor ', 'mg', 'F');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Sodium','Sód', 'mg', 'Na');
INSERT INTO Nutrient(Name, Pol_Name, Unit, Short_Name)
VALUES('Potassium','Potas', 'mg', 'K');



--   Chlorine         FLOAT NULL,
--   Cholina       FLOAT NULL,
--   A             FLOAT NULL,
--   D             FLOAT NULL,
--   E             FLOAT NULL,
--   K             FLOAT NULL,
--   C             FLOAT NULL,
--   Tymina        FLOAT NULL,
--   Ryboflawina   FLOAT NULL,
--   Niacyna       FLOAT NULL,
--   KwasPantotenowy FLOAT NULL,
--   B6            FLOAT NULL,
INSERT INTO Product_Nutrient
(
  Nutrient_Id,
  Product_Id,
  Amount
)
VALUES(
  1,
  3,
  54
);

INSERT INTO Product_Nutrient
(
  Nutrient_Id,
  Product_Id,
  Amount
)
VALUES(
  2,
  3,
  37
);