DROP DATABASE IF EXISTS helpdesk_db;
CREATE DATABASE helpdesk_db;
USE helpdesk_db;

CREATE TABLE user
(
    id INT NOT NULL AUTO_INCREMENT,
    userName VARCHAR(50) NOT NULL,
    userPass VARCHAR(50) NOT NULL,
    PRIMARY KEY (id)

);

INSERT INTO user (userName, userPass)
VALUES ("keiltest","testpassword");

INSERT INTO user (userName, userPass)
VALUES ("keilsecond","testsecondpassword");

INSERT INTO user (userName, userPass)
VALUES ("keilthirdtest","testthirdpassword");
