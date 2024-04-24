CREATE DATABASE eCommerce;

CREATE TABLE users (
  user_id SERIAL PRIMARY KEY,
  full_name VARCHAR(100),
  phone VARCHAR(10) UNIQUE NOT NULL,
  email VARCHAR(100) UNIQUE NOT NULL,
  password VARCHAR(30),
  createAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  role VARCHAR(100),
  isBanned BOOLEAN DEFAULT FALSE
);

-- =======================================================================

CREATE TABLE category (
  category_id SERIAL PRIMARY KEY,
  name VARCHAR(100) UNIQUE NOT NULL,
  slug VARCHAR(100) UNIQUE NOT NULL
);

-- =======================================================================

CREATE TABLE product (
  product_id SERIAL PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  image VARCHAR(150), 
  price NUMERIC(10, 2) NOT NULL,
  description TEXT,
  slug VARCHAR(100) UNIQUE NOT NULL,
  category_id INTEGER,
  FOREIGN KEY (category_id) REFERENCES category(category_id)
);

-- =======================================================================

CREATE TABLE orders (
  order_id SERIAL PRIMARY KEY,
  total_price NUMERIC(12, 2),
  order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  payment_method VARCHAR(100),
  order_status VARCHAR(100) DEFAULT 'pending',
  user_id INTEGER,
  FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- =======================================================================

CREATE TABLE order_item (
  item_id SERIAL PRIMARY KEY,
  quantity INTEGER,
  price NUMERIC(10, 2),
  product_id INTEGER,
  user_id INTEGER,
  order_id INTEGER,
  FOREIGN KEY (user_id) REFERENCES users(user_id),
  FOREIGN KEY (product_id) REFERENCES product(product_id),
  FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

-- =======================================================================

CREATE TABLE shipment (
  shipment_id SERIAL PRIMARY KEY,
  user_id  INTEGER,
  order_id  INTEGER,
  delivery_date TIMESTAMP,
  delivery_address VARCHAR(100),
  shipment_status VARCHAR(100) DEFAULT 'Processing',
  FOREIGN KEY (user_id) REFERENCES users(user_id),
  FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

-- =======================================================================

CREATE TABLE review (
  review_id SERIAL PRIMARY KEY,
  product_id INTEGER,
  user_id  INTEGER,
  comment TEXT,
  FOREIGN KEY (product_id) REFERENCES product(product_id),
  FOREIGN KEY (user_id) REFERENCES users(user_id) 
);