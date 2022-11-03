USE maindb;

-- v0.1
CREATE TABLE IF NOT EXISTS Team(
	TeamId int NOT NULL AUTO_INCREMENT,
	Name VARCHAR(80) NOT NULL,
	PrimaryColor VARCHAR(7) NOT NULL,
	SecondaryColor VARCHAR(7) NOT NULL,
	PRIMARY KEY (TeamId)
);

-- v0.2
CREATE TABLE IF NOT EXISTS User(
	UserId int NOT NULL AUTO_INCREMENT,
	Username VARCHAR(80) NOT NULL,
	Password VARCHAR(200) NOT NULL,
	Role VARCHAR(50) NOT NULL,
	PRIMARY KEY (UserId)
);

INSERT INTO User(Username, Password, Role) VALUES('admin', '@gamee2022', 'administrator')