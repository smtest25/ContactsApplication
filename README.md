# ContactsApplication

Just a simple app to manage contacts.

### Notes

- The connection string in `appsettings.json` must point to a MSSQL database.
- The database should not contain a table called `Contacts`.
- When the application starts, if the `Contacts` table is empty, it will be seeded with test data.