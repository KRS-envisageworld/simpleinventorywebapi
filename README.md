## SimpleInventoryAPI
Inventory sample web API developed with .netcore without.

## Installation
1. Clone the project to your local using clone git link.
2. Open the `web.config` and change the `ConncetionString` with your database connection.
3. Give it a build and open the Nuget Package Manger and run bellow command.
> update-database
4. It will create the database to your database with some sample data.
5. Now, you ready to go and test play aroung with it.

## Usage

This API has usual methods like `GET`, `POST`, `PUT`, `DELETE`. Its very easy to use, let's get deep dive into it. To keep it simple, this api has only two resources Category and Items has one to one relation with Category.

Let's get first familiar with few things like parameters, models & its properties, return status codes and response structure used in this service.
#### Parameters
Here, we will get familiar with querystring parameters mandatory as well as optional.

##### **Mandtory**
- `moniker: string` : For some APIs, this param is mandatory. This indicate unique identity of the Categories and Items. 

##### **Optional**
- `includeItems=true: bool' : By defaluft, it will set as `false`. It tells the APIs, that user needs item list also.
- `forceFully=true: bool` : By default, it will set as `fasle`. It tells the APIs, that do this operation force fully.

### Models
Models are something that, user will required when he needs to add or modify something into the databse. There are two models Category and Items below is the description of their properties.

**Category**
```
{
   "moniker: string" : It should be 8 characters long and **Required** field. Should have alpha numeric characters only without any space/s.
   "name: string" : Its a **Required** field and maximum length is 100 characters.
   "items: list" : Its a collection of the items.
}
```
**Items**
```
{
   "moniker: string" : It should be 8 characters long and **Required** field. Should have alpha numeric characters only without any space/s.
   "name: string" : Its a **Required** field and maximum length is 100 characters.
   "description: string" : It has maximum length is 500.
   "price: double" : default value is 0.0
   "availableQuantity: int" : default is 0
}
```

### Status codes
| Code | Description |
| :---         | :--- |
|200 (OK) | The API call excecuted successfully. |
|201 (Created) | Record has been created successfully. In Response header user can get the location of created record by `Location` attribute |
|400 (OK) | Request contains unsual data. |
|405 (Method Not allowed) | The request resource or method doesn't exist |
|500 (Internal Server Error) | Error occured while executing the request. |
 
### Response object
##### `Success`
```
{
    "success": bool
    "object" : requeted data
}
```
##### `Bad request`
```
{
    "message": Returned meesage.
    "modelState": object with invalid properties and message.
}
```
##### `Exception`
```
{
    "message": message
    "exceptionMessage": exception message
    "exceptionType": type of exception
    "stackTrace": error detail
}
```
## URL Formats

**Category**
##### `GET`
```
/api/category
/api/category?includeItems=false/true
```
##### `POST`
```
/api/category
```
##### `PUT`
```
/api/category/{moniker}
```
##### `DELETE`
```
/api/category/{moniker}
```

**Items**
##### `GET`
```
/api/category/{categoryMoniker}/items
/api/category/{categoryMoniker}/items/{mokiner}
```
##### `POST`
```
/api/category/{categoryMoniker}/items
```
##### `POST`
```
/api/category/{categoryMoniker}/items/{moniker}
```
##### `DELETE`
```
/api/category/{categoryMoniker}/items/{moniker}
```
## Sample 

Here, few examples with the Request and Response. If you need more examples then you can download the JSON file of Postman [Click to view] () and can use it for demo by importing to your local. It includes the examples also.

##### `GET` Categories with items
##### **Request**
```
https://localhost:44353/api/category?includeItems=true
```
##### **Output**
```JSON
{
    "success": true,
    "category": [
        {
            "moniker": "CAT123AB",
            "name": "Category 1",
            "items": [
                {
                    "moniker": "ITM111AB",
                    "name": "Item 1",
                    "description": "This is for demo only.",
                    "price": 500,
                    "availableQuantity": 10
                },
                {
                    "moniker": "ITM122BC",
                    "name": "Item 2",
                    "description": "This is for demo only.",
                    "price": 45,
                    "availableQuantity": 10
                }
            ]
        },
        {
            "moniker": "CAT122BA",
            "name": "Category 2",
            "items": [
                {
                    "moniker": "ITM333AC",
                    "name": "Item 3",
                    "description": "This is for demo only.",
                    "price": 100,
                    "availableQuantity": 10
                },
                {
                    "moniker": "ITM444BC",
                    "name": "Item 4",
                    "description": "This is for demo only.",
                    "price": 200,
                    "availableQuantity": 10
                }
            ]
        }
    ]
}
```
##### `GET` Iems inside the Category
##### **Request**
```
https://localhost:44353/api/category/CAT122BA/items
```
##### **Output**
```JSON
{
    "success": true,
    "items": [
        {
            "moniker": "ITM333AC",
            "name": "Item 3",
            "description": "This is for demo only.",
            "price": 100,
            "availableQuantity": 10
        },
        {
            "moniker": "ITM444BC",
            "name": "Item 4",
            "description": "This is for demo only.",
            "price": 200,
            "availableQuantity": 10
        },
        {
            "moniker": "ITM999AT",
            "name": "Demo item",
            "description": "This item has modified",
            "price": 500,
            "availableQuantity": 60
        },
        {
            "moniker": "ITM909AQ",
            "name": "Demo item 1",
            "description": "This item has modified",
            "price": 100,
            "availableQuantity": 12
        }
    ]
}
```

##### `GET` particular Item 
##### **Request**
```
https://localhost:44353/api/category/CAT122BA/items/ITM444BC
```
**Output**
```JSON
{
    "success": true,
    "items": {
        "moniker": "ITM444BC",
        "name": "Item 4",
        "description": "This is for demo only.",
        "price": 200,
        "availableQuantity": 10
    }
}
```

##### `POST` Category
##### **Request**
```
https://localhost:44353/api/category
```
#### **Body**
```JSON
{
    "moniker": "CAT123Ao",
    "name": "Category 1"
}
```
#### **Output**
```JSON
{
    "moniker": "CAT123AO",
    "name": "Category 1",
    "items": []
}
```
##### `DELETE` Category
##### **Request**
```
https://localhost:44353/api/category/CAT123AB?forcefully=true
```
##### **Output**
```JSON
{
    "success": true,
    "category": [
        {
            "moniker": "CAT123AC",
            "name": "Category 1",
            "items": []
        },
        {
            "moniker": "CAT123AO",
            "name": "Category 1",
            "items": []
        },
        {
            "moniker": "CAT123RQ",
            "name": "Category To delete",
            "items": []
        }
    ]
}
```
