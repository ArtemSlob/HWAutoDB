Feature: DataBaseWork
	As a user
	I want the database to correctly process queries

@InsertData
Scenario Outline:  It is possible to insert data to XSHOPX DB
	When I create row in table 'Persons' with data
		| FirstName   | LastName   | Age   | City   |
		| <firstName> | <lastName> | <age> | <city> |
	When I select whole 'Persons' table
	Then Last row in table Persons contains data
		| FirstName   | LastName   | Age   | City   |
		| <firstName> | <lastName> | <age> | <city> |
	When I delete last row in table 'Persons'

	Examples:
		| firstName   | lastName | age | city        |
		| Worthington | McGurn   | 36  | Santa Clara |