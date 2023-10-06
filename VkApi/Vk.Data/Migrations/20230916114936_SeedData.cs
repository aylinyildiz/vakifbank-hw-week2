using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vk.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
               
INSERT INTO [dbo].[Customer]([CustomerNumber],[Email] ,[Password] ,[FirstName] ,[LastName] ,[Role] ,[LastActivityDate] ,[PasswordRetryCount] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (100001 ,'lucy1@gmail.com' ,'fd234f234234234' ,'Lucy 1' ,'Sellen' ,'admin' ,'2023-07-07' ,0 ,0 ,'2023-07-07' ,0 ,'2023-07-07' ,1)

INSERT INTO [dbo].[Customer]([CustomerNumber],[Email] ,[Password] ,[FirstName] ,[LastName] ,[Role] ,[LastActivityDate] ,[PasswordRetryCount] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (100002 ,'lucy2@gmail.com' ,'fd234f234234234' ,'Lucy 2' ,'Sellen' ,'admin' ,'2023-07-07' ,0 ,0 ,'2023-07-07' ,0 ,'2023-07-07' ,1)

INSERT INTO [dbo].[Customer]([CustomerNumber],[Email] ,[Password] ,[FirstName] ,[LastName] ,[Role] ,[LastActivityDate] ,[PasswordRetryCount] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (100003 ,'lucy3@gmail.com' ,'fd234f234234234' ,'Lucy 3' ,'Sellen' ,'admin' ,'2023-07-07' ,0 ,0 ,'2023-07-07' ,0 ,'2023-07-07' ,1)

INSERT INTO [dbo].[Customer]([CustomerNumber],[Email] ,[Password] ,[FirstName] ,[LastName] ,[Role] ,[LastActivityDate] ,[PasswordRetryCount] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (100004 ,'lucy4@gmail.com' ,'fd234f234234234' ,'Lucy 4' ,'Sellen' ,'admin' ,'2023-07-07' ,0 ,0 ,'2023-07-07' ,0 ,'2023-07-07' ,1)



INSERT INTO [dbo].[Account] ([CustomerId] ,[Name] ,[AccountNumber] ,[IBAN] ,[Balance] ,[CurrencyCode] ,[OpenDate] ,[CloseDate] ,[CardId] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (1,'Lucy1 Account 1',687567,'TR3460000634630000046346',500,'TRY','2023-05-05',null,null,1,'2023-05-05',0,null,1)

INSERT INTO [dbo].[Account] ([CustomerId] ,[Name] ,[AccountNumber] ,[IBAN] ,[Balance] ,[CurrencyCode] ,[OpenDate] ,[CloseDate] ,[CardId] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (1,'Lucy1 Account 2',687568,'TR3460000634630000046346',500,'TRY','2023-05-05',null,null,1,'2023-05-05',0,null,1)

INSERT INTO [dbo].[Account] ([CustomerId] ,[Name] ,[AccountNumber] ,[IBAN] ,[Balance] ,[CurrencyCode] ,[OpenDate] ,[CloseDate] ,[CardId] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (1,'Lucy1 Account 3',687569,'TR3460000634630000046346',500,'TRY','2023-05-05',null,null,1,'2023-05-05',0,null,1)

INSERT INTO [dbo].[Account] ([CustomerId] ,[Name] ,[AccountNumber] ,[IBAN] ,[Balance] ,[CurrencyCode] ,[OpenDate] ,[CloseDate] ,[CardId] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (1,'Lucy1 Account 4',687570,'TR3460000634630000046346',500,'TRY','2023-05-05',null,null,1,'2023-05-05',0,null,1)

INSERT INTO [dbo].[Account] ([CustomerId] ,[Name] ,[AccountNumber] ,[IBAN] ,[Balance] ,[CurrencyCode] ,[OpenDate] ,[CloseDate] ,[CardId] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (2,'Lucy2 Account 1',857123,'TR3460000634630000046346',500,'TRY','2023-05-05',null,null,1,'2023-05-05',0,null,1)

INSERT INTO [dbo].[Account] ([CustomerId] ,[Name] ,[AccountNumber] ,[IBAN] ,[Balance] ,[CurrencyCode] ,[OpenDate] ,[CloseDate] ,[CardId] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (2,'Lucy2 Account 2',857124,'TR3460000634630000046346',500,'TRY','2023-05-05',null,null,1,'2023-05-05',0,null,1)

INSERT INTO [dbo].[Account] ([CustomerId] ,[Name] ,[AccountNumber] ,[IBAN] ,[Balance] ,[CurrencyCode] ,[OpenDate] ,[CloseDate] ,[CardId] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (3,'Lucy3 Account 1',857125,'TR3460000634630000046346',500,'TRY','2023-05-05',null,null,1,'2023-05-05',0,null,1)

INSERT INTO [dbo].[Account] ([CustomerId] ,[Name] ,[AccountNumber] ,[IBAN] ,[Balance] ,[CurrencyCode] ,[OpenDate] ,[CloseDate] ,[CardId] ,[InsertUserId] ,[InsertDate] ,[UpdateUserId] ,[UpdateDate] ,[IsActive])
VALUES (3,'Lucy3 Account 2',857126,'TR3460000634630000046346',500,'TRY','2023-05-05',null,null,1,'2023-05-05',0,null,1)














");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
