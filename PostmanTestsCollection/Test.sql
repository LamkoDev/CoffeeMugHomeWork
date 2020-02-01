{
	"info": {
		"_postman_id": "a86960fa-e7c8-40f0-b30d-f14f8a5c7e10",
		"name": "TestCoffeeMug",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
	},
	"item": [
		{
			"name": "Get All",
			"request": {
				"method": "GET",
				"header": [],
				"url": {
					"raw": ""
				}
			},
			"response": []
		},
		{
			"name": "Get ID",
			"request": {
				"method": "GET",
				"header": [],
				"url": {
					"raw": ""
				}
			},
			"response": [
				{
					"name": "Get ID",
					"originalRequest": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "https://localhost:44334/Get/?id=c773448d-bf45-40c1-8f1b-4720a79db78c",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "44334",
							"path": [
								"Get",
								""
							],
							"query": [
								{
									"key": "id",
									"value": "c773448d-bf45-40c1-8f1b-4720a79db78c"
								}
							]
						}
					},
					"status": "OK",
					"code": 200,
					"_postman_previewlanguage": "json",
					"header": [
						{
							"key": "Content-Type",
							"value": "application/json; charset=utf-8"
						},
						{
							"key": "Server",
							"value": "Microsoft-IIS/10.0"
						},
						{
							"key": "X-Powered-By",
							"value": "ASP.NET"
						},
						{
							"key": "Date",
							"value": "Fri, 31 Jan 2020 14:40:20 GMT"
						},
						{
							"key": "Content-Length",
							"value": "81"
						}
					],
					"cookie": [],
					"body": "{\n    \"id\": \"c773448d-bf45-40c1-8f1b-4720a79db78c\",\n    \"name\": \"Samochód\",\n    \"price\": 45020\n}"
				},
				{
					"name": "Get ID Null",
					"originalRequest": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "https://localhost:44334/Get/?id=",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "44334",
							"path": [
								"Get",
								""
							],
							"query": [
								{
									"key": "id",
									"value": ""
								}
							]
						}
					},
					"status": "OK",
					"code": 200,
					"_postman_previewlanguage": "json",
					"header": [
						{
							"key": "Transfer-Encoding",
							"value": "chunked"
						},
						{
							"key": "Content-Type",
							"value": "application/json; charset=utf-8"
						},
						{
							"key": "Server",
							"value": "Microsoft-IIS/10.0"
						},
						{
							"key": "X-Powered-By",
							"value": "ASP.NET"
						},
						{
							"key": "Date",
							"value": "Fri, 31 Jan 2020 14:42:26 GMT"
						}
					],
					"cookie": [],
					"body": "{\n    \"id\": \"00000000-0000-0000-0000-000000000000\",\n    \"name\": null,\n    \"price\": 0\n}"
				},
				{
					"name": "Get ID Wrong",
					"originalRequest": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "https://localhost:44334/Get/?id=c773448d-bf45-40c1-8f1b-4720a79db78cxxx",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "44334",
							"path": [
								"Get",
								""
							],
							"query": [
								{
									"key": "id",
									"value": "c773448d-bf45-40c1-8f1b-4720a79db78cxxx"
								}
							]
						}
					},
					"status": "OK",
					"code": 200,
					"_postman_previewlanguage": "json",
					"header": [
						{
							"key": "Content-Type",
							"value": "application/json; charset=utf-8"
						},
						{
							"key": "Server",
							"value": "Microsoft-IIS/10.0"
						},
						{
							"key": "X-Powered-By",
							"value": "ASP.NET"
						},
						{
							"key": "Date",
							"value": "Fri, 31 Jan 2020 14:40:52 GMT"
						},
						{
							"key": "Content-Length",
							"value": "67"
						}
					],
					"cookie": [],
					"body": "{\n    \"id\": \"00000000-0000-0000-0000-000000000000\",\n    \"name\": null,\n    \"price\": 0\n}"
				}
			]
		},
		{
			"name": "Delete",
			"request": {
				"method": "GET",
				"header": [],
				"url": {
					"raw": ""
				}
			},
			"response": []
		},
		{
			"name": "Post",
			"request": {
				"method": "GET",
				"header": [],
				"url": {
					"raw": ""
				}
			},
			"response": [
				{
					"name": "Post",
					"originalRequest": {
						"method": "POST",
						"header": [],
						"url": {
							"raw": "https://localhost:44334/Post/?Name=Auto&Price=152",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "44334",
							"path": [
								"Post",
								""
							],
							"query": [
								{
									"key": "Name",
									"value": "Auto"
								},
								{
									"key": "Price",
									"value": "152"
								}
							]
						}
					},
					"status": "OK",
					"code": 200,
					"_postman_previewlanguage": "plain",
					"header": [
						{
							"key": "Content-Length",
							"value": "36"
						},
						{
							"key": "Content-Type",
							"value": "text/plain; charset=utf-8"
						},
						{
							"key": "Server",
							"value": "Microsoft-IIS/10.0"
						},
						{
							"key": "X-Powered-By",
							"value": "ASP.NET"
						},
						{
							"key": "Date",
							"value": "Sat, 01 Feb 2020 16:46:31 GMT"
						}
					],
					"cookie": [],
					"body": "4ddaa021-9254-4f09-aa77-421399419c50"
				}
			]
		},
		{
			"name": "Put",
			"request": {
				"method": "GET",
				"header": [],
				"url": {
					"raw": ""
				}
			},
			"response": []
		}
	],
	"protocolProfileBehavior": {}
}