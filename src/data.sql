CREATE DATABASE eCommerce;

CREATE TABLE Customer (
  Customers_id SERIAL PRIMARY KEY,
  Full_name Varchar(100),
  Phone Varchar(10) UNIQUE NOT NULL,
  Email Varchar(50) UNIQUE NOT NULL,
  Password Varchar(30),
  Role Varchar(100) 
);

-- =======================================================================

CREATE TABLE Categories (
  id serial PRIMARY KEY,
  Type_name Varchar(100) UNIQUE NOT NULL
);

-- =======================================================================

CREATE TABLE Product (
  Product_id SERIAL PRIMARY KEY,
  Name Varchar(100) NOT NULL,
  Price Decimal NOT NULL,
  Description Varchar(1000),
  Category_id INTEGER,
  FOREIGN KEY (Category_id) REFERENCES Categories(id) 
);

-- =======================================================================

CREATE TABLE Cart_item (
  Cart_id serial PRIMARY KEY,
  Product_id Integer,
  Customer_id Integer,
  Order_id Integer,
  Quantity Integer,
  FOREIGN KEY (Customer_id) REFERENCES Customer(Customers_id),
  FOREIGN KEY (Product_id) REFERENCES Product(Product_id),
  FOREIGN KEY (Order_id) REFERENCES Orders(Order_id)
);

-- =======================================================================

CREATE TABLE Orders (
  Order_id serial PRIMARY KEY,
  Customer_id INTEGER,
  Total_price DECIMAL(7,2),
  Order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (Customer_id) REFERENCES Customer(Customers_id)
);

-- =======================================================================

CREATE TABLE Payment (
  Payment_id  serial PRIMARY KEY,
  Order_id Integer,
  Payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  Payment_method Varchar(100),
  Amount Decimal,
  FOREIGN KEY (Order_id) REFERENCES Orders(Order_id)
);

-- =======================================================================

CREATE TABLE Shipment (
  Shipment_id serial PRIMARY KEY,
  Customer_id  Integer,
  Order_id  Integer,
  Shipment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  Address Varchar(100),
  Status Varchar(50),
  FOREIGN KEY (Customer_id) REFERENCES Customer(Customers_id),
  FOREIGN KEY (Order_id) REFERENCES Orders(Order_id)
);


-- =======================================================================

CREATE TABLE Review (
  Review_id serial PRIMARY KEY,
  Product_id Integer,
  Customer_id  Integer,
  Comment Varchar(1000),
  FOREIGN KEY (Product_id) REFERENCES Product(Product_id),
  FOREIGN KEY (Customer_id) REFERENCES Customer(Customers_id) 
);





