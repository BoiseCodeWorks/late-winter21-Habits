CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS habits(
  id INT AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  description TEXT NOT NULL COMMENT 'description of the habit',
  creatorId VARCHAR(255) NOT NULL COMMENT 'The ownwer/creator of the habit',

  FOREIGN KEY (creatorId)
    REFERENCES accounts(id)
    ON DELETE CASCADE

) default charset utf8 COMMENT '';

ALTER TABLE habits ADD isPrivate TINYINT;


CREATE TABLE IF NOT EXISTS trackedhabits (
  id INT AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  accountId VARCHAR(255) NOT NULL COMMENT 'The ownwer/creator of the habit being tracked',
  habitId INT NOT NULL,
  completedAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Completed',

  FOREIGN KEY (accountId)
    REFERENCES accounts(id)
    ON DELETE CASCADE,

  FOREIGN KEY (habitId)
    REFERENCES habits(id)
    ON DELETE CASCADE
) default charset utf8 COMMENT '';