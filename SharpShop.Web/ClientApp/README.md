# SharpShop Angular Frontend

This Angular project replaces the former Blazor UI for SharpShop.

## Development

1. Install Node.js 20+.
2. From `ClientApp` run `npm install`.
3. Run the backend solution (F5 in Visual Studio or `dotnet run` for `SharpShop.ApiService`).
4. Start Angular dev server:
```
npm start
```
The proxy forwards `/api` calls to the backend.

## Production Build
```
npm run build
```
Copy or let the ASP.NET Core app serve the `ClientApp/dist` folder (already configured).
