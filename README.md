Things Missing:

- Installation steps incorredt in Readme
- No tests
- Database models reflected in API
- Changing port option missing
- Calcualting the tota amount of products incorret (OrderService AddProductsToOrder Line 134)
- If the ordes is paid, cannot change the order

E-Commerce API

Overview
The E-Commerce API is a RESTful web service built with .NET 6 that provides endpoints for managing orders and products in an e-commerce platform.
Features
Order Management: Create, retrieve, and update orders.
Product Management: Create, retrieve, and update products.
Order-Product Relationship: Associate products with orders and manage their quantities.
Swagger Documentation: Explore and test API endpoints using Swagger UI.

Installation
Clone the repository: git clone https://github.com/your-username/e-commerce-api.git
Navigate to the project directory: cd e-commerce-api
Build the project: dotnet build
Run the project: dotnet run
Access the API endpoints at https://localhost:<port>/api
Configuration
Port Configuration: You can configure the port by modifying the appsettings.json file or using environment variables.

Endpoints
Orders
GET /api/orders: Retrieve all orders.
GET /api/orders/{order_id}: Retrieve a specific order by ID.
POST /api/orders: Create a new order.
PATCH /api/orders/{order_id}: Update an existing order.
Products
GET /api/products: Retrieve all products.
GET /api/products/{product_id}: Retrieve a specific product by ID.
POST /api/products: Create a new product.
PATCH /api/products/{product_id}: Update an existing product.

Usage
Use tools like Postman or cURL to send HTTP requests to the API endpoints.
Explore the API documentation and test endpoints using Swagger UI (https://localhost:<port>/swagger).

Dependencies
.NET 6 SDK
