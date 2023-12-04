Please restore the DB, the backup is can be found here: https://github.com/KgaboMaraka/IncomeTax/tree/master/DB%20Backup/Tax.bak
Change the connection string in https://github.com/KgaboMaraka/IncomeTax/tree/master/IncomeTax\appsettings.json to point to the SQL instance you restored the database into(replace NBNMIHQ004\\KGABOLOCAL with your server name). Connection string name : TaxConnection
This is a backup from Microsoft SQL Server 2022 (RTM)

Please open the project in Visual Studio 2022
