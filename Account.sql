CREATE TABLE Account 
(
	ID serial PRIMARY KEY,
    FirstName VARCHAR (50) NOT NULL,
	LastName VARCHAR (50) NOT NULL,
    Address VARCHAR (255) NOT NULL, 
    Age integer NOT NULL, 
    Interests VARCHAR(256) NOT NULL,
    ImageUrl varvhar(255)
);

