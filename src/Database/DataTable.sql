CREATE DATABASE eCommerce;

CREATE TABLE users (
  user_id UUid PRIMARY KEY,
  full_name VARCHAR(100),
  phone VARCHAR(10) UNIQUE NOT NULL,
  email VARCHAR(100) UNIQUE NOT NULL,
  password VARCHAR(30),
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  role VARCHAR(100),
  is_banned BOOLEAN DEFAULT FALSE
);

-- =======================================================================

CREATE TABLE category (
  category_id UUid PRIMARY KEY,
  name VARCHAR(100),
  slug VARCHAR(100) 
);

-- =======================================================================

CREATE TABLE product (
  product_id UUid PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  image VARCHAR(150),quantity NUMERIC(10,2),
  price NUMERIC(10, 2) NOT NULL,
  description TEXT,
  slug VARCHAR(100) UNIQUE NOT NULL,
  category_id UUid,
  FOREIGN KEY (category_id) REFERENCES category(category_id)
);

-- =======================================================================

CREATE TABLE orders (
  order_id UUid PRIMARY KEY,
  total_price NUMERIC(12, 2),
  order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  delivery_date TIMESTAMP,
  delivery_address VARCHAR(100)
  payment_method VARCHAR(100),
  order_status VARCHAR(100) DEFAULT 'pending',
  user_id UUid,
  FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- =======================================================================

CREATE TABLE order_item (
  item_id UUid PRIMARY KEY,
  quantity INTEGER,
  price NUMERIC(10, 2),
  product_id UUid,
  user_id UUid,
  order_id UUid,
  FOREIGN KEY (user_id) REFERENCES users(user_id),
  FOREIGN KEY (product_id) REFERENCES product(product_id),
  FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

-- =======================================================================

CREATE TABLE review (
  review_id SERIAL PRIMARY KEY,
  product_id UUid,
  user_id  UUid,
  comment TEXT,
  FOREIGN KEY (product_id) REFERENCES product(product_id),
  FOREIGN KEY (user_id) REFERENCES users(user_id) 
);