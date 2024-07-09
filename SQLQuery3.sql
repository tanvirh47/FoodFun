CREATE DATABASE FoodFunDB;

USE FoodFunDB;

CREATE TABLE FoodItems (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Description NVARCHAR(500),
    Price DECIMAL(10, 2),
    ImageUrl NVARCHAR(255)
);

INSERT INTO FoodItems (Name, Description, Price, ImageUrl) VALUES
('Mexican Eggrolls', 'Face together given moveth divided form Of Seasons that fruitful.', 14.50, '~/assets/images/food1.jpg'),
('Chicken Burger', 'Face together given moveth divided form Of Seasons that fruitful.', 9.50, '~/assets/images/food2.jpg'),
('Topu Lasange', 'Face together given moveth divided form Of Seasons that fruitful.', 12.50, '~/assets/images/food3.jpg'),
('Pepper Potatoes', 'Face together given moveth divided form Of Seasons that fruitful.', 14.50, '~/assets/images/food4.jpg'),
('Bean Salad', 'Face together given moveth divided form Of Seasons that fruitful.', 8.50, '~/assets/images/food5.jpg'),
('Beatball Hoagie', 'Face together given moveth divided form Of Seasons that fruitful.', 11.50, '~/assets/images/food6.jpg');
