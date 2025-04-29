-- Create the database
CREATE DATABASE carrier_ai_service;
GO

-- Switch to the newly created database
USE carrier_ai_service;
GO

-- Create the mobile plan table
CREATE TABLE plan_table (
    id INT IDENTITY(1,1) PRIMARY KEY,
    plan_name VARCHAR(100) NOT NULL,
    voice_minutes INT DEFAULT 0,
    data_pkg_GB INT DEFAULT 0,
    sms_count INT DEFAULT 0,
    is_unlimited_data BIT DEFAULT 0,
    speed_limit_after INT NULL,
    prepaid BIT NOT NULL,
    validity_days INT DEFAULT 30,
    price DECIMAL(10, 2) NOT NULL,
    description TEXT NULL,
    is_active BIT DEFAULT 1
);
GO

-- Create the client table
CREATE TABLE client_table (
    id INT IDENTITY(1,1) PRIMARY KEY,
    client_name VARCHAR(50) NOT NULL,
    phone_number VARCHAR(15) NOT NULL UNIQUE,
    date_of_birth DATE NOT NULL,
    plan_id INT NOT NULL,
    start_at DATE NOT NULL,
    FOREIGN KEY (plan_id) REFERENCES plan_table(id)
);
GO

-- Insert sample mobile plans
INSERT INTO plan_table (plan_name, voice_minutes, data_pkg_GB, sms_count, is_unlimited_data, speed_limit_after, prepaid, validity_days, price, description)
VALUES 
('Starter 5GB Prepaid', 300, 5, 100, 0, NULL, 1, 30, 19.99, 'Entry-level plan with 5GB data and 300 minutes voice.'),
('Unlimited Talk & Text', 9999, 3, 999, 0, 1, 1, 30, 24.99, 'Unlimited calls and texts, 3GB high-speed data then 1Mbps throttle.'),
('Family Share 20GB', 2000, 20, 500, 0, 1, 0, 30, 49.99, 'Ideal for families sharing 20GB with 2000 voice minutes.'),
('Unlimited Everything', 9999, 100, 9999, 1, NULL, 0, 30, 79.99, 'Truly unlimited plan with no speed cap.'),
('Youth Plan 10GB', 500, 10, 200, 0, 2, 1, 30, 15.99, 'Special offer for students under 25 with capped 2Mbps after quota.'),
('Annual Saver 50GB', 2000, 50, 1000, 0, 5, 1, 365, 199.99, 'Prepaid annual plan with 50GB/month and big savings.'),
('Senior Plan 2GB', 400, 2, 200, 0, NULL, 0, 30, 12.99, 'Basic postpaid plan for seniors with fewer data needs.'),
('Unlimited 5G Premium', 9999, 150, 9999, 1, NULL, 0, 30, 99.99, 'Best plan with 5G unlimited high-speed data and premium support.');
GO

-- Insert sample clients
INSERT INTO client_table (client_name, phone_number, date_of_birth, plan_id, start_at)
VALUES 
('Alice Johnson', '2145551234', '1990-06-15', 1, '2025-04-01'),
('Bob Smith', '4695555678', '1985-02-20', 4, '2025-03-15'),
('Charlie Lee', '6825553456', '2000-09-10', 2, '2025-04-10'),
('Diana Adams', '8175557890', '1975-12-05', 7, '2025-01-01'),
('Ethan Clark', '9725559012', '1998-08-08', 5, '2025-04-05'),
('Fiona Davis', '4305554321', '1992-11-30', 6, '2025-02-20'),
('George Nguyen', '2145556789', '1980-04-25', 3, '2025-03-28'),
('Hannah Kim', '4695558888', '2002-03-17', 8, '2025-04-15');
GO

