CREATE TABLE `products`
(
  `id` INT NOT NULL,
  `owner_id` INT NOT NULL,
  `source` VARCHAR(50) NOT NULL,
  `name` VARCHAR(128) NOT NULL,
  `kcal` INT NOT NULL,
  `carbohydrates` DOUBLE NOT NULL,
  `sugar` DOUBLE NOT NULL,
  `fat` DOUBLE NOT NULL,
  `saturated_fat` DOUBLE NOT NULL,
  `protein` DOUBLE NOT NULL,
  `fiber` DOUBLE NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `nutrients`
(
  `id` INT NOT NULL,
  `name` VARCHAR(50) NOT NULL,
  `unit` VARCHAR(50) NOT NULL,
  `short_name` VARCHAR(50) NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `products_nutrients`
(
  `nutrient_id` INT NOT NULL,
  `product_id` INT NOT NULL,
  `amount` DOUBLE NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `home_measures`
(
  `product_id` INT NOT NULL,
  `name` VARCHAR(50) NOT NULL,
  `mass` INT NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `recipes`
(
  `id` INT NOT NULL,
  `name` VARCHAR(50) NOT NULL,
  `instruction` TEXT NOT NULL,
  `time` INT NOT NULL,
  `portions` INT NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `recipes_products`
(
  `recipe_id` INT NOT NULL,
  `product_id` INT NOT NULL,
  `amount` INT NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `meals`
(
  `id` INT NOT NULL,
  `diet_plan_id` INT NOT NULL,
  `day` INT NOT NULL,
  `position` INT NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `meals_recipes`
(
  `meal_id` INT NOT NULL,
  `recipe_id` INT NOT NULL,
  `portions_amount` DOUBLE NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `diet_plans`
(
  `id` INT NOT NULL,
  `relation_id` INT NOT NULL,
  `demands_template_id` INT NOT NULL,
  `send_date` DATE NULL,
  `start_date` DATE NULL,
  `as_template` BIT NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE customer
(
  id INT NOT NULL,
  name VARCHAR(50) NOT NULL,
  surname VARCHAR(50) NOT NULL,
  username VARCHAR(50) NOT NULL,
  email VARCHAR(50) NOT NULL,
  phone_number VARCHAR(50) NULL,
  birth_date VARCHAR(50) NOT NULL,
  gender INT NOT NULL,
  city VARCHAR(50) NOT NULL,
)

CREATE TABLE `specialists`
(
  `id` INT NOT NULL,
  `name` VARCHAR(50) NOT NULL,
  `surname` VARCHAR(50) NOT NULL,
  `username` VARCHAR(50) NOT NULL,
  `email` VARCHAR(256) NOT NULL,
  `phone_number` VARCHAR(50) NULL,
  `birth_date` DATE NOT NULL,
  `gender` ENUM('A','B','C') NOT NULL,
  `city` VARCHAR(50) NOT NULL,
  `photo_url` VARCHAR(50) NULL,
  `role` ENUM('A','B','C') NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `relations`
(
  `id` INT NOT NULL,
  `specialist_id` INT NOT NULL,
  `customer_id` INT NOT NULL,
  `type` ENUM('A','B','C') NOT NULL,
  `status` ENUM('A','B','C') NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `body_measurements`
(
  `id` INT NOT NULL,
  `customer_id` INT NOT NULL,
  `height` DOUBLE NOT NULL,
  `weight` DOUBLE NOT NULL,
  `neck` DOUBLE NOT NULL,
  `chest` DOUBLE NULL,
  `waist` DOUBLE NOT NULL,
  `abdomen` DOUBLE NOT NULL,
  `wrist` DOUBLE NULL,
  `hips` DOUBLE NOT NULL,
  `thigh` DOUBLE NULL,
  `calf` DOUBLE NULL,
  `ankle` DOUBLE NULL,
  `date` DATE NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `food_preferences`
(
  `customer_id` INT NOT NULL,
  `product` VARCHAR(50) NOT NULL,
  `relation_type` ENUM('A','B','C') NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `diet_recommendations`
(
  `id` INT NOT NULL,
  `relation_id` INT NOT NULL,
  `demands_template_id` INT NOT NULL,
  `send_date` DATE NULL,
  `text` TEXT NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `demands_templates`
(
  `id` INT NOT NULL,
  `kcal` INT NOT NULL,
  `carbohydrates` INT NOT NULL,
  `fat` INT NOT NULL,
  `protein` INT NOT NULL,
  `fibre` DOUBLE NOT NULL,
  `magnez` DOUBLE NULL,
  `calcium` DOUBLE NULL,
  `iron` DOUBLE NULL,
  `biotyna` DOUBLE NULL,
  `fosfor` DOUBLE NULL,
  `foliany` DOUBLE NULL,
  `B12` DOUBLE NULL,
  `cynk` DOUBLE NULL,
  `miedz` DOUBLE NULL,
  `jod` DOUBLE NULL,
  `selen` DOUBLE NULL,
  `fluor` DOUBLE NULL,
  `sod` DOUBLE NULL,
  `potas` DOUBLE NULL,
  `chlor` DOUBLE NULL,
  `cholina` DOUBLE NULL,
  `A` DOUBLE NULL,
  `D` DOUBLE NULL,
  `E` DOUBLE NULL,
  `K` DOUBLE NULL,
  `C` DOUBLE NULL,
  `tymina` DOUBLE NULL,
  `ryboflawina` DOUBLE NULL,
  `niacyna` DOUBLE NULL,
  `kwas_pantotenowy` DOUBLE NULL,
  `B6` DOUBLE NULL,
  `name` DOUBLE NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `base_recipes`
(
  `id` INT NOT NULL,
  `owner_id` INT NOT NULL,
  `name` VARCHAR(50) NOT NULL,
  `time` INT NOT NULL,
  `portions` INT NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

CREATE TABLE `base_recipes_products`
(
  `recipe_id` INT NOT NULL,
  `product_id` INT NOT NULL,
  `amount` INT NOT NULL
) ENGINE=InnoDB COLLATE=utf8_general_ci;

ALTER TABLE `products` ADD PRIMARY KEY (`id`);
ALTER TABLE `nutrients` ADD PRIMARY KEY (`id`);
ALTER TABLE `recipes` ADD PRIMARY KEY (`id`);
ALTER TABLE `meals` ADD PRIMARY KEY (`id`);
ALTER TABLE `diet_plans` ADD PRIMARY KEY (`id`);
ALTER TABLE `relations` ADD PRIMARY KEY (`id`);
ALTER TABLE `specialists` ADD PRIMARY KEY (`id`);
ALTER TABLE `diet_recommendations` ADD PRIMARY KEY (`id`);
ALTER TABLE `body_measurements` ADD PRIMARY KEY (`id`);
ALTER TABLE `demands_templates` ADD PRIMARY KEY (`id`);
ALTER TABLE `base_recipes` ADD PRIMARY KEY (`id`);
ALTER TABLE `products` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `nutrients` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `recipes` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `meals` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `diet_plans` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `relations` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `specialists` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `customers` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `diet_recommendations` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `body_measurements` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `demands_templates` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `base_recipes` CHANGE COLUMN `id` `id`  INT NOT NULL AUTO_INCREMENT;
ALTER TABLE `products_nutrients` ADD CONSTRAINT `fk_product_to_product_nutrients` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`);
ALTER TABLE `products_nutrients` ADD CONSTRAINT `fk_nutrients_to_products_nutrients` FOREIGN KEY (`nutrient_id`) REFERENCES `nutrients` (`id`);
ALTER TABLE `home_measures` ADD CONSTRAINT `fk_products_to_home_measures` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`);
ALTER TABLE `recipes_products` ADD CONSTRAINT `fk_recipes_to_recipes_products` FOREIGN KEY (`recipe_id`) REFERENCES `recipes` (`id`);
ALTER TABLE `recipes_products` ADD CONSTRAINT `fk_products_to_recipes_products` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`);
ALTER TABLE `meals_recipes` ADD CONSTRAINT `fk_recipes_to_meals_recipes` FOREIGN KEY (`recipe_id`) REFERENCES `recipes` (`id`);
ALTER TABLE `meals_recipes` ADD CONSTRAINT `fk_meals_to_meals_recipes` FOREIGN KEY (`meal_id`) REFERENCES `meals` (`id`);
ALTER TABLE `meals` ADD CONSTRAINT `fk_diet_plans_to_meals` FOREIGN KEY (`diet_plan_id`) REFERENCES `diet_plans` (`id`);
ALTER TABLE `relations` ADD CONSTRAINT `fk_specialists_to_relations` FOREIGN KEY (`specialist_id`) REFERENCES `specialists` (`id`);
ALTER TABLE `relations` ADD CONSTRAINT `fk_customers_to_relations` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`id`);
ALTER TABLE `diet_plans` ADD CONSTRAINT `fk_relations_to_diet_plans` FOREIGN KEY (`relation_id`) REFERENCES `relations` (`id`);
ALTER TABLE `body_measurements` ADD CONSTRAINT `fk_customers_to_body_measurements` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`id`);
ALTER TABLE `food_preferences` ADD CONSTRAINT `fk_customers_to_food_preferences` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`id`);
ALTER TABLE `diet_recommendations` ADD CONSTRAINT `fk_relations_to_diet_recommendations` FOREIGN KEY (`relation_id`) REFERENCES `relations` (`id`);
ALTER TABLE `diet_plans` ADD CONSTRAINT `fk_demands_templates_to_diet_plans` FOREIGN KEY (`demands_template_id`) REFERENCES `demands_templates` (`id`);
ALTER TABLE `diet_recommendations` ADD CONSTRAINT `fk_demands_templates_to_diet_recommendations` FOREIGN KEY (`demands_template_id`) REFERENCES `demands_templates` (`id`);
ALTER TABLE `base_recipes_products` ADD CONSTRAINT `fk_base_recipes_to_products` FOREIGN KEY (`recipe_id`) REFERENCES `base_recipes` (`id`);
ALTER TABLE `base_recipes_products` ADD CONSTRAINT `fk_products_to_base_recipes` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`);