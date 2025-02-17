PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Accounts

CREATE TABLE Accounts (
  Id INTEGER CONSTRAINT PK_Accounts_Id PRIMARY KEY AUTOINCREMENT NOT NULL, 
  Name NVARCHAR (255) NOT NULL UNIQUE, 
  Password NVARCHAR (255) NOT NULL, 
  PremiumDays INTEGER NOT NULL
);

INSERT INTO Accounts (Id, Name, Password, PremiumDays) VALUES (1, '1', '1', 0);

-- Bans

CREATE TABLE Bans (
  Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  Type INTEGER NOT NULL, 
  AccountId INTEGER REFERENCES Accounts (Id) ON DELETE CASCADE, 
  PlayerId INTEGER REFERENCES Players (Id) ON DELETE CASCADE, 
  IpAddress NVARCHAR (255), 
  Message NVARCHAR (255) NOT NULL, 
  CreationDate DATETIME NOT NULL
);

-- BugReports

CREATE TABLE BugReports (
  Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  PlayerId INTEGER REFERENCES Players (Id) ON DELETE CASCADE NOT NULL, 
  Message NVARCHAR (255) NOT NULL, 
  CreationDate DATETIME NOT NULL
);
CREATE INDEX IX_BugReports_PlayerId ON BugReports (PlayerId);

-- DebugAsserts

CREATE TABLE DebugAsserts (
  Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  PlayerId INTEGER REFERENCES Players (Id) ON DELETE CASCADE NOT NULL, 
  AssertLine NVARCHAR (255) NOT NULL, 
  ReportDate NVARCHAR (255) NOT NULL, 
  Description NVARCHAR (255) NOT NULL, 
  Comment NVARCHAR (255), 
  CreationDate DATETIME NOT NULL
);
CREATE INDEX IX_DebugAsserts_PlayerId ON DebugAsserts (PlayerId);

-- Motd

CREATE TABLE Motd (
  Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  Message NVARCHAR (255) NOT NULL
);

INSERT INTO Motd (Id, Message) VALUES (0, 'An open Tibia server developed by mtanksl');

-- PlayerDepotItems

CREATE TABLE PlayerDepotItems (
  PlayerId INTEGER NOT NULL REFERENCES Players (Id) ON DELETE CASCADE, 
  SequenceId INTEGER NOT NULL, 
  ParentId INTEGER NOT NULL, 
  OpenTibiaId INTEGER NOT NULL, 
  Count INTEGER NOT NULL, 
  PRIMARY KEY (PlayerId, SequenceId)
);
CREATE INDEX IX_PlayerDepotItems_PlayerId ON PlayerDepotItems (PlayerId);

-- PlayerItems

CREATE TABLE PlayerItems (
  PlayerId INTEGER NOT NULL REFERENCES Players (Id) ON DELETE CASCADE, 
  SequenceId INTEGER NOT NULL, 
  ParentId INTEGER NOT NULL, 
  OpenTibiaId INTEGER NOT NULL, 
  Count INTEGER NOT NULL, 
  PRIMARY KEY (PlayerId, SequenceId)
);
CREATE INDEX IX_PlayerItems_PlayerId ON PlayerItems (PlayerId);

INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (1, 101, 3, 1987, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (1, 102, 101, 2120, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (1, 103, 101, 2554, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (2, 101, 3, 1987, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (2, 102, 101, 2120, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (2, 103, 101, 2554, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (3, 101, 3, 1987, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (3, 102, 101, 2120, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (3, 103, 101, 2554, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (4, 101, 3, 1987, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (4, 102, 101, 2120, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (4, 103, 101, 2554, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (5, 101, 3, 1987, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (5, 102, 101, 2120, 1);
INSERT INTO PlayerItems (PlayerId, SequenceId, ParentId, OpenTibiaId, Count) VALUES (5, 103, 101, 2554, 1);

-- Players

CREATE TABLE Players (
  Id INTEGER CONSTRAINT PK_Players_Id PRIMARY KEY AUTOINCREMENT NOT NULL, 
  AccountId INTEGER NOT NULL REFERENCES Accounts (Id) ON DELETE CASCADE, 
  WorldId INTEGER NOT NULL REFERENCES Worlds (Id), 
  Name NVARCHAR (255) NOT NULL UNIQUE, 
  Health INTEGER NOT NULL, 
  MaxHealth INTEGER NOT NULL, 
  Direction INTEGER NOT NULL, 
  BaseOutfitItemId INTEGER NOT NULL, 
  BaseOutfitId INTEGER NOT NULL, 
  BaseOutfitHead INTEGER NOT NULL, 
  BaseOutfitBody INTEGER NOT NULL, 
  BaseOutfitLegs INTEGER NOT NULL, 
  BaseOutfitFeet INTEGER NOT NULL, 
  BaseOutfitAddon INTEGER NOT NULL, 
  OutfitItemId INTEGER NOT NULL, 
  OutfitId INTEGER NOT NULL, 
  OutfitHead INTEGER NOT NULL, 
  OutfitBody INTEGER NOT NULL, 
  OutfitLegs INTEGER NOT NULL, 
  OutfitFeet INTEGER NOT NULL, 
  OutfitAddon INTEGER NOT NULL, 
  BaseSpeed INTEGER NOT NULL, 
  Speed INTEGER NOT NULL, 
  Invisible BOOLEAN NOT NULL, 
  SkillMagicLevel INTEGER NOT NULL, 
  SkillMagicLevelPercent INTEGER NOT NULL, 
  SkillFist INTEGER NOT NULL, 
  SkillFistPercent INTEGER NOT NULL, 
  SkillClub INTEGER NOT NULL, 
  SkillClubPercent INTEGER NOT NULL, 
  SkillSword INTEGER NOT NULL, 
  SkillSwordPercent INTEGER NOT NULL, 
  SkillAxe INTEGER NOT NULL, 
  SkillAxePercent INTEGER NOT NULL, 
  SkillDistance INTEGER NOT NULL, 
  SkillDistancePercent INTEGER NOT NULL, 
  SkillShield INTEGER NOT NULL, 
  SkillShieldPercent INTEGER NOT NULL, 
  SkillFish INTEGER NOT NULL, 
  SkillFishPercent INTEGER NOT NULL, 
  Experience INTEGER NOT NULL, 
  Level INTEGER NOT NULL, 
  LevelPercent INTEGER NOT NULL, 
  Mana INTEGER NOT NULL, 
  MaxMana INTEGER NOT NULL, 
  Soul INTEGER NOT NULL, 
  Capacity INTEGER NOT NULL, 
  Stamina INTEGER NOT NULL, 
  Gender INTEGER NOT NULL, 
  Vocation INTEGER NOT NULL, 
  Rank INTEGER NOT NULL, 
  SpawnX INTEGER NOT NULL, 
  SpawnY INTEGER NOT NULL, 
  SpawnZ INTEGER NOT NULL, 
  TownX INTEGER NOT NULL, 
  TownY INTEGER NOT NULL, 
  TownZ INTEGER NOT NULL
);
CREATE INDEX IX_Players_AccountId ON Players (AccountId);
CREATE INDEX IX_Players_WorldId ON Players (WorldId);

INSERT INTO Players (Id, AccountId, WorldId, NAME, Health, MaxHealth, Direction, BaseOutfitItemId, BaseOutfitId, BaseOutfitHead, BaseOutfitBody, BaseOutfitLegs, BaseOutfitFeet, BaseOutfitAddon, OutfitItemId, OutfitId, OutfitHead, OutfitBody, OutfitLegs, OutfitFeet, OutfitAddon, BaseSpeed, Speed, Invisible, SkillMagicLevel, SkillMagicLevelPercent, SkillFist, SkillFistPercent, SkillClub, SkillClubPercent, SkillSword, SkillSwordPercent, SkillAxe, SkillAxePercent, SkillDistance, SkillDistancePercent, SkillShield, SkillShieldPercent, SkillFish, SkillFishPercent, Experience, Level, LevelPercent, Mana, MaxMana, Soul, Capacity, Stamina, Gender, Vocation, Rank, SpawnX, SpawnY, SpawnZ, TownX, TownY, TownZ) VALUES (1, 1, 1, 'Gamemaster', 645, 645, 2, 0, 266, 0, 0, 0, 0, 0, 0, 266, 0, 0, 0, 0, 0, 2218, 2218, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15694800, 100, 0, 550, 550, 100, 139000, 2520, 0, 0, 2, 921, 771, 6, 921, 771, 6);
INSERT INTO Players (Id, AccountId, WorldId, NAME, Health, MaxHealth, Direction, BaseOutfitItemId, BaseOutfitId, BaseOutfitHead, BaseOutfitBody, BaseOutfitLegs, BaseOutfitFeet, BaseOutfitAddon, OutfitItemId, OutfitId, OutfitHead, OutfitBody, OutfitLegs, OutfitFeet, OutfitAddon, BaseSpeed, Speed, Invisible, SkillMagicLevel, SkillMagicLevelPercent, SkillFist, SkillFistPercent, SkillClub, SkillClubPercent, SkillSword, SkillSwordPercent, SkillAxe, SkillAxePercent, SkillDistance, SkillDistancePercent, SkillShield, SkillShieldPercent, SkillFish, SkillFishPercent, Experience, Level, LevelPercent, Mana, MaxMana, Soul, Capacity, Stamina, Gender, Vocation, Rank, SpawnX, SpawnY, SpawnZ, TownX, TownY, TownZ) VALUES (2, 1, 1, 'Knight', 1565, 1565, 2, 0, 131, 78, 69, 58, 76, 0, 0, 131, 78, 69, 58, 76, 0, 418, 418, 0, 4, 0, 0, 0, 0, 0, 90, 0, 0, 0, 0, 0, 80, 0, 0, 0, 15694800, 100, 0, 550, 550, 100, 277000, 2520, 0, 1, 0, 921, 771, 6, 921, 771, 6);
INSERT INTO Players (Id, AccountId, WorldId, NAME, Health, MaxHealth, Direction, BaseOutfitItemId, BaseOutfitId, BaseOutfitHead, BaseOutfitBody, BaseOutfitLegs, BaseOutfitFeet, BaseOutfitAddon, OutfitItemId, OutfitId, OutfitHead, OutfitBody, OutfitLegs, OutfitFeet, OutfitAddon, BaseSpeed, Speed, Invisible, SkillMagicLevel, SkillMagicLevelPercent, SkillFist, SkillFistPercent, SkillClub, SkillClubPercent, SkillSword, SkillSwordPercent, SkillAxe, SkillAxePercent, SkillDistance, SkillDistancePercent, SkillShield, SkillShieldPercent, SkillFish, SkillFishPercent, Experience, Level, LevelPercent, Mana, MaxMana, Soul, Capacity, Stamina, Gender, Vocation, Rank, SpawnX, SpawnY, SpawnZ, TownX, TownY, TownZ) VALUES (3, 1, 1, 'Paladin', 1105, 1105, 2, 0, 129, 78, 69, 58, 76, 0, 0, 129, 78, 69, 58, 76, 0, 418, 418, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 70, 0, 40, 0, 0, 0, 15694800, 100, 0, 1470, 1470, 100, 231000, 2520, 0, 2, 0, 921, 771, 6, 921, 771, 6);
INSERT INTO Players (Id, AccountId, WorldId, NAME, Health, MaxHealth, Direction, BaseOutfitItemId, BaseOutfitId, BaseOutfitHead, BaseOutfitBody, BaseOutfitLegs, BaseOutfitFeet, BaseOutfitAddon, OutfitItemId, OutfitId, OutfitHead, OutfitBody, OutfitLegs, OutfitFeet, OutfitAddon, BaseSpeed, Speed, Invisible, SkillMagicLevel, SkillMagicLevelPercent, SkillFist, SkillFistPercent, SkillClub, SkillClubPercent, SkillSword, SkillSwordPercent, SkillAxe, SkillAxePercent, SkillDistance, SkillDistancePercent, SkillShield, SkillShieldPercent, SkillFish, SkillFishPercent, Experience, Level, LevelPercent, Mana, MaxMana, Soul, Capacity, Stamina, Gender, Vocation, Rank, SpawnX, SpawnY, SpawnZ, TownX, TownY, TownZ) VALUES (4, 1, 1, 'Sorcerer', 645, 645, 2, 0, 130, 78, 69, 58, 76, 0, 0, 130, 78, 69, 58, 76, 0, 418, 418, 0, 70, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15694800, 100, 0, 2850, 2850, 100, 139000, 2520, 0, 4, 0, 921, 771, 6, 921, 771, 6);
INSERT INTO Players (Id, AccountId, WorldId, NAME, Health, MaxHealth, Direction, BaseOutfitItemId, BaseOutfitId, BaseOutfitHead, BaseOutfitBody, BaseOutfitLegs, BaseOutfitFeet, BaseOutfitAddon, OutfitItemId, OutfitId, OutfitHead, OutfitBody, OutfitLegs, OutfitFeet, OutfitAddon, BaseSpeed, Speed, Invisible, SkillMagicLevel, SkillMagicLevelPercent, SkillFist, SkillFistPercent, SkillClub, SkillClubPercent, SkillSword, SkillSwordPercent, SkillAxe, SkillAxePercent, SkillDistance, SkillDistancePercent, SkillShield, SkillShieldPercent, SkillFish, SkillFishPercent, Experience, Level, LevelPercent, Mana, MaxMana, Soul, Capacity, Stamina, Gender, Vocation, Rank, SpawnX, SpawnY, SpawnZ, TownX, TownY, TownZ) VALUES (5, 1, 1, 'Druid', 645, 645, 2, 0, 130, 78, 69, 58, 76, 0, 0, 130, 78, 69, 58, 76, 0, 418, 418, 0, 70, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15694800, 100, 0, 2850, 2850, 100, 139000, 2520, 0, 3, 0, 921, 771, 6, 921, 771, 6);

-- PlayerStorages

CREATE TABLE PlayerStorages (
  PlayerId INTEGER NOT NULL REFERENCES Players (Id) ON DELETE CASCADE, 
  "Key" INTEGER NOT NULL, 
  Value INTEGER NOT NULL, 
  PRIMARY KEY (PlayerId, "Key")
);
CREATE INDEX IX_PlayerStorages_PlayerId ON PlayerStorages (PlayerId);

-- PlayerOutfits

CREATE TABLE PlayerOutfits (
    PlayerId INTEGER NOT NULL REFERENCES Players (Id) ON DELETE CASCADE,
    OutfitId INTEGER NOT NULL,
    OutfitAddon INTEGER NOT NULL,
    PRIMARY KEY (PlayerId, OutfitId)
);
CREATE INDEX IX_PlayerOutfits_PlayerId ON PlayerOutfits (PlayerId);

-- PlayerSpells

CREATE TABLE PlayerSpells (
    PlayerId INTEGER NOT NULL REFERENCES Players (Id) ON DELETE CASCADE,
    Name NVARCHAR (255) NOT NULL, 
    PRIMARY KEY (PlayerId, Name)
);
CREATE INDEX IX_PlayerSpells_PlayerId ON PlayerSpells (PlayerId);

-- PlayerVips

CREATE TABLE PlayerVips (
  PlayerId INTEGER NOT NULL REFERENCES Players (Id) ON DELETE CASCADE, 
  VipId INTEGER NOT NULL REFERENCES Players (Id) ON DELETE CASCADE, 
  PRIMARY KEY (PlayerId, VipId)
);
CREATE INDEX IX_PlayerVips_PlayerId ON PlayerVips (PlayerId);
CREATE INDEX IX_PlayerVips_VipId ON PlayerVips (VipId);

-- RuleViolationReports

CREATE TABLE RuleViolationReports (
  Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  PlayerId INTEGER REFERENCES Players (Id) ON DELETE CASCADE NOT NULL, 
  Type INTEGER NOT NULL, 
  RuleViolation INTEGER NOT NULL, 
  Name NVARCHAR (255) NOT NULL, 
  Comment NVARCHAR (255) NOT NULL, 
  Translation NVARCHAR (255), 
  Statment NVARCHAR (255), 
  CreationDate DATETIME NOT NULL
);
CREATE INDEX IX_RuleViolationReports_PlayerId ON RuleViolationReports (PlayerId);

-- Worlds

CREATE TABLE Worlds (
  Id INTEGER CONSTRAINT PK_Worlds_Id PRIMARY KEY AUTOINCREMENT NOT NULL, 
  Name NVARCHAR (255) NOT NULL, 
  Ip NVARCHAR (255) NOT NULL, 
  Port INTEGER NOT NULL
);

INSERT INTO Worlds (Id, NAME, Ip, Port) VALUES (1, 'Cormaya', '127.0.0.1', 7172);

COMMIT TRANSACTION;
PRAGMA foreign_keys = ON;