# CSC 202 Commerce Project
This project is a technical demonstration designed to showcase the programming capabilities of Ben and Michael. It simulates the backend workings of a commerce system with minimal user interface.
## Features
Allows you to simulate manager and user accounts, along with the purchasing and restocking of various products. The simple object classes are interfaced with JSON Serializer libraries built into C#, allowing data to persist between sessions of the program. There is also a separate thread that simulates randomized purchases of products by other "users".
# Usage
Using this program is pretty simple. The program will prompt you to make an account, whether you are managing or a customer buying things. Managers can add and remove and restock products. Customers can buy products, and that's about it. The program will automatically save the list of managers, list of customers, and list of products, whenever an action is taken. User passwords are stored as hashes and not plaintext, but I'd recommend against using any you use in your daily life for this.
