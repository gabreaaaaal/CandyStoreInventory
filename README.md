# Polangui Candy Corner — Inventory Management System

A CLI-Based Inventory Management System built with **C# Console Application** using Object-Oriented Programming principles.

---

## Features

- Add, view, search, update, and delete products
- Manage categories and suppliers
- Restock and deduct stock
- View transaction history
- Display low-stock items
- Compute total inventory value

---

## Models

| Model | Description |
|---|---|
| `Product` | Stores product info (name, price, stock, etc.) |
| `Category` | Groups products by type |
| `Supplier` | Tracks supplier contact info |
| `User` | Handles login and role access |
| `TransactionRecord` | Logs every inventory action |

---

## Project Structure

```
CandyStoreInventory/
├── CandyStoreInventory.csproj
├── Program.cs
├── InventoryManager.cs
└── Models/
    ├── Product.cs
    ├── Category.cs
    ├── Supplier.cs
    ├── User.cs
    └── TransactionRecord.cs
```

---

## Requirements

-.NET 8.0 SDK

---

## How to Run

dotnet run --project CandyStoreInventory.csproj

---

## OOP Concepts Used

- Classes and Objects
- Constructors
- Properties
- Encapsulation
- Access Modifiers (`public`, `private`)
- Methods
- Exception Handling (`try-catch`)
