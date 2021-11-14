CREATE LOGIN TransactionUser WITH PASSWORD = '123'	
GO

CREATE DATABASE [TransactionsDB]
GO

USE [TransactionsDB]

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'TransactionUser')
BEGIN
    CREATE USER TransactionUser FOR LOGIN TransactionUser
    EXEC sp_addrolemember N'db_owner', N'TransactionUser'
END;
GO

CREATE TABLE TransactionStatus
(
	ID TINYINT PRIMARY KEY,
	Name CHAR(1)
)

INSERT INTO TransactionStatus(ID,Name) VALUES(1,'A')
INSERT INTO TransactionStatus(ID,Name) VALUES(2,'R')
INSERT INTO TransactionStatus(ID,Name) VALUES(3,'D')

CREATE TABLE Currency 
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name   VARCHAR(20),
	Code   VARCHAR(3)
)

INSERT INTO Currency (Name, Code) VALUES ('Leke', 'ALL');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'USD');
INSERT INTO Currency (Name, Code) VALUES ('Afghanis', 'AFN');
INSERT INTO Currency (Name, Code) VALUES ('Pesos', 'ARS');
INSERT INTO Currency (Name, Code) VALUES ('Guilders', 'AWG');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'AUD');
INSERT INTO Currency (Name, Code) VALUES ('New Manats', 'AZN');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'BSD');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'BBD');
INSERT INTO Currency (Name, Code) VALUES ('Rubles', 'BYR');
INSERT INTO Currency (Name, Code) VALUES ('Euro', 'EUR');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'BZD');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'BMD');
INSERT INTO Currency (Name, Code) VALUES ('Bolivianos', 'BOB');
INSERT INTO Currency (Name, Code) VALUES ('Convertible Marka', 'BAM');
INSERT INTO Currency (Name, Code) VALUES ('Pula', 'BWP');
INSERT INTO Currency (Name, Code) VALUES ('Leva', 'BGN');
INSERT INTO Currency (Name, Code) VALUES ('Reais', 'BRL');
INSERT INTO Currency (Name, Code) VALUES ('Pounds', 'GBP');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'BND');
INSERT INTO Currency (Name, Code) VALUES ('Riels', 'KHR');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'CAD');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'KYD');
INSERT INTO Currency (Name, Code) VALUES ('Pesos', 'CLP');
INSERT INTO Currency (Name, Code) VALUES ('Yuan Renminbi', 'CNY');
INSERT INTO Currency (Name, Code) VALUES ('Pesos', 'COP');
INSERT INTO Currency (Name, Code) VALUES ('Colón', 'CRC');
INSERT INTO Currency (Name, Code) VALUES ('Kuna', 'HRK');
INSERT INTO Currency (Name, Code) VALUES ('Pesos', 'CUP');
INSERT INTO Currency (Name, Code) VALUES ('Koruny', 'CZK');
INSERT INTO Currency (Name, Code) VALUES ('Kroner', 'DKK');
INSERT INTO Currency (Name, Code) VALUES ('Pesos', 'DOP ');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'XCD');
INSERT INTO Currency (Name, Code) VALUES ('Pounds', 'EGP');
INSERT INTO Currency (Name, Code) VALUES ('Colones', 'SVC');
INSERT INTO Currency (Name, Code) VALUES ('Pounds', 'FKP');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'FJD');
INSERT INTO Currency (Name, Code) VALUES ('Cedis', 'GHC');
INSERT INTO Currency (Name, Code) VALUES ('Pounds', 'GIP');
INSERT INTO Currency (Name, Code) VALUES ('Quetzales', 'GTQ');
INSERT INTO Currency (Name, Code) VALUES ('Pounds', 'GGP');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'GYD');
INSERT INTO Currency (Name, Code) VALUES ('Lempiras', 'HNL');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'HKD');
INSERT INTO Currency (Name, Code) VALUES ('Forint', 'HUF');
INSERT INTO Currency (Name, Code) VALUES ('Kronur', 'ISK');
INSERT INTO Currency (Name, Code) VALUES ('Rupees', 'INR');
INSERT INTO Currency (Name, Code) VALUES ('Rupiahs', 'IDR');
INSERT INTO Currency (Name, Code) VALUES ('Rials', 'IRR');
INSERT INTO Currency (Name, Code) VALUES ('Pounds', 'IMP');
INSERT INTO Currency (Name, Code) VALUES ('New Shekels', 'ILS');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'JMD');
INSERT INTO Currency (Name, Code) VALUES ('Yen', 'JPY');
INSERT INTO Currency (Name, Code) VALUES ('Pounds', 'JEP');
INSERT INTO Currency (Name, Code) VALUES ('Tenge', 'KZT');
INSERT INTO Currency (Name, Code) VALUES ('Won', 'KPW');
INSERT INTO Currency (Name, Code) VALUES ('Won', 'KRW');
INSERT INTO Currency (Name, Code) VALUES ('Soms', 'KGS');
INSERT INTO Currency (Name, Code) VALUES ('Kips', 'LAK');
INSERT INTO Currency (Name, Code) VALUES ('Lati', 'LVL');
INSERT INTO Currency (Name, Code) VALUES ('Pounds', 'LBP');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'LRD');
INSERT INTO Currency (Name, Code) VALUES ('Switzerland Francs', 'CHF');
INSERT INTO Currency (Name, Code) VALUES ('Litai', 'LTL');
INSERT INTO Currency (Name, Code) VALUES ('Denars', 'MKD');
INSERT INTO Currency (Name, Code) VALUES ('Ringgits', 'MYR');
INSERT INTO Currency (Name, Code) VALUES ('Rupees', 'MUR');
INSERT INTO Currency (Name, Code) VALUES ('Pesos', 'MXN');
INSERT INTO Currency (Name, Code) VALUES ('Tugriks', 'MNT');
INSERT INTO Currency (Name, Code) VALUES ('Meticais', 'MZN');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'NAD');
INSERT INTO Currency (Name, Code) VALUES ('Rupees', 'NPR');
INSERT INTO Currency (Name, Code) VALUES ('Guilders', 'ANG');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'NZD');
INSERT INTO Currency (Name, Code) VALUES ('Cordobas', 'NIO');
INSERT INTO Currency (Name, Code) VALUES ('Nairas', 'NGN');
INSERT INTO Currency (Name, Code) VALUES ('Krone', 'NOK');
INSERT INTO Currency (Name, Code) VALUES ('Rials', 'OMR');
INSERT INTO Currency (Name, Code) VALUES ('Rupees', 'PKR');
INSERT INTO Currency (Name, Code) VALUES ('Balboa', 'PAB');
INSERT INTO Currency (Name, Code) VALUES ('Guarani', 'PYG');
INSERT INTO Currency (Name, Code) VALUES ('Nuevos Soles', 'PEN');
INSERT INTO Currency (Name, Code) VALUES ('Pesos', 'PHP');
INSERT INTO Currency (Name, Code) VALUES ('Zlotych', 'PLN');
INSERT INTO Currency (Name, Code) VALUES ('Rials', 'QAR');
INSERT INTO Currency (Name, Code) VALUES ('New Lei', 'RON');
INSERT INTO Currency (Name, Code) VALUES ('Rubles', 'RUB');
INSERT INTO Currency (Name, Code) VALUES ('Pounds', 'SHP');
INSERT INTO Currency (Name, Code) VALUES ('Riyals', 'SAR');
INSERT INTO Currency (Name, Code) VALUES ('Dinars', 'RSD');
INSERT INTO Currency (Name, Code) VALUES ('Rupees', 'SCR');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'SGD');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'SBD');
INSERT INTO Currency (Name, Code) VALUES ('Shillings', 'SOS');
INSERT INTO Currency (Name, Code) VALUES ('Rand', 'ZAR');
INSERT INTO Currency (Name, Code) VALUES ('Rupees', 'LKR');
INSERT INTO Currency (Name, Code) VALUES ('Kronor', 'SEK');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'SRD');
INSERT INTO Currency (Name, Code) VALUES ('Pounds', 'SYP');
INSERT INTO Currency (Name, Code) VALUES ('New Dollars', 'TWD');
INSERT INTO Currency (Name, Code) VALUES ('Baht', 'THB');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'TTD');
INSERT INTO Currency (Name, Code) VALUES ('Lira', 'TRY');
INSERT INTO Currency (Name, Code) VALUES ('Liras', 'TRL');
INSERT INTO Currency (Name, Code) VALUES ('Dollars', 'TVD');
INSERT INTO Currency (Name, Code) VALUES ('Hryvnia', 'UAH');
INSERT INTO Currency (Name, Code) VALUES ('Pesos', 'UYU');
INSERT INTO Currency (Name, Code) VALUES ('Sums', 'UZS');
INSERT INTO Currency (Name, Code) VALUES ('Bolivares Fuertes', 'VEF');
INSERT INTO Currency (Name, Code) VALUES ('Dong', 'VND');
INSERT INTO Currency (Name, Code) VALUES ('Rials', 'YER');
INSERT INTO Currency (Name, Code) VALUES ('Zimbabwe Dollars', 'ZWD');
INSERT INTO Currency (Name, Code) VALUES ('Rupees', 'INR');

CREATE TABLE Transactions
(
	ID NVARCHAR(50) PRIMARY KEY,
	Amount DECIMAL(10,2) NOT NULL,
	CurrencyID INT NOT NULL,
	TrasactionDate DATETIME NOT NULL,
	StatusID TINYINT NOT NULL,
	CONSTRAINT FK_Transactions_Currnecy FOREIGN KEY(CurrencyID) REFERENCES Currency(ID),
	CONSTRAINT FK_Transactions_Status FOREIGN KEY(StatusID) REFERENCES TransactionStatus(ID)
)