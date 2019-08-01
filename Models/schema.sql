DROP DATABASE IF EXISTS helpdesk_db;
CREATE DATABASE helpdesk_db;
USE helpdesk_db;

CREATE TABLE loginUser
(
    id INT NOT NULL AUTO_INCREMENT,
    userName VARCHAR(50) NOT NULL,
    userPass VARCHAR(50) NOT NULL,
    PRIMARY KEY (id)

);

CREATE TABLE userRole
(
    id INT NOT NULL AUTO_INCREMENT,
    adminRole BOOLEAN DEFAULT false,
    helpDesk BOOLEAN  DEFAULT false,
    userCred BOOLEAN  DEFAULT false,
    PRIMARY KEY (id)
    
);

CREATE TABLE user
(
    id INT NOT NULL AUTO_INCREMENT,
    loginID INT,
    roleID INT,
    PRIMARY KEY (id),
    FOREIGN KEY (loginID) REFERENCES loginUser(id),
    FOREIGN KEY (roleID) REFERENCES userRole(id)

);

CREATE TABLE ticket
(
    id INT NOT NULL AUTO_INCREMENT,
    content VARCHAR(500),
    TT VARCHAR(255),
    userID INT,
    PRIMARY KEY(id),
    FOREIGN KEY (userID) REFERENCES user(id)

);

CREATE TABLE history
(
    id INT NOT NULL AUTO_INCREMENT,
    userID INT,
    PRIMARY KEY (id),
    FOREIGN KEY (userID) REFERENCES user(id)

);

INSERT INTO loginUser (userName, userPass)
VALUES ("keiladmin","testpasswordFIRST");
INSERT INTO userRole (adminRole)
VALUES (true);

INSERT INTO user(loginID, roleID)
VALUEs(LAST_INSERT_ID(), LAST_INSERT_ID());
INSERT INTO history (userID) VALUES(LAST_INSERT_ID());


INSERT INTO loginUser (userName, userPass)
VALUES ("keilhelp","testsecondpassword");
INSERT INTO userRole (helpDesk)
VALUES (true);

INSERT INTO user(loginID, roleID)
VALUEs(LAST_INSERT_ID(), LAST_INSERT_ID());
INSERT INTO history (userID) VALUES(LAST_INSERT_ID());


INSERT INTO loginUser (userName, userPass)
VALUES ("keiluser","testthirdpassword");
INSERT INTO userRole (userCred)
VALUES (true);
INSERT INTO user(loginID, roleID)
VALUEs(LAST_INSERT_ID(), LAST_INSERT_ID());
INSERT INTO history (userID) VALUES(LAST_INSERT_ID());

