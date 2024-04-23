CREATE DATABASE eCommerce;

CREATE TABLE customer (
  customer_id SERIAL PRIMARY KEY,
  full_name varchar(100),
  phone varchar(10) UNIQUE NOT NULL,
  email varchar(50) UNIQUE NOT NULL,
  password varchar(30),
  role varchar(100) 
);

-- =======================================================================

CREATE TABLE category (
  category_id serial PRIMARY KEY,
  type_name varchar(100) UNIQUE NOT NULL
);

-- =======================================================================

CREATE TABLE product (
  product_id SERIAL PRIMARY KEY,
  name varchar(100) NOT NULL,
  price decimal NOT NULL,
  description varchar(1000),
  category_id integer,
  FOREIGN KEY (category_id) REFERENCES category(category_id)
);

-- =======================================================================


CREATE TABLE orders (
  order_id serial PRIMARY KEY,
  total_price decimal,
  order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  customer_id integer,
  FOREIGN KEY (customer_id) REFERENCES customer(customer_id)
);

-- =======================================================================

CREATE TABLE order_item (
  item_id serial PRIMARY KEY,
  quantity integer,
  price decimal,
  product_id integer,
  customer_id integer,
  order_id integer,
  FOREIGN KEY (customer_id) REFERENCES customer(customer_id),
  FOREIGN KEY (product_id) REFERENCES product(product_id),
  FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

-- =======================================================================

CREATE TABLE payment (
  payment_id  serial PRIMARY KEY,
  payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  method varchar(100),
  order_id integer UNIQUE,
  FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

-- =======================================================================

CREATE TABLE shipment (
  shipment_id serial PRIMARY KEY,
  customer_id  integer,
  order_id  integer,
  shipment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  address varchar(100),
  status varchar(50),
  FOREIGN KEY (customer_id) REFERENCES customer(customer_id),
  FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

-- =======================================================================

CREATE TABLE review (
  review_id serial PRIMARY KEY,
  product_id integer,
  customer_id  integer,
  comment varchar(1000),
  FOREIGN KEY (product_id) REFERENCES product(product_id),
  FOREIGN KEY (customer_id) REFERENCES customer(customer_id) 
);





