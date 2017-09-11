create table Currency (
   CurrencyId           integer  PRIMARY KEY AUTOINCREMENT,
   Name                 nvarchar(16)         not null,
   TakerRate            decimal              not null,
   MakerRate            decimal              null,
   DefaultProfit        decimal              null,
   UpdateDate           nvarchar(128)              not null ,
   CreateDate           nvarchar(128)              not null 
);

create table TradeBook (
   TradeId              integer  PRIMARY KEY AUTOINCREMENT,
   BuyOrderId           nvarchar(64)               not null,
   BuyPrice             decimal              not null,
   BuyVolume            decimal              not null,
   BuyTradeVolume   decimal              not null,
   BuyTradePrice   decimal              not null,
   BuyAmount            decimal              not null,
   SellOrderId          nvarchar(64)               not null,
   SellPrice            decimal              not null,
   SellVolume           decimal              not null,
   SellTradeVolume  decimal              not null,
   SellTradePrice  decimal              not null,
   SellAmount           decimal              not null,
   Profit               decimal              not null,
   [Status]             nvarchar(64)               not null,
   UpdateDate           nvarchar(128)              not null ,
   CreateDate           nvarchar(128)              not null 
);

INSERT INTO Currency(CurrencyId,Name,TakerRate,MakerRate,DefaultProfit,UpdateDate,CreateDate) VALUES(NULL,'BTC',0.0017,0.0001,10,datetime(CURRENT_TIMESTAMP,'localtime'),datetime(CURRENT_TIMESTAMP,'localtime'));
INSERT INTO Currency(CurrencyId,Name,TakerRate,MakerRate,DefaultProfit,UpdateDate,CreateDate) VALUES(NULL,'LTC',0.0017,0.0001,1,datetime(CURRENT_TIMESTAMP,'localtime'),datetime(CURRENT_TIMESTAMP,'localtime'));
INSERT INTO Currency(CurrencyId,Name,TakerRate,MakerRate,DefaultProfit,UpdateDate,CreateDate) VALUES(NULL,'ETH',0.00047,0.0001,1,datetime(CURRENT_TIMESTAMP,'localtime'),datetime(CURRENT_TIMESTAMP,'localtime'));
INSERT INTO Currency(CurrencyId,Name,TakerRate,MakerRate,DefaultProfit,UpdateDate,CreateDate) VALUES(NULL,'ETC',0.00047,0.0001,1,datetime(CURRENT_TIMESTAMP,'localtime'),datetime(CURRENT_TIMESTAMP,'localtime'));
INSERT INTO Currency(CurrencyId,Name,TakerRate,MakerRate,DefaultProfit,UpdateDate,CreateDate) VALUES(NULL,'BTS',0.00085,0.0004,0.0005,datetime(CURRENT_TIMESTAMP,'localtime'),datetime(CURRENT_TIMESTAMP,'localtime'));
INSERT INTO Currency(CurrencyId,Name,TakerRate,MakerRate,DefaultProfit,UpdateDate,CreateDate) VALUES(NULL,'EOS',0.001,0.001,0.001,datetime(CURRENT_TIMESTAMP,'localtime'),datetime(CURRENT_TIMESTAMP,'localtime'));
INSERT INTO Currency(CurrencyId,Name,TakerRate,MakerRate,DefaultProfit,UpdateDate,CreateDate) VALUES(NULL,'BCC',0.001,0.001,1,datetime(CURRENT_TIMESTAMP,'localtime'),datetime(CURRENT_TIMESTAMP,'localtime'));
INSERT INTO Currency(CurrencyId,Name,TakerRate,MakerRate,DefaultProfit,UpdateDate,CreateDate) VALUES(NULL,'QTUM',0.001,0.001,0.1,datetime(CURRENT_TIMESTAMP,'localtime'),datetime(CURRENT_TIMESTAMP,'localtime'));
INSERT INTO Currency(CurrencyId,Name,TakerRate,MakerRate,DefaultProfit,UpdateDate,CreateDate) VALUES(NULL,'HSR',0.001,0.001,0.1,datetime(CURRENT_TIMESTAMP,'localtime'),datetime(CURRENT_TIMESTAMP,'localtime'));