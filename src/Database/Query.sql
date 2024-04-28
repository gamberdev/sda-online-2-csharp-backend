-- insert users
INSERT INTO users (full_name, phone, email, password, role) VALUES 
 ('Rockie Luna', '7228395224', 'rluna0@pcworld.com', 'sY0>d7tt', 'admin'),
 ('Harmonia Brauns', '2358174483', 'hbrauns1@storify.com', 'mE1"8`~1', 'customer'),
 ('Claudina Grombridge', '7004675340', 'cgrombridge2@technorati.com', 'fW0"CjOHOo&1T', 'customer'),
 ('Monah MacLaren', '8449979448', 'mmaclaren3@woothemes.com', 'eI5&B3#ey(T*O', 'customer'),
 ('Wendy Penylton', '6554340737', 'wpenylton4@photobucket.com', 'wN2=bP{Cy/', 'customer'),
 ('Hi Collymore', '2022729652', 'hcollymore5@weebly.com', 'mD6~~2}lY+z3en"', 'customer'),
 ('Ford Chaffen', '2606792219', 'fchaffen6@cargocollective.com', 'fF0(tM1f\rw188Ud', 'customer'),
 ('Gwyn Valadez', '2799415474', 'gvaladez7@cbslocal.com', 'oB8|{,)7cIN&Q', 'customer'),
 ('Hilde Piggens', '9723410790', 'hpiggens8@blogspot.com', 'eU4,Ceut', 'customer'),
 ('Krissie Parrin', '6933921821', 'kparrin9@meetup.com', 'yT8~*2J#nz8xou', 'customer');

SELECT * FROM users;

-- insert category
INSERT INTO category (name, slug) VALUES 
('Phone', 'phone'),
('Laptop', 'laptop'),
('Headphone', 'headphone');

SELECT * FROM category;

-- insert product
INSERT INTO product (name, image, price, description, slug, category_id) VALUES 
('iPhone 15 Pro Max', 'http://dummyimage.com/150x100.png/ff4444/ffffff', 1548.53, 'W/craft fall NEC-skier', 'iphone-15-pro-max', 1),
('iPhone 14', 'http://dummyimage.com/107x100.png/dddddd/000000', 741.19, 'Dysphagia NEC', 'iphone-14', 1),
('iPhone 13 Plus', 'http://dummyimage.com/127x100.png/dddddd/000000', 1997.46, 'Abd preg w/o intrau preg', 'iphone-13-plus', 1),
('Samsung Galaxy A15', 'http://dummyimage.com/158x100.png/dddddd/000000', 1233.54, 'Exhaustion-exposure', 'samsung-galaxy-A15', 1),
('Samsung Galaxy M32', 'http://dummyimage.com/128x100.png/ff4444/ffffff', 669.06, 'Jt contracture-pelvis', 'samsung-galaxy-M32', 1),
('Samsung Galaxy A14 LTE', 'http://dummyimage.com/241x100.png/5fa2dd/ffffff', 1992.61, 'Perinat jaund:hemolysis', 'samsung-galaxy-A14-LTE', 1);

INSERT INTO product (name, image, price, description, slug, category_id) VALUES 
('Samsung Galaxy Buds Pro', 'http://dummyimage.com/136x100.png/cc0000/ffffff', 340.02, 'Acetonuria', 'samsung-galaxy-buds-pro', 3),
('IHAO Swimming Headphones', 'http://dummyimage.com/102x100.png/5fa2dd/ffffff', 198.16, 'Glaucoma w ocular trauma', 'IHAO-swimming', 3),
('SHOKZ OpenMove Wireless Headphones', 'http://dummyimage.com/207x100.png/ff4444/ffffff', 384.17, 'TB pneumothorax-unspec', 'SHOKZ-openMove-wireless', 3);

INSERT INTO product (name, image, price, description, slug, category_id) VALUES 
('Asus C423NA', 'http://dummyimage.com/131x100.png/dddddd/000000', 2854.42, 'Hered prog musc dystrphy', 'Asus-C423NA', 2),
('HP 14 Laptop', 'http://dummyimage.com/210x100.png/ff4444/ffffff', 2828.13, 'Pruritic disorder NOS', 'HP-14-laptop', 2),
('Acer Spin 311', 'http://dummyimage.com/142x100.png/5fa2dd/ffffff', 3226.56, 'DMII wo cmp nt st uncntr', 'Acer-spin-311', 2);

SELECT * FROM product;

-- insert order_item
INSERT INTO order_item (quantity, price, product_id, user_id)
SELECT 1, p.price * 1, p.product_id, 1
FROM product p
WHERE p.product_id = 10;

INSERT INTO order_item (quantity, price, product_id, user_id)
SELECT 2, p.price * 2, p.product_id, 1
FROM product p
WHERE p.product_id = 5;


SELECT * FROM order_item;

-- insert into orders
INSERT INTO orders (total_price, payment_method, user_id)
SELECT SUM(i.price),'cash',1
FROM order_item i
WHERE i.user_id = 1 AND i.order_id IS NULL;

SELECT * FROM orders;

--Then, update order_item 
UPDATE order_item SET order_id = (SELECT order_id
FROM orders
WHERE user_id = 1
ORDER BY order_date DESC
LIMIT 1) where order_id IS NULL;

-- Then, insert shipment
INSERT INTO shipment (user_id, order_id, delivery_date, delivery_address) 
SELECT o.user_id, o.order_id, o.order_date + INTERVAL '5 days', 'Riyadh 6787 554jh'
FROM orders o
WHERE o.user_id = 1 AND NOT EXISTS(SELECT * from shipment s WHERE s.order_id = o.order_id )
ORDER BY o.order_date DESC
LIMIT 1;

SELECT * FROM shipment;

-- insert review
INSERT INTO review (product_id, user_id, comment) VALUES (1, 1, 'The product work fine');

SELECT * FROM review;